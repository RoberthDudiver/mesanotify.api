using App.Api.Common;
using Newtonsoft.Json;
using App.Core.Domain.DTO;
using App.Core.Utils;

using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using MediatR;
using System;
using ZstdSharp;

namespace App.Api.Application.UseCase.V1.UserOperation.Queries.Get
{
    public record struct GetUser : IRequest<Response<User>>
    {
        public string Id { get; set; }
    }

    public class GetCallsHandler : IRequestHandler<GetUser, Response<User>>
    {
        private readonly IMongoRepository<User> _repository;
        private readonly ILogger<GetCallsHandler> _logger;

        public GetCallsHandler(IMongoRepository<User> repository , ILogger<GetCallsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<User>> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id);

            if(result == null)
            {
                 throw new  UnauthorizedAccessException("Not found");
            }
            _logger.LogInformation("Trajo muchas cosas");
            return new Response<User>
            {
                Content = result.ToConvertObjects<User>(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

    
    }
}
