using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_backend.Data;
using test_backend.Models;
using test_backend.ViewModels;

namespace test_backend.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public UserController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public IActionResult AddUser ([FromBody] UserAddVM x)
        {
            if (x.FirstName == "" || x.LastName == "" || x.Email == "" || x.Username == "" || x.Password == "")
                return BadRequest("Include all fields");

            var newUser = new User()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Username = x.Username,
                Password = x.Password
            };

            _dbContext.User.Add(newUser);
            _dbContext.SaveChanges();

            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser ([FromBody] UserAddVM x, int id)
        {
            User user = _dbContext.User.FirstOrDefault(x=>x.Id==id);
            if (user == null)
                return BadRequest("no user with that id");

            user.FirstName = x.FirstName;
            user.LastName = x.LastName;
            user.Email = x.Email;
            user.Username = x.Username;
            user.Password = x.Password;

            _dbContext.SaveChanges();
            return Ok(user);

        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _dbContext.User.Find(id);
            if (user == null)
                return BadRequest("No movie with:" + id + " id.");

            _dbContext.User.Remove(user);
            _dbContext.SaveChanges();
            return Ok(user);
        }
        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            var data = _dbContext.User.ToList();
            return data;
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(_dbContext.User.FirstOrDefault(s => s.Id == id));
        }

    }
}
