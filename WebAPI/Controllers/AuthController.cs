using Business.Handlers.Authorizations.Commands;
using Business.Handlers.Authorizations.Queries;
using Business.Handlers.Users.Commands;
using Business.Services.Authentication.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [ProducesResponseType(typeof(LoginUserResult),200)]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserQuery loginModel)
        {
            var result = await Mediator.Send(loginModel);
            if (result.Success)
                return Ok(result);
            return Unauthorized(result.Message);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerUser)
        {
            var result = await Mediator.Send(registerUser);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpPut("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand forgotPassword)
        {
            var result = await Mediator.Send(forgotPassword);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpPut("changeuserpassword")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] UserChangePasswordCommand command)
        {
            var result = await Mediator.Send(command);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost("verify")]
        public async Task<IActionResult> Verification([FromBody] VerifyCidQuery verifyCid)
        {
            var result = await Mediator.Send(verifyCid);
            if (result.Success)
                return Ok(result.Message);

            return BadRequest(result.Message);
        }

    }
}
