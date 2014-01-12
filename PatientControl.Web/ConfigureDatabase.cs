using AcklenAvenue.Data.NHibernate;
using Autofac;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using PatientControl.Data;

namespace PatientControl.Web
{
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
                ConnectionString(x => x.FromConnectionStringWithKey("PatientControlDev"));

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