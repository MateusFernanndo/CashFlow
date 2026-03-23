using CashFlow.Application.UseCase.Expenses.Delete;
using CashFlow.Application.UseCase.Expenses.GetAll;
using CashFlow.Application.UseCase.Expenses.GetById;
using CashFlow.Application.UseCase.Expenses.Register;
using CashFlow.Application.UseCase.Expenses.Uptade;
using CashFlow.Communication.Request;
using CashFlow.Communication.Response;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost] //Registar despesas
    [ProducesResponseType(typeof(ResponseRegisterExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpenseUseCase useCase,
        [FromBody] RequestExpenseJson request)
    {

        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet] //Listar todas as despesas
    [ProducesResponseType(typeof(ResponseAllExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllExpenses(
        [FromServices] IGetAllExpenseUseCase useCase
        )
    {
        var response = await useCase.Execute();
        if (response.Expenses.Count != 0)
            return Ok(response);

        return NoContent();
    }

    [HttpGet] // Listar despesas por Id
    [Route("{id}")] //adicionar id na rota
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetExpenseByIdUseCase useCase,
        [FromRoute] long id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [HttpDelete] // Deletar despesa
    [Route("{id}")] 
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteExpenseUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);
        return NoContent();
    }
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateExpenseUseCase useCase,
        [FromRoute] long Id,
        [FromBody] RequestExpenseJson request)
    {
        await useCase.Execute(Id, request);
        return NoContent();
    }

}
