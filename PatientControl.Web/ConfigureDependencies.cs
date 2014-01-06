using AutoMapper;
using Autofac;
using ClassLibrary1;
using NHibernate;
using PatientControl.Data;

namespace PatientControl.Web
{
    public class ConfigureDependencies : IBootstrapperTask
    {
        readonly ContainerBuilder _containerBuilder;

        public ConfigureDependencies(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public void Run()
        {
            _containerBuilder.RegisterType<ReadOnlyRepository>().As<IReadOnlyRepository>();
            _containerBuilder.RegisterType<WriteableRepository>().As<IWriteableRepository>();
            _containerBuilder.Register(c => c.Resolve<ISessionFactory>().OpenSession())
                .As<ISession>()
                .InstancePerLifetimeScope();
            _containerBuilder.RegisterInstance(Mapper.Engine).As<IMappingEngine>();
        }
    }

   
}