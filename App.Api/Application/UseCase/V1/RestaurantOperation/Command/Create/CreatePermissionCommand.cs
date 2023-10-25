using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using Azure.Core;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;
namespace App.Api.Application.UseCase.V1.RestaurantOperation.Command.Create
{
    public class CreateRestaurantCommand : IRequest<Response<CreateRestaurantResponse>>
    {

        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }

        public List<ContactBase> Contact { get; set; }

        public List<TableBase> Tables { get; set; }

        public string MenuImage { get; set; }
    }
    public class CreateRestaurantommandHandler : IRequestHandler<CreateRestaurantCommand, Response<CreateRestaurantResponse>>
    {
        private readonly IMongoRepository<Restaurant> _repository;
        private readonly ILogger<CreateRestaurantommandHandler> _logger;

        public CreateRestaurantommandHandler(IMongoRepository<Restaurant> repository, ILogger<CreateRestaurantommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<CreateRestaurantResponse>> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var entity = new Restaurant
            {
                Name = request.Name,
                Contact = request.Contact,
                Tables = request.Tables,
                MenuImage = request.MenuImage,
                Address = request.Address,
                Banner = request.Banner,
                Logo = request.Logo,
                Country = request.Country,
                DateAdd = DateTime.UtcNow,
                DateUpd = DateTime.UtcNow,
                UpdateBy = "System",
                CreateBy = "System",
                Active = true
            };

            await _repository.CreateAsync(entity);
            _logger.LogDebug("the restaurant  was add correctly");

            return new Response<CreateRestaurantResponse>
            {
                Content = new CreateRestaurantResponse
                {
                    Message = "Success",
                    Id = entity.Id
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }

}
