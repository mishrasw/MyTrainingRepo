using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using log4net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace UnleasedBDD.utils
{
    [Binding]
    class hooks
    {
        [assembly: log4net.Config.XmlConfigurator(Watch = true)]
        public string applicationURL= ConfigurationManager.AppSettings["testURL"];
        public IWebDriver driver;
        public ILog log;
        

        [Before]
        public void launchURL()
        {
            driver = new ChromeDriver();
            log4net.Config.DOMConfigurator.Configure();
            log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            ScenarioContext.Current.Set<IWebDriver>(driver,"driver");
            ScenarioContext.Current.Set<ILog>(log, "log");
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
