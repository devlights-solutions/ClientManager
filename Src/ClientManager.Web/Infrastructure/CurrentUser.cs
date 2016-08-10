using System.Security.Principal;
using System.Web;
using Microsoft.AspNet.Identity;
using ClientManager.Web.Models;

namespace ClientManager.Web.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IIdentity _identity;
        private readonly ApplicationUserManager _applicationUserManager;

        private ApplicationUser _user;

        public CurrentUser(IIdentity identity, ApplicationUserManager applicationUserManager)
        {
            _identity = identity;
            _applicationUserManager = applicationUserManager;
        }

        public ApplicationUser User
        {
            get { return _user ?? (_user = _applicationUserManager.FindById(_identity.GetUserId())); }
        }
    }
}