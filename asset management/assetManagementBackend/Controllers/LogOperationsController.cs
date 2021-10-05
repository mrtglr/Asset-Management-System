using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assetManagementBackend.Data;
using assetManagementBackend.DTOS;
using assetManagementBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assetManagementBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogOperationsController: ControllerBase
    {
        private readonly AssetContext _context;

        public LogOperationsController(AssetContext context)
        {
            _context = context;
        }
        
        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<IActionResult> GetLogs()
        {
            var logs = await _context.LogOperations.OrderByDescending(x=>x.date_time).ToListAsync();           
            return Ok(logs);
        }        

        [HttpGet("filter/{filter}")]
        public async Task<IActionResult> SearchLogs(string filter)
        {
            filter = filter.ToLower();
            var logs = new List<LogOperations>();
            logs = await _context.LogOperations
            .Where(x=>
            (x.process.ToLower().Contains(filter))||
            (x.statement.ToLower().Contains(filter))||
            (x.date_time.ToString().Contains(filter))||
            (x.user_ip.ToLower().Contains(filter))).ToListAsync();
            return Ok(logs);      
        }    

        [HttpGet("List")]
        public IActionResult List([FromQuery] string searchText, [FromQuery] int? page, [FromQuery] int pagesize=7){

            var query = string.IsNullOrEmpty(searchText)? _context.LogOperations
                                                        : _context.LogOperations.Where(e =>
                                                                        e.statement.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.process.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.user_ip.ToLower().Contains(searchText.ToLower()));

            int totalCount = query.Count();                                              

            PageResult<LogOperations> result = new PageResult<LogOperations>{
                
                Count = totalCount,
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = query.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
            };   
            return Ok(result);
        }
 
    }
}