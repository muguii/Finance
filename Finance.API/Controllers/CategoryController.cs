using Finance.Application.Commands.Category.Create;
using Finance.Application.Commands.Category.Update;
using Finance.Application.Commands.Category.Delete;
using Finance.Application.Commands.Category.Shelve;
using Finance.Application.Commands.Category.Unshelve;
using Finance.Application.Queries.Category.GetAll;
using Finance.Application.Queries.Category.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Finance.API.Controllers
{
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetAllCategoriesQuery getAllCategoriesQuery)
        {
            return Ok(await _mediator.Send(getAllCategoriesQuery));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid(int id)
        {
            var categoryDetails = await _mediator.Send(new GetCategoryByIdQuery(id));

            if (categoryDetails == null)
                return NotFound();

            return Ok(categoryDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var id = await _mediator.Send(createCategoryCommand);
            return CreatedAtAction(nameof(GetByid), new { id }, createCategoryCommand);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            await _mediator.Send(updateCategoryCommand);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }

        [HttpPut("{id}/shelve")]
        public async Task<IActionResult> Shelve(int id)
        {
            await _mediator.Send(new ShelveCategoryCommand(id));
            return NoContent();
        }

        [HttpPut("{id}/unshelve")]
        public async Task<IActionResult> Unshelve(int id)
        {
            await _mediator.Send(new UnshelveCategoryCommand(id));
            return NoContent();
        }
    }
}
