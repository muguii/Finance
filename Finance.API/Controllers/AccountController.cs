using Finance.Application.Commands.Account.Create;
using Finance.Application.Commands.Account.Delete;
using Finance.Application.Commands.Account.Shelve;
using Finance.Application.Commands.Account.Unshelve;
using Finance.Application.Commands.Account.Update;
using Finance.Application.Queries.Account.GetAll;
using Finance.Application.Queries.Account.GetById;
using Finance.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetAllAccountsQuery getAllAccountsQuery)
        {
            return Ok(await _mediator.Send(getAllAccountsQuery));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var accountDetails = await _mediator.Send(new GetAccountByIdQuery(id));

            if (accountDetails == null)
                return NotFound();

            return Ok(accountDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateAccountCommand createAccountCommand)
        {
            int id = await _mediator.Send(createAccountCommand);
            return CreatedAtAction(nameof(GetById), new { id }, createAccountCommand);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateAccountCommand updateAccountCommand)
        {
            try
            {
                await _mediator.Send(updateAccountCommand);
                return NoContent();
            }
            catch (AccountNotExistsException accountNotExists)
            {
                return NotFound(accountNotExists.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _mediator.Send(new DeleteAccountCommand(id));
                return NoContent();
            }
            catch (AccountNotExistsException accountNotExists)
            {
                return NotFound(accountNotExists.Message);
            }
        }

        [HttpPut("{id}/shelve")]
        public async Task<IActionResult> Shelve(int id)
        {
            try
            {
                await _mediator.Send(new ShelveAccountCommand(id));
                return NoContent();
            }
            catch (AccountNotExistsException accountNotExists)
            {
                return NotFound(accountNotExists.Message);
            }
        }

        [HttpPut("{id}/unshelve")]
        public async Task<IActionResult> Unshelve(int id)
        {
            try
            {
                await _mediator.Send(new UnshelveAccountCommand(id));
                return NoContent();
            }
            catch (AccountNotExistsException accountNotExists)
            {
                return NotFound(accountNotExists.Message);
            }
        }
    }
}
