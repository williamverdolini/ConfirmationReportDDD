using System.Linq;
using AutoMapper;
using Castle.Windsor;
using CR.Application.Persistence.EF.Profiles;
using Castle.MicroKernel.Registration;
using System;
using Castle.MicroKernel.Resolvers;

namespace CR.Application
{
    public class AutoMapperConfig
    {
        public static void Configure(IWindsorContainer container)
        {
            //Mapper.Initialize(x => GetConfiguration(Mapper.Configuration, container));

            //var config1 = new MapperConfiguration(cfg => { });
            //container.Register(Component.For<IMapper>().UsingFactoryMethod(() => config1.CreateMapper()));
            //config1 = new MapperConfiguration(cfg => GetConfiguration(cfg, container));

            container.Register(Component.For<ILazyComponentLoader>().ImplementedBy<LazyOfTComponentLoader>());
            var config = new MapperConfiguration(cfg => GetConfiguration(cfg, container));
            container.Register(Component.For<IMapper>().UsingFactoryMethod(() => config.CreateMapper()));

        }

        private static void GetConfiguration(IConfiguration configuration, IWindsorContainer container)
        {
            //AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("CR.")).ToList().ForEach(a => {
            //    a.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x)).OfType<Profile>().ToList()
            //        .ForEach(
            //        configuration.AddProfile
            //        );
            //    });

            AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.Contains("CR.")).ToList().ForEach(a =>
            {
                a.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x)).ToList().ForEach(profile =>
                    configuration.AddProfile(container.Resolve(profile) as Profile));
            });
            //IMapperConfiguration config = 
            //var profiles = typeof(AuthServiceProfile).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));

            //foreach (var profile in profiles)
            //{
            //    configuration.AddProfile(container.Resolve(profile) as Profile);
            //}
        }
    }
}