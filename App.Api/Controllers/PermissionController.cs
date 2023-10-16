using App.Api.Application.UseCase.V1.PermissionOperation.Command.Create;
using App.Api.Application.UseCase.V1.PermissionOperation.Queries.GetList;
using App.Api.Common;
using App.Core.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    public class PermissionController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(CreatePermissionCommand), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreatePermissionCommand body) => Result(await Mediator.Send(body));


        [HttpGet]
        [ProducesResponseType(typeof(List<PermissionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get() => Result(await Mediator.Send(new ListPermission()));

    }
}
