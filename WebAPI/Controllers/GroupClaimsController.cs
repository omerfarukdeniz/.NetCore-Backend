using Business.Handlers.GroupClaims.Commands;
using Business.Handlers.GroupClaims.Queries;
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
    public class GroupClaimsController : BaseApiController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetGroupClaimsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetGroupClaimQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getgroupclaimsbygroupid")]
        public async Task<IActionResult> GetGroupClaimsByGroupId(int id)
        {
            var result = await Mediator.Send(new GetGroupClaimsLookupByGroupIdQuery { GroupId = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGroupClaimCommand createGroupClaim)
        {
            var result = await Mediator.Send(createGroupClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGroupClaimCommand updateGroupClaim)
        {
            var result = await Mediator.Send(updateGroupClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGroupClaimCommand deleteGroupClaim)
        {
            var result = await Mediator.Send(deleteGroupClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
