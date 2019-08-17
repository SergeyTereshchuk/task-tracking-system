namespace TaskTrackingSystem.BLL.Services
{
    using System.Collections.Generic;
    using AutoMapper;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;
    using TaskTrackingSystem.DAL.Interfaces;
    using TaskTrackingSystem.DAL.Models;

    public class PositionsService : IPositionsService
    {
        private readonly IDataUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PositionsService(IDataUnitOfWork positionUW, IMapper positionMapper)
        {
            _unitOfWork = positionUW;
            _mapper = positionMapper;
        }

        IEnumerable<PositionDTO> IPositionsService.GetPositions()
        {
            return _mapper.Map<IEnumerable<PositionDTO>>(_unitOfWork.Positions.GetAll());
        }

        PositionDTO IPositionsService.GetPositionById(int id)
        {
            return _mapper.Map<PositionDTO>(_unitOfWork.Positions.Get(id));
        }

        PositionDTO IPositionsService.AddPosition(PositionDTO newPosition)
        {
            Position addedPosition = _unitOfWork.Positions.Create(_mapper.Map<Position>(newPosition));
            _unitOfWork.Save();
            return _mapper.Map<PositionDTO>(addedPosition);
        }

        PositionDTO IPositionsService.RemovePosition(int id)
        {
            Position removedPosition = _unitOfWork.Positions.Delete(id);
            _unitOfWork.Save();
            return _mapper.Map<PositionDTO>(removedPosition);
        }

        PositionDTO IPositionsService.UpdatePosition(PositionDTO newPosition)
        {
            Position updatedPosition = _unitOfWork.Positions.Update(_mapper.Map<Position>(newPosition));
            _unitOfWork.Save();
            return _mapper.Map<PositionDTO>(updatedPosition);
        }

        bool IPositionsService.PositionExists(int id)
        {
            return _unitOfWork.Positions.Get(id) != null;
        }
    }
}
