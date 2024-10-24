public static class StudentMappingExtensions
{
    public static Student StudentCreateInfoToStudent(this StudentCreateInfo studentCreate)
    {
        return new Student()
        {
            FirstName = studentCreate.FirstName,
            LastName = studentCreate.LastName,
            Age = studentCreate.Age
        };
    }

    public static Student StudentUpdateInfoToStudent(this Student student, StudentUpdateInfo studentUpdate)
    {
        student.FirstName = studentUpdate.FirstName;
        student.LastName = studentUpdate.LastName;
        student.Age = studentUpdate.Age;
        return student;
    }

    public static StudentReadInfo StudentToStudentReadInfo(this Student student)
    {
        return new StudentReadInfo()
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Age = student.Age
        };
    }
}