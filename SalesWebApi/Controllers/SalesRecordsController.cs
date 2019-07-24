using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SalesWebApi.Data;
using SalesWebApi.Models.ServiceModels;

namespace SalesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesRecordsController : ControllerBase
    {
        private readonly SalesContext _context;

        public SalesRecordsController(SalesContext context)
        {
            _context = context;
        }
        
        [HttpGet("{jsonValues}")]
        public async Task<IActionResult> FindByDateAsync(string jsonValues)
        {
            try
            {
                SalesRecordByDateIncluding request = 
                    JsonConvert.DeserializeObject<SalesRecordByDateIncluding>(jsonValues);

                var result = _context.SalesRecord.Select(p => p);

                if (request.MinDate.HasValue)
                    result = result.Where(p => p.Date >= request.MinDate.Value);

                if (request.MaxDate.HasValue)
                    result = result.Where(p => p.Date <= request.MaxDate.Value);

                foreach (var classToInclude in request.IncludeList)
                {
                    result = result.Include(classToInclude);
                }

                result = result.OrderByDescending(p => p.Date);
                                
                if (request.GroupBySellerDepartment)
                {
                    List<MyGrouping> response = 
                        await result.GroupBy(p => p.Seller.Department, 
                                                  (key, group) => new MyGrouping {
                                                                        Key = key,
                                                                        Sales = group
                                             }).ToListAsync();

                    return Ok(response);
                }
                
                return Ok(await result.ToListAsync());
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}