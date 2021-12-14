namespace DWD.UI.Monetary.Service.Controllers;

using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Base behavior for all controllers.
/// </summary>
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
public abstract class BaseApiController : ControllerBase { }
