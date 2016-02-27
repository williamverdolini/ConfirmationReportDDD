using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CR.Infrastructure;
using System;
using System.Web.Http.Controllers;

namespace CR.Application.Infrastructure.Injection.Installers
{
    /// <summary>
    /// Windsor.Castle ControllerInstaller
    /// see http://docs.castleproject.org/Windsor.Windsor-tutorial-ASP-NET-MVC-3-application-To-be-Seen.ashx
    /// </summary>
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Contract.Requires<ArgumentNullException>(container != null, "container");

            // Register WebAPI Controllers
            container.Register(
                Classes
                .FromThisAssembly()
                .BasedOn<IHttpController>()
                .LifestyleTransient());
        }
    }
}