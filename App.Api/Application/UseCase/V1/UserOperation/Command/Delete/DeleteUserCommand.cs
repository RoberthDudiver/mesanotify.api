using App.Api.Application.UseCase.V1.UserOperation.Command.Create;
using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using Azure.Core;
using MediatR;
using System;
namespace App.Api.Application.UseCase.V1.UserOperation.Command.Delete
{
    public class DeleteUserCommand : IRequest<Response<DeleteUserResponse>>
    {
        public string Id { get; set; }

    }
    public class DeleteUserommandHandler : IRequestHandler<DeleteUserCommand, Response<DeleteUserResponse>>
    {
        private readonly IMongoRepository<User> _repository;
        private readonly ILogger<DeleteUserommandHandler> _logger;

        public DeleteUserommandHandler(IMongoRepository<User> repository, ILogger<DeleteUserommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<DeleteUserResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
       

            await _repository.DeleteAsync(request.Id);
            _logger.LogDebug("the Calls  was Delete correctly");

            return new Response<DeleteUserResponse>
            {
                Content = new DeleteUserResponse
                {
                    Message = "Success",
                
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }

       
    }

}
