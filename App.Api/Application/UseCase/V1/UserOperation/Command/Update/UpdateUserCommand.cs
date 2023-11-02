using App.Api.Application.UseCase.V1.UserOperation.Command.Delete;
using App.Api.Common;
using App.Core.Domain.Entities;
using App.Core.Utils;
using App.Infrastructure.Repositories;
using Azure.Core;
using MediatR;
using System;
using System.Net;
using System.Web.Http;

namespace App.Api.Application.UseCase.V1.UserOperation.Command.Update
{
    public class UpdateUserCommand : IRequest<Response<UpdateUserResponse>>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }
    }
    public class UpdateUserommandHandler : IRequestHandler<UpdateUserCommand, Response<UpdateUserResponse>>
    {
        private readonly IMongoRepository<User> _repository;

        private readonly ILogger<UpdateUserommandHandler> _logger;

        public UpdateUserommandHandler(IMongoRepository<User> repository, ILogger<UpdateUserommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<UpdateUserResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {

            var result = await _repository.GetByIdAsync(request.Id);

            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            byte[] passwordHash = null;
            byte[] passwordSalt = null;
            ObjectExtensions.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

            result.PasswordHash = passwordHash;
            result.PasswordSalt = passwordSalt;
            result.Email = request.Email;


            result.DateUpd = DateTime.UtcNow;
            result.UpdateBy = "System";
            result.Active = request.Active;
            

            await _repository.UpdateAsync(request.Id, result);
            _logger.LogDebug("the user  was update correctly");

            return new Response<UpdateUserResponse>
            {
                Content = new UpdateUserResponse
                {
                    Message = "Success",
                    Id = result.Id
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }

       
    }

}
