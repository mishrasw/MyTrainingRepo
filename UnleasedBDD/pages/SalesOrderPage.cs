using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UnleasedBDD.utils;

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

        [FindsBy(How = How.Id, Using = "AvailableAddLine")]
        private IWebElement txtProductAvailable;

        [FindsBy(How = How.XPath, Using = "//h1[text()='Complete Order']")]
        private IWebElement hrd_CompleteOrder;

        [FindsBy(How = How.Id, Using = "generic-confirm-modal-yes")]
        private IWebElement btn_confirmComplete;
        
        [FindsBy(How = How.XPath, Using = "(//*[@role='alert' and contains(.,'You have successfully Completed Sales Order')])[1]")]
        private IWebElement salesOrderSuccessful;

        [FindsBy(How = How.Id, Using = "OrderNumberDisplay")]
        private IWebElement hdr_salesOrderID;

        [FindsBy(How = How.Id, Using = "SalesOrderLinesList_DXDataRow0")]
        private IWebElement tbl_firstRow;
        




        #endregion

        public void CreateSalesOrder(Table table)
        {
            
            IEnumerable<dynamic> tabs = table.CreateDynamicSet();

            foreach(var tab in tabs)
            {
            string customerCode = tab.CustomerCode.ToString().Trim();
            string productCode = tab.ProductCode.ToString().Trim();
            string quantity = tab.Quantity.ToString().Trim();
            ScenarioContext.Current["stocksUtilised"] = quantity;
            ScenarioContext.Current["masterQuantity"] = new ProductPage(driver).getStocksInHand(productCode);
            lbl_salesModule.Click();
            wait.Until(driver => lbl_salesOrders.Displayed);
            lbl_salesOrders.Click();
            wait.Until(driver => lbl_addSalesProducts.Displayed);
            lbl_addSalesProducts.Click();

            wait.Until(driver => txt_customerCode.Displayed);
                
                txt_customerCode.SendKeys(customerCode);
                driver.FindElement(By.XPath("//li[contains(.,'" + customerCode + "')]")).Click();
                Thread.Sleep(2000);
                txt_productLineSelect.Click();
                txt_productLineSelect.SendKeys(productCode);
                Thread.Sleep(1000);
                //ScenarioContext.Current["masterQuantity"] = driver.FindElement(By.XPath("//li[contains(.,'" + productCode + "')]//div[@class='align-left availability']")).Text.Split(':')[1].Trim();
                driver.FindElement(By.XPath("//li[contains(.,'" + productCode + "')]")).Click();
                //Assert.IsFalse(isElementPresent(alertText), "Error:while creating Product.Please choose a different Name");
                txt_quantityLine.SendKeys(quantity);
                Thread.Sleep(2000);
                addProductBtn.Click();
                wait.Until(driver => tbl_firstRow.Displayed); 


            }

   
        }

        public void CompleteSalesOrder()
        {
            completeBtn.Click();
            wait.Until(driver => hrd_CompleteOrder.Displayed);
            btn_confirmComplete.Click();
            Assert.IsTrue(new OtherUtils(driver).isElementPresent(salesOrderSuccessful), "Sales Order Creation Success Message");
            ScenarioContext.Current["salesOrderID"] = hdr_salesOrderID.Text;


        }

        public void verifyStockInHand(Table table)
        {
            IEnumerable<dynamic> tabs = table.CreateDynamicSet();

            foreach (var tab in tabs)
            {
                string productCode = tab.ProductCode;
                ScenarioContext.Current["availableQuantity"] = new ProductPage(driver).getStocksInHand(productCode);

                int resultQty = Int32.Parse(ScenarioContext.Current["masterQuantity"].ToString().Split('.')[0]) - Int32.Parse(ScenarioContext.Current["stocksUtilised"].ToString());
                Assert.IsTrue(resultQty.Equals(Int32.Parse(ScenarioContext.Current["availableQuantity"].ToString().Split('.')[0])),"Stocks available verified");
            }

            }



    }
}
