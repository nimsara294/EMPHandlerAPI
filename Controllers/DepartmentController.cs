using EMPHandlerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMPHandlerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentContext _departmentContext;
        public DepartmentController(DepartmentContext departmentContext)
        {
            _departmentContext = departmentContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartments()
        {
            if (_departmentContext.Departments == null)
            {
                return NotFound();
            }
            return await _departmentContext.Departments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            if (_departmentContext.Departments == null)
            {
                return NotFound();
            }
            var department = await _departmentContext.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return department;
        }

        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartment(Department department)
        {
            _departmentContext.Departments.Add(department);
            await _departmentContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDepartment), new { id = department.Id }, department);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            _departmentContext.Entry(department).State = EntityState.Modified;
            try
            {
                await _departmentContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDepartment(int id)
        {
            if (_departmentContext.Departments == null)
            {
                return NotFound();
            }

            var department = await _departmentContext.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            _departmentContext.Departments.Remove(department);
            await _departmentContext.SaveChangesAsync();

            return Ok();
        }
    }
}
