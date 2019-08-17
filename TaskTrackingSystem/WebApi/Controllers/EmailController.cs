namespace TaskTrackingSystem.WebApi.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using MailKit.Net.Smtp;
    using MimeKit;
    using TaskTrackingSystem.BLL.DTO;
    using TaskTrackingSystem.BLL.Interfaces;

    [Authorize(Roles = "admin,manager")]
    [RoutePrefix("api")]
    public class EmailController : ApiController
    {
        private readonly IUsersService _service;

        public EmailController(IUsersService usersService)
        {
            _service = usersService;
        }

        [HttpGet]
        [Route("mail/users/{id:guid}/tasks")]
        public async Task<IHttpActionResult> NotifyOnNewTaskAsync(string id)
        {
            UserDTO user = await _service.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Task Tracking System", "project.tasktrackingsystem@gmail.com"));
            message.To.Add(new MailboxAddress("User", user.Email));
            message.Subject = "You have new task!";

            message.Body = new TextPart("plain")
            {
                Text = @"You've been assigned a new task.",
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("project.tasktrackingsystem@gmail.com", "wefcdsnonef");

                client.Send(message);
                client.Disconnect(true);
            }

            return Ok();
        }
    }
}
