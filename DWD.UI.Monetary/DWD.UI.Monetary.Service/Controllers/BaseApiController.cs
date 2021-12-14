namespace DWD.UI.Monetary.Service.Controllers;

using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private IMediator mediator;

    protected IMediator Mediator => this.mediator ??= this.HttpContext.RequestServices.GetService<IMediator>();
}
