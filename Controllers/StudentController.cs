using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/students/")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] StudentCreateInfo studentCreate)
    {
        bool res = await studentService.Create(studentCreate);
        if (!res)
            return BadRequest(ApiResponse<bool>.Fail(null, false));
        return Ok(ApiResponse<bool>.Success(null, true));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll([FromQuery] StudentFilter filter)
    {
        PaginationResponse<IEnumerable<StudentReadInfo>> students = studentService.GetAll(filter);
        if (students is null)
            return NotFound(ApiResponse<PaginationResponse<IEnumerable<StudentReadInfo>>>.Fail(null, students));

        return Ok(ApiResponse<PaginationResponse<IEnumerable<StudentReadInfo>>>.Success(null, students));

    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] StudentUpdateInfo studentUpdate)
    {
        bool res = await studentService.Update(studentUpdate);
        if (!res)
            return BadRequest(ApiResponse<bool>.Fail(null, false));
        return Ok(ApiResponse<bool>.Success(null, true));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        StudentReadInfo? student = await studentService.GetById(id);
        if (student is null)
            return NotFound(ApiResponse<StudentReadInfo?>.Fail(null, student));
        return Ok(ApiResponse<StudentReadInfo?>.Success(null, student));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        bool res = await studentService.Delete(id);
        if (!res)
            return BadRequest(ApiResponse<bool>.Fail(null, false));
        return Ok(ApiResponse<bool>.Success(null, true));
    }
}