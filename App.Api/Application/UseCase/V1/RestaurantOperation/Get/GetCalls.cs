using App.Api.Common;
using Newtonsoft.Json;
using App.Core.Domain.DTO;
using App.Core.Utils;

using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using MediatR;
using System;
using ZstdSharp;

namespace App.Api.Application.UseCase.V1.RestaurantOperation.Queries.Get
{
    public record struct GetRestaurant : IRequest<Response<Restaurant>>
    {
        public string Id { get; set; }
    }

    public class GetCallsHandler : IRequestHandler<GetRestaurant, Response<Restaurant>>
    {
        private readonly IMongoRepository<Restaurant> _repository;
        private readonly ILogger<GetCallsHandler> _logger;

        public GetCallsHandler(IMongoRepository<Restaurant> repository , ILogger<GetCallsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<Restaurant>> Handle(GetRestaurant request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id);

            if(result == null)
            {
                 throw new  UnauthorizedAccessException("Not found");
            }
            _logger.LogInformation("Trajo uchas cosas");
            return new Response<Restaurant>
            {
                Content = result.ToConvertObjects<Restaurant>(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

    
    }
}
