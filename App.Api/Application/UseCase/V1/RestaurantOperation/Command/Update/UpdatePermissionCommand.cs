using App.Api.Application.UseCase.V1.RestaurantOperation.Command.Delete;
using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using Azure.Core;
using MediatR;
using System;
namespace App.Api.Application.UseCase.V1.RestaurantOperation.Command.Update
{
    public class UpdateRestaurantCommand : IRequest<Response<UpdateRestaurantResponse>>
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }

        public string Address { get; set; }
        public List<ContactBase> Contact { get; set; }

        public List<TableBase> Tables { get; set; }

        public string MenuImage { get; set; }


        public bool Active { get; set; }
    }
    public class UpdateRestaurantommandHandler : IRequestHandler<UpdateRestaurantCommand, Response<UpdateRestaurantResponse>>
    {
        private readonly IMongoRepository<Restaurant> _repository;
        private readonly ILogger<UpdateRestaurantommandHandler> _logger;

        public UpdateRestaurantommandHandler(IMongoRepository<Restaurant> repository, ILogger<UpdateRestaurantommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<UpdateRestaurantResponse>> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var entity = new Restaurant
            {
                Id=request.Id,
                Name = request.Name,
                Contact = request.Contact,
                 Country= request.Country,
                 Tables = request.Tables,
                 MenuImage = request.MenuImage,
                  Address = request.Address,
                 DateUpd= DateTime.UtcNow,
                 UpdateBy="System",
                Active =  request.Active
            };

            await _repository.UpdateAsync(request.Id,entity);
            _logger.LogDebug("the restaurant  was update correctly");

            return new Response<UpdateRestaurantResponse>
            {
                Content = new UpdateRestaurantResponse
                {
                    Message = "Success",
                    Id = entity.Id
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }

       
    }

}
