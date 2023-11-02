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

namespace App.Api.Application.UseCase.V1.UserOperation.Queries.GetSearch
{
    public record struct SearchUser : IRequest<Response<List<User>>>
    {
        public string SearchText { get; set; }
       
    }

    public class SearchPersonHandler : IRequestHandler<SearchUser, Response<List<User>>>
    {
        private readonly IMongoRepository<User> _repository;
        private readonly ILogger<SearchPersonHandler> _logger;

        public SearchPersonHandler(IMongoRepository<User> repository, ILogger<SearchPersonHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<List<User>>> Handle(SearchUser request, CancellationToken cancellationToken)
        {
            Expression<Func<User, bool>> filterExpression = x =>
             (string.IsNullOrEmpty(request.SearchText) || x.Username.Contains(request.SearchText));


            var result = await _repository.SearchAsync(filterExpression);

            _logger.LogInformation("Get Many uers");

            return new Response<List<User>>
            {
                Content = result.ToConvertObjects<List<User>>(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
