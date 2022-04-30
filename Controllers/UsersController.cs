using Kibernetik.Data;
using Kibernetik.Data.DataUser;
using Kibernetik.Helper;
using Kibernetik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kibernetik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppEFContext _context;

        public UsersController(AppEFContext context)
        {
            _context = context;
        }

        [Route("all")]
        [HttpGet]
        public IActionResult All()
        {
            var users = _context.users.Select(x => x).ToList();
            return Ok(users);
        }

        [Route("deleteuser/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0)
            {
                return StatusCode(404);
            }

            var User = _context.users.SingleOrDefault(x => x.id == id);
            if (User == null)
            {
                return StatusCode(404);
            }
            else
            {
                _context.Remove(User);
                await _context.SaveChangesAsync();
                return Ok(User.id);
            }
        }


        [Route("edituser/{id}")]
        [HttpGet]
        public IActionResult EditCourse(int id)
        {
            var user = _context.users
                .SingleOrDefault(x => x.id == id);
            if (user == null)
            {
                return StatusCode(404);
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPut("saveuser")]
        public async Task<IActionResult> SaveEditedStudent([FromForm] UserModel model)
        {
            var check = _context.users.SingleOrDefault(x => x.id == model.id);

            if (check == null)
            {
                return StatusCode(404);
            }

            if (check.email != model.email)
            {
                check.email = model.email;
                check.login = model.login;
            }
            else
            {
                check.login = model.login;
            }

            _context.Entry(check).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("adduser")]
        public async Task<IActionResult> AddUser([FromForm] AddNewUser model)
        {
            var user = _context.users.SingleOrDefault(x=>x.email == model.email);
            if (user != null)
            {
                return StatusCode(403);
            }

            string HashPassword = EncryptionHelper.HashedPassword(model.password, model.email);

            User newUser = new User { email = model.email, login = model.login, password = HashPassword };

            await _context.users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("searchuser")]
        public IActionResult Search([FromForm] SearchUser text)
        {
            if (text.word == null)
            {
                return Ok(_context.users.Select(x=>x));
            }

            IQueryable<User> users = _context.users.Where(
                  x => x.login.ToLower().Contains(text.word.ToLower())
                  || x.email.ToLower().Contains(text.word.ToLower()));

            return Ok(users);
        }
    }
}
