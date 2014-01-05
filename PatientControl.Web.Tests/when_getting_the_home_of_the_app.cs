using System.Web.Mvc;
using Machine.Specifications;
using PatientControl.Web.Controllers;

namespace PatientControl.Web.Tests
{
    public class when_getting_the_home_of_the_app
    {
        static HomeController _controller;
        static ActionResult _result;

        Establish context =
            () => { _controller = new HomeController(); };

        Because of =
            () => _result = _controller.GetHome();

        It should_return_a_html_file =
            () => _result.ShouldNotBeNull();
    }
}