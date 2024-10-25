using ElviraIvanovaKt_44_21.Database;
using ElviraIvanovaKt_44_21.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using ElviraIvanovaKt_44_21.Filters.StudentFilters;
using ElviraIvanovaKt_44_21.Filters.StudentFioFilters;
using ElviraIvanovaKt_44_21.Interfaces.StudentsInterfaces;

namespace ElviraIvanovaKt_44_21.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;
        private StudentDbContext _context;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService, StudentDbContext context)
        {
            _logger = logger;
            _studentService = studentService;
            _context = context;
        }


        [HttpPost(Name = "GetStudentsByGroup")]
        public async Task<IActionResult> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpPost("GetStudentsByFio")]
        public async Task<IActionResult> GetStudentsByFioAsync(StudentFioFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByFioAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpPost("AddStudent", Name = "AddStudent")]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }

        [HttpPut("EditStudent")]
        public IActionResult UpdateStudent(string firstname, [FromBody] Student updatedStudent)
        {
            var existingStudent = _context.Students.FirstOrDefault(g => g.FirstName == firstname);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.FirstName = updatedStudent.FirstName;
            existingStudent.LastName = updatedStudent.LastName;
            existingStudent.MiddleName = updatedStudent.MiddleName;
            existingStudent.GroupId = updatedStudent.GroupId;
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost("AddGroup", Name = "AddGroup")]
        public IActionResult CreateGroup([FromBody] ElviraIvanovaKt_44_21.Models.Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Groups.Add(group);
            _context.SaveChanges();
            return Ok(group);
        }

        [HttpPut("EditGroup")]
        public IActionResult UpdateGroup(string groupname, [FromBody] StudentGroupFilter updatedGroup)
        {
            var existingGroup = _context.Groups.FirstOrDefault(g => g.GroupName == groupname);

            if (existingGroup == null)
            {
                return NotFound();
            }

            existingGroup.GroupName = updatedGroup.GroupName;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("DeleteGroup")]
        public IActionResult DeleteGroup(string groupName, ElviraIvanovaKt_44_21.Models.Group updatedGroup)
        {
            var existingGroup = _context.Groups.FirstOrDefault(g => g.GroupName == groupName);

            if (existingGroup == null)
            {
                return NotFound();
            }
            _context.Groups.Remove(existingGroup);
            _context.SaveChanges();

            return Ok();
        }
    }
}