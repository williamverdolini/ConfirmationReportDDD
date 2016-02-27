using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CR.Application.Persistence.EF.Repos;
using CR.Domain.Model;
using CR.Domain.Persistence.EF.Repos;
using CR.Infrastructure;
using CR.Infrastructure.Db;
using CR.Infrastructure.Repo;
using System;

namespace CR.Application.Infrastructure.Injection.Installers
{
    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Contract.Requires<ArgumentNullException>(container != null, "container");
            container.Register(Component.For<ConfirmReportContext>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IAuthRepository>().ImplementedBy<AuthRepository>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IRepository<ConfirmationReport>>().ImplementedBy<ConfirmationReportRepository>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IDatabase<CR.Domain.Persistence.EF.Models.ConfirmationReport>>().ImplementedBy<ConfirmationReportDatabase>().LifeStyle.PerWebRequest);
            
        }
    }
}