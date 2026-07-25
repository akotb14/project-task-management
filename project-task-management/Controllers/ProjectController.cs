using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_task_management.Application.Features.Projects.Command.Requests;
using project_task_management.Application.Features.Projects.Queries.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_task_management.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByUser()
        {
            return Ok(await _mediator.Send(new GetProductsByUserQuery()));
        }

        // GET api/<Project>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdByUser(int id)
        {
             

            return Ok(await _mediator.Send(new GetProductByIdByUserQuery { Id =id}));
        }

        // POST api/<Project>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProjectRequest createProjectRequest)
        {
           return Ok( await _mediator.Send(createProjectRequest));
        }

        // PUT api/<Project>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id , [FromBody] UpdateProjectRequest updateProjectRequest)
        {
            updateProjectRequest.Id = id;
            return Ok(await _mediator.Send(updateProjectRequest));
        }

        // DELETE api/<Project>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteProjectRequest { Id = id }));
        }
    }
}
