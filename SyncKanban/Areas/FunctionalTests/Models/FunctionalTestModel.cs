using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Serialization;

namespace FunctionalTests.Models
{
    public class FunctionalTestModel
    {
        public const string DefaultFixtureIndex = "Index";

        public FunctionalTestModel(Type fixtureController)
        {
            FixtureAction = DefaultFixtureIndex;
            FixtureController = fixtureController.Name.Replace("Controller", string.Empty);
        }

        public string TestName { get { return FixtureController; } }
        public string FixtureAction { get; private set; }
        public string FixtureController { get; private set; }

        public HtmlString GetFixtureUrl(UrlHelper url, object parameters = null)
        {
            var routevalues = new RouteValueDictionary(parameters ?? new object()) { { "controller", FixtureController }, { "action", FixtureAction } };

            return new HtmlString(url.RouteUrl(FunctionalTestsAreaRegistration.FuncTestsFixtures, routevalues));
        }

        public string GetTestPartial()
        {
            return string.Format("~/Areas/FunctionalTests/Views/{0}/Test.cshtml", FixtureController);
        }
    }


    [XmlRoot("Fixtures")]
    public class Fixtures : List<Fixture>
    {
        public Fixtures(IEnumerable<Fixture> collection)
            : base(collection)
        {
        }

        public Fixtures()
        {
        }
    }

    public class Fixture
    {
        public Fixture()
        {
        }

        public Fixture(string url)
        {
            Url = url;
        }

        public string Url { get; set; }
    }

}
