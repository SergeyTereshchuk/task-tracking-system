namespace TaskTrackingSystem.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Description;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;

    [Authorize]
    [RoutePrefix("api")]
    public class TasksController : ApiController
    {
        private readonly IWorkTasksService _service;

        public TasksController(IWorkTasksService tasksService)
        {
            _service = tasksService;
        }

        [Route("tasks")]
        public IHttpActionResult GetWorkTasks()
        {
            return Ok(_service.GetWorkTasks());
        }

        [Route("tasks/{id}", Name = "GetWorkTask")]
        [ResponseType(typeof(WorkTaskDTO))]
        public IHttpActionResult GetWorkTask(int id)
        {
            WorkTaskDTO workTask = _service.GetWorkTaskById(id);
            if (workTask == null)
            {
                return NotFound();
            }

            return Ok(workTask);
        }

        [Route("tasks")]
        [ResponseType(typeof(WorkTaskDTO))]
        public IHttpActionResult PutWorkTask(WorkTaskDTO workTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!WorkTaskExists(workTask.Id))
            {
                return NotFound();
            }

            return Ok(_service.UpdateWorkTask(workTask));
        }

        [Route("tasks")]
        [ResponseType(typeof(WorkTaskDTO))]
        public IHttpActionResult PostWorkTask(WorkTaskDTO workTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (WorkTaskExists(workTask.Id))
            {
                return BadRequest("Order exists");
            }

            return CreatedAtRoute("GetWorkTask", new { id = workTask.Id }, _service.AddWorkTask(workTask));
        }

        [Route("tasks/{id}")]
        [ResponseType(typeof(WorkTaskDTO))]
        public IHttpActionResult DeleteWorkTask(int id)
        {
            if (!WorkTaskExists(id))
            {
                return NotFound();
            }

            return Ok(_service.RemoveWorkTask(id));
        }

        private bool WorkTaskExists(int id)
        {
            return _service.WorkTaskExists(id);
        }
    }
}
