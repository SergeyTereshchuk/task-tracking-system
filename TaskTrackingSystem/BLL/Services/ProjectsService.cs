namespace TaskTrackingSystem.BLL.Services
{
    using System.Collections.Generic;
    using AutoMapper;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;
    using TaskTrackingSystem.DAL.Interfaces;
    using TaskTrackingSystem.DAL.Models;

    public class ProjectsService : IProjectsService
    {
        private readonly IDataUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectsService(IDataUnitOfWork projectUW, IMapper projectMapper)
        {
            _unitOfWork = projectUW;
            _mapper = projectMapper;
        }

        IEnumerable<ProjectDTO> IProjectsService.GetProjects()
        {
            return _mapper.Map<IEnumerable<ProjectDTO>>(_unitOfWork.Projects.GetAll());
        }

        ProjectDTO IProjectsService.GetProjectById(int id)
        {
            return _mapper.Map<ProjectDTO>(_unitOfWork.Projects.Get(id));
        }

        ProjectDTO IProjectsService.AddProject(ProjectDTO newProject)
        {
            var addedProject = _unitOfWork.Projects.Create(_mapper.Map<Project>(newProject));
            _unitOfWork.Save();
            return _mapper.Map<ProjectDTO>(addedProject);
        }

        ProjectDTO IProjectsService.RemoveProject(int id)
        {
            var removedProject = _unitOfWork.Projects.Delete(id);
            _unitOfWork.Save();
            return _mapper.Map<ProjectDTO>(removedProject);
        }

        ProjectDTO IProjectsService.UpdateProject(ProjectDTO newProject)
        {
            var updatedProject = _unitOfWork.Projects.Update(_mapper.Map<Project>(newProject));
            _unitOfWork.Save();
            return _mapper.Map<ProjectDTO>(updatedProject);
        }

        bool IProjectsService.ProjectExists(int id)
        {
            return _unitOfWork.Projects.Get(id) != null;
        }
    }
}
