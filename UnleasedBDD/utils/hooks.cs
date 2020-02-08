using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace UnleasedBDD.utils
{
    [Binding]
    class hooks
    {
        
        public string applicationURL= ConfigurationManager.AppSettings["testURL"];
        public IWebDriver driver;

        [Before]
        public void launchURL()
        {
            driver = new ChromeDriver();
            ScenarioContext.Current.Set<IWebDriver>(driver,"driver");
            driver.Navigate().GoToUrl(applicationURL);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }
        [After]
        public void close()
        {
            driver.Close();
            driver.Quit();
        }
    }
}
