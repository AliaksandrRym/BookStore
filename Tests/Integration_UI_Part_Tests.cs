namespace BookStore.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;

    [TestClass]
    public class Integration_UI_Part_Tests: IntegrationUIBaseTest
    {
        [Ignore]
        [TestMethod]
        public void Add_Booking_UI_Test()
        {
            _driver.FindElement(By.LinkText("Booking")).Click();
            var bookingCountBeforeAdding = _driver.FindElements(By.XPath("//div[@class = 'col-sm-4']")).Count();

            _driver.FindElement(By.LinkText("Catalogue")).Click();

           _driver.FindElements(By.XPath("//div[@class = 'panel panel-primary']"))
                .Last().FindElement(By.LinkText("Book")).Click();

            _driver.FindElement(By.Id("Delivery_Adress")).SendKeys("Test Adress");

            var deliveryDate = "92002023" + string.Empty +"33";
            _driver.FindElement(By.Id("Delivery_date")).SendKeys(deliveryDate);
            _driver.FindElement(By.Id("Delivery_Time")).SendKeys(deliveryDate);
            _driver.FindElement(By.XPath("//input[@class = 'btn btn-primary']")).Click();

            var bookingCountAfterAdding = _driver.FindElements(By.XPath("//div[@class = 'col-sm-4']")).Count;

            Assert.AreEqual(bookingCountBeforeAdding +1, bookingCountAfterAdding, "Booking was not created");

        }
    }
}
