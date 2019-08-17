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

    public class WorkTasksService : IWorkTasksService
    {
        private readonly IDataUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkTasksService(IDataUnitOfWork workTaskUW, IMapper workTaskMapper)
        {
            _unitOfWork = workTaskUW;
            _mapper = workTaskMapper;
        }

        IEnumerable<WorkTaskDTO> IWorkTasksService.GetWorkTasks()
        {
            return _mapper.Map<IEnumerable<WorkTaskDTO>>(_unitOfWork.WorkTasks.GetAll());
        }

        WorkTaskDTO IWorkTasksService.GetWorkTaskById(int id)
        {
            return _mapper.Map<WorkTaskDTO>(_unitOfWork.WorkTasks.Get(id));
        }

        IEnumerable<WorkTaskDTO> IWorkTasksService.GetWorkTasksByUserId(string id)
        {
            IEnumerable<Position> userPositions = _unitOfWork.Positions.Filter(p => p.IdUser == id);
            IEnumerable<int> userPositionsIds = userPositions.Select(p => p.Id);
            return _mapper.Map<IEnumerable<WorkTaskDTO>>(_unitOfWork.WorkTasks.Filter(p => userPositionsIds.Contains(p.IdPerformer)));
        }

        IEnumerable<WorkTaskDTO> IWorkTasksService.GetWorkTasksByManagerId(string id)
        {
            IEnumerable<Position> managerPositions = _unitOfWork.Positions.Filter(p => p.IdUser == id);
            IEnumerable<int> managerProjectsIds = managerPositions.Select(p => p.IdProject);
            return _mapper.Map<IEnumerable<WorkTaskDTO>>(_unitOfWork.WorkTasks.Filter(p => managerProjectsIds.Contains(p.IdProject)));
        }

        WorkTaskDTO IWorkTasksService.AddWorkTask(WorkTaskDTO newTask, string performerId)
        {
            IEnumerable<Position> performerPosition = _unitOfWork.Positions.Filter(p => p.IdProject == newTask.IdProject && p.IdUser == performerId);

            Position assignPosition;
            if (performerPosition.Count() == 0)
            {
                assignPosition = _unitOfWork.Positions.Create(new Position { IdProject = newTask.IdProject, IdUser = performerId, Name = "performer" });
                _unitOfWork.Save();
            }
            else
            {
                assignPosition = performerPosition.First();
            }

            newTask.StartDate = DateTime.Now;
            newTask.IdPerformer = assignPosition.Id;
            var addedTask = _unitOfWork.WorkTasks.Create(_mapper.Map<WorkTask>(newTask));
            _unitOfWork.Save();
            return _mapper.Map<WorkTaskDTO>(addedTask);
        }

        WorkTaskDTO IWorkTasksService.RemoveWorkTask(int id)
        {
            WorkTask removedTask = _unitOfWork.WorkTasks.Delete(id);
            _unitOfWork.Save();
            return _mapper.Map<WorkTaskDTO>(removedTask);
        }

        WorkTaskDTO IWorkTasksService.UpdateWorkTask(WorkTaskDTO newTask)
        {
            var updatedTask = _unitOfWork.WorkTasks.Update(_mapper.Map<WorkTask>(newTask));
            _unitOfWork.Save();
            return _mapper.Map<WorkTaskDTO>(updatedTask);
        }

        bool IWorkTasksService.WorkTaskExists(int id)
        {
            return _unitOfWork.WorkTasks.Get(id) != null;
        }
    }
}
