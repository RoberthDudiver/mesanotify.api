﻿using App.Api.Common;
using Newtonsoft.Json;
using App.Core.Domain.DTO;
using App.Core.Utils;

using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using MediatR;
using System;

namespace App.Api.Application.UseCase.V1.CallsOperation.Queries.GetList
{
    public record struct ListCalls : IRequest<Response<List<Calls>>>
    {
    }

    public class GetCallsHandler : IRequestHandler<ListCalls, Response<List<Calls>>>
    {
        private readonly IMongoRepository<Calls> _repository;
        private readonly ILogger<GetCallsHandler> _logger;

        public GetCallsHandler(IMongoRepository<Calls> repository , ILogger<GetCallsHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<List<Calls>>> Handle(ListCalls request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();


            _logger.LogInformation("Trajo uchas cosas");
            return new Response<List<Calls>>
            {
                Content = result.ToConvertObjects<List<Calls>>(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

    
    }
}
