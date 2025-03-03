using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Task2
{
    public class Task2Test
    {
        IWebDriver driver;
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
        public void Test1()
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/productInformation.csv";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

             IWebElement table = driver.FindElement(By.XPath("//div[@class='contentText']//table"));

            ReadOnlyCollection<IWebElement> tableRows = table.FindElements(By.XPath("//div[@class='contentText']//tbody//tr"));

            foreach (var row in tableRows)
            {
                ReadOnlyCollection<IWebElement> tableData = row.FindElements(By.TagName("td"));

                foreach (var currentTableRow in tableData)
                {
                    string data = currentTableRow.Text;

                    string[] productInfo = data.Split("\n");
                    string infotoFile = productInfo[0] + "," + productInfo[1].Trim() + "\n";

                    File.AppendAllText(path, infotoFile);
                }
            }
            Assert.That(File.Exists(path), Is.True, "Csv file was not create");
            Assert.That(new FileInfo(path).Length, Is.GreaterThan(0),"Csv file is empty");



        }
    }
}