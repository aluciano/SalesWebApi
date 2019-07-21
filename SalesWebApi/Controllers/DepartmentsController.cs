using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SalesWebApi.Data;
using SalesWebApi.Models;

namespace SalesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly SalesContext _context;

        public DepartmentsController(SalesContext context)
        {
            _context = context;
        }

        // GET api/departments
        [HttpGet]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(_context.Department.ToList());
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // GET api/departments/5
        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            try
            {
                var department = _context.Department.FirstOrDefault(p => p.Id == id);
                return Ok(department);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // POST api/departments
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string jsonValues)
        {
            try
            {
                var department = new Department();
                JsonConvert.PopulateObject(jsonValues, department);

                if (!TryValidateModel(department))
                    return BadRequest(ModelState);

                _context.Department.Add(department);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // PUT api/departments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string jsonValues)
        {
            try
            {
                var department = _context.Department.First(p => p.Id == id);
                JsonConvert.PopulateObject(jsonValues, department);

                if (!TryValidateModel(department))
                    return BadRequest(ModelState);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // DELETE api/departments/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                var department = await _context.Department.FindAsync(id);
                _context.Department.Remove(department);

                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
