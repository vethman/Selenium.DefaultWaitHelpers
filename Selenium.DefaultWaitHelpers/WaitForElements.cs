namespace Selenium.DefaultWaitHelpers
{
    /// <summary>
    /// Automatically perform an ExpectedConditionsSearchContext and return elements.
    /// </summary>
    public enum WaitForElements
    {
        /// <summary>
        /// No ExpectedConditionsSearchContext, use default FindElements
        /// </summary>
        None,
        /// <summary>
        /// ExpectedConditionsSearchContext VisibilityOfAllElementsLocatedBy
        /// </summary>
        Visible,
        /// <summary>
        /// ExpectedConditionsSearchContext PresenceOfAllElementsLocatedBy
        /// </summary>
        Exists,
    }
}
