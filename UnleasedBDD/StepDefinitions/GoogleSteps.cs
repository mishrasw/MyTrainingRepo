using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using UnleasedBDD.pages;

namespace UnleasedBDD.StepDefinitions
{
    [Binding]
    class GoogleSteps
    {

        private IWebDriver driver;
        private readonly ScenarioContext context;
        private readonly ILog log;

        public GoogleSteps(ScenarioContext context)
        {
            this.context = context;
            driver = context.Get<IWebDriver>("driver");
            log = context.Get<ILog>("log");
        }


        [Given(@"I have searched the details in Google Page")]
        public void GivenIHaveSearchedTheDetailsInGooglePage(Table table)
        {
            log.Info("Searching into Google Home Page");
            new GooglePage(driver).searchGooglePage(table);
        }

        [Then(@"it should display the results on webpage")]
        public void ThenItShouldDisplayTheResultsOnWebpage(Table table)
        {
            log.Info("Verifying results page");
            new GooglePage(driver).verifyResult(table);
        }

    }
}
