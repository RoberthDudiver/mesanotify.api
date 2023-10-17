using App.Api.Application.UseCase.V1.CallsOperation.Command.Create;
using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using Azure.Core;
using MediatR;
using System;
namespace App.Api.Application.UseCase.V1.CallsOperation.Command.Delete
{
    public class DeleteCallsCommand : IRequest<Response<DeleteCallsResponse>>
    {
        public string Id { get; set; }

    }
    public class DeleteCallsCommandHandler : IRequestHandler<DeleteCallsCommand, Response<DeleteCallsResponse>>
    {
        private readonly IMongoRepository<Calls> _repository;
        private readonly ILogger<DeleteCallsCommandHandler> _logger;

        public DeleteCallsCommandHandler(IMongoRepository<Calls> repository, ILogger<DeleteCallsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<DeleteCallsResponse>> Handle(DeleteCallsCommand request, CancellationToken cancellationToken)
        {
       

            await _repository.DeleteAsync(request.Id);
            _logger.LogDebug("the Calls  was Delete correctly");

            return new Response<DeleteCallsResponse>
            {
                Content = new DeleteCallsResponse
                {
                    Message = "Success",
                
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }

       
    }

}
