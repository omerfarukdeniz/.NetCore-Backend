using Business.Handlers.UserGroups.Commands;
using Business.Handlers.UserGroups.Queries;
using Microsoft.AspNetCore.Authorization;
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
    public class UserGroupsController : BaseApiController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetUserGroupsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var result = await Mediator.Send(new GetUserGroupQuery { UserId = userId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpGet("getusergroupbyuserid")]
        public async Task<IActionResult> GetGroupClaimsByUserId(int id)
        {
            var result = await Mediator.Send(new GetUserGroupLookupByUserIdQuery { UserId = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }


        [HttpGet("getusersingroupbygroupid")]
        public async Task<IActionResult> GetUsersInGroupByGroupId(int id)
        {
            var result = await Mediator.Send(new GetUsersInGroupLookupByGroupIdQuery { GroupId = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserGroupCommand createUserGroup)
        {
            var result = await Mediator.Send(createUserGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserGroupCommand updateUserGroup)
        {
            var result = await Mediator.Send(updateUserGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("updatebygroupid")]
        public async Task<IActionResult> UpdateByGroupId([FromBody] UpdateUserGroupByGroupIdCommand updateUserGroup)
        {
            var result = await Mediator.Send(updateUserGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserGroupCommand deleteUserGroup)
        {
            var result = await Mediator.Send(deleteUserGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
