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
            switch (waitForElement)
            {
                case WaitForElement.None:
                    return webDriver.FindElement(by);
                case WaitForElement.Visible:
                    return webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementIsVisible(by));
                case WaitForElement.Clickable:
                    return webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementToBeClickable(by));
                case WaitForElement.Exists:
                    return webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementExists(by));
                default:
                    throw new ArgumentException($"WaitForElement {waitForElement} is not implemented");
            };
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebDriver webDriver, By by, WaitForElements waitForElements)
        {
            switch(waitForElements)
            {
                case WaitForElements.None:
                    return webDriver.FindElements(by);
                case WaitForElements.Visible:
                    return webDriver.WaitUntil(ExpectedConditionsSearchContext.VisibilityOfAllElementsLocatedBy(by));
                case WaitForElements.Exists:
                    return webDriver.WaitUntil(ExpectedConditionsSearchContext.PresenceOfAllElementsLocatedBy(by));
                default:
                    throw new ArgumentException($"WaitForElements {waitForElements} is not implemented");
            };
        }
    }
}
