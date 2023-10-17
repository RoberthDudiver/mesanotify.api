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

namespace App.Api.Application.UseCase.V1.CallsOperation.Queries.GetSearch
{
    public record struct SearchCalls : IRequest<Response<List<Calls>>>
    {
        public string SearchText { get; set; }
       
    }

    public class SearchPersonHandler : IRequestHandler<SearchCalls, Response<List<Calls>>>
    {
        private readonly IMongoRepository<Calls> _repository;
        private readonly ILogger<SearchPersonHandler> _logger;

        public SearchPersonHandler(IMongoRepository<Calls> repository, ILogger<SearchPersonHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<List<Calls>>> Handle(SearchCalls request, CancellationToken cancellationToken)
        {
            Expression<Func<Calls, bool>> filterExpression = x =>
             (string.IsNullOrEmpty(request.SearchText) || x.TableId.Contains(request.SearchText)) ||
             (string.IsNullOrEmpty(request.SearchText) || x.Table.TableNumber.Contains(request.SearchText));


            var result = await _repository.SearchAsync(filterExpression);

            _logger.LogInformation("Get Many Callss");

            return new Response<List<Calls>>
            {
                Content = result.ToConvertObjects<List<Calls>>(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
