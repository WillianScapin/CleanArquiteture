using MediatR;


namespace CleanArchiteture.Application.UseCases.GetAllUser
{
    public sealed record GetAllUserRequest : 
                             IRequest<List<GetAllUserResponse>>
    {
    }
}
