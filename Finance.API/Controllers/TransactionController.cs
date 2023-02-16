using Finance.Application.Commands.Transaction.Create;
using Finance.Application.Commands.Transaction.Delete;
using Finance.Application.Commands.Transaction.Update;
using Finance.Application.Queries.Transaction.GetAll;
using Finance.Application.Queries.Transaction.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetAllTransactionsQuery getAllTransactionsQuery)
        {
            return Ok(await _mediator.Send(getAllTransactionsQuery));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transactionDetails = await _mediator.Send(new GetTransactionByIdQuery(id));

            if (transactionDetails == null)
                return NotFound();

            return Ok(transactionDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTransactionCommand createTransactionCommand)
        {
            var id = await _mediator.Send(createTransactionCommand);
            return CreatedAtAction(nameof(GetById), new { id }, createTransactionCommand);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateTransactionCommand updateTransactionCommand)
        {
            await _mediator.Send(updateTransactionCommand);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteTransactionCommand(id));
            return NoContent();
        }
    }
}
