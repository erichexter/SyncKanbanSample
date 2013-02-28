using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using FunctionalTests.Models;

namespace FunctionalTests.Controllers
{
        public class RunnerController : Controller
        {
            readonly string[] ie9Only = new[] { "Internet Explorer 9" };


            public ViewResult Index(string test)
            {
                var model = new FunctionalTestRunnerModel(test);

                return View(model);
            }

            public JsonResult List(string[] correlation)
            {
                var expected_correlation = GetExpectedCorrelation(correlation);

                var model = new FunctionalTestRunnerModel(null);

                var descriptor = new JobDescriptor
                {
                    Name = string.Format("Functional Tests {0}", expected_correlation),
                    Runs = GetTests(model),
                    UserAgentNames = ie9Only,
                };

                return Json(descriptor, JsonRequestBehavior.AllowGet);
            }

            public ActionResult FixtureList()
            {
                var fixtureUrls = GetAllFixtures(Url);

                return new XmlResult(fixtureUrls);
            }

            Fixtures GetAllFixtures(UrlHelper urlHelper)
            {
                var fixtures = from controller in typeof(AlwaysPassController).Assembly.GetTypes()
                               let url =
                                   urlHelper.Action("Index", controller.Name.ToLower().Replace("controller", string.Empty))
                               let deployedUrl = GetDeployedUrl(url)
                               where typeof(IController).IsAssignableFrom(controller)
                                     && controller.Namespace == typeof(AlwaysPassController).Namespace && controller!=typeof(RunnerController)
                               orderby controller.Name
                               select new Fixture(deployedUrl);

                return new Fixtures(fixtures);
            }

            public ViewResult RequestInspection()
            {
                return View();
            }

            static string GetExpectedCorrelation(string[] correlation)
            {
                string expected_correlation;
                if (correlation == null || correlation.Length == 0)
                    expected_correlation = "no correlation";
                else
                    expected_correlation = correlation[0];
                return expected_correlation;
            }

            IEnumerable<RunDescriptor> GetTests(FunctionalTestRunnerModel model)
            {
                return
                    model.Tests.Select(
                        test => new RunDescriptor { Name = test.TestName, Url = GetRunUrl(test.FixtureController) });
            }

            string GetRunUrl(string test)
            {
                var relativeUrl = Url.RouteUrl("FunctionalTests_default",
                                               new { test, controller = "Runner", action = "Index" });
                var absoluteUrl = GetDeployedUrl(relativeUrl);
                return absoluteUrl;
            }

            string GetDeployedUrl(string relativeUrl)
            {
                return Request.Url.GetLeftPart(UriPartial.Authority) + relativeUrl;
            }

            class JobDescriptor
            {
                public string Name { get; set; }
                public IEnumerable<RunDescriptor> Runs { get; set; }
                public IEnumerable<string> UserAgentNames { get; set; }
            }

            class RunDescriptor
            {
                public string Url { get; set; }
                public string Name { get; set; }
            }
        }

        public class XmlResult : ActionResult
        {
            readonly object _model;

            public XmlResult(object model)
            {
                _model = model;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                if (_model == null) return;

                var xs = new XmlSerializer(_model.GetType());
                context.HttpContext.Response.ContentType = "text/xml";
                xs.Serialize(context.HttpContext.Response.Output, _model);
            }
        }

    }

