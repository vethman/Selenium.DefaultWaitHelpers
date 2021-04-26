using FluentAssertions;
using Microsoft.Edge.SeleniumTools;
using NUnit.Framework;
using OpenQA.Selenium;
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
        public void WebDriver_TitleIs_Success()
        {
            _webDriver.Navigate().GoToUrl("https://github.com/vethman");
            
            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.TitleIs("vethman (Ronald Veth) · GitHub")))
                .Should().NotThrow();
        }

        [Test]
        public void WebDriver_TitleIs_Failure()
        {
            _webDriver.Navigate().GoToUrl("https://github.com/vethman");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.TitleIs("FaultyTitle"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void WebDriver_TitleContains_Success()
        {
            _webDriver.Navigate().GoToUrl("https://github.com/vethman");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.TitleContains("vethman (Ronald Veth)")))
                .Should().NotThrow();
        }

        [Test]
        public void WebDriver_TitleContains_Failure()
        {
            _webDriver.Navigate().GoToUrl("https://github.com/vethman");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.TitleContains("vethman (Ronald faulty)"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void WebDriver_UrlToBe_Success()
        {
            _webDriver.Navigate().GoToUrl("https://github.com/vethman");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.UrlToBe("https://github.com/vethman")))
                .Should().NotThrow();
        }

        [Test]
        public void WebDriver_UrlToBe_Failure()
        {
            _webDriver.Navigate().GoToUrl("https://github.com/vethman");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.UrlToBe("https://github.com/faulty"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void WebDriver_UrlMatches_Success()
        {
            _webDriver.Navigate().GoToUrl("https://github.com/vethman");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.UrlMatches(@"\/vethman")))
                .Should().NotThrow();
        }

        [Test]
        public void WebDriver_UrlMatches_Failure()
        {
            _webDriver.Navigate().GoToUrl("https://github.com/vethman");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.UrlMatches(@"\/faulty"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void WebDriver_FrameToBeAvailableAndSwitchToIt_FrameLocator_Success()
        {
            OpenDemoHtml("iframe");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.FrameToBeAvailableAndSwitchToIt("Iframe_example")))
                .Should().NotThrow();
        }

        [Test]
        public void WebDriver_FrameToBeAvailableAndSwitchToIt_FrameLocator_Failure()
        {
            OpenDemoHtml("iframe");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.FrameToBeAvailableAndSwitchToIt("Iframe_faulty"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void WebDriver_FrameToBeAvailableAndSwitchToIt_ByLocator_Success()
        {
            OpenDemoHtml("iframe");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.FrameToBeAvailableAndSwitchToIt(By.TagName("iframe"))))
                .Should().NotThrow();
        }

        [Test]
        public void WebDriver_FrameToBeAvailableAndSwitchToIt_ByLocator_Failure()
        {
            OpenDemoHtml("iframe");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.FrameToBeAvailableAndSwitchToIt(By.TagName("iframefaulty")), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void WebDriver_AlertIsPresent_Success()
        {
            OpenDemoHtml("alert");

            _webDriver.FindElement(By.TagName("button")).Click();

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.AlertIsPresent()))
                .Should().NotThrow();

            _webDriver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void WebDriver_AlertIsPresent_Failure()
        {
            OpenDemoHtml("alert");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.AlertIsPresent(), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void WebDriver_AlertState_true_Success()
        {
            OpenDemoHtml("alert");

            _webDriver.FindElement(By.TagName("button")).Click();

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.AlertState(true)))
                .Should().NotThrow();

            _webDriver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void WebDriver_AlertState_true_Failure()
        {
            OpenDemoHtml("alert");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.AlertState(true), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void WebDriver_AlertState_false_Success()
        {
            OpenDemoHtml("alert");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.AlertState(false)))
                .Should().NotThrow();
        }

        [Test]
        public void WebDriver_AlertState_false_Failure()
        {
            OpenDemoHtml("alert");

            _webDriver.FindElement(By.TagName("button")).Click();

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsWebDriver.AlertState(false), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();

            _webDriver.SwitchTo().Alert().Accept();
        }

        private void OpenDemoHtml(string demoHtml)
        {
            _webDriver.Navigate().GoToUrl(new Uri(Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), $"DemoHtml/{demoHtml}.html")).AbsoluteUri);
        }
    }
}