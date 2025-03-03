using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWeb
{
    public class Tests
    {

        public IWebDriver driver;
        [SetUp]

        public void SetUp()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();

        }

        [Test]
        public void TaskTest1()
        {
            driver.FindElement(By.XPath("//span[text()='My Account']")).Click();
            driver.FindElement(By.XPath("//span[text()='Continue']")).Click();

            var gender = driver.FindElement(By.CssSelector("[value=f]"));

            gender.Click();


            var firstName = driver.FindElement(By.Name("firstname"));

            firstName.SendKeys("Violeta");

            var lastName = driver.FindElement(By.Name("lastname"));

            lastName.SendKeys("Ivanova");

            driver.FindElement(By.XPath("//input[@id='dob']")).SendKeys("07/10/1985"+ Keys.Enter);

            Random random = new Random();
            int ranNumber = random.Next(1000, 9999);

            string email = "User_email" + ranNumber.ToString() + "@gmail.com";

            driver.FindElement(By.Name("email_address")).SendKeys(email);

            driver.FindElement(By.CssSelector("[name=company]")).SendKeys("NewBig Company LTD");

            driver.FindElement(By.XPath("//input[@name='street_address']")).SendKeys("25 New Street");

            driver.FindElement(By.Name("suburb")).SendKeys("Sofia suburb");

            driver.FindElement(By.XPath("//input[@name='postcode']")).SendKeys("1500");
            driver.FindElement(By.Name("city")).SendKeys("Sofia city");

            driver.FindElement(By.Name("state")).SendKeys("Sofia");

            new SelectElement(driver.FindElement(By.Name("country"))).SelectByValue("33");

            driver.FindElement(By.XPath("//input[@name='telephone']")).SendKeys("0888520369");

            driver.FindElement(By.XPath("//input[@type='checkbox']")).Click();

            driver.FindElement(By.Name("password")).SendKeys("12345");

            driver.FindElement(By.Name("confirmation")).SendKeys("12345");


            driver.FindElement(By.XPath("//span[text()='Continue']")).Click();

            var message = driver.FindElement(By.TagName("h1"));

            Assert.That(message.Text, Is.EqualTo("Your Account Has Been Created!"));


            driver.FindElement(By.XPath("//span[text()='Log Off']")).Click();

            driver.FindElement(By.XPath("//span[text()='Continue']")).Click();
            Console.WriteLine("User accoun created with email: " + email);



           
        }
    }
}