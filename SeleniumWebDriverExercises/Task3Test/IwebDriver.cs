using OpenQA.Selenium.Chrome;

namespace Task3Test
{
    internal class IwebDriver
    {
        public static implicit operator IwebDriver(ChromeDriver v)
        {
            throw new NotImplementedException();
        }
    }
}