﻿using Finance.Application.Commands.CreateUser;
using Finance.Application.Commands.UpdateUser;
using Finance.Application.Queries.GetAllUsers;
using Finance.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet] // Apenas admin
        public async Task<IActionResult> Get(GetAllUsersQuery getAllUserQuery)
        {
            return Ok(await _mediator.Send(getAllUserQuery));
        }

        [HttpGet("{id}")] // Apenas admin
        public async Task<IActionResult> GetById(int id)
        {
            var userDetails = await _mediator.Send(new GetUserByIdQuery(id));

            if (userDetails == null)
                return NotFound();

            return Ok(userDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand createUserCommand)
        {
            var id = await _mediator.Send(createUserCommand);

            return Created(string.Empty, new { id });
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommand updateUserCommand)
        {
            await _mediator.Send(updateUserCommand);

            return NoContent();
        }
    }
}