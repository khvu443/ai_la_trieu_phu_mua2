using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WhoWantsToBeAMillionaire_API.DataAccess;
using WhoWantsToBeAMillionaire_API.Models;
using WhoWantsToBeAMillionaire_API.Service;

namespace WhoWantsToBeAMillionaire_API.Controllers
{
    [EnableCors("AllowAllHeaders")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly UserDAO userDAO = new UserDAO();
        readonly RoleDAO roleDAO = new RoleDAO();
        
        ApplicationService service = new ApplicationService();

        private readonly IConfiguration _configuration;

        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Register([FromBody] Users user)
        {
            userDAO.CreateUser(user);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLogin login)
        {
            var user = userDAO.GetUser(login);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.username),
                    new Claim(ClaimTypes.Role, roleDAO.GetRoleById(user.role_id).role_name)
                };

                string token = service.CreateToken(claims, _configuration);

                SetCookie("access_token", token, true);
                SetCookie("uid", user.user_id.ToString(), false);
                return Ok(user);
            }
            return BadRequest("User not found");
        }

        [HttpPost, Authorize]
        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                System.Diagnostics.Debug.Write("Cookie: " + cookie);
                Response.Cookies.Delete(cookie, new CookieOptions()
                {
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Secure = true
                });
            }
            return Ok();
        }

        [HttpGet("{id:int}"), Authorize]
        public ActionResult GetUserById(int id)
        {
            return Ok(userDAO.GetUserByUid(id));
        }

        [HttpPut("{id:int}"), Authorize]
        public void UpdateUser(int id, Users user)
        {
            userDAO.UpdateUser(id, user);
        }

        private void SetCookie(string name, string value, bool httpOnly)
        {
            Response.Cookies.Append(name, value, new CookieOptions()
            {
                IsEssential = true,
                Expires = DateTime.Now.AddHours(3),
                Secure = true,
                HttpOnly = httpOnly,
                SameSite = SameSiteMode.None
            });
        }
    }
}
