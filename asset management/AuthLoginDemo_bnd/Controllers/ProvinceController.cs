using System.Collections.Generic;
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
    public class ProvinceController: ControllerBase
    {
        private readonly AuthenticationContext _context;

        public ProvinceController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvince()
        {
            return await _context.Provinces.ToListAsync();
        }   

    }
}