public interface IStudentService
{
    Task<bool> Create(StudentCreateInfo studentCreate);
    Task<bool> Update(StudentUpdateInfo studentUpdate);
    Task<bool> Delete(int id);
    Task<StudentReadInfo?> GetById(int id);
    PaginationResponse<IEnumerable<StudentReadInfo>> GetAll(StudentFilter filter);
}