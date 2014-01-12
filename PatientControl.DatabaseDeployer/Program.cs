using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AcklenAvenue.Data.NHibernate;
using ClassLibrary1;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using PatientControl.Data;
using DomainDrivenDatabaseDeployer;

namespace PatientControl.DatabaseDeployer
{
    class Program
    {
        static void Main(string[] args)
        {
            MsSqlConfiguration databaseConfiguration =
                MsSqlConfiguration.MsSql2008.ShowSql().ConnectionString(x => x.FromConnectionStringWithKey("PatientControlDev"));

            DomainDrivenDatabaseDeployer.DatabaseDeployer dd = null;
            ISessionFactory sessionFactory = new SessionFactoryBuilder(new MappingScheme(), databaseConfiguration)
                .Build(cfg => { dd = new DomainDrivenDatabaseDeployer.DatabaseDeployer(cfg); });

            dd.Drop();
            Console.WriteLine("");
            Console.WriteLine("Database dropped.");
            Thread.Sleep(1000);

            dd.Create();
            Console.WriteLine("");
            Console.WriteLine("Database created.");

            ISession session = sessionFactory.OpenSession();
            using (ITransaction tx = session.BeginTransaction())
            {
                dd.Seed(new List<IDataSeeder>
                            {
                                //add data seeders here.
                                new RoomsSeeder(session),
                            });
                tx.Commit();
            }
            session.Close();
            sessionFactory.Close();
            Console.WriteLine("");
            Console.WriteLine("Seed data added.");
            Thread.Sleep(2000);
        }
    }

    class RoomsSeeder : IDataSeeder
    {
        readonly ISession _session;

        public RoomsSeeder(ISession session)
        {
            _session = session;
        }

        public void Seed()
        {
            _session.Save(new Room()
                              {
                                  Id = Guid.NewGuid(),
                                  Name = "Room 1"
                              });
            _session.Save(new Room()
            {
                Id = Guid.NewGuid(),
                Name = "Room 2"
            });
            _session.Save(new Room()
            {
                Id = Guid.NewGuid(),
                Name = "Room 3"
            });
        }
    }
}
