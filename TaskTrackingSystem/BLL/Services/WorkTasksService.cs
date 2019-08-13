namespace TaskTrackingSystem.BLL.Services
{
    using System.Collections.Generic;
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

        WorkTaskDTO IWorkTasksService.AddWorkTask(WorkTaskDTO newTask)
        {
            var addedTask = _unitOfWork.WorkTasks.Create(_mapper.Map<WorkTask>(newTask));
            _unitOfWork.Save();
            return _mapper.Map<WorkTaskDTO>(addedTask);
        }

        WorkTaskDTO IWorkTasksService.RemoveWorkTask(int id)
        {
            var removedTask = _unitOfWork.WorkTasks.Delete(id);
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
