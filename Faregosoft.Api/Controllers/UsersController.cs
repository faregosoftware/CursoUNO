using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Faregosoft.Api.Data;
using Faregosoft.Api.Data.Entities;
using Faregosoft.Api.Models;

namespace Faregosoft.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        //////// GET: api/Users
        //////[HttpGet]
        //////public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        //////{
        //////    return await _context.Users.ToListAsync();
        //////}

        //////// GET: api/Users/5
        //////[HttpGet("{id}")]
        //////public async Task<ActionResult<User>> GetUser(int id)
        //////{
        //////    var user = await _context.Users.FindAsync(id);

        //////    if (user == null)
        //////    {
        //////        return NotFound();
        //////    }

        //////    return user;
        //////}

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

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

        [HttpPut("ActivaterUser/{id}")]
        public async Task<IActionResult> ActivaterUser(int id)
        {
            User user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = true;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost,Route("Login")]
        public async Task<ActionResult<User>> Login(LoginRequest model)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);
            if (user == null)
            {
                return BadRequest("Email o Contraseña incorrecta");
            }
            if (!user.IsActive)
            {
                return BadRequest("Usuario no activo. Llamar a sistemas");
            }
            if (user.IsBlock)
            {
                return BadRequest("Usuario estas Bloqueado. Llamar a sistemas");
            }
            return Ok(user);


        }
            // POST: api/Users
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
        public async Task<ActionResult<User>> PostUser(RegisterUser model)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user!=null)
            {
                return BadRequest("Usuario ya registrado");
            }
            user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                IsActive = true
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            User user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsActive = false;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
