using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace Selenium.DefaultWaitHelpers
{
    public static class WebDriverExtensions
    {
        private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan _defaultPollingInterval = TimeSpan.FromMilliseconds(50);

        public static T WaitUntil<T>(this IWebDriver webDriver, Func<IWebDriver, T> condition, params Type[] ignoreExceptionTypes)
        {
            return webDriver.WaitUntil(condition, _defaultTimeout, ignoreExceptionTypes);
        }

        public static T WaitUntil<T>(this IWebDriver webDriver, Func<IWebDriver, T> condition, TimeSpan timeout, params Type[] ignoreExceptionTypes)
        {
            return webDriver.WaitUntil(condition, timeout, _defaultPollingInterval, ignoreExceptionTypes);
        }

        public static T WaitUntil<T>(this IWebDriver webDriver, Func<IWebDriver, T> condition, TimeSpan timeout, TimeSpan pollingInterval, params Type[] ignoreExceptionTypes)
        {
            var wait = new WebDriverWait(new SystemClock(), webDriver, timeout, pollingInterval);
            wait.IgnoreExceptionTypes(ignoreExceptionTypes);
            return wait.Until(condition);
        }

        public static IWebElement FindElement(this IWebDriver webDriver, By by, WaitForElement waitForElement)
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
