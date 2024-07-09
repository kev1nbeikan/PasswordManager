using AngularApp1.Server.Contracts;
using Microsoft.AspNetCore.Mvc;
using PasswordsSaver.Core.Abstractions;
using PasswordsSaver.Core.Abstractions.Infastructure;

namespace AngularApp1.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class PasswordController : ControllerBase
{
    private readonly ILogger<PasswordController> _logger;
    private readonly IPasswordSaverService _passwordSaverService;

    public PasswordController(ILogger<PasswordController> logger, IPasswordSaverService passwordSaverService)
    {
        _logger = logger;
        _passwordSaverService = passwordSaverService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleServiceResult(
            await _passwordSaverService.GetAll()
        );
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return HandleServiceResult(
            await _passwordSaverService.Get(id)
        );
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return HandleServiceResult(
            await _passwordSaverService.Delete(id)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Save(SavedPasswordRequest request)
    {
        return HandleServiceResult(
            await _passwordSaverService.Save(
                request.Source,
                request.SourceType,
                request.Password
            )
        );
    }


    private IActionResult HandleServiceResult<T>(IServiceResult<T> result)
    {
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.ErrorMessage);
    }
}