using App.Api.Common;
using Newtonsoft.Json;
using App.Core.Domain.DTO;
using App.Core.Utils;

using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using MediatR;
using System;

namespace App.Api.Application.UseCase.V1.UserOperation.Queries.GetList
{
    public record struct ListUser : IRequest<Response<List<User>>>
    {
    }

    public class GetUserHandler : IRequestHandler<ListUser, Response<List<User>>>
    {
        private readonly IMongoRepository<User> _repository;
        private readonly ILogger<GetUserHandler> _logger;

        public GetUserHandler(IMongoRepository<User> repository , ILogger<GetUserHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<List<User>>> Handle(ListUser request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();


            _logger.LogInformation("Trajo uchas cosas");
            return new Response<List<User>>
            {
                Content = result.ToConvertObjects<List<User>>(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

    
    }
}
