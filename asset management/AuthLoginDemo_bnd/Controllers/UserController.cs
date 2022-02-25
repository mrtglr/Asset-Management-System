using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthLoginDemo_bnd.Helpers;
using AuthLoginDemo_bnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthLoginDemo_bnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AuthenticationContext _context;
        private UserManager<ApplicationUser> _userManager;

        public UserController(AuthenticationContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Filter")]
        public async Task<IActionResult> SearchUser(string filter)
        {
            filter = filter.ToLower();
            var kullanici = new List<ApplicationUser>();
            kullanici = await _context.ApplicationUsers
            .Where(x=>(x.isActive==true)&&
            ((x.FullName.ToLower().Contains(filter))||
            (x.UserAddress.ToLower().Contains(filter))||
            (x.Email.ToLower().Contains(filter)))).ToListAsync();
            return Ok(kullanici);      
        }    

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetUser()
        {
            var ApplicationUsers = await _context.ApplicationUsers.Select(x => new {
                
                ApplicationUser_id = x.Id,
                ApplicationUser_name = x,
                ApplicationUser_email = x.Email,
                ApplicationUser_role = x.UserRole,
                isActive = x.isActive,
                ApplicationUser_adress = x.UserAddress,
                ApplicationUser_password = x.PasswordHash

            }).Where(x=>x.isActive==true).ToListAsync();
            return Ok(ApplicationUsers);
        }        

        [HttpGet]
        [Route("List")]
        public IActionResult List([FromQuery] string searchText, [FromQuery] int? page, [FromQuery] int pagesize=7){

            var query = string.IsNullOrEmpty(searchText)? _context.ApplicationUsers
                                                        : _context.ApplicationUsers.Where(e =>
                                                                        e.FullName.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.Email.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.UserAddress.ToLower().Contains(searchText.ToLower()));

            int totalCount = query.Count();                                              

            PageResult<ApplicationUser> result = new PageResult<ApplicationUser>{
                
                Count = totalCount,
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = query.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
            };   
            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser(ApplicationUser ApplicationUser)
        {
            string _AuthorizeduserID = User.Claims.First(c => c.Type == "UserID").Value;
           
            if(isUserExcact(ApplicationUser) || _AuthorizeduserID.Equals(ApplicationUser.Id))
            {
                Helper.createLog(_context, false, _AuthorizeduserID, "ApplicationUser update", "failed because the ApplicationUser information is the same");
                await _context.SaveChangesAsync();
                return BadRequest();                
            }

            _context.Entry(ApplicationUser).State = EntityState.Modified;

            Helper.createLog(_context, true, _AuthorizeduserID, "ApplicationUser update", "ApplicationUser is updated successfully");
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(_AuthorizeduserID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<ApplicationUser>> GetUserById()
        {
            string _AuthorizeduserID = User.Claims.First(c => c.Type == "UserID").Value;
            var ApplicationUser = await _context.ApplicationUsers.FindAsync(_AuthorizeduserID);

            if (ApplicationUser == null)
            {
                return NotFound();
            }

            return Ok(ApplicationUser);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult<ApplicationUser>> DeleteUser()
        {
            string _AuthorizeduserID = User.Claims.First(c => c.Type == "UserID").Value;

            var ApplicationUser = await _context.ApplicationUsers.FindAsync(_AuthorizeduserID);
            if (ApplicationUser == null)
            {
                return NotFound();
            }
            
            Helper.createLog(_context, true, _AuthorizeduserID, "ApplicationUser delete", "ApplicationUser is deleted");
            ApplicationUser.isActive = false;
            await _context.SaveChangesAsync();

            return Ok(ApplicationUser);
        }

        private bool UserExists(string id)
        {
            return _context.ApplicationUsers.Any(e => e.Id.Equals(id));
        }

        private bool isUserExcact(ApplicationUser ApplicationUser) {
            return _context.ApplicationUsers.Any(e => 
            (e.FullName==ApplicationUser.FullName  
            && e.Email==ApplicationUser.Email 
            && e.UserAddress==ApplicationUser.UserAddress 
            && e.UserRole==ApplicationUser.UserRole));
        }        

    }
}