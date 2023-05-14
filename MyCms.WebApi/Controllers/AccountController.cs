using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyCms.DomainClasses.Gallery;
using MyCms.DomainClasses.UserX;

namespace MyCms.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UserManager<IdentityUser> UserManager;
        private SignInManager<IdentityUser> SignInManager;
        private MyCmsDbContext _contex;

        public AccountController(UserManager<IdentityUser> _UserManager, SignInManager<IdentityUser> _SignInManager,
           MyCmsDbContext contex)
        {
            this.SignInManager = _SignInManager;
            this.UserManager = _UserManager;
            this._contex = contex;
        }


        [HttpGet("Get")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "ve1", "ue2" };
        }

        //[Authorize]
        [HttpGet("GetAccounts/{userid}")]
        public async Task<IActionResult> GetAccounts([FromRoute]string userid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await this.UserManager.FindByIdAsync(userid);

            return Ok(user);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserX userX)
        {
            var user = new IdentityUser()
            {
                Email = userX.Email,
                UserName = userX.UserName,
                PhoneNumber = userX.Phone
            };
            var result = await UserManager.CreateAsync(user, userX.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await SignInManager.SignInAsync(user, isPersistent: false);

            return  Ok(createtoken(user));
        }

        [HttpPost("loginForVue")]
        public async Task<IActionResult> loginForVue([FromBody] UserX userX)
        {
            Dictionary<string, string> ReturnObj = new Dictionary<string, string>();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await SignInManager.PasswordSignInAsync(userX.UserName, userX.Password, false, false);//1

            if (!result.Succeeded)
            {
                ReturnObj.Add("CorrectPass", "false");
                ReturnObj.Add("Result", "false");
                return Ok(ReturnObj);
            }

            var user = await UserManager.FindByEmailAsync(userX.Email);//2
            if (user == null)
            {
                ReturnObj.Add("CorrectEmail", "false");
                ReturnObj.Add("Result", "false");
                return Ok(ReturnObj);
            }

            ReturnObj.Add("token", createtoken(user));
            ReturnObj.Add("Result", "true");
            ReturnObj.Add("CorrectEmail", "true");
            ReturnObj.Add("CorrectPass", "true");
            ReturnObj.Add("UserName", user.UserName);
            ReturnObj.Add("expireTime", "60");
            //return Ok(createtoken(user));
            //return Ok(new { token = createtoken(user), expireTime = 60 });//3
            return Ok(ReturnObj);//3
        }

        [HttpPost("RegisterForVue")]
        public async Task<IActionResult> RegisterForVue([FromBody] UserX userX)
        {
            Dictionary<string, object> ReturnObj = new Dictionary<string, object>();
            var user = new IdentityUser()
            {
                Email = userX.Email,
                UserName = userX.UserName,
                PhoneNumber = userX.Phone
            };
            var result = await UserManager.CreateAsync(user, userX.Password);
            if (!result.Succeeded)
            {
                ReturnObj.Add("BadRequest", result.Errors);
                ReturnObj.Add("Result", "false");
                return Ok(ReturnObj);
            }
            //if (!result.Succeeded)
            //    return BadRequest(result.Errors);

            await SignInManager.SignInAsync(user, isPersistent: false);
            ReturnObj.Add("Result", "true");
            ReturnObj.Add("BadRequest", false);
            ReturnObj.Add("token", createtoken(user));

            return Ok(ReturnObj);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] UserX userX)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await SignInManager.PasswordSignInAsync(userX.Email, userX.Password, false, false);//1

            if (!result.Succeeded)//2
                return BadRequest();

            var user = await UserManager.FindByEmailAsync(userX.Email);//2
            if (user == null) return NotFound();
            //return Ok(createtoken(user));
            return Ok(new { token = createtoken(user), expireTime = 60 });//3
        }

        string createtoken(IdentityUser user)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secret key"));

            var jwt = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                ,claims: new List<Claim>
                   {
                            new Claim(JwtRegisteredClaimNames.Sub , user.Id),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                   }
                ,expires: DateTime.Now.AddMinutes(60)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);

        }


    }
}