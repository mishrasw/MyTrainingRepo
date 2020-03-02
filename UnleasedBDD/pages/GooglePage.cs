using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UnleasedBDD.utils;

namespace UnleasedBDD.pages
{
    [Binding]
    class GooglePage
    {

        private IWebDriver driver;
        private WebDriverWait wait;
        private readonly ILog log;
        private readonly ExtentTest test;
        private readonly ScenarioContext context;


        public GooglePage(IWebDriver driver, ScenarioContext context)
        {
            this.context = context;
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            log = context.Get<ILog>("log");
            test = context.Get<ExtentTest>("extentTest");


        }
        [FindsBy(How = How.XPath, Using = "//input[@name='q']")]
        private IWebElement txt_searchBox;

        [FindsBy(How = How.XPath, Using = "(//input[@value='Google Search'])[2]")]
        private IWebElement btn_searchResults;

        [FindsBy(How = How.Name, Using = "(//span[text()='ValueMomentum'])[1]")]
        private IWebElement lbl_ValTab;

        public void searchGooglePage(Table table)
        {
            IEnumerable<dynamic> tabs = table.CreateDynamicSet();

            foreach (var tab in tabs)
            {
                txt_searchBox.Click();
                string var = tab.searchText;
                txt_searchBox.SendKeys(var);
                txt_searchBox.SendKeys(Keys.Tab);
                btn_searchResults.Click();
                wait.Until(ExpectedConditions.ElementExists(By.XPath("(//span[text()='ValueMomentum'])[1]")));
                log.Info("Successfully Clicked on Search Button");
                test.Log(Status.Pass, "Successfully Clicked on Search Button");

            }
        }

        public void verifyResult(Table table)
        {
            IEnumerable<dynamic> tabs = table.CreateDynamicSet();

            foreach (var tab in tabs)
            {
                string lnkResults = tab.linkToBeDisplayed;
                Console.WriteLine(tab.linkToBeDisplayed);
                IWebElement linkDisplayed = driver.FindElement(By.XPath("//h3[text()='"+ lnkResults + "']"));
                Assert.IsTrue(new OtherUtils(driver).isElementPresent(linkDisplayed), "Link Displayed successfully");
                log.Info("Link Displayed successfully");
                test.Log(Status.Pass, "Link Displayed successfully");
            }
        }
    }
}
