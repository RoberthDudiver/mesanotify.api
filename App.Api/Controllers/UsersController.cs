
using App.Api.Application.UseCase.V1.UserOperation.Command.Create;
using App.Api.Application.UseCase.V1.UserOperation.Command.Delete;
using App.Api.Application.UseCase.V1.UserOperation.Command.Update;
using App.Api.Application.UseCase.V1.UserOperation.Queries.Get;
using App.Api.Application.UseCase.V1.UserOperation.Queries.GetList;
using App.Api.Application.UseCase.V1.UserOperation.Queries.GetSearch;

using App.Api.Common;
using App.Core.Domain.DTO;
using App.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreateUserCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateUserCommand body) => Result(await Mediator.Send(body));
        [HttpPut]
        [ProducesResponseType(typeof(UpdateUserCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateUserCommand body) => Result(await Mediator.Send(body));

        [HttpDelete]
        [ProducesResponseType(typeof(DeleteUserCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(DeleteUserCommand body) => Result(await Mediator.Send(body));



        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get() => Result(await Mediator.Send(new ListUser()));

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string Id)
        {
            var get = new GetUser
            {
                Id = Id
            };

            var result = await Mediator.Send(get);
            return Result(result);
        }

        [HttpGet("Search")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromQuery] string SearchText)
        {
            var searchRequest = new SearchUser
            {
                SearchText = SearchText
            };

            var result = await Mediator.Send(searchRequest);
            return Result(result);
        }


    }

}
