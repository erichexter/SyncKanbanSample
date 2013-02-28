using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FunctionalTests.Controllers;

namespace FunctionalTests.Models
{
    public class FunctionalTestRunnerModel
    {
        public static Lazy<IEnumerable<FunctionalTestModel>> all =
            new Lazy<IEnumerable<FunctionalTestModel>>(GetAllAvailableTestModels);

        public FunctionalTestRunnerModel(string testName)
        {
            if (!string.IsNullOrWhiteSpace(testName))
            {
                testName = testName;

                var theOneModel = all.Value.SingleOrDefault(x => string.Equals(x.FixtureController, testName, StringComparison.OrdinalIgnoreCase));

                if (theOneModel == null)
                    throw new HttpException(404, string.Format("could not find a test with name {0}", testName));

                Tests = new[] { theOneModel };
            }
            else
            {
                RunningAllTests = true;

                Tests = all.Value;
            }
        }

        public IEnumerable<FunctionalTestModel> Tests { get; private set; }
        public bool RunningAllTests { get; private set; }

        static IEnumerable<FunctionalTestModel> GetAllAvailableTestModels()
        {
            var fixtureNamespace = typeof(AlwaysPassController).Namespace;

            var fixtureControllers =
                typeof(AlwaysPassController).Assembly.GetTypes().Where(
                    x => string.Equals(x.Namespace, fixtureNamespace)).Where(
                        x => typeof(Controller).IsAssignableFrom(x)&& x!=typeof(RunnerController));

            IEnumerable<FunctionalTestModel> models =
                fixtureControllers.Select(x => new FunctionalTestModel(x)).OrderBy(x => x.FixtureController);

            return models;
        }
    }
}