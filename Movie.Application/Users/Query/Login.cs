using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Movie.Application.Common.Interface;
using Movie.Application.Dtos;
using Movie.Domain.Entities;

namespace Movie.Application.Users.Query
{
    public class Login
    {
         
        public class LoginQuery : IRequest<User>
        {
          public string Username {get; set;}
          public string Password {get; set;}
        }

        public class Handler : IRequestHandler<LoginQuery, User>
        {
          
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {
            _context = context;
            
            }
            public async Task<User> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
              
              var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == request.Username && x.Password == request.Password );
            
              return user;
         
       
          
            }

            
        }
    }
}