using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using Azure.Core;
using App.Core.Utils;

using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net;
using System.Web.Http;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Api.Application.UseCase.V1.AuthServiceOperation.Command.Create
{
    public class CreateAuthServiceCommand : IRequest<Response<CreateAuthServiceResponse>>
    {
        public string Password { get; set; }
        public string Username { get; set; }
    }
    public class CreateAuthServicommandHandler : IRequestHandler<CreateAuthServiceCommand, Response<CreateAuthServiceResponse>>
    {
        private readonly IMongoRepository<User> _repository;

        private readonly ILogger<CreateAuthServicommandHandler> _logger;
        private readonly IConfiguration _configuration;

        public CreateAuthServicommandHandler(IMongoRepository<User> repository, ILogger<CreateAuthServicommandHandler> logger, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Response<CreateAuthServiceResponse>> Handle(CreateAuthServiceCommand request, CancellationToken cancellationToken)
        {



            Expression<Func<User, bool>> filterExpression = x =>
               (string.IsNullOrEmpty(request.Username) || x.Username.ToLower().Equals(request.Username.ToLower()));

            var result = await _repository.SearchAsync(filterExpression);


            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            var user = result.FirstOrDefault();

            var validate =  ObjectExtensions.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);



            if (!validate)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

      
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtConfig:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenCr = tokenHandler.CreateToken(tokenDescriptor);
            var token= tokenHandler.WriteToken(tokenCr);

 
            _logger.LogDebug("the user  was loged correctly");

            return new Response<CreateAuthServiceResponse>
            {
                Content = new CreateAuthServiceResponse
                {
                    Message = "Success",
                    Token = token
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }

}
