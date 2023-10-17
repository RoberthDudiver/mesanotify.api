using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using Azure.Core;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net;
using System.Web.Http;

namespace App.Api.Application.UseCase.V1.CallsOperation.Command.Create
{
    public class CreateCallsCommand : IRequest<Response<CreateCallsResponse>>
    {
        public string CallType { get; set; }
        public string TableId { get; set; }
        public string RestaurantId { get; set; }

        public bool Active { get; set; }
    }
    public class CreateCallsommandHandler : IRequestHandler<CreateCallsCommand, Response<CreateCallsResponse>>
    {
        private readonly IMongoRepository<Calls> _repository;
        private readonly IMongoRepository<Restaurant> _restaurantrepository;

        private readonly ILogger<CreateCallsommandHandler> _logger;

        public CreateCallsommandHandler(IMongoRepository<Calls> repository, ILogger<CreateCallsommandHandler> logger, IMongoRepository<Restaurant> restaurantrepository)
        {
            _repository = repository;
            _logger = logger;
            _restaurantrepository=  restaurantrepository;
        }

        public async Task<Response<CreateCallsResponse>> Handle(CreateCallsCommand request, CancellationToken cancellationToken)
        {

            var rest = await _restaurantrepository.GetByIdAsync(request.RestaurantId);

            if (rest == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            var table = rest.Tables.SingleOrDefault(x => x.TableId == request.TableId);

            if (table == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }


            var entity = new Calls
            {
                CallType = request.CallType,
                TableId = request.TableId,
                Table = table,
                DateAdd = DateTime.UtcNow,
                DateUpd = DateTime.UtcNow,
                UpdateBy = "System",
                CreateBy = "System",
                Active = request.Active
            };

            await _repository.CreateAsync(entity);
            _logger.LogDebug("the calls  was add correctly");

            return new Response<CreateCallsResponse>
            {
                Content = new CreateCallsResponse
                {
                    Message = "Success",
                    Id = entity.Id
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }

}
