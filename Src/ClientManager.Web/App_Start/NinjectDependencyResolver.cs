using System.Web.Http.Dependencies;
using Ninject;
using Ninject.Web.WebApi;

namespace ClientManager.Web
{
    public class NinjectDependencyResolverApi : NinjectDependencyScope, IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolverApi(IKernel kernel) : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }

    }
}