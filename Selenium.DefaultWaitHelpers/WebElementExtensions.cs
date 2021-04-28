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
            var wait = new WebElementWait(new SystemClock(), webElement, timeout, pollingInterval);
            wait.IgnoreExceptionTypes(ignoreExceptionTypes);
            return wait.Until(condition);
        }

        public static IWebElement FindElement(this IWebElement webElement, By by, WaitForElement waitForElement)
        {
            switch (waitForElement)
            {
                case WaitForElement.None:
                    return webElement.FindElement(by);
                case WaitForElement.Visible:
                    return webElement.WaitUntil(ExpectedConditionsSearchContext.ElementIsVisible(by));
                case WaitForElement.Clickable:
                    return webElement.WaitUntil(ExpectedConditionsSearchContext.ElementToBeClickable(by));
                case WaitForElement.Exists:
                    return webElement.WaitUntil(ExpectedConditionsSearchContext.ElementExists(by));
                default:
                    throw new ArgumentException($"WaitForElement {waitForElement} is not implemented");
            };
        }

        public static ReadOnlyCollection<IWebElement> FindElements(this IWebElement webElement, By by, WaitForElements waitForElements)
        {
            switch (waitForElements)
            {
                case WaitForElements.None:
                    return webElement.FindElements(by);
                case WaitForElements.Visible:
                    return webElement.WaitUntil(ExpectedConditionsSearchContext.VisibilityOfAllElementsLocatedBy(by));
                case WaitForElements.Exists:
                    return webElement.WaitUntil(ExpectedConditionsSearchContext.PresenceOfAllElementsLocatedBy(by));
                default:
                    throw new ArgumentException($"WaitForElements {waitForElements} is not implemented");
            };
        }
    }
}
