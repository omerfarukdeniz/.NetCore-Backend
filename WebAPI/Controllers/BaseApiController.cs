using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.Results;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();


        [NonAction]
        protected IActionResult Success<T>(ApiResult<T> data)
        {
            return Ok();
        }
        
        [NonAction]
        protected IActionResult Created<T>(ApiResult<T> data)
        {
            return StatusCode(201, data);
        }

        [NonAction]
        protected IActionResult NoContent<T>(ApiResult<T> data)
        {
            return StatusCode(204, data);
        }

        [NonAction]
        protected IActionResult BadRequest<T>(ApiResult<T> data)
        {
            return StatusCode(400, data);
        }

        [NonAction]
        protected IActionResult Unauthorized<T>(ApiResult<T> data)
        {
            return StatusCode(401, data);
        }

        [NonAction]
        protected IActionResult Forbidden<T>(ApiResult<T> data)
        {
            return StatusCode(403, data);
        }

        [NonAction]
        protected IActionResult NotFound<T>(ApiResult<T> data)
        {
            return StatusCode(404, data);
        }

        [NonAction]
        protected IActionResult Error<T>(ApiResult<T> data)
        {
            return StatusCode(500, data);
        }

        
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        [NonAction]
        protected IActionResult Success<T>(string message, string internalMessage, T data)
        {
            return Success(new ApiResult<T>
            {
                Success = true,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }


        [NonAction]
        protected IActionResult Created<T>(string message, string internalMessage, T data)
        {
            return Created(new ApiResult<T>
            {
                Success = true,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult NoContent<T>(string message, string internalMessage, T data)
        {
            return NoContent(new ApiResult<T>
            {
                Success = true,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult BadRequest<T>(string message, string internalMessage, T data)
        {
            return BadRequest(new ApiResult<T>
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult Unauthorized<T>(string message, string internalMessage, T data)
        {
            return Unauthorized(new ApiResult<T>
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult Forbidden<T>(string message, string internalMessage, T data)
        {
            return Forbidden(new ApiResult<T>
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult NotFound<T>(string message, string internalMessage, T data)
        {
            return NotFound(new ApiResult<T>
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }


        [NonAction]
        protected IActionResult Error<T>(string message, string internalMessage, T data)
        {
            return Error(new ApiResult<T>
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }
    }
}
