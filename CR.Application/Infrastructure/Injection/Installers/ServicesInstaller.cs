using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CR.Application.Abstractions.Services;
using CR.Application.Persistence.EF.Services;
using CR.Domain.Persistence.EF.Services;
using CR.Infrastructure;
using System;

namespace CR.Application.Infrastructure.Injection.Installers
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Contract.Requires<ArgumentNullException>(container != null, "container");

            container.Register(Component.For<IAuthService>().ImplementedBy<AuthService>().LifeStyle.Transient);
            container.Register(Component.For<IConfirmationReportCommandService>().ImplementedBy<ConfirmationReportCommandService>().LifeStyle.Transient);
            container.Register(Component.For<IConfirmationReportQueryService>().ImplementedBy<ConfirmationReportQueryService>().LifeStyle.Transient);
        }
    }
}