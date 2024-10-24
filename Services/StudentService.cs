
using Microsoft.EntityFrameworkCore;

public class StudentService(AppDbContext context) : IStudentService
{
    public async Task<bool> Create(StudentCreateInfo studentCreate)
    {
        try
        {
            int maxId = await (from s in context.Students
                               orderby s.Id descending
                               select s.Id).FirstOrDefaultAsync();

            Student student = studentCreate.StudentCreateInfoToStudent();
            student.Id = maxId + 1;
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            Student? student = await context.Students.FindAsync(id);
            if (student is null)
                return false;
            context.Students.Remove(student);
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public PaginationResponse<IEnumerable<StudentReadInfo>> GetAll(StudentFilter filter)
    {
        try
        {
            IQueryable<Student> students = context.Students;

            if (filter is not null)
                students = students.Where(x => x.Age == filter.Age);

            IQueryable<StudentReadInfo> studentRead = students.Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(x => x.StudentToStudentReadInfo());
            int totalRecords = context.Students.Count();
            return PaginationResponse<IEnumerable<StudentReadInfo>>.Create(filter.PageNumber, filter.PageSize, totalRecords, studentRead);
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<StudentReadInfo?> GetById(int id)
    {
        try
        {
            Student? student = await context.Students.FindAsync(id);
            if (student is null)
                return null;
            return student.StudentToStudentReadInfo();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }

    public async Task<bool> Update(StudentUpdateInfo studentUpdate)
    {
        try
        {
            Student? student = await context.Students.FindAsync(studentUpdate.Id);
            if (student is null)
                return false;
            context.Students.Update(student.StudentUpdateInfoToStudent(studentUpdate));
            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex.Message);
            throw;
        }
    }
}