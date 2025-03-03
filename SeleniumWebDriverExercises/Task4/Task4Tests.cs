using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task4
{
    public class Task4Tests
    {

        IWebDriver driver;
        IWebElement firstNumberElement;
        IWebElement dropDownOperation;
        IWebElement secondNumberElement;
        IWebElement calculateButton;
        IWebElement resetButton;
        IWebElement resultButton; 
        [OneTimeSetUp]
        public void Setup()
        {

            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:8080/number-calculator.html");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            firstNumberElement = driver.FindElement(By.CssSelector("[name=number1]"));
            dropDownOperation = driver.FindElement(By.CssSelector("[name=operation]"));
            secondNumberElement = driver.FindElement(By.CssSelector("[name=number2]"));
            calculateButton = driver.FindElement(By.XPath("//input[@id='calcButton']"));
            resetButton = driver.FindElement(By.XPath("//input[@id='resetButton']"));
            resultButton = driver.FindElement(By.XPath("//div[@id='result']"));
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }

        public void Calculate(string firstNumber,string operation, string secondNumber,string expectedResult)
        {
            resetButton.Click();
            if (!string.IsNullOrEmpty(firstNumber))
            {
                firstNumberElement.SendKeys(firstNumber);
            }
            if (!string.IsNullOrEmpty(secondNumber))
            {
                secondNumberElement.SendKeys(secondNumber);
            }
            if (!string.IsNullOrEmpty(operation))
            {
                new SelectElement(dropDownOperation).SelectByText(operation);
            }

            calculateButton.Click();

            Assert.That(resultButton.Text, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("5", "+ (sum)", "3", "Result: 8")]
        [TestCase("15", "+ (sum)", "13", "Result: 28")]
        [TestCase("15", "- (subtract)", "13", "Result: 2")]
        [TestCase("5", "* (multiply)", "3", "Result: 15")]
        [TestCase("15", "/ (divide)", "3", "Result: 5")]
        [TestCase("5", "+ (sum)", "", "Result: invalid input")]
        [TestCase("Infinity", "* (multiply)","Infinity", "Result: Infinity")]
        [TestCase("Infinity", "+ (sum)", "Infinity", "Result: Infinity")]
        [TestCase("fgfgfggf", "/ (divide)", "hghjghjg", "Result: invalid input")]
        [TestCase("ghjggh", "", "infinity", "Result: invalid input")]

        public void TestNumberCalculation(string firstNumber,string operation,string secondNumber,string expectedResult)
        {
            Calculate(firstNumber, operation, secondNumber, expectedResult);
        }
    }
}