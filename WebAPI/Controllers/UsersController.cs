using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {

        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetUserQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getuserlookup")]
        public async Task<IActionResult> GetUserLookup()
        {
            var result = await Mediator.Send(new GetUserLookupQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int userId)
        {
            var result = await Mediator.Send(new GetUserQuery { UserId = userId});
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyfirstname")]
        public async Task<IActionResult> GetByFirstName(string firstName)
        {
            var result = await Mediator.Send(new GetUserQueryByFirstName { FirstName = firstName });
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbylastname")]
        public async Task<IActionResult> GetByLastName(string lastName)
        {
            var result = await Mediator.Send(new GetUserQueryByLastName { LastName = lastName });
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand createUser)
        {
            var result = await Mediator.Send(createUser);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUser)
        {
            var result = await Mediator.Send(updateUser);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUser)
        {
            var result = await Mediator.Send(deleteUser);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
}
