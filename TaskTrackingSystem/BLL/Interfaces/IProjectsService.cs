namespace TaskTrackingSystem.BLL.Interfaces
{
    using System.Collections.Generic;
    using TaskTrackingSystem.BLL.DTO;

    public interface IProjectsService
    {
        IEnumerable<ProjectDTO> GetProjects();

        ProjectDTO GetProjectById(int id);

        ProjectDTO AddProject(ProjectDTO newProject);

        ProjectDTO RemoveProject(int id);

        ProjectDTO UpdateProject(ProjectDTO updatedProject);

        bool ProjectExists(int id);
    }
}
