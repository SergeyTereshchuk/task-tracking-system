using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackingSystem.DAL.Models;

namespace TaskTrackingSystem.DAL.Repositories
{
    public class UserRepository : UserStore<ApplicationUser>
    {
    }
}
