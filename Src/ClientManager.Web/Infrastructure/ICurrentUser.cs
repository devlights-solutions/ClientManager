using ClientManager.Web.Models;

namespace ClientManager.Web.Infrastructure
{
	public interface ICurrentUser
	{
        ApplicationUser User { get; }
    }
}