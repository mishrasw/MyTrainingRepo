using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using UnleasedBDD.pages;

namespace UnleasedBDD.steps
{
    [Binding]
    class ProductSteps
    {

        private IWebDriver driver;
        private readonly ScenarioContext context;

        public ProductSteps(ScenarioContext context)
        {
            this.context = context;
            driver = context.Get<IWebDriver>("driver");
        }

        [Given(@"I have navigated to product screen and entered product details")]
        public void GivenIHaveNavigatedToProductScreenAndEnteredProductDetails(Table table)
        {
            new ProductPage(driver).CreateProduct(table);
        }

        [Then(@"I verify the product is created successfully")]
        public void ThenIVerifyTheProductIsCreatedSuccessfully(Table table)
        {           
            new ProductPage(driver).ViewProduct(table);
        }


    }
}
