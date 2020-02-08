using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using UnleasedBDD.utils;

namespace UnleasedBDD.pages
{
    class signInPage
    {

        private IWebDriver driver;
        private WebDriverWait wait;
    
        public signInPage(IWebDriver driver)
        {
             this.driver = driver;
             PageFactory.InitElements(driver, this);
             wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));

        }

        #region LoginElements

        [FindsBy(How = How.Id, Using = "username")]
        private IWebElement txt_userName;

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement txt_password;

        [FindsBy(How = How.Id, Using = "btnLogOn")]
        private IWebElement btn_signIn;

        [FindsBy(How = How.XPath, Using = "//span[text()='Dashboard']")]
        private IWebElement lbl_dashboard;
        

        #endregion

        public void unleasedLogin()
        {
            txt_userName.SendKeys(ConfigurationManager.AppSettings["userID"]);
            txt_password.SendKeys(ConfigurationManager.AppSettings["password"]);
            btn_signIn.Click();
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//span[text()='Dashboard']")));

        }

    }
}
