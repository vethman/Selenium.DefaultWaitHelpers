using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;

namespace Selenium.DefaultWaitHelpers
{
    /// <summary>
    /// Provides extension methods for convenience in using WebDriver.
    /// </summary>
    public static class WebDriverExtensions
    {
        private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(30);
        private static readonly TimeSpan _defaultPollingInterval = TimeSpan.FromMilliseconds(50);

        /// <summary>
        /// Repeatedly applies this instance's input value to the given function until one of the following
        /// occurs:
        /// <para>
        /// <list type="bullet">
        /// <item>the function returns neither null nor false</item>
        /// <item>the function throws an exception that is not in the list of ignored exception types</item>
        /// <item>the timeout expires (Default = 30 seconds with pollinginterval of 50 milliseconds)</item>
        /// </list>
        /// </para>
        /// </summary>
        /// <typeparam name="T">The delegate's expected return type.</typeparam>
        /// <param name="webDriver">The webDriver instance to extend.</param>
        /// <param name="condition">A delegate taking an object of type T as its parameter, and returning a TResult.</param>
        /// <param name="ignoreExceptionTypes">The types of exceptions to ignore.</param>
        /// <returns>The delegate's return value.</returns>
        public static T WaitUntil<T>(this IWebDriver webDriver, Func<IWebDriver, T> condition, params Type[] ignoreExceptionTypes)
        {
            return webDriver.WaitUntil(condition, _defaultTimeout, ignoreExceptionTypes);
        }

        /// <summary>
        /// Repeatedly applies this instance's input value to the given function until one of the following
        /// occurs:
        /// <para>
        /// <list type="bullet">
        /// <item>the function returns neither null nor false</item>
        /// <item>the function throws an exception that is not in the list of ignored exception types</item>
        /// <item>the timeout expires (Default pollinginterval of 50 milliseconds)</item>
        /// </list>
        /// </para>
        /// </summary>
        /// <typeparam name="T">The delegate's expected return type.</typeparam>
        /// <param name="webDriver">The webDriver instance to extend.</param>
        /// <param name="condition">A delegate taking an object of type T as its parameter, and returning a TResult.</param>
        /// <param name="timeout">The timeout value indicating how long to wait for the condition.</param>
        /// <param name="ignoreExceptionTypes">The types of exceptions to ignore.</param>
        /// <returns>The delegate's return value.</returns>
        public static T WaitUntil<T>(this IWebDriver webDriver, Func<IWebDriver, T> condition, TimeSpan timeout, params Type[] ignoreExceptionTypes)
        {
            return webDriver.WaitUntil(condition, timeout, _defaultPollingInterval, ignoreExceptionTypes);
        }

        /// <summary>
        /// Repeatedly applies this instance's input value to the given function until one of the following
        /// occurs:
        /// <para>
        /// <list type="bullet">
        /// <item>the function returns neither null nor false</item>
        /// <item>the function throws an exception that is not in the list of ignored exception types</item>
        /// <item>the timeout expires</item>
        /// </list>
        /// </para>
        /// </summary>
        /// <typeparam name="T">The delegate's expected return type.</typeparam>
        /// <param name="webDriver">The webDriver instance to extend.</param>
        /// <param name="condition">A delegate taking an object of type T as its parameter, and returning a TResult.</param>
        /// <param name="timeout">The timeout value indicating how long to wait for the condition.</param>
        /// <param name="pollingInterval">A System.TimeSpan value indicating how often to check for the condition to be true.</param>
        /// <param name="ignoreExceptionTypes">The types of exceptions to ignore.</param>
        /// <returns>The delegate's return value.</returns>
        public static T WaitUntil<T>(this IWebDriver webDriver, Func<IWebDriver, T> condition, TimeSpan timeout, TimeSpan pollingInterval, params Type[] ignoreExceptionTypes)
        {
            var wait = new WebDriverWait(new SystemClock(), webDriver, timeout, pollingInterval);
            wait.IgnoreExceptionTypes(ignoreExceptionTypes);
            return wait.Until(condition);
        }

        /// <summary>
        /// Finds the first OpenQA.Selenium.IWebElement using the given method.
        /// </summary>
        /// <param name="webDriver">The webDriver instance to extend.</param>
        /// <param name="by">The locating mechanism to use.</param>
        /// <param name="waitForElement">Automatically perform an ExpectedConditionsSearchContext and return element.</param>
        /// <returns>The first matching OpenQA.Selenium.IWebElement on the current context.</returns>
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

        /// <summary>
        /// Finds all IWebElements within the current context using the given mechanism.
        /// </summary>
        /// <param name="webDriver">The webDriver instance to extend.</param>
        /// <param name="by">The locating mechanism to use.</param>
        /// <param name="waitForElements">Automatically perform an ExpectedConditionsSearchContext and return elements.</param>
        /// <returns>A System.Collections.ObjectModel.ReadOnlyCollection`1 of all WebElements matching 
        /// the current criteria, or an empty list if nothing matches.</returns>
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
