using AutoMapper;
using FluentValidation;
using MediatR;
using Movie.Application.Common.Interface;
using Movie.Domain.Entities;

namespace Movie.Application.Users.Command
{
    public class CreateUser
    {
        public class Command : IRequest<User>
        {
            public User user { get; set; }
        }


        public class Handler : IRequestHandler<Command, User>
     
        {  
            private readonly IValidator< User > _validator;
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            
            public Handler(IValidator<User> validator, IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _validator = validator;
                _mapper = mapper;
            }
            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                  var result = await _validator.ValidateAsync(request.user);
                    if (!result.IsValid)
                       return null;
                    var user = _mapper.Map<User>(request.user);
                    _context.Users.Add(user);
                    var result1 = await _context.SaveChangesAsync(cancellationToken);
                    return user;
               
            } 
            
        }
    }
}