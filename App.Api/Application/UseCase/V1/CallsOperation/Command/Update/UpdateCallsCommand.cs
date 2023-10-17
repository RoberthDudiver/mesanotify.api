using App.Api.Application.UseCase.V1.CallsOperation.Command.Delete;
using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using Azure.Core;
using MediatR;
using System;
using System.Net;
using System.Web.Http;

namespace App.Api.Application.UseCase.V1.CallsOperation.Command.Update
{
    public class UpdateCallsCommand : IRequest<Response<UpdateCallsResponse>>
    {
        public string Id { get; set; }

        public string CallType { get; set; }
        public bool Active { get; set; }
    }
    public class UpdateCallsommandHandler : IRequestHandler<UpdateCallsCommand, Response<UpdateCallsResponse>>
    {
        private readonly IMongoRepository<Calls> _repository;

        private readonly ILogger<UpdateCallsommandHandler> _logger;

        public UpdateCallsommandHandler(IMongoRepository<Calls> repository, ILogger<UpdateCallsommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<UpdateCallsResponse>> Handle(UpdateCallsCommand request, CancellationToken cancellationToken)
        {

            var result = await _repository.GetByIdAsync(request.Id);

            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            result.CallType = request.CallType;

            result.DateUpd = DateTime.UtcNow;
            result.UpdateBy = "System";
            result.Active = request.Active;
            

            await _repository.UpdateAsync(request.Id, result);
            _logger.LogDebug("the Calls  was update correctly");

            return new Response<UpdateCallsResponse>
            {
                Content = new UpdateCallsResponse
                {
                    Message = "Success",
                    Id = result.Id
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }

       
    }

}
