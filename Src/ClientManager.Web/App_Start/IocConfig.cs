using System.Security.Principal;
using System.Web;
using Framework.Common.Utility;
using Framework.Data.EntityFramework.Helpers;
using Framework.Ioc;
using ClientManager.Data;
using ClientManager.Data.Helpers;
using ClientManager.Data.Interfaces;
using ClientManager.Service;
using ClientManager.Web.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Web.Common;

namespace ClientManager.Web
{
    public class IoCConfig
    {
        public static void Config(IKernel kernel)
        {
            RegisterBindings(kernel);

            IocContainer.Initialize(new NinjectIocContainer(kernel));
        }

        /// <summary>
        /// Register all the binding.
        /// </summary>
        /// <param name="kernel"></param>
        private static void RegisterBindings(IKernel kernel)
        {
            kernel.Bind<RepositoryFactories>().To<BaseProjectRepositoryFactories>().InSingletonScope();
            kernel.Bind<IClock>().To<Clock>().InSingletonScope();
            kernel.Bind<IRepositoryProvider>().To<RepositoryProvider>();
            kernel.Bind<IBaseProjectUow>().To<BaseProjectUow>().InRequestScope();

            kernel.Bind<ApplicationRoleManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>());
            kernel.Bind<ApplicationUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
            kernel.Bind<ApplicationSignInManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>());

            kernel.Bind(x => x.FromAssemblyContaining<ServiceBase>()
                            .SelectAllClasses()
                            .BindAllInterfaces()
                            .Configure(c => c.InRequestScope()));

            kernel.Bind<ICurrentUser>().To<CurrentUser>().InRequestScope();
            kernel.Bind<IIdentity>().ToMethod(c => HttpContext.Current.User.Identity);
        }
    }
}