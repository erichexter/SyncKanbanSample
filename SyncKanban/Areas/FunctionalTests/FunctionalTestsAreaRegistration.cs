using System.Web.Mvc;
using FunctionalTests.Controllers;

namespace FunctionalTests
{
    public class FunctionalTestsAreaRegistration : AreaRegistration
    {
        public const string FuncTestsFixtures = "FunctionalTests_Fixtures";
        public const string FuncTestsRunner = "FunctionalTests_Runner";

        public override string AreaName
        {
            get
            {
                return "FunctionalTests";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {

            context.MapRoute(FuncTestsFixtures,
                                AreaName + "/Fixtures/{controller}/{action}",
                                new { action = "Index" },
                                new[] { typeof(AlwaysPassController).Namespace });


            context.MapRoute(FuncTestsRunner,
                                          AreaName + "/Run/{action}",
                                          new { action = "Index", controller = "FunctionalTestRunner" },
                                          new[] { typeof(RunnerController).Namespace });

            context.MapRoute(
                "FunctionalTests_default",
                "FunctionalTests/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
