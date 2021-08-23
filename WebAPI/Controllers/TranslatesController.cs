using Business.Handlers.Translates.Commands;
using Business.Handlers.Translates.Queries;
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
    public class TranslatesController : BaseApiController
    {
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetTranslatesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int translateId)
        {
            var result = await Mediator.Send(new GetTranslateQuery { Id = translateId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("gettranslatelistdto")]
        public async Task<IActionResult> GetTranslateListDto()
        {
            var result = await Mediator.Send(new GetTranslateListDtoQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("gettranslatesbylang")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTranslatesByLang(string lang)
        {
            var result = await Mediator.Send(new GetTranslatesByLangQuery() { Lang = lang });
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTranslateCommand createTranslate)
        {
            var result = await Mediator.Send(createTranslate);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTranslateCommand updateTranslate)
        {
            var result = await Mediator.Send(updateTranslate);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTranslateCommand deleteTranslate)
        {
            var result = await Mediator.Send(deleteTranslate);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
