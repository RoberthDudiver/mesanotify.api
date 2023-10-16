using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using Azure.Core;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
namespace App.Api.Application.UseCase.V1.PermissionOperation.Command.Create
{
    public class CreatePermissionCommand: IRequest<Response<CreatePermissionResponse>>
    {

        public string Name { get; set; }

        public string Lastname { get; set; }
        public int PermissionTypeId { get; set; }
    }
    public class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, Response<CreatePermissionResponse>>
    {
        private readonly IRepository<Permission> _repository;
        private readonly ILogger<CreatePermissionCommandHandler> _logger;

        public CreatePermissionCommandHandler(IRepository<Permission> repository, ILogger<CreatePermissionCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<CreatePermissionResponse>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = new Permission
            {
                Name = request.Name,
                Lastname = request.Lastname,
                PermissionTypeId = request.PermissionTypeId,
                DateAdd = DateTime.UtcNow,
                 DateUpd= DateTime.UtcNow,
                 UpdateBy="System",
                CreateBy="System",
                Active = true
            };

            await _repository.CreateAsync(entity);
            _logger.LogDebug("the restaurant  was add correctly");

            return new Response<CreatePermissionResponse>
            {
                Content = new CreatePermissionResponse
                {
                    Message = "Success",
                    PersonId = entity.Id
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }

}
