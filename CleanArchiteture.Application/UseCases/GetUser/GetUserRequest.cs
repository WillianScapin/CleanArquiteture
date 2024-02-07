using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Application.UseCases.GetUser
{
    public sealed record GetUserRequest(Guid Id)
                            : IRequest<GetUserResponse>
    {
    }
}
