using Api.Data;
using Api.Models.UserModel;
using E_Commerce_Api.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class AuthenticationController : ControllerBase
    {

        private readonly SqlContext _context;

        public AuthenticationController(SqlContext context)
        {
            _context = context;
        }
        #region Register
        [HttpPost("/api/User/Register")]
        public async Task<ActionResult<UserEntity>> RegisterUser(RegisterModel model)
        {
            var _user = await _context.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

            if (_user == null)
            {

                var user = new UserEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PasswordHash = new PasswordHashEntity
                    {
                        Password = model.password
                    }
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }

            else
                return new ConflictResult();
        }

        #endregion

        #region LogIn
        [HttpPost("/api/User/LogIn")]
        public async Task<ActionResult<UserEntity>> LogIn(LogInModel model)
        {
            var _user = await _context.Users.Include(x => x.PasswordHash).FirstOrDefaultAsync(x=> x.Email == model.Email);
            if (_user == null)
            {
                return new BadRequestObjectResult("The Email adress or password is wrong");
            }
   
            if(_user.PasswordHash.Password.ToString() == model.password.ToString())
                return  Ok(_user);    
            else
            return new BadRequestObjectResult("The password is wrong");
            
        }

        #endregion

    }

}
