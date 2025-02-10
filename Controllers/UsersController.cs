using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // For simplicity, we use an in-memory list.
        // In a production app, you would likely use a database.
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Name = "Alice", Email = "alice@example.com" },
            new User { Id = 2, Name = "Bob", Email = "bob@example.com" }
        };

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> Post([FromBody] User user)
        {
            // Simple validation
            if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Email))
                return BadRequest("Both Name and Email are required.");

            // Generate a new ID for the user (in a real app, this would be handled by the database)
            user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
            users.Add(user);

            // Return a Created response with a link to the new resource
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(updatedUser.Name) || string.IsNullOrWhiteSpace(updatedUser.Email))
                return BadRequest("Both Name and Email are required.");

            user.Name = updatedUser.Name;
            user.Email = updatedUser.Email;
            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound();

            users.Remove(user);
            return NoContent();
        }
    }
}
