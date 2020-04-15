using JwtServer.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtServer.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class TokenAuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public TokenAuthController(IUserService userService,
            ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            await Task.CompletedTask;

            return "Welcome";
        }

        [HttpPost]
        public async Task<string> TokenAsync([FromBody]UserRequest user)
        {
            var userLogin = await _userService.LoginAsync(user.Name, user.Password);
            if (user == null)
                return "Login Failed";

            var token = _tokenService.GetToken(userLogin);

            var response = new
            {
                Status = true,
                Token = token,
                Type = "Bearer"
            };

            return JsonConvert.SerializeObject(response);
        }
    }
    public class UserRequest
    {
        public string Name { get; set; }

        public string Password { get; set; }

        //public decimal Price { get; set; }

        //public string Category { get; set; }

        //public string Author { get; set; }
    }
}
