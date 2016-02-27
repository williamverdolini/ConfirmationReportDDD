using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using CR.Application.Persistence.EF.Profiles;
using CR.Domain.Persistence.EF.Profiles;
using CR.Infrastructure.Mappings;

namespace CR.Application.Infrastructure.Injection.Installers
{
    public class MappersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Component.For<IMappingEngine>().UsingFactoryMethod(() => AutoMapper.M config.CreateMapper()  Mapper.Engine));
            //container.Register(Component.For<AutoMapper.IMapper>().UsingFactoryMethod(() => AutoMapper.));
            container.Register(Component.For<CR.Infrastructure.Mappings.IMapper>().ImplementedBy<LightMapper>());
            container.Register(Classes.FromThisAssembly().BasedOn<Profile>().Configure(c => c.LifestyleTransient()));
            container.Register(Classes.FromAssemblyContaining<AuthServiceProfile>().BasedOn<Profile>().Configure(c => c.LifestyleTransient()));
            container.Register(Classes.FromAssemblyContaining<ConfirmationReportProfile>().BasedOn<Profile>().Configure(c => c.LifestyleTransient()));
        }
    }
}