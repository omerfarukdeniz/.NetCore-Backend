using Business.Handlers.UserClaims.Commands;
using Business.Handlers.UserClaims.Queries;
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
    public class UserClaimsController : BaseApiController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetUserClaimsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await Mediator.Send(new GetUserClaimLookupQuery { UserId = userId });
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getoperationclaimbyuserid")]
        public async Task<IActionResult> GetOperationClaimByUserId(int id)
        {
            var result = await Mediator.Send(new GetUserClaimLookupByUserIdQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserClaimCommand createUserClaim)
        {
            var result = await Mediator.Send(createUserClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserClaimCommand updateUserClaim)
        {
            var result = await Mediator.Send(updateUserClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserClaimCommand deleteUserClaim)
        {
            var result = await Mediator.Send(deleteUserClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

    }
}
