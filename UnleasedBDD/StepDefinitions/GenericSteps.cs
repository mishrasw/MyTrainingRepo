using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using UnleasedBDD.pages;

namespace UnleasedBDD.steps
{
  [Binding]
    class GenericSteps
    {
        
        private IWebDriver driver;
        private readonly ScenarioContext context;

        public GenericSteps(ScenarioContext context)
        {
            this.context = context;
            driver = context.Get<IWebDriver>("driver");
        }

        [Given(@"User logs in to Unleased")]
        public void GivenUserLogsInToUnleased()
        {           
            new signInPage(driver).unleasedLogin();
        }

    }
}
