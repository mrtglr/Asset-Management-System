using System.Linq;
using System.Threading.Tasks;
using AuthLoginDemo_bnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthLoginDemo_bnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DistrictController: ControllerBase
    {
        private readonly AuthenticationContext _context;

        public DistrictController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetDistrict()
        {
            var ilce = await _context.Districts.ToListAsync();           
            return Ok(ilce);
        }   

        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> SearchDistrict(int province_id)
        {
            var district = await _context.Districts
            .Where(x=>x.province_id==province_id).ToListAsync();
            return Ok(district);      
        }    
    }
}