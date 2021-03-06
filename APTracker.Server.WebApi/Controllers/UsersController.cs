using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APTracker.Server.WebApi.Commands.User.Modify;
using APTracker.Server.WebApi.Persistence;
using APTracker.Server.WebApi.Persistence.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APTracker.Server.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(_context.Users.OrderBy(x => x.Id));
        }

/*        [HttpPost("setBags")]
        public async Task<IActionResult> SetBags([FromBody] UserSetBagsCommand resource)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == resource.Id);
            if (user == null) return NotFound();
            
            var bags = new List<Bag>();
            foreach (var bagId in resource.Bags)
            {
                var bag = await _context.Bags.FirstOrDefaultAsync(x => x.Id == bagId);
                if (bag == null || bag.Responsible != null)
                    return BadRequest();
                bag.Responsible = user;
                bags.Add(bag);
            }

            _context.Bags.UpdateRange(bags);
            await _context.SaveChangesAsync();
            return Ok();
        }*/

        [HttpPut]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] UserModifyRequest resource)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == resource.Id);
            if (user != null)
            {
                user.Name = resource.Name;
                user.Rate = resource.Rate;
                var entry = _context.Update(user);
                await _context.SaveChangesAsync();
                return Ok(entry.Entity);
            }

            return NotFound();
        }
    }
}