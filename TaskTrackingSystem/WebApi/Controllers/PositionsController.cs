namespace TaskTrackingSystem.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Description;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;

    [Authorize]
    [RoutePrefix("api")]
    public class PositionsController : ApiController
    {
        private readonly IPositionsService _service;

        public PositionsController(IPositionsService positionsService)
        {
            _service = positionsService;
        }

        [Route("positions")]
        public IHttpActionResult GetPositions()
        {
            return Ok(_service.GetPositions());
        }

        [Route("positions/{id}", Name = "GetPosition")]
        [ResponseType(typeof(PositionDTO))]
        public IHttpActionResult GetPosition(int id)
        {
            PositionDTO position = _service.GetPositionById(id);
            if (position == null)
            {
                return NotFound();
            }

            return Ok(position);
        }

        [Route("positions")]
        [ResponseType(typeof(PositionDTO))]
        public IHttpActionResult PutPosition(PositionDTO position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!PositionExists(position.Id))
            {
                return NotFound();
            }

            return Ok(_service.UpdatePosition(position));
        }

        [Route("positions")]
        [ResponseType(typeof(PositionDTO))]
        public IHttpActionResult PostPosition(PositionDTO position)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (PositionExists(position.Id))
            {
                return BadRequest("Order exists");
            }

            return CreatedAtRoute("GetPosition", new { id = position.Id }, _service.AddPosition(position));
        }

        [Route("positions/{id}")]
        [ResponseType(typeof(PositionDTO))]
        public IHttpActionResult DeletePosition(int id)
        {
            if (!PositionExists(id))
            {
                return NotFound();
            }

            return Ok(_service.RemovePosition(id));
        }

        private bool PositionExists(int id)
        {
            return _service.PositionExists(id);
        }
    }
}