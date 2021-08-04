using Business.Handlers.Groups.Commands;
using Business.Handlers.Groups.Queries;
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
    public class GroupsController : BaseApiController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetGroupsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int groupId)
        {
            var result = await Mediator.Send(new GetGroupQuery { GroupId = groupId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getgrouplookup")]
        public async Task<IActionResult> GetSelectedList()
        {
            var result = await Mediator.Send(new GetGroupLookupQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("searchgroupbygroupname")]
        public async Task<IActionResult> SearchGroupByGroupName(string groupName)
        {
            var result = await Mediator.Send(new SearchGroupsByNameQuery { GroupName = groupName});
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGroupCommand createGroup)
        {
            var result = await Mediator.Send(createGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGroupCommand updateGroup)
        {
            var result = await Mediator.Send(updateGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGroupCommand deleteGroup)
        {
            var result = await Mediator.Send(deleteGroup);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
