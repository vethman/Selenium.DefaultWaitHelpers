using Microsoft.Edge.SeleniumTools;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.IO;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium.DefaultWaitHelpers.UiTests
{
    public class ExpectedConditionsSearchContextTests
    {
        private EdgeDriver _webDriver;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());

            var optionsEdge = new EdgeOptions()
            {
                UseChromium = true
            };

            _webDriver = new EdgeDriver(optionsEdge);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void ISearchContext_WebDriver_WaitForSpinnerCompleted()
        {
            OpenDemoHtml("spinner");

            _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementIsVisible(By.Id("loader")));
            _webDriver.WaitUntil(ExpectedConditionsSearchContext.InvisibilityOfElementLocated(By.Id("loader")));
        }

        [Test]
        public void ISearchContext_WebElement_WaitForSpinnerCompleted()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElementByTagName("body");

            _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementIsVisible(By.Id("loader")));
            _webDriver.WaitUntil(ExpectedConditionsSearchContext.InvisibilityOfElementLocated(By.Id("loader")));
        }

        private void OpenDemoHtml(string demoHtml)
        {
            _webDriver.Navigate().GoToUrl(new Uri(Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), $"DemoHtml/{demoHtml}.html")).AbsoluteUri);
        }
    }
}