using BaseProject.Web.Models;

namespace BaseProject.Web.Infrastructure
{
	public interface ICurrentUser
	{
        ApplicationUser User { get; }
    }
}