using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace Selenium.DefaultWaitHelpers
{
    public static class WebElementExtensions
    {
        private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan _defaultPollingInterval = TimeSpan.FromMilliseconds(50);

        public static T WaitUntil<T>(this IWebElement webElement, Func<IWebElement, T> condition, params Type[] ignoreExceptionTypes)
        {
            return webElement.WaitUntil(condition, _defaultTimeout, ignoreExceptionTypes);
        }

        public static T WaitUntil<T>(this IWebElement webElement, Func<IWebElement, T> condition, TimeSpan timeout, params Type[] ignoreExceptionTypes)
        {
            return webElement.WaitUntil(condition, timeout, _defaultPollingInterval, ignoreExceptionTypes);
        }

        public static T WaitUntil<T>(this IWebElement webElement, Func<IWebElement, T> condition, TimeSpan timeout, TimeSpan pollingInterval, params Type[] ignoreExceptionTypes)
        {
            var wait = new DefaultWait<IWebElement>(webElement, new SystemClock())
            {
                Timeout = timeout,
                PollingInterval = pollingInterval
            };
            wait.IgnoreExceptionTypes(ignoreExceptionTypes);
            return wait.Until(condition);
        }

        public static IWebElement FindElement(this IWebElement webDriver, By by, WaitForElement waitForElement)
        {
            return waitForElement switch
            {
                WaitForElement.None => webDriver.FindElement(by),
                WaitForElement.Visible => webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementIsVisible(by)),
                WaitForElement.Clickable => webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementToBeClickable(by)),
                WaitForElement.Exists => webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementExists(by)),
                _ => throw new ArgumentException($"WaitForElement {waitForElement} is not implemented")
            };
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver webDriver, By by, WaitForElements waitForElements)
        {
            return waitForElements switch
            {
                WaitForElements.None => webDriver.FindElements(by),
                WaitForElements.Visible => webDriver.WaitUntil(ExpectedConditionsSearchContext.VisibilityOfAllElementsLocatedBy(by)),
                WaitForElements.Exists => webDriver.WaitUntil(ExpectedConditionsSearchContext.PresenceOfAllElementsLocatedBy(by)),
                _ => throw new ArgumentException($"WaitForElements {waitForElements} is not implemented")
            };
        }
    }
}
