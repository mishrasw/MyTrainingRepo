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
    class SalesOrder
    {

        private IWebDriver driver;
        private readonly ScenarioContext context;

        public SalesOrder(ScenarioContext context)
        {
            this.context = context;
            driver = context.Get<IWebDriver>("driver");
        }

  
        [Given(@"I have created a new sales order")]
        public void GivenIHaveCreatedANewSalesOrder(Table table)
        {
            new SalesOrderPage(driver).CreateSalesOrder(table);
        }




    }
}
