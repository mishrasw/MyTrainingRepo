using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using UnleasedBDD.utils;

namespace UnleasedBDD.pages
{
    class ProductPage
    {

        private IWebDriver driver;
        private WebDriverWait wait;

        public ProductPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        }

        #region LoginElements

       
        [FindsBy(How = How.XPath, Using = "//span[text()='Inventory']")]
        private IWebElement lbl_inventory;

        [FindsBy(How = How.XPath, Using = "//span[text()='Products']")]
        private IWebElement lbl_products;

        [FindsBy(How = How.XPath, Using = "//span[text()='Add Product']")]
        private IWebElement lbl_addProducts;

        [FindsBy(How = How.XPath, Using = "//span[text()='View Products']")]
        private IWebElement lbl_viewProducts;

        [FindsBy(How = How.Id, Using = "Product_ProductCode")]
        private IWebElement txt_productCode;
        
        [FindsBy(How = How.Id, Using = "Product_ProductDescription")]
        private IWebElement txt_productDescription;

        [FindsBy(How = How.Id, Using = "btnSave")]
        private IWebElement btn_save;

        [FindsBy(How = How.XPath, Using = "(//div[@role='alert' and contains(.,'The Product Code must be unique')])[1]")]
        private IWebElement alertText;

        [FindsBy(How = How.XPath, Using = "(//*[@role='alert' and contains(.,'You have updated the product successfully.')])[1]")]
        private IWebElement productSuccessful;

        [FindsBy(How = How.Id, Using = "ProductFilter")]
        private IWebElement txt_productFilter;

        [FindsBy(How = How.XPath, Using = "//td[@id='ProductList_tccell0_7']/a")]
        private IWebElement lnk_qtyInHand;
        






        #endregion

        public void CreateProduct(Table table)
        {
            IEnumerable<dynamic> tabs = table.CreateDynamicSet();

            foreach(var tab in tabs)
            {

            lbl_inventory.Click();
            wait.Until(driver => lbl_products.Displayed);
            lbl_products.Click();
            wait.Until(driver => lbl_addProducts.Displayed);
            lbl_addProducts.Click();

            wait.Until(driver => txt_productCode.Displayed);
                string productCode = tab.ProductCode.ToString().Trim();
                string productDesc = tab.ProductDescription.ToString().Trim();
                txt_productCode.SendKeys(productCode);
                txt_productDescription.Click();
                Assert.IsFalse(new OtherUtils(driver).isElementPresent(alertText), "Error:while creating Product.Please choose a different Name");
                txt_productDescription.SendKeys(productDesc);
                btn_save.Click();
                Assert.IsTrue(new OtherUtils(driver).isElementPresent(productSuccessful), "Product Creation Success Message");

            }

   
        }

        public void ViewProduct(Table table)
        {
            IEnumerable<dynamic> tabs = table.CreateDynamicSet();

            lbl_viewProducts.Click();
            wait.Until(driver => txt_productFilter.Displayed);

            foreach (var tab in tabs)
            {
                string productCode = tab.ProductCode.ToString().Trim();
                txt_productFilter.SendKeys(productCode);
                txt_productFilter.SendKeys(Keys.Enter);
                IWebElement linkDisplayed = driver.FindElement(By.XPath("//a[text()='"+ productCode + "']"));
                Assert.IsTrue(new OtherUtils(driver).isElementPresent(linkDisplayed), "Product Displayed successfully");
            }
            }

        public string getStocksInHand(string productCode)
        {
            lbl_inventory.Click();
            wait.Until(driver => lbl_products.Displayed);
            lbl_products.Click();
            wait.Until(driver => lbl_viewProducts.Displayed);
            lbl_viewProducts.Click();
            wait.Until(driver => txt_productFilter.Displayed);

            txt_productFilter.SendKeys(productCode);
            txt_productFilter.SendKeys(Keys.Enter);
            Thread.Sleep(2000);
            wait.Until(driver => lnk_qtyInHand.Displayed);
            string productQty =  lnk_qtyInHand.Text;
            lbl_inventory.Click();
            Thread.Sleep(2000);
            return productQty;

        }
            
    }
}
