using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Movie.Application.Common.Dtos;
using Movie.Application.Common.Interface;
using Movie.Application.Dtos;
using Movie.Domain.Entities;

namespace Movie.Application.Users.Command
{
    public class Register
    {
        public class Command : IRequest<Unit>
        {
           public RegisterDto registerDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request.registerDto);
                _context.Users.Add(user);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
    }
}
}