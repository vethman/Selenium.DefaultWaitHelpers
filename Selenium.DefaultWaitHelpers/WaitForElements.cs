namespace Selenium.DefaultWaitHelpers
{
    /// <summary>
    /// Automatically perform an ExpectedConditionsSearchContext and return elements.
    /// </summary>
    public enum WaitForElements
    {
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
