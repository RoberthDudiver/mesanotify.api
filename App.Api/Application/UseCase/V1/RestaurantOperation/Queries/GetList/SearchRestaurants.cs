using App.Api.Common;
using Newtonsoft.Json;
using App.Core.Domain.DTO;
using App.Core.Utils;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace App.Api.Application.UseCase.V1.RestaurantOperation.Queries.GetSearch
{
    public record struct SearchRestaurant : IRequest<Response<List<Restaurant>>>
    {
        public string SearchText { get; set; }
       
    }

    public class SearchPersonHandler : IRequestHandler<SearchRestaurant, Response<List<Restaurant>>>
    {
        private readonly IMongoRepository<Restaurant> _repository;
        private readonly ILogger<SearchPersonHandler> _logger;

        public SearchPersonHandler(IMongoRepository<Restaurant> repository, ILogger<SearchPersonHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<List<Restaurant>>> Handle(SearchRestaurant request, CancellationToken cancellationToken)
        {
            Expression<Func<Restaurant, bool>> filterExpression = x =>
             (string.IsNullOrEmpty(request.SearchText) || x.Name.Contains(request.SearchText)) ||
             (string.IsNullOrEmpty(request.SearchText) || x.UserId.Equals(request.SearchText)) ||
             (string.IsNullOrEmpty(request.SearchText) || x.Address.Contains(request.SearchText));


           var result = await _repository.SearchAsync(filterExpression);

            _logger.LogInformation("Get Many restaurants");

            return new Response<List<Restaurant>>
            {
                Content = result.ToConvertObjects<List<Restaurant>>(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
