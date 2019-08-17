namespace TaskTrackingSystem.WebApi.Controllers
{
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;

    [Authorize(Roles = "admin,manager")]
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
        public IHttpActionResult GetProject(int id)
        {
            ProjectDTO project = _service.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(project);
        }

        [Route("user/projects")]
        [OverrideAuthorization]
        [Authorize]
        public IHttpActionResult GetUserProjects()
        {
            return Ok(_service.GetProjectsByUserId(RequestContext.Principal.Identity.GetUserId()));
        }

        [Route("projects")]
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
        public IHttpActionResult PostProject(ProjectDTO project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (ProjectExists(project.Id))
            {
                return BadRequest("Project exists");
            }

            return CreatedAtRoute("GetProject", new { id = project.Id }, _service.AddProject(project, RequestContext.Principal.Identity.GetUserId()));
        }

        [Route("projects/{id}")]
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