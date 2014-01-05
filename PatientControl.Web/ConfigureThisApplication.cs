using Autofac;
using Autofac.Integration.Mvc;
using PatientControl.Web.Controllers;

namespace PatientControl.Web
{
    public class ConfigureThisApplication : IBootstrapperTask
    {
        readonly ContainerBuilder _containerBuilder;

        public ConfigureThisApplication(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public void Run()
        {
            _containerBuilder.RegisterControllers((typeof(HomeController).Assembly));
        }
    }
}