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
    public class UserController : ControllerBase
    {
        private readonly AssetContext _context;

        public UserController(AssetContext context)
        {
            _context = context;
        }

        [HttpGet("filter/{filter}")]
        public async Task<IActionResult> SearchUser(string filter)
        {
            filter = filter.ToLower();
            var kullanici = new List<User>();
            kullanici = await _context.Users
            .Where(x=>(x.isActive==true)&&
            ((x.user_name.ToLower().Contains(filter))||
            (x.user_surname.ToLower().Contains(filter))||
            (x.user_adress.ToLower().Contains(filter))||
            (x.user_email.ToLower().Contains(filter)))).ToListAsync();
            return Ok(kullanici);      
        }    

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _context.Users.Select(x => new {
                
                user_id = x.user_id,
                user_name = x.user_name,
                user_surname = x.user_surname,
                user_email = x.user_email,
                user_role = x.user_role,
                isActive = x.isActive,
                user_adress = x.user_adress,
                user_password = DTOS.Helper.DecodeFrom64(x.user_password)

            }).Where(x=>x.isActive==true).ToListAsync();
            return Ok(users);
        }        

        [HttpGet("List")]
        public IActionResult List([FromQuery] string searchText, [FromQuery] int? page, [FromQuery] int pagesize=7){

            var query = string.IsNullOrEmpty(searchText)? _context.Users
                                                        : _context.Users.Where(e =>
                                                                        e.user_name.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.user_surname.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.user_email.ToLower().Contains(searchText.ToLower()) ||
                                                                        e.user_adress.ToLower().Contains(searchText.ToLower()));

            int totalCount = query.Count();                                              

            PageResult<User> result = new PageResult<User>{
                
                Count = totalCount,
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = query.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
            };   
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if(isUserExist(user) || id!=user.user_id)
            {
                Helper.createLog(_context, false, 0, "user update", "failed because the user information is the same");
                await _context.SaveChangesAsync();
                return BadRequest();                
            }
            user.user_password = Helper.EncodePasswordToBase64(user.user_password);
            _context.Entry(user).State = EntityState.Modified;

            Helper.createLog(_context, true, 0, "user update", "user is updated successfully");
            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("login/{id}")]
        public async Task<ActionResult<User>> LOginUser(int id)
        {
            Helper.createLog(_context, true, id, "login", "user is logged in");
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("logout/{id}")]
        public async Task<IActionResult> LogOutUser(int id)
        {
            Helper.createLog(_context, true, id, "logout", "user is logged out");
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);      
        }    

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.isActive = true;
            if(!isUserExist(user)){
                _context.Users.Add(user);
                Helper.createLog(_context, true, 0, "user add", "user is added successfully");
                user.user_password = Helper.EncodePasswordToBase64(user.user_password);
                await _context.SaveChangesAsync();
            }
            else{
                Helper.createLog(_context, false, 0, "user add", "failed because the user information is same with another user");            
                await _context.SaveChangesAsync();
                return BadRequest();
            }            
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            Helper.createLog(_context, true, 0, "user delete", "user is deleted");
            user.isActive = false;
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.user_id == id);
        }

        private bool isUserExist(User user) {
            return _context.Users.Any(e => (e.user_name==user.user_name 
            && e.user_surname==user.user_surname 
            && e.user_email==user.user_email 
            && e.user_adress==user.user_adress 
            && e.user_role==user.user_role));
        }        

    }
}