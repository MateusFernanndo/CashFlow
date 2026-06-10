using CashFlow.Application.UseCase.Users.ChangePassword;
using CashFlow.Application.UseCase.Users.Delete;
using CashFlow.Application.UseCase.Users.Profile;
using CashFlow.Application.UseCase.Users.Register;
using CashFlow.Application.UseCase.Users.Update;
using CashFlow.Communication.Request;
using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProfile(
        [FromServices] IGetUserProfileUseCase useCAse)
    {
        var response = await useCAse.Execute();
        return Ok(response);
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProfile(
        [FromServices] IUpdateUserUseCase useCase,
        [FromBody] RequestUpdateUserJson request)
    {
        await useCase.Execute(request);
        return NoContent();
    }
    [HttpPut("change-password")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword(
        [FromServices] IChangePasswordUseCase useCase,
        [FromBody] RequestChangePasswordJson request)
    {
        await useCase.Execute(request);
        return NoContent();
    }

    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeletePRofile(
        [FromServices] IDeleteUserAccountUseCase useCase)
    {
        await useCase.Execute();
        return NoContent();
    }
}
