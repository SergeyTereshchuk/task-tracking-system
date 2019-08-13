namespace TaskTrackingSystem.BLL.Interfaces
{
    using System.Collections.Generic;
    using TaskTrackingSystem.BLL.DTO;

    public interface IWorkTasksService
    {
        IEnumerable<WorkTaskDTO> GetWorkTasks();

        WorkTaskDTO GetWorkTaskById(int id);

        WorkTaskDTO AddWorkTask(WorkTaskDTO newTask);

        WorkTaskDTO RemoveWorkTask(int id);

        WorkTaskDTO UpdateWorkTask(WorkTaskDTO updatedTask);

        bool WorkTaskExists(int id);
    }
}