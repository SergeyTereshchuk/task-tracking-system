namespace TaskTrackingSystem.WebApi.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
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

        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(_service.GetUsers());
        }

        [Route("users/{id:guid}", Name = "GetUser")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await _service.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Route("users/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            var appUser = await _service.FindByIdAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            var result = await _service.DeleteAsync(appUser);

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
