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
        public void ISearchContext_WebDriver_ElementExists_Success()
        {
            OpenDemoHtml("button_delayed_toggle_div");

            _webDriver.FindElement(By.Id("button_createRemoveDiv")).Click();

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementExists(By.Id("createRemoveDiv"))))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebDriver_ElementExists_Failure()
        {
            OpenDemoHtml("button_delayed_toggle_div");

            _webDriver.FindElement(By.Id("button_createRemoveDiv")).Click();

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementExists(By.Id("createRemoveDiv")), TimeSpan.FromMilliseconds(500)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebElement_ElementExists_Success()
        {
            OpenDemoHtml("button_delayed_toggle_div");

            _webDriver.FindElement(By.Id("button_createRemoveDiv")).Click();

            var parentElement = _webDriver.FindElement(By.Id("parentCreateRemoveDiv"));
            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementExists(By.Id("createRemoveDiv"))))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebElement_ElementExists_Failure()
        {
            OpenDemoHtml("button_delayed_toggle_div");

            _webDriver.FindElement(By.Id("button_createRemoveDiv")).Click();

            var parentElement = _webDriver.FindElement(By.Id("parentCreateRemoveDiv"));
            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementExists(By.Id("createRemoveDiv")), TimeSpan.FromMilliseconds(500)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebDriver_ElementIsVisible_InvisibilityOfElementLocated_Success()
        {
            OpenDemoHtml("spinner");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementIsVisible(By.Id("loader"))))
                .Should().NotThrow();

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.InvisibilityOfElementLocated(By.Id("loader"))))
                .Should().NotThrow();
        }


        [Test]
        public void ISearchContext_WebElement_ElementIsVisible_InvisibilityOfElementLocated_Success()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElementByTagName("body");

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementIsVisible(By.Id("loader"))))
                .Should().NotThrow();

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.InvisibilityOfElementLocated(By.Id("loader"))))
                .Should().NotThrow();
        }


        [Test]
        public void ISearchContext_WebDriver_ElementIsVisible_Failure()
        {
            OpenDemoHtml("spinner");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementIsVisible(By.Id("faulty")), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebElement_ElementIsVisible_Failure()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElementByTagName("body");

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementIsVisible(By.Id("faulty")), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebDriver_InvisibilityOfElementLocated_Failure()
        {
            OpenDemoHtml("spinner");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.InvisibilityOfElementLocated(By.Id("loader")), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebElement_InvisibilityOfElementLocated_Failure()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElementByTagName("body");

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.InvisibilityOfElementLocated(By.Id("loader")), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebDriver_StalenessOf_Success()
        {
            OpenDemoHtml("button_delayed_toggle_div");

            var element = _webDriver.FindElement(By.Id("removeCreateDiv"));

            _webDriver.FindElement(By.Id("button_removeCreateDiv")).Click();

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.StalenessOf(element)))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebElement_StalenessOf_Success()
        {
            OpenDemoHtml("button_delayed_toggle_div");

            var parentElement = _webDriver.FindElement(By.Id("parentRemoveCreateDiv"));

            var element = parentElement.FindElement(By.Id("removeCreateDiv"));

            parentElement.FindElement(By.Id("button_removeCreateDiv")).Click();

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.StalenessOf(element)))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebDriver_StalenessOf_Failure()
        {
            OpenDemoHtml("button_delayed_toggle_div");

            var element = _webDriver.FindElement(By.Id("removeCreateDiv"));

            _webDriver.FindElement(By.Id("button_removeCreateDiv")).Click();

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.StalenessOf(element), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebElement_StalenessOf_Failure()
        {
            OpenDemoHtml("button_delayed_toggle_div");

            var parentElement = _webDriver.FindElement(By.Id("parentRemoveCreateDiv"));

            var element = parentElement.FindElement(By.Id("removeCreateDiv"));

            parentElement.FindElement(By.Id("button_removeCreateDiv")).Click();

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.StalenessOf(element), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebDriver_ElementContainsClass_ByLocator_Success()
        {
            OpenDemoHtml("spinner");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementContainsClass(By.Id("classchanger"), "loadedclass")))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebElement_ElementContainsClass_ByLocator_Success()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElement(By.TagName("body"));

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementContainsClass(By.Id("classchanger"), "loadedclass")))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebDriver_ElementContainsClass_ByLocator_Failure()
        {
            OpenDemoHtml("spinner");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementContainsClass(By.Id("classchanger"), "loadedclass"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebElement_ElementContainsClass_ByLocator_Failure()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElement(By.TagName("body"));

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementContainsClass(By.Id("classchanger"), "loadedclass"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebDriver_ElementContainsClass_Element_Success()
        {
            OpenDemoHtml("spinner");

            var element = _webDriver.FindElement(By.Id("classchanger"));

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementContainsClass(element, "loadedclass")))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebElement_ElementContainsClass_Element_Success()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElement(By.TagName("body"));
            var element = _webDriver.FindElement(By.Id("classchanger"));

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementContainsClass(element, "loadedclass")))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebDriver_ElementContainsClass_Element_Failure()
        {
            OpenDemoHtml("spinner");

            var element = _webDriver.FindElement(By.Id("classchanger"));

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementContainsClass(element, "loadedclass"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebElement_ElementContainsClass_Element_Failure()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElement(By.TagName("body"));
            var element = _webDriver.FindElement(By.Id("classchanger"));

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementContainsClass(element, "loadedclass"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebDriver_ElementNotContainsClass_ByLocator_Success()
        {
            OpenDemoHtml("spinner");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementNotContainsClass(By.Id("classchanger"), "loadingclass")))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebElement_ElementNotContainsClass_ByLocator_Success()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElement(By.TagName("body"));

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementNotContainsClass(By.Id("classchanger"), "loadingclass")))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebDriver_ElementNotContainsClass_ByLocator_Failure()
        {
            OpenDemoHtml("spinner");

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementNotContainsClass(By.Id("classchanger"), "loadingclass"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebElement_ElementNotContainsClass_ByLocator_Failure()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElement(By.TagName("body"));

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementNotContainsClass(By.Id("classchanger"), "loadingclass"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebDriver_ElementNotContainsClass_Element_Success()
        {
            OpenDemoHtml("spinner");

            var element = _webDriver.FindElement(By.Id("classchanger"));

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementNotContainsClass(element, "loadingclass")))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebElement_ElementNotContainsClass_Element_Success()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElement(By.TagName("body"));
            var element = _webDriver.FindElement(By.Id("classchanger"));

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementNotContainsClass(element, "loadingclass")))
                .Should().NotThrow();
        }

        [Test]
        public void ISearchContext_WebDriver_ElementNotContainsClass_Element_Failure()
        {
            OpenDemoHtml("spinner");

            var element = _webDriver.FindElement(By.Id("classchanger"));

            FluentActions.Invoking(() =>
                _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementNotContainsClass(element, "loadingclass"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        [Test]
        public void ISearchContext_WebElement_ElementNotContainsClass_Element_Failure()
        {
            OpenDemoHtml("spinner");

            var parentElement = _webDriver.FindElement(By.TagName("body"));
            var element = _webDriver.FindElement(By.Id("classchanger"));

            FluentActions.Invoking(() =>
                parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementNotContainsClass(element, "loadingclass"), TimeSpan.FromMilliseconds(100)))
                .Should().Throw<WebDriverTimeoutException>();
        }

        private void OpenDemoHtml(string demoHtml)
        {
            _webDriver.Navigate().GoToUrl(new Uri(Path.Combine(Path.GetDirectoryName(GetType().Assembly.Location), $"DemoHtml/{demoHtml}.html")).AbsoluteUri);
        }
    }
}