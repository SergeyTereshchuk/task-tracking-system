namespace TaskTrackingSystem.BLL.Interfaces
{
    using System.Collections.Generic;
    using TaskTrackingSystem.BLL.DTO;

    public interface IWorkTasksService
    {
        IEnumerable<WorkTaskDTO> GetWorkTasks();

        WorkTaskDTO GetWorkTaskById(int id);

        IEnumerable<WorkTaskDTO> GetWorkTasksByUserId(string id);

        IEnumerable<WorkTaskDTO> GetWorkTasksByManagerId(string id);

        WorkTaskDTO AddWorkTask(WorkTaskDTO newTask, string performerId);

        WorkTaskDTO RemoveWorkTask(int id);

        WorkTaskDTO UpdateWorkTask(WorkTaskDTO updatedTask);

        bool WorkTaskExists(int id);
    }
}