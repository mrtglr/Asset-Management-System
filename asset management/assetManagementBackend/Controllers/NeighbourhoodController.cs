using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assetManagementBackend.Data;
using assetManagementBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assetManagementBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NeighbourhoodController: ControllerBase
    {
        private readonly AssetContext _context;

        public NeighbourhoodController(AssetContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetail
        [HttpGet]
        public async Task<IActionResult> GetNeighbourhood()
        {
            var neighbourhoods = await _context.Neighbourhoods.ToListAsync();           
            return Ok(neighbourhoods);
        }   

        [HttpGet("{id}")]
        public async Task<ActionResult<Neighbourhood>> GetNeighbourhood(int id)
        {
            var neighbourhoods = await _context.Neighbourhoods.FindAsync(id);

            if (neighbourhoods == null)
            {
                return NotFound();
            }

            return Ok(neighbourhoods);
        }

        [HttpGet("filter/{district_id}")]
        public async Task<IActionResult> SearchNeighbourhood(int district_id)
        {
            var neighbourhood = await _context.Neighbourhoods
            .Where(x=>x.district_id==district_id).ToListAsync();
            return Ok(neighbourhood);      
        }    
    }
}