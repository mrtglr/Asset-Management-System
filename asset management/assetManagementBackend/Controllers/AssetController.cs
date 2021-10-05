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
    public class AssetController : ControllerBase
    {
        private readonly AssetContext _context;

        public AssetController(AssetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsset()
        {
            var assets = await _context.Assets.Select(x => new {
                
                asset_id = x.asset_id, 
                user_id = x.User.user_id,
                User = x.User,
                worth = x.worth,
                attribute = x.attribute,
                adress = x.adress,
                isActive = x.isActive,
                neighbourhood_id = x.neighbourhood_id,
                neighbourhood_name = x.Neighbourhood.neighbourhood_name,
                district_name = x.Neighbourhood.District.district_name,
                province_name = x.Neighbourhood.District.Province.province_name

            }).Where(x=>x.isActive==true).ToListAsync();
            return Ok(assets);
        }      

        [HttpGet("asset/{user_id}")]
        public async Task<ActionResult<User>> GetUserAssets(int user_id)
        {
                var assets = await _context.Assets.Select(x => new {
                
                asset_id = x.asset_id, 
                user_id = x.User.user_id,
                worth = x.worth,
                attribute = x.attribute,
                adress = x.adress,
                isActive = x.isActive,
                neighbourhood_id = x.neighbourhood_id,
                neighbourhood_name = x.Neighbourhood.neighbourhood_name,
                district_name = x.Neighbourhood.District.district_name,
                province_name = x.Neighbourhood.District.Province.province_name

            }).Where(x=>(x.isActive==true) && user_id == x.user_id).ToListAsync();
            return Ok(assets);
        }  

        [HttpGet("List")]
        public IActionResult List([FromQuery] string searchText, [FromQuery] int? page, [FromQuery] int pagesize=7){

            var query = string.IsNullOrEmpty(searchText)? _context.Assets
                                                        : _context.Assets.Where(x =>
                                                        ((x.adress.ToLower().StartsWith(searchText.ToLower()))||
                                                        (x.worth.ToString().StartsWith(searchText.ToLower()))||
                                                        (x.attribute.ToLower().StartsWith(searchText.ToLower()))));

            int totalCount = query.Count();                                              

            PageResult<Asset> result = new PageResult<Asset>{
                
                Count = totalCount,
                PageIndex = page ?? 1,
                PageSize = pagesize,
                Items = query.Skip((page - 1 ?? 0) * pagesize).Take(pagesize).ToList()
            };   
            return Ok(result);
        }

        [HttpGet("filter/{filter}")]
        public async Task<IActionResult> SearchAsset(string filter)
        {
            filter = filter.ToLower();
            var assets = await _context.Assets.Select(x => new {
                
                asset_id = x.asset_id, 
                user_id = x.User.user_id,
                worth = x.worth,
                attribute = x.attribute,
                adress = x.adress,
                isActive = x.isActive,
                neighbourhood_id = x.neighbourhood_id,
                neighbourhood_name = x.Neighbourhood.neighbourhood_name,
                district_name = x.Neighbourhood.District.district_name,
                province_name = x.Neighbourhood.District.Province.province_name

            }).Where(x=>(x.isActive==true)&&
            ((x.adress.ToLower().Contains(filter))||
            (x.worth.ToString().Contains(filter))||
            (x.province_name.ToLower().Contains(filter))||
            (x.district_name.ToLower().Contains(filter))||
            (x.neighbourhood_name.ToLower().Contains(filter))||
            (x.attribute.ToLower().Contains(filter)))).ToListAsync();
            return Ok(assets);      
        }    

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(int id, Asset asset)
        {
            if(isAssetExist(asset) || id!=asset.asset_id)
            {
                Helper.createLog(_context, false, 0, "asset update", "failed because the asset information is the same");
                await _context.SaveChangesAsync();
                return BadRequest();
            }
            Helper.createLog(_context, true, 0, "asset update", "asset is updated successfully");
            await _context.SaveChangesAsync();
            _context.Entry(asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
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

        // GET: api/PaymentDetail/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            return Ok(asset);
        }

        // POST: api/PaymentDetail
        [HttpPost]
        public async Task<ActionResult<Asset>> PostAsset([FromBody]Asset asset)
        {
            asset.isActive = true;
            if(!isAssetExist(asset)){
                 _context.Assets.Add(asset);
                Helper.createLog(_context, true, 0, "asset add", "asset is added successfully");
                await _context.SaveChangesAsync();
            }
            else{
                Helper.createLog(_context, false, 0, "asset add", "failed because the asset information is same with another asset");
                await _context.SaveChangesAsync();
                return BadRequest();
            }          
            return Ok(asset);
        }

        // DELETE: api/PaymentDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Asset>> DeleteAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            if (asset == null)
            {
                return NotFound();
            }

            Helper.createLog(_context, true, 0, "asset delete", "asset is deleted successfully");
            asset.isActive = false;
            await _context.SaveChangesAsync();

            return Ok(asset);
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.asset_id == id);
        }

        private bool isAssetExist(Asset asset) {
            return _context.Assets.Any(e => (e.neighbourhood_id==asset.neighbourhood_id  
            && e.attribute==asset.attribute 
            && e.user_id==asset.user_id
            && e.adress==asset.adress));
        }      
    }
}