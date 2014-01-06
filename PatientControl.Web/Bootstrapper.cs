using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AcklenAvenue.Data.NHibernate;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using PatientControl.Data;

namespace PatientControl.Web
{
    public class Bootstrapper
    {
        readonly ContainerBuilder _containerBuilder;

        public Bootstrapper(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public IContainer Run()
        {
            new List<IBootstrapperTask>
                {
                    new ConfigureThisApplication(_containerBuilder),
                    new ConfigureDatabase(_containerBuilder),
                    new ConfigureDependencies(_containerBuilder),
                    new ConfigureAutoMapper(),
                    
                }.ForEach(x => x.Run());
            return BuildContainer();
        }

        IContainer BuildContainer()
        {
            var container = _containerBuilder.Build();
            AfterContainerIsBuilt(container);
            return container;
        }

        void AfterContainerIsBuilt(ILifetimeScope container)
        {
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }

    public class ConfigureDatabase : IBootstrapperTask
    {
        readonly ContainerBuilder _containerBuilder;

        public ConfigureDatabase(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public void Run()
        {
            MsSqlConfiguration databaseConfiguration = MsSqlConfiguration.MsSql2008.ShowSql().
                ConnectionString(x => x.FromConnectionStringWithKey("connectionStrings"));

            _containerBuilder.Register(c => { return c.Resolve<ISessionFactory>().OpenSession(); }).As
                                   <ISession>()
                                   .InstancePerLifetimeScope()
                                   .OnActivating(c =>
                                   {
                                       if (!c.Instance.Transaction.IsActive)
                                           c.Instance.BeginTransaction();
                                   }
                                   )
                                   .OnRelease(c =>
                                   {
                                       if (c.Transaction.IsActive)
                                       {
                                           c.Transaction.Commit();
                                       }
                                       c.Dispose();
                                   });

            _containerBuilder.Register(c =>
                               new SessionFactoryBuilder(new MappingScheme(), databaseConfiguration).Build())
                .SingleInstance()
                .As<ISessionFactory>();

        }
    }
}