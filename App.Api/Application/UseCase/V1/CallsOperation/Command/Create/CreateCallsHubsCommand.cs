using App.Api.Common;
using App.Api.Hubs;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson.IO;
using Newtonsoft;
using Js= Newtonsoft.Json;

namespace App.Api.Application.UseCase.V1.CallsOperation.Command.Create
{
    public class CreateCallsHubCommand : IRequest<Response<CreateCallsResponse>>
    {

        public string CallType { get; set; }
            public string TableId { get; set; }
            public string RestaurantId { get; set; }
            public bool Active { get; set; }
        }

        public class CreateCallsCommandHandler : IRequestHandler<CreateCallsHubCommand, Response<CreateCallsResponse>>
        {
            private readonly IMongoRepository<Calls> _repository;
            private readonly IMongoRepository<Restaurant> _restaurantRepository;
            private readonly IHubContext<CallsHub> _hubContext;

            public CreateCallsCommandHandler(IMongoRepository<Calls> repository, IMongoRepository<Restaurant> restaurantRepository, IHubContext<CallsHub> hubContext)
            {
                _repository = repository;
                _restaurantRepository = restaurantRepository;
                _hubContext = hubContext;
            }

            public async Task<Response<CreateCallsResponse>> Handle(CreateCallsHubCommand request, CancellationToken cancellationToken)
            {
                var rest = await _restaurantRepository.GetByIdAsync(request.RestaurantId);

                if (rest == null)
                {
                    throw new Exception("Restaurant not found");
                }

                var table = rest.Tables.SingleOrDefault(x => x.TableId == request.TableId);

                if (table == null)
                {
                    throw new Exception("Table not found");
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
            var guid = Guid.NewGuid().ToString();
            await _hubContext.Clients.All.SendAsync("receivecallupdate", guid, Js.JsonConvert.SerializeObject(entity));

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
