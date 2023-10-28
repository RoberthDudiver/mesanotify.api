using App.Api.Common;
using Newtonsoft.Json;
using App.Core.Domain.DTO;
using App.Core.Utils;

using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using MediatR;
using System;
using ZstdSharp;

namespace App.Api.Application.UseCase.V1.CallsOperation.Queries.Get
{
    public record struct GetCalls : IRequest<Response<Calls>>
    {
        public string Id { get; set; }
    }

    public class GetCallsHandler : IRequestHandler<GetCalls, Response<Calls>>
    {
        private readonly IMongoRepository<Calls> _repository;
        private readonly ILogger<GetCallsHandler> _logger;

        public GetCallsHandler(IMongoRepository<Calls> repository , ILogger<GetCallsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<Calls>> Handle(GetCalls request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id);

            if(result == null)
            {
                 throw new  UnauthorizedAccessException("Not found");
            }
            _logger.LogInformation("Trajo uchas cosas");
            return new Response<Calls>
            {
                Content = result.ToConvertObjects<Calls>(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

    
    }
}
