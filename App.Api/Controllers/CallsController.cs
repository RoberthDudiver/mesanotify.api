
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
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreateAuthServiceCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateAuthServiceCommand body) => Result(await Mediator.Send(body));
      

    }

}
