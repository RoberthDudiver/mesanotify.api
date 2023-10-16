using App.Api.Application.UseCase.V1.RestaurantOperation.Command.Create;
using App.Api.Common;
using App.Core.Domain.Entities;
using App.Infrastructure.Repositories;
using Azure.Core;
using MediatR;
using System;
namespace App.Api.Application.UseCase.V1.RestaurantOperation.Command.Delete
{
    public class DeleteRestaurantCommand : IRequest<Response<DeleteRestaurantResponse>>
    {
        public string Id { get; set; }

    }
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, Response<DeleteRestaurantResponse>>
    {
        private readonly IMongoRepository<Restaurant> _repository;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;

        public DeleteRestaurantCommandHandler(IMongoRepository<Restaurant> repository, ILogger<DeleteRestaurantCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<DeleteRestaurantResponse>> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
       

            await _repository.DeleteAsync(request.Id);
            _logger.LogDebug("the restaurant  was Delete correctly");

            return new Response<DeleteRestaurantResponse>
            {
                Content = new DeleteRestaurantResponse
                {
                    Message = "Success",
                
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }

       
    }

}
