namespace TaskTrackingSystem.WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;
    using TaskTrackingSystem.WebApi.Models;

    [Authorize(Roles = "admin")]
    [RoutePrefix("api/account")]
    public class UsersController : ApiController
    {
        private readonly IUsersService _service;

        public UsersController(IUsersService usersService)
        {
            _service = usersService;
        }

        [OverrideAuthorization]
        [Authorize(Roles = "admin,manager")]
        [Route("users")]
        public async Task<IHttpActionResult> GetUsers()
        {
            var usersInfo = new List<InfoUserModel>();
            foreach (var user in _service.GetUsers())
            {
                var userRoles = await _service.GetRolesAsync(user.Id);
                usersInfo.Add(GetInfoUserModel(user, userRoles));
            }

            return Ok(usersInfo);
        }

        [Route("users/{id:guid}", Name = "GetUser")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await _service.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _service.GetRolesAsync(user.Id);

            return Ok(GetInfoUserModel(user, userRoles));
        }

        [OverrideAuthorization]
        [Authorize]
        [Route("user")]
        public async Task<IHttpActionResult> GetCurrentUser()
        {
            return await GetUser(RequestContext.Principal.Identity.GetUserId());
        }

        [Route("users/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            IdentityResult result = await _service.DeleteAsync(id);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok($"User {id} deleted");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IHttpActionResult> Register(RegisterUserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _service.CreateAsync(model.Email, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            var newUser = await _service.FindByEmailAsync(model.Email);

            return CreatedAtRoute("GetUser", new { id = newUser.Id }, newUser);
        }

        private InfoUserModel GetInfoUserModel(UserDTO user, IList<string> roles)
        {
            var rolesString = string.Join(", ", roles);
            return new InfoUserModel
            {
                Id = user.Id,
                Email = user.Email,
                Role = rolesString,
            };
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
                        ModelState.AddModelError(string.Empty, error);
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
