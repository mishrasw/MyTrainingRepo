using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UnleasedBDD.pages
{
    class SalesOrderPage
    {

        private IWebDriver driver;
        private WebDriverWait wait;

        public SalesOrderPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }

        #region SalesOrderPage


        [FindsBy(How = How.XPath, Using = "//span[text()='Sales']")]
        private IWebElement lbl_salesModule;

        [FindsBy(How = How.XPath, Using = "//span[text()='Orders']")]
        private IWebElement lbl_salesOrders;

        [FindsBy(How = How.XPath, Using = "//span[text()='Add Sales Order']")]
        private IWebElement lbl_addSalesProducts;

        [FindsBy(How = How.Id, Using = "SelectedCustomerCode")]
        private IWebElement txt_customerCode;
        
        [FindsBy(How = How.Id, Using = "ProductAddLine")]
        private IWebElement txt_productLineSelect;

        [FindsBy(How = How.Id, Using = "QtyAddLine")]
        private IWebElement txt_quantityLine;

        [FindsBy(How = How.Id, Using = "btnComplete")]
        private IWebElement completeBtn;

        [FindsBy(How = How.Id, Using = "btnAddOrderLine")]
        private IWebElement addProductBtn;

        [FindsBy(How = How.Id, Using = "ProductFilter")]
        private IWebElement txt_productFilter;
        





        #endregion

        public void CreateSalesOrder(Table table)
        {
            IEnumerable<dynamic> tabs = table.CreateDynamicSet();

            foreach(var tab in tabs)
            {

            lbl_salesModule.Click();
            wait.Until(driver => lbl_salesOrders.Displayed);
            lbl_salesOrders.Click();
            wait.Until(driver => lbl_addSalesProducts.Displayed);
            lbl_addSalesProducts.Click();

            wait.Until(driver => txt_customerCode.Displayed);
                string customerCode = tab.CustomerCode.ToString().Trim();
                string productCode = tab.ProductCode.ToString().Trim();
                string quantity = tab.Quantity.ToString().Trim();
                txt_customerCode.SendKeys(customerCode);
                driver.FindElement(By.XPath("//li[contains(.,'" + customerCode + "')]")).Click();
                txt_productLineSelect.Click();
                txt_productLineSelect.SendKeys(productCode);
                driver.FindElement(By.XPath("//li[contains(.,'" + productCode + "')]")).Click();
                //Assert.IsFalse(isElementPresent(alertText), "Error:while creating Product.Please choose a different Name");
                txt_quantityLine.SendKeys(quantity);
                addProductBtn.Click();
                completeBtn.Click();
            }

   
        }

     
    }
}
