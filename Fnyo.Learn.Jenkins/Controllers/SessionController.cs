using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Fnyo.Learn.Jenkins.Controllers
{
    [ApiController]
    [Route("session")]
    public class SessionController:ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccesor;
        public SessionController(IHttpContextAccessor httpContextAccesor)
        {
            _httpContextAccesor= httpContextAccesor;
        }
        [HttpGet("get")]
        public async Task<string> GetCookie(string userName,string password)
        {

            // request  response

            // response  Header  Set-Cookie:  identity=username=admin&password=admin
            await Task.Delay(500);
            if(userName == "admin" && password == "admin")
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                _httpContextAccesor.HttpContext.Response.Cookies.Append("identity",
                    $"username={userName}&password={password}");
                return "Login Success";
            }
            else
            {
                _httpContextAccesor.HttpContext.Response.Cookies.Delete("identity");
                return "Login Fail";
            }
        }

        [HttpGet("resource")]
        public async Task<string> GetResource()
        {

            // request  Header Cookie: identity = username=admin&password=admin
            await Task.Delay(500);
            var returnstring = $"you already login , your username is ";
            var cookie = _httpContextAccesor.HttpContext.Request.Cookies;
            if (cookie.TryGetValue("identity", out string identity))
            {
                var principal = Decode(identity);
                returnstring += principal.username;
            }
            else
            {
                return "please login";
            }


            return returnstring;
        }

        [NonAction]
        private (string username,string password) Decode(string encrypt)
        {
            string username = string.Empty;
            string password = string.Empty; 
            string[] identity = encrypt.Split('&');
            foreach(string part in identity)
            {
                var parts = part.Split('=');
                var key = parts[0];
                var value = parts[1];
                if(key == "username") username = value;
                if(key == "password") password = value;
            }
            return (username,password);
        }

    }
}
