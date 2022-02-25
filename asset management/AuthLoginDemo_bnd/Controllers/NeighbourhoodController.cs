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
    public class NeighbourhoodController: ControllerBase
    {
        private readonly AuthenticationContext _context;

        public NeighbourhoodController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetNeighbourhood()
        {
            var neighbourhoods = await _context.Neighbourhoods.ToListAsync();           
            return Ok(neighbourhoods);
        }   

        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<Neighbourhood>> GetNeighbourhood(int id)
        {
            var neighbourhoods = await _context.Neighbourhoods.FindAsync(id);

            if (neighbourhoods == null)
            {
                return NotFound();
            }

            return Ok(neighbourhoods);
        }

        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> SearchNeighbourhood(int district_id)
        {
            var neighbourhood = await _context.Neighbourhoods
            .Where(x=>x.district_id==district_id).ToListAsync();
            return Ok(neighbourhood);      
        }    
    }
}