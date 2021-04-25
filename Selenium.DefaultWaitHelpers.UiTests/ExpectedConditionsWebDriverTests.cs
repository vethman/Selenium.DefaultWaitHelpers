using Microsoft.Edge.SeleniumTools;
using NUnit.Framework;
using System;
using System.IO;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Selenium.DefaultWaitHelpers.UiTests
{
    public class ExpectedConditionsWebDriverTests
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
        public void WebDriver_TitleIs()
        {
            _webDriver.Navigate().GoToUrl("https://github.com/vethman");
            _webDriver.WaitUntil(ExpectedConditionsWebDriver.TitleIs("vethman (Ronald Veth) · GitHub"));
        }

        private void OpenDemoHtml(string demoHtml)
        {
            _webDriver.Navigate().GoToUrl(new Uri(Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), $"DemoHtml/{demoHtml}.html")).AbsoluteUri);
        }
    }
}