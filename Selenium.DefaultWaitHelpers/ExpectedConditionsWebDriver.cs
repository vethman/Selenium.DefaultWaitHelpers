using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;

namespace Selenium.DefaultWaitHelpers
{
    /// <summary>
    /// Supplies a set of common conditions that can be waited for using.
    /// </summary>
    /// <example>
    /// <code>
    /// IWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3))
    /// IWebElement element = wait.Until(ExpectedConditions.ElementExists(By.Id("foo")));
    /// </code>
    /// </example>
    public sealed class ExpectedConditionsWebDriver
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="ExpectedConditionsWebDriver"/> class from being created.
        /// </summary>
        private ExpectedConditionsWebDriver()
        {
        }

        /// <summary>
        /// An expectation for checking the title of a page.
        /// </summary>
        /// <param name="title">The expected title, which must be an exact match.</param>
        /// <returns><see langword="true"/> when the title matches; otherwise, <see langword="false"/>.</returns>
        public static Func<IWebDriver, bool> TitleIs(string title)
        {
            return (driver) => { return title == driver.Title; };
        }

        /// <summary>
        /// An expectation for checking that the title of a page contains a case-sensitive substring.
        /// </summary>
        /// <param name="title">The fragment of title expected.</param>
        /// <returns><see langword="true"/> when the title matches; otherwise, <see langword="false"/>.</returns>
        public static Func<IWebDriver, bool> TitleContains(string title)
        {
            return (driver) => { return driver.Title.Contains(title); };
        }

        /// <summary>
        /// An expectation for the URL of the current page to be a specific URL.
        /// </summary>
        /// <param name="url">The URL that the page should be on</param>
        /// <returns><see langword="true"/> when the URL is what it should be; otherwise, <see langword="false"/>.</returns>
        public static Func<IWebDriver, bool> UrlToBe(string url)
        {
            return (driver) => { return driver.Url.ToLowerInvariant().Equals(url.ToLowerInvariant()); };
        }

        /// <summary>
        /// An expectation for the URL of the current page to be a specific URL.
        /// </summary>
        /// <param name="fraction">The fraction of the url that the page should be on</param>
        /// <returns><see langword="true"/> when the URL contains the text; otherwise, <see langword="false"/>.</returns>
        public static Func<IWebDriver, bool> UrlContains(string fraction)
        {
            return (driver) => { return driver.Url.ToLowerInvariant().Contains(fraction.ToLowerInvariant()); };
        }

        /// <summary>
        /// An expectation for the URL of the current page to be a specific URL.
        /// </summary>
        /// <param name="regex">The regular expression that the URL should match</param>
        /// <returns><see langword="true"/> if the URL matches the specified regular expression; otherwise, <see langword="false"/>.</returns>
        public static Func<IWebDriver, bool> UrlMatches(string regex)
        {
            return (driver) =>
            {
                var currentUrl = driver.Url;
                var pattern = new Regex(regex, RegexOptions.IgnoreCase);
                var match = pattern.Match(currentUrl);
                return match.Success;
            };
        }

        /// <summary>
        /// An expectation for checking whether the given frame is available to switch
        /// to. If the frame is available it switches the given driver to the
        /// specified frame.
        /// </summary>
        /// <param name="frameLocator">Used to find the frame (id or name)</param>
        /// <returns><see cref="IWebDriver"/></returns>
        public static Func<IWebDriver, IWebDriver> FrameToBeAvailableAndSwitchToIt(string frameLocator)
        {
            return (driver) =>
            {
                try
                {
                    return driver.SwitchTo().Frame(frameLocator);
                }
                catch (NoSuchFrameException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// An expectation for checking whether the given frame is available to switch
        /// to. If the frame is available it switches the given driver to the
        /// specified frame.
        /// </summary>
        /// <param name="locator">Locator for the Frame</param>
        /// <returns><see cref="IWebDriver"/></returns>
        public static Func<IWebDriver, IWebDriver> FrameToBeAvailableAndSwitchToIt(By locator)
        {
            return (driver) =>
            {
                try
                {
                    var frameElement = driver.FindElement(locator);
                    return driver.SwitchTo().Frame(frameElement);
                }
                catch (NoSuchFrameException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// An expectation for checking the AlterIsPresent
        /// </summary>
        /// <returns>Alert </returns>
        public static Func<IWebDriver, IAlert> AlertIsPresent()
        {
            return (driver) =>
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// An expectation for checking the Alert State
        /// </summary>
        /// <param name="state">A value indicating whether or not an alert should be displayed in order to meet this condition.</param>
        /// <returns><see langword="true"/> alert is in correct state present or not present; otherwise, <see langword="false"/>.</returns>
        public static Func<IWebDriver, bool> AlertState(bool state)
        {
            return (driver) =>
            {
                var alertState = false;
                try
                {
                    driver.SwitchTo().Alert();
                    alertState = true;
                    return alertState == state;
                }
                catch (NoAlertPresentException)
                {
                    alertState = false;
                    return alertState == state;
                }
            };
        }
    }
}
