using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CR.Application.Workers;
using CR.Infrastructure;
using System;

namespace CR.Application.Infrastructure.Injection.Installers
{
    public class WorkersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Contract.Requires<ArgumentNullException>(container != null, "container");

            container.Register(Component.For<IConfirmationReportWorker>().ImplementedBy<ConfirmationReportWorker>().LifeStyle.Transient);
            container.Register(Component.For<IAuthWorker>().ImplementedBy<AuthWorker>().LifeStyle.Transient);
        }
    }
}