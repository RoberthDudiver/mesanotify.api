using App.Api.Common;
using Newtonsoft.Json;
using App.Core.Domain.DTO;
using App.Core.Utils;

using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using MediatR;
using System;
using App.Api.Application.UseCase.V1.PermissionOperation.Command.Create;

namespace App.Api.Application.UseCase.V1.PermissionOperation.Queries.GetList
{
    public record struct ListPermission : IRequest<Response<List<PermissionDTO>>>
    {
    }

    public class ListPersonHandler : IRequestHandler<ListPermission, Response<List<PermissionDTO>>>
    {
        private readonly IRepository<Permission> _repository;
        private readonly ILogger<ListPersonHandler> _logger;

        public ListPersonHandler(IRepository<Permission> repository , ILogger<ListPersonHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<List<PermissionDTO>>> Handle(ListPermission request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();


            _logger.LogInformation("Trajo uchas cosas");
            return new Response<List<PermissionDTO>>
            {
                Content = result.ToConvertObjects<List<PermissionDTO>>(),
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
