using Finance.Application.Commands.User.Create;
using Finance.Application.Commands.User.Update;
using Finance.Application.Queries.User.GetById;
using Finance.Application.Queries.User.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Finance.Core.Exceptions;
using Finance.Application.Commands.User.Login;
using Microsoft.AspNetCore.Authorization;

namespace Finance.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get(GetAllUsersQuery getAllUserQuery)
        {
            return Ok(await _mediator.Send(getAllUserQuery));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var userDetails = await _mediator.Send(new GetUserByIdQuery(id));

            if (userDetails == null)
                return NotFound();

            return Ok(userDetails);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand createUserCommand)
        {
            var id = await _mediator.Send(createUserCommand);

            return Created(string.Empty, new { id });
        }

        [HttpPut]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommand updateUserCommand)
        {
            try
            {
                await _mediator.Send(updateUserCommand);
                return NoContent();
            }
            catch (UserNotExistsException userNotExists)
            {
                return NotFound(userNotExists.Message);
            }
        }

        [HttpPut("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var userToken = await _mediator.Send(loginUserCommand);

            if (userToken == null)
                return BadRequest();

            return Ok(userToken);
        }
    }
}
