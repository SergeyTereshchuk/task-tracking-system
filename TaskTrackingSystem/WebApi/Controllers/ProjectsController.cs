namespace TaskTrackingSystem.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Description;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;

    [Authorize]
    [RoutePrefix("api")]
    public class ProjectsController : ApiController
    {
        private readonly IProjectsService _service;

        public ProjectsController(IProjectsService projectsService)
        {
            _service = projectsService;
        }

        [Route("projects")]
        public IHttpActionResult GetProjects()
        {
            return Ok(_service.GetProjects());
        }

        [Route("projects/{id}", Name = "GetProject")]
        [ResponseType(typeof(ProjectDTO))]
        public IHttpActionResult GetProject(int id)
        {
            ProjectDTO project = _service.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [Route("projects")]
        [ResponseType(typeof(ProjectDTO))]
        public IHttpActionResult PutProject(ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!ProjectExists(project.Id))
            {
                return NotFound();
            }

            return Ok(_service.UpdateProject(project));
        }

        [Route("projects")]
        [ResponseType(typeof(ProjectDTO))]
        public IHttpActionResult PostProject(ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ProjectExists(project.Id))
            {
                return BadRequest("Order exists");
            }

            return CreatedAtRoute("GetProject", new { id = project.Id }, _service.AddProject(project));
        }

        [Route("projects/{id}")]
        [ResponseType(typeof(ProjectDTO))]
        public IHttpActionResult DeleteProject(int id)
        {
            if (!ProjectExists(id))
            {
                return NotFound();
            }

            return Ok(_service.RemoveProject(id));
        }

        private bool ProjectExists(int id)
        {
            return _service.ProjectExists(id);
        }
    }
}