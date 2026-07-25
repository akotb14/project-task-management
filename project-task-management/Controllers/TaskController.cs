using MediatR;
using Microsoft.AspNetCore.Mvc;
using project_task_management.Application.Features.Tasks.Commands.Requests;
using project_task_management.Application.Features.Tasks.Queries.Requests;
using project_task_management.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace project_task_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetTaskByProjectByUser(int projectId)
        {

            return Ok(await _mediator.Send(new GetTaskByProjectByUserQueryRequests { ProjectId = projectId }));
        }

        // POST api/<TaskController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTaskCommandRequest request)
        {
            return Ok(await _mediator.Send(request));

        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTaskCommandRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
        [HttpPut("status/{id}")]
        public async Task<IActionResult> PutTaskStatus(int id, [FromBody] UpdateTaskStatusCommandRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }

        // DELETE api/<TaskController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, [FromBody] DeleteTaskCommandRequest request)
        {
            request.Id = id;
            return Ok(await _mediator.Send(request));
        }
    }
}
