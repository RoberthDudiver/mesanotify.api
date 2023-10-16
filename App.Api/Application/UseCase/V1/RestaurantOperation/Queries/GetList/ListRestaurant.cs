using App.Api.Common;
using Newtonsoft.Json;
using App.Core.Domain.DTO;
using App.Core.Utils;

using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using MediatR;
using System;

namespace App.Api.Application.UseCase.V1.RestaurantOperation.Queries.GetList
{
    public record struct ListRestaurant : IRequest<Response<List<Restaurant>>>
    {
    }

    public class ListPersonHandler : IRequestHandler<ListRestaurant, Response<List<Restaurant>>>
    {
        private readonly IMongoRepository<Restaurant> _repository;
        private readonly ILogger<ListPersonHandler> _logger;

        public ListPersonHandler(IMongoRepository<Restaurant> repository , ILogger<ListPersonHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<List<Restaurant>>> Handle(ListRestaurant request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();


            _logger.LogInformation("Trajo uchas cosas");
            return new Response<List<Restaurant>>
            {
                Content = result.ToConvertObjects<List<Restaurant>>(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

    
    }
}
