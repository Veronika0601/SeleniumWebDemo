using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Task3Tests
{
    public class Tests3
    {

        IWebDriver driver;
        [SetUp]

        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://practice.bpbonline.com/");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Test]
        public void Test1()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "manufacturers.txt" ;

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            SelectElement manufacturerDropDown = new SelectElement (driver.FindElement(By.XPath("//select[@name='manufacturers_id']")));

            IList<IWebElement> allManufacturersOptions = manufacturerDropDown.Options;

            List<string> manufacturersName = new List<string>();

            foreach (var element in allManufacturersOptions)
            {
                manufacturersName.Add(element.Text);
            }
            manufacturersName.RemoveAt(0);

            foreach (var itemName in manufacturersName)
            {
                manufacturerDropDown.SelectByText(itemName);

                manufacturerDropDown = new SelectElement(driver.FindElement(By.XPath("//select[@name='manufacturers_id']")));
                if (driver.PageSource.Contains("There are no products available in this category."))
                {
                    File.AppendAllText(path, $"The manufacturer{itemName} has no products!");
                }
                else
                {
                    IWebElement table = driver.FindElement(By.XPath("//table[@class='productListingData']"));
                    File.AppendAllText(path, $"\n\nThe manufacturer ${itemName} products are listed--\n");

                    IReadOnlyCollection<IWebElement> tableRows = table.FindElements(By.XPath("//tbody//tr"));
                    foreach (var rows in tableRows)
                    {
                        File.AppendAllText(path, rows.Text + "\n");
                    }

                }
            }
        }
    }
}