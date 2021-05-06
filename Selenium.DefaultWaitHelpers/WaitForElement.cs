namespace Selenium.DefaultWaitHelpers
{
    /// <summary>
    /// Automatically perform an ExpectedConditionsSearchContext and return element.
    /// </summary>
    public enum WaitForElement
    {
        /// <summary>
        /// ExpectedConditionsSearchContext ElementIsVisible
        /// </summary>
        Visible,
        /// <summary>
        /// ExpectedConditionsSearchContext ElementToBeClickable
        /// </summary>
        Clickable,
        /// <summary>
        /// ExpectedConditionsSearchContext ElementExists
        /// </summary>
        Exists,
    }
}
