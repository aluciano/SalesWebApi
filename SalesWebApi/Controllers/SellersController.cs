using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalesWebApi.Data;
using SalesWebApi.Models;

namespace SalesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly SalesContext _context;

        public SellersController(SalesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult FindAll()
        {
            try
            {
                return Ok(_context.Seller.ToList());
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public IActionResult FindById(int id)
        {
            try
            {
                var seller = _context.Seller.FirstOrDefault(p => p.Id == id);
                return Ok(seller);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}/{classToInclude}")]
        public IActionResult FindByIdIncluding(int id, string classToInclude)
        {
            try
            {
                var seller = _context.Seller
                                     .Include(classToInclude)
                                     .FirstOrDefault(p => p.Id == id);

                return Ok(seller);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string jsonValues)
        {
            try
            {
                var seller = new Seller();
                JsonConvert.PopulateObject(jsonValues, seller);

                if (!TryValidateModel(seller))
                    return BadRequest(ModelState);

                _context.Seller.Add(seller);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string jsonValues)
        {
            try
            {
                var seller = _context.Seller.First(p => p.Id == id);
                JsonConvert.PopulateObject(jsonValues, seller);

                if (!TryValidateModel(seller))
                    return BadRequest(ModelState);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            try
            {
                var seller = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(seller);

                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}