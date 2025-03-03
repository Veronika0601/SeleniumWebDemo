using OpenQA.Selenium.Chrome;

namespace Task3Test
{
    public class UnitTes2
    {
        IwebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

        }
        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}