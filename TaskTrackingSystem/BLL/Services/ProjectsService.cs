namespace TaskTrackingSystem.BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        IEnumerable<ProjectDTO> IProjectsService.GetProjectsByUserId(string id)
        {
            IEnumerable<Position> userPositions = _unitOfWork.Positions.Filter(p => p.IdUser == id);
            IEnumerable<int> userProjectIds = userPositions.Select(p => p.IdProject);
            return _mapper.Map<IEnumerable<ProjectDTO>>(_unitOfWork.Projects.Filter(p => userProjectIds.Contains(p.Id)));
        }

        ProjectDTO IProjectsService.AddProject(ProjectDTO newProject, string creatorId)
        {
            var projectToAdd = _mapper.Map<Project>(newProject);
            projectToAdd.StartDate = DateTime.Now;

            Project addedProject = _unitOfWork.Projects.Create(projectToAdd);
            _unitOfWork.Save();

            _unitOfWork.Positions.Create(new Position { IdProject = addedProject.Id, IdUser = creatorId, Name = "creator" });
            _unitOfWork.Save();

            return _mapper.Map<ProjectDTO>(addedProject);
        }

        ProjectDTO IProjectsService.RemoveProject(int id)
        {
            Project removedProject = _unitOfWork.Projects.Delete(id);
            _unitOfWork.Save();
            return _mapper.Map<ProjectDTO>(removedProject);
        }

        ProjectDTO IProjectsService.UpdateProject(ProjectDTO newProject)
        {
            Project updatedProject = _unitOfWork.Projects.Update(_mapper.Map<Project>(newProject));
            _unitOfWork.Save();
            return _mapper.Map<ProjectDTO>(updatedProject);
        }

        bool IProjectsService.ProjectExists(int id)
        {
            return _unitOfWork.Projects.Get(id) != null;
        }
    }
}
