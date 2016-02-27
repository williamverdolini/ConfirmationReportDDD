using AutoMapper;
using System;
using System.Linq;

namespace CR.Infrastructure.Mappings
{
    public class LightMapper : IMapper
    {
        //private readonly IMappingEngine engine;
        private readonly Lazy<AutoMapper.IMapper> engine;

        //public LightMapper(IMappingEngine engine)
        public LightMapper(Lazy<AutoMapper.IMapper> engine)
        {
            Contract.Requires<ArgumentNullException>(engine != null, "IMappingEngine");
            this.engine = engine;
        }

        public TDestination Map<TDestination>(object source)
        {            
            //if (source.GetType().Namespace.Equals("System.Data.Entity.DynamicProxies"))
            //    return ((IQueryable)source).ProjectToFirst<TDestination>(engine.ConfigurationProvider);
            return engine.Value.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return engine.Value.Map<TDestination>(source);
        }
    }
}