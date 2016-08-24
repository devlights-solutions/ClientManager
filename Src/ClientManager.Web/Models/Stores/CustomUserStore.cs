using ClientManager.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;


namespace ClientManager.Web.Models
{
    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
    CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
