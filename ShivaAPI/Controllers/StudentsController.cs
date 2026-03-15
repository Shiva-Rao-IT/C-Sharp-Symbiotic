using Microsoft.AspNetCore.Mvc;
using ShivaAPI.Models;

namespace ShivaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        // In-memory data
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, FirstName = "Shiva", LastName = "Yadav", Email = "shiva@email.com" },
            new Student { Id = 2, FirstName = "Rahul", LastName = "Sharma", Email = "rahul@email.com" }
        };

        // GET: api/students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(students);
        }

        // GET: api/students/1
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public ActionResult<Student> AddStudent(Student student)
        {
            student.Id = students.Max(s => s.Id) + 1;
            students.Add(student);

            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // DELETE: api/students/1
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            students.Remove(student);
            return NoContent();
        }
    }
}