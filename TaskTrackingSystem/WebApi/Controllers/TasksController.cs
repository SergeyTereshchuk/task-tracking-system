namespace TaskTrackingSystem.WebApi.Controllers
{
    using System.Web.Http;
    using System.Web.Http.Description;
    using Microsoft.AspNet.Identity;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;

    [Authorize(Roles = "admin,manager")]
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

        [Route("user/tasks")]
        [OverrideAuthorization]
        [Authorize]
        public IHttpActionResult GetUserTasks()
        {
            return Ok(_service.GetWorkTasksByUserId(RequestContext.Principal.Identity.GetUserId()));
        }

        [Route("user/projects/tasks")]
        public IHttpActionResult GetUserManagedTasks()
        {
            return Ok(_service.GetWorkTasksByManagerId(RequestContext.Principal.Identity.GetUserId()));
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

        [Route("users/{id:guid}/tasks")]
        [ResponseType(typeof(WorkTaskDTO))]
        public IHttpActionResult PostWorkTask([FromUri] string id, [FromBody] WorkTaskDTO workTask)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (WorkTaskExists(workTask.Id))
            {
                return BadRequest("Order exists");
            }

            return CreatedAtRoute("GetWorkTask", new { id = workTask.Id }, _service.AddWorkTask(workTask, id));
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
