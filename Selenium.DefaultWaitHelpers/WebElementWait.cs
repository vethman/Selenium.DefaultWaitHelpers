using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace Selenium.DefaultWaitHelpers
{
    /// <summary>
    /// Provides the ability to wait for an arbitrary condition during test execution.
    /// </summary>
    /// <example>
    /// <code>
    /// IWait wait = new WebElementWait(element, TimeSpan.FromSeconds(3))
    /// IWebElement element = wait.Until(element => element.FindElement(By.Name("q")));
    /// </code>
    /// </example>
    public class WebElementWait : DefaultWait<IWebElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebElementWait"/> class.
        /// </summary>
        /// <param name="element">The WebElement instance used to wait.</param>
        /// <param name="timeout">The timeout value indicating how long to wait for the condition.</param>
        public WebElementWait(IWebElement element, TimeSpan timeout)
            : this(new SystemClock(), element, timeout, DefaultSleepTimeout)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebElementWait"/> class.
        /// </summary>
        /// <param name="clock">An object implementing the <see cref="IClock"/> interface used to determine when time has passed.</param>
        /// <param name="element">The WebElement instance used to wait.</param>
        /// <param name="timeout">The timeout value indicating how long to wait for the condition.</param>
        /// <param name="sleepInterval">A <see cref="TimeSpan"/> value indicating how often to check for the condition to be true.</param>
        public WebElementWait(IClock clock, IWebElement element, TimeSpan timeout, TimeSpan sleepInterval)
            : base(element, clock)
        {
            Timeout = timeout;
            PollingInterval = sleepInterval;
            IgnoreExceptionTypes(typeof(NotFoundException));
        }

        private static TimeSpan DefaultSleepTimeout
        {
            get { return TimeSpan.FromMilliseconds(500); }
        }
    }
}
