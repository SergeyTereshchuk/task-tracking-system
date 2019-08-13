namespace TaskTrackingSystem.WebApi.Controllers
{
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using TaskTrackingSystem.BLL.Interfaces;
    using TaskTrackingSystem.WebApi.Models;

    [Authorize(Roles = "admin")]
    [RoutePrefix("api")]
    public class RolesController : ApiController
    {
        private readonly IUsersService _usersService;
        private readonly IRolesService _rolesService;

        public RolesController(IUsersService usersService, IRolesService rolesService)
        {
            _usersService = usersService;
            _rolesService = rolesService;
        }

        [Route("roles", Name = "GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            return Ok(_rolesService.GetRoles());
        }

        [Route("roles/{id:guid}", Name = "GetRole")]
        public async Task<IHttpActionResult> GetRole(string id)
        {
            var role = await _rolesService.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpPost]
        [Route("roles")]
        public async Task<IHttpActionResult> Create(CreateRoleModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _rolesService.CreateAsync(model.Name);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            var newRole = await _rolesService.FindByNameAsync(model.Name);

            return CreatedAtRoute("GetRole", new { id = newRole.Id }, newRole);
        }

        [Route("roles/{id:guid}")]
        public async Task<IHttpActionResult> DeleteRole(string id)
        {

            var role = await _rolesService.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            var result = await _rolesService.DeleteAsync(role);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok($"Role {id} deleted");
        }

        [HttpPut]
        [Route("users/{id:guid}/roles")]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] newRoles)
        {
            var user = await _usersService.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _usersService.GetRolesAsync(user.Id);

            var incorrectRoles = newRoles.Except(_rolesService.GetRoles().Select(x => x.Name)).ToArray();

            if (incorrectRoles.Count() > 0)
            {
                ModelState.AddModelError(string.Empty, $"Roles '{string.Join(",", incorrectRoles)}' does not exixts in the system");
                return BadRequest(ModelState);
            }

            var removeResult = await _usersService.RemoveFromRolesAsync(user.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            var addResult = await _usersService.AddToRolesAsync(user.Id, newRoles);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok($"Roles '{string.Join(",", newRoles)}' added to user {id}");
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(String.Empty, error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
