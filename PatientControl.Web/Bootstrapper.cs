using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

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
}