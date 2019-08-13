namespace TaskTrackingSystem.BLL.Interfaces
{
    using System.Collections.Generic;
    using TaskTrackingSystem.BLL.DTO;

    public interface IPositionsService
    {
        IEnumerable<PositionDTO> GetPositions();

        PositionDTO GetPositionById(int id);

        PositionDTO AddPosition(PositionDTO newPosition);

        PositionDTO RemovePosition(int id);

        PositionDTO UpdatePosition(PositionDTO updatedPosition);

        bool PositionExists(int id);
    }
}