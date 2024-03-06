using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPITest.Data.Models;

namespace RestAPITest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public AccountController(UserManager<APPUser> userManager)
        {
            _userManager = userManager;
        }
        private readonly UserManager<APPUser> _userManager;
        [HttpPost]
        public async Task<IActionResult> RegistNewUser(DTONewUser dtouser)
        {
            if(ModelState.IsValid)
            {
                APPUser appuser = new()
                {
                    UserName = dtouser.UserName,
                    Email = dtouser.Email
                };
                IdentityResult result= await  _userManager.CreateAsync(appuser, dtouser.Password);
                if (result.Succeeded)
                {
                    return Ok("success");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);

                    }
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(DTOLogin login)
        {
            if (ModelState.IsValid)
            {
                APPUser? user = await _userManager.FindByNameAsync(login.UserName);
                if(user != null)
                {
                    if (await _userManager.CheckPasswordAsync(user, login.Password))
                    {
                        return Ok("token");
                    }
                }
                else
                {
                    return Unauthorized();
                }
            }
            return BadRequest(ModelState);
        }
    }
}
