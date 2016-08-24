using ClientManager.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClientManager.Web.Models
{
    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
