using AngularApp1.Server.Contracts;
using Microsoft.AspNetCore.Mvc;
using PasswordsSaver.Core;
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
        return HandleServiceEnumerableResult(
            await _passwordSaverService.GetAll()
        );
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        return HandleServiceResultBase(
            await _passwordSaverService.Get(id)
        );
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        return HandleServiceResultBase(
            await _passwordSaverService.Delete(id)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Save(NewPasswordRequest request)
    {
        return HandleServiceResultBase(
            await _passwordSaverService.Save(
                request.Source,
                request.SourceType,
                request.Password
            )
        );
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? searchString)
    {
        return HandleServiceEnumerableResult(
            await _passwordSaverService.Search(searchString ?? "")
        );
    }


    private IActionResult HandleServiceResultBase<T>(IServiceResult<T> result)
    {
        if (result.IsSuccess) return Ok(result.Data);
        return BadRequest(result.ErrorMessage);
    }


    private IActionResult HandleServiceEnumerableResult(IServiceResult<IEnumerable<SavedPassword>> result)
    {
        if (!result.IsSuccess) return BadRequest(result.ErrorMessage);

        return Ok(result.Data.Select(
            savedPassword => new SavedPasswordResponse
            {
                Id = savedPassword.Id,
                Source = savedPassword.Source,
                SourceType = (int)savedPassword.SourceType,
                Password = savedPassword.Password,
                CreatedDate = savedPassword.CreatedDate.Date.ToString("yyyy-MM-dd")
            }
        ));
    }
}