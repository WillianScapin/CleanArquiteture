using MediatR;

namespace CleanArchiteture.Application.UseCases.DeleteUser
{
    public sealed record DeleteUserRequest(Guid Id)
                            :IRequest<DeleteUserResponse>
    {
    }
}
