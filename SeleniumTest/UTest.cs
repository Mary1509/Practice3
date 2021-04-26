using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTest
{
    class UTest
    {
        IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://www.google.com/";
        }

        [Test]
        public void TestAgenda()
        {
            IWebElement searchLine = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/form/div[1]/div[1]/div[1]/div/div[2]/input"));
            searchLine.SendKeys("Розклад КПІ");
            searchLine.Submit();
            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));
            IWebElement firstRes = driver.FindElements(By.ClassName("yuRUbf")).First();
            firstRes.Click();
            IWebElement groupSearchLine = driver.FindElement(By.XPath("//*[@id=\"ctl00_MainContent_ctl00_txtboxGroup\"]"));
            groupSearchLine.SendKeys("КП-92");
            groupSearchLine.SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));
            IWebElement table = driver.FindElement(By.XPath("//*[@id=\"ctl00_MainContent_FirstScheduleTable\"]"));
            IWebElement cell = table.FindElements(By.TagName("tr"))[1].FindElements(By.TagName("td"))[3];
            IWebElement subject = cell.FindElements(By.ClassName("plainLink")).First();
            Console.WriteLine("\nПредмет: " + subject.Text);
            Assert.IsTrue(subject.Text.Contains("Якість та тестування програмного забезпечення"));
            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));

        }

        [Test]
        public void TestEpi()
        {
            IWebElement searchLine = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/form/div[1]/div[1]/div[1]/div/div[2]/input"));
            searchLine.SendKeys("Епіцентр");
            searchLine.Submit();
            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));
            IWebElement firstRes = driver.FindElements(By.ClassName("yuRUbf")).First();
            firstRes.Click();
            IWebElement optionsList = driver.FindElement(By.ClassName("header__info-toggle-text"));
            IWebElement contactsUrl = driver.FindElement(By.XPath("/html/body/div[2]/header/div/div[8]/div/div[2]/div[2]/a[2]"));
            optionsList.Click();
            contactsUrl.Click();
            //System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));
            IWebElement time = driver.FindElement(By.ClassName("company__content")).FindElement(By.TagName("h3"));
            Console.WriteLine("Розклад роботи: " + time.Text);
            Assert.IsTrue(time.Text.Contains("7:30") && time.Text.Contains("22:30"));
            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));

        }

        [Test]
        public void TestAir()
        {
            IWebElement searchLine = driver.FindElement(By.XPath("/html/body/div[1]/div[3]/form/div[1]/div[1]/div[1]/div/div[2]/input"));
            searchLine.SendKeys("Accuweather");
            searchLine.Submit();
            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));
            IWebElement firstRes = driver.FindElements(By.ClassName("yuRUbf")).First();
            firstRes.Click();
            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));
            IWebElement airTab = driver.FindElement(By.XPath("/html/body/div/div[3]/div/div[3]/a[6]"));
            airTab.Click();
            IWebElement airQuality = driver.FindElement(By.ClassName("aq-number"));
            Console.WriteLine("Поточнеа якість повітря: " + airQuality.Text);
            Assert.IsTrue(Convert.ToInt32(airQuality.Text) < 30);
            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));

        }



        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }

    }
}
