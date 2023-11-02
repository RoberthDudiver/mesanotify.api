
using App.Api.Application.UseCase.V1.AuthServiceOperation.Command.Create;
using App.Api.Application.UseCase.V1.CallsOperation.Command.Create;
using App.Api.Application.UseCase.V1.CallsOperation.Command.Delete;
using App.Api.Application.UseCase.V1.CallsOperation.Command.Update;
using App.Api.Application.UseCase.V1.CallsOperation.Queries.Get;
using App.Api.Application.UseCase.V1.CallsOperation.Queries.GetList;
using App.Api.Application.UseCase.V1.CallsOperation.Queries.GetSearch;

using App.Api.Common;
using App.Core.Domain.DTO;
using App.Core.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
  

    public class CallsController : ApiControllerBase
    {
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(CreateCallsHubCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateCallsHubCommand body) => Result(await Mediator.Send(body));
        [Authorize]
        [HttpPut]
        [ProducesResponseType(typeof(UpdateCallsCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(UpdateCallsCommand body) => Result(await Mediator.Send(body));
        [Authorize]

        [HttpDelete]
        [ProducesResponseType(typeof(DeleteCallsCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(DeleteCallsCommand body) => Result(await Mediator.Send(body));


        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(List<Calls>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get() => Result(await Mediator.Send(new ListCalls()));
        [Authorize]

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(List<Calls>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string Id)
        {
            var get = new GetCalls
            {
                Id = Id
            };

            var result = await Mediator.Send(get);
            return Result(result);
        }
        [Authorize]

        [HttpGet("Search")]
        [ProducesResponseType(typeof(List<Calls>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Search([FromQuery] string SearchText)
        {
            var searchRequest = new SearchCalls
            {
                SearchText = SearchText
            };

            var result = await Mediator.Send(searchRequest);
            return Result(result);
        }


    }

}
