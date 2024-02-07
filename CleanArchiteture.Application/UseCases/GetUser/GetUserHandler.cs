using AutoMapper;
using CleanArchiteture.Application.UseCases.GetAllUser;
using CleanArchiteture.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchiteture.Application.UseCases.GetUser
{
    public sealed class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Id, cancellationToken);
            return _mapper.Map<GetUserResponse>(user);
        }
    }
}
