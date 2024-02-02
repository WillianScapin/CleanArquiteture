using CleanArchiteture.Application.UseCases.CreateUser;
using CleanArchiteture.Application.UseCases.DeleteUser;
using CleanArchiteture.Application.UseCases.GetAllUser;
using CleanArchiteture.Application.UseCases.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArquiteture.WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<ActionResult<CreateUserResponse>> Create(CreateUserRequest request)
        {
            var userId = await _mediator.Send(request);
            return Ok(userId);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetAllUserResponse>>> GetAll(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllUserResponse(), cancellationToken);
            return Ok(response);
        }

        [HttpPut("Update")]
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
        public async Task<ActionResult<DeleteUserResponse>> Delete(Guid? Id,
                                                                CancellationToken cancellationToken)
        {
            if(Id is null)
                return BadRequest();

            var deleteUserRequest = new DeleteUserRequest(Id.Value);

            var response = await _mediator.Send(deleteUserRequest, cancellationToken);
            return Ok(response);
        }

    }
}
