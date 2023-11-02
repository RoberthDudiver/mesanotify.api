
using App.Api.Application.UseCase.V1.RestaurantOperation.Command.Create;
using App.Api.Application.UseCase.V1.RestaurantOperation.Command.Delete;
using App.Api.Application.UseCase.V1.RestaurantOperation.Command.Update;
using App.Api.Application.UseCase.V1.RestaurantOperation.Queries.Get;
using App.Api.Application.UseCase.V1.RestaurantOperation.Queries.GetList;
using App.Api.Application.UseCase.V1.RestaurantOperation.Queries.GetSearch;
using App.Api.Common;
using App.Core.Domain.DTO;
using App.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    public class RestaurantController : ApiControllerBase
    {
        [Authorize]

        [HttpPost]
        [ProducesResponseType(typeof(CreateRestaurantCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateRestaurantCommand body) => Result(await Mediator.Send(body));
        [Authorize]

        [HttpPut]
        [ProducesResponseType(typeof(UpdateRestaurantCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateRestaurantCommand body) => Result(await Mediator.Send(body));

        [Authorize]

        [HttpDelete]
        [ProducesResponseType(typeof(DeleteRestaurantCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(DeleteRestaurantCommand body) => Result(await Mediator.Send(body));


        [Authorize]

        [HttpGet]
        [ProducesResponseType(typeof(List<Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get() => Result(await Mediator.Send(new ListRestaurant()));
        [Authorize]

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(List<Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get( string Id)
        {
            var get = new GetRestaurant
            {
                Id = Id
            };

            var result = await Mediator.Send(get);
            return Result(result);
        }
        [Authorize]

        [HttpGet("Search")]
        [ProducesResponseType(typeof(List<Restaurant>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromQuery] string SearchText)
        {
            var searchRequest = new SearchRestaurant
            {
                SearchText = SearchText
            };

            var result = await Mediator.Send(searchRequest);
            return Result(result);
        }


    }
}
