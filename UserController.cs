using JwtToken.DAL;
using JwtToken.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
//using System.Security.Claims;
using System.Threading.Tasks;

namespace JwtToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private UserManager<RegisterMas> _UserManager;
        //private SignInManager<RegisterMas> _SignInManager;
        private readonly ApplicationSettings appSettings;

        private readonly StudentDbContext db;
        //public UserController(StudentDbContext _db,IOptions<ApplicationSettings> _appSettings,UserManager<RegisterMas> UserManager, SignInManager<RegisterMas> SignInManager)
        public UserController(StudentDbContext _db, IOptions<ApplicationSettings> _appSettings)
        {
            db = _db;
            appSettings = _appSettings.Value;
            //_UserManager = UserManager;
            //_SignInManager = SignInManager;
        }

        [HttpPost]
        [Route("PostLogin")]
        public async Task<IActionResult> PostLogin(LoginModel loginModel)
        {

            //int Code = 0;

            //object[] param = { new SqlParameter("@MODE", 3), new SqlParameter("@CODE",Code),
            //new SqlParameter("@EMAIL", loginModel.Email),new SqlParameter("@PASSWORD", loginModel.Password)};



            //var user = await db.LoginModels.FromSqlRaw("SP_REGISTERMAS @MODE,@CODE,@EMAIL,@PASSWORD", param).FirstOrDefaultAsync();
            var user = await (from s in db.registerMas where s.Email ==loginModel.Email && s.Password == loginModel.Password select s).FirstOrDefaultAsync();

            //var user = await _UserManager.FindByNameAsync(loginModel.Email);
            //if (user != null && await _UserManager.CheckPasswordAsync(user, loginModel.Password))
            if (user != null)
            {
                var tokenDescripter = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Code.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescripter);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Email or Password is incorrect!" });

        }



    }
}
