using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using App.Core.Utils;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net;
using System.Web.Http;

namespace App.Api.Application.UseCase.V1.UserOperation.Command.Create
{
    public class CreateUserCommand : IRequest<Response<CreateUserResponse>>
    {
        public string? ExternalId { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }


    }
    public class CreateCallsommandHandler : IRequestHandler<CreateUserCommand, Response<CreateUserResponse>>
    {
        private readonly IMongoRepository<User> _repository;

        private readonly ILogger<CreateCallsommandHandler> _logger;

        public CreateCallsommandHandler(IMongoRepository<User> repository, ILogger<CreateCallsommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {


            Expression<Func<User, bool>> filterExpression = x =>
    (string.IsNullOrEmpty(request.Username) || x.Username.Contains(request.Username));

            var rest = await _repository.SearchAsync(filterExpression);


            if (rest != null && rest.Count()>0)
            {
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }

            byte[] passwordHash = null;
            byte[] passwordSalt = null;
            ObjectExtensions. CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            var entity = new User
            {
                ExternalId = request.ExternalId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Email = request.Email,
                Username = request.Username,
                DateAdd = DateTime.UtcNow,
                DateUpd = DateTime.UtcNow,
                UpdateBy = "System",
                CreateBy = "System",
                Active = true
            };

            await _repository.CreateAsync(entity);
            _logger.LogDebug("the calls  was add correctly");

            return new Response<CreateUserResponse>
            {
                Content = new CreateUserResponse
                {
                    Message = "Success",
                    Id = entity.Id
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }


  


    }

}
