using System.Linq;
using System.Threading.Tasks;
using assetManagementBackend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assetManagementBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DistrictController: ControllerBase
    {
        private readonly AssetContext _context;

        public DistrictController(AssetContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<IActionResult> GetDistrict()
        {
            var ilce = await _context.Districts.ToListAsync();           
            return Ok(ilce);
        }   

        [HttpGet("filter/{province_id}")]
        public async Task<IActionResult> SearchDistrict(int province_id)
        {
            var district = await _context.Districts
            .Where(x=>x.province_id==province_id).ToListAsync();
            return Ok(district);      
        }    
    }
}