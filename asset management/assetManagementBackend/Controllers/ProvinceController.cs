using System.Collections.Generic;
using System.Threading.Tasks;
using assetManagementBackend.Data;
using assetManagementBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assetManagementBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvinceController: ControllerBase
    {
        private readonly AssetContext _context;

        public ProvinceController(AssetContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvince()
        {
            return await _context.Provinces.ToListAsync();
        }   

    }
}