using CleanArchiteture.Application.UseCases.CreateUser;
using CleanArchiteture.Application.UseCases.DeleteUser;
using CleanArchiteture.Application.UseCases.GetAllUser;
using CleanArchiteture.Application.UseCases.GetUser;
using CleanArchiteture.Application.UseCases.UpdateUser;
using CleanArchiteture.Domain.Entities;
using CleanArquiteture.WebAPI.AuthenticationServices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArquiteture.WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        

        public UsersController(IMediator mediator, IJwtAuthenticationService jwtAuthenticationService)
        {
            _mediator = mediator;
            _jwtAuthenticationService = jwtAuthenticationService;
        }

        [HttpPost("GetUser")]
        public async Task<ActionResult<GetUserResponse>> GetUser(GetUserRequest request)
        {
            var user = await _mediator.Send(request);
            string jwt = _jwtAuthenticationService.GenerateToken(user.Id);
            _jwtAuthenticationService.SetTokenCookie(HttpContext, jwt);

            return Ok(user);
        }


        [HttpPost("Create")]
        public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request)
        {
            var user = await _mediator.Send(request);
            string jwt = _jwtAuthenticationService.GenerateToken(user.Id);
            _jwtAuthenticationService.SetTokenCookie(HttpContext, jwt);

            return Ok(user);
        }

        [HttpGet("GetAll")]
        [Authorize]
        public async Task<ActionResult<List<GetAllUserResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUserRequest(), cancellationToken);
            return Ok(response);
        }

        [HttpPut("Update")]
        [Authorize]
        public async Task<ActionResult<UpdateUserResponse>> Update(Guid Id,
                                                                UpdateUserRequest request,
                                                                CancellationToken cancellationToken)
        {
            if (Id != request.Id)
                return BadRequest();

            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<ActionResult<DeleteUserResponse>> Delete(Guid? Id,
                                                                CancellationToken cancellationToken)
        {
            if (Id is null)
                return BadRequest();

            var deleteUserRequest = new DeleteUserRequest(Id.Value);

            var response = await _mediator.Send(deleteUserRequest, cancellationToken);
            return Ok(response);
        }

    }
}
