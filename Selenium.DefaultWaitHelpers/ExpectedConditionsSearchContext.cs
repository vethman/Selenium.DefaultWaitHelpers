using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
    public sealed class ExpectedConditionsSearchContext
    {
        private static readonly Regex ClassNameRegex = new Regex("[_a-zA-Z0-9-]+", RegexOptions.Compiled);

        /// <summary>
        /// Prevents a default instance of the <see cref="ExpectedConditionsSearchContext"/> class from being created.
        /// </summary>
        private ExpectedConditionsSearchContext()
        {
        }

        /// <summary>
        /// An expectation for checking that an element is present on the DOM of a
        /// page. This does not necessarily mean that the element is visible.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The <see cref="IWebElement"/> once it is located.</returns>
        public static Func<ISearchContext, IWebElement> ElementExists(By locator)
        {
            return (searchContext) => { return searchContext.FindElement(locator); };
        }

        /// <summary>
        /// An expectation for checking that an element is present on the DOM of a page
        /// and visible. Visibility means that the element is not only displayed but
        /// also has a height and width that is greater than 0.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The <see cref="IWebElement"/> once it is located and visible.</returns>
        public static Func<ISearchContext, IWebElement> ElementIsVisible(By locator)
        {
            return (searchContext) =>
            {
                try
                {
                    return ElementIfVisible(searchContext.FindElement(locator));
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// An expectation for checking that all elements present on the web page that
        /// match the locator are visible. Visibility means that the elements are not
        /// only displayed but also have a height and width that is greater than 0.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The list of <see cref="IWebElement"/> once it is located and visible.</returns>
        public static Func<ISearchContext, ReadOnlyCollection<IWebElement>> VisibilityOfAllElementsLocatedBy(By locator)
        {
            return (searchContext) =>
            {
                try
                {
                    var elements = searchContext.FindElements(locator);
                    if (elements.Any(element => !element.Displayed))
                    {
                        return null;
                    }

                    return elements.Any() ? elements : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// An expectation for checking that all elements present on the web page that
        /// match the locator are visible. Visibility means that the elements are not
        /// only displayed but also have a height and width that is greater than 0.
        /// </summary>
        /// <param name="elements">list of WebElements</param>
        /// <returns>The list of <see cref="IWebElement"/> once it is located and visible.</returns>
        public static Func<ISearchContext, ReadOnlyCollection<IWebElement>> VisibilityOfAllElementsLocatedBy(ReadOnlyCollection<IWebElement> elements)
        {
            return (searchContext) =>
            {
                try
                {
                    if (elements.Any(element => !element.Displayed))
                    {
                        return null;
                    }

                    return elements.Any() ? elements : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// An expectation for checking that all elements present on the web page that
        /// match the locator.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The list of <see cref="IWebElement"/> once it is located.</returns>
        public static Func<ISearchContext, ReadOnlyCollection<IWebElement>> PresenceOfAllElementsLocatedBy(By locator)
        {
            return (searchContext) =>
            {
                try
                {
                    var elements = searchContext.FindElements(locator);
                    return elements.Any() ? elements : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// An expectation for checking if the given text is present in the specified element.
        /// </summary>
        /// <param name="element">The WebElement</param>
        /// <param name="text">Text to be present in the element</param>
        /// <returns><see langword="true"/> once the element contains the given text; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> TextToBePresentInElement(IWebElement element, string text)
        {
            return (searchContext) =>
            {
                try
                {
                    var elementText = element.Text;
                    return elementText.Contains(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// An expectation for checking if the given text is present in the element that matches the given locator.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="text">Text to be present in the element</param>
        /// <returns><see langword="true"/> once the element contains the given text; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> TextToBePresentInElementLocated(By locator, string text)
        {
            return (searchContext) =>
            {
                try
                {
                    var element = searchContext.FindElement(locator);
                    var elementText = element.Text;
                    return elementText.Contains(text);
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// An expectation for checking if the given text is present in the specified elements value attribute.
        /// </summary>
        /// <param name="element">The WebElement</param>
        /// <param name="text">Text to be present in the element</param>
        /// <returns><see langword="true"/> once the element contains the given text; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> TextToBePresentInElementValue(IWebElement element, string text)
        {
            return (searchContext) =>
            {
                try
                {
                    var elementValue = element.GetAttribute("value");
                    if (elementValue != null)
                    {
                        return elementValue.Contains(text);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// An expectation for checking if the given text is present in the specified elements value attribute.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="text">Text to be present in the element</param>
        /// <returns><see langword="true"/> once the element contains the given text; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> TextToBePresentInElementValue(By locator, string text)
        {
            return (searchContext) =>
            {
                try
                {
                    var element = searchContext.FindElement(locator);
                    var elementValue = element.GetAttribute("value");
                    if (elementValue != null)
                    {
                        return elementValue.Contains(text);
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// An expectation for checking that an element is either invisible or not present on the DOM.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns><see langword="true"/> if the element is not displayed; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> InvisibilityOfElementLocated(By locator)
        {
            return (searchContext) =>
            {
                try
                {
                    var element = searchContext.FindElement(locator);
                    return !element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    // Returns true because the element is not present in DOM. The
                    // try block checks if the element is present but is invisible.
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    // Returns true because stale element reference implies that element
                    // is no longer visible.
                    return true;
                }
            };
        }

        /// <summary>
        /// An expectation for checking that an element with text is either invisible or not present on the DOM.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="text">Text of the element</param>
        /// <returns><see langword="true"/> if the element is not displayed; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> InvisibilityOfElementWithText(By locator, string text)
        {
            return (searchContext) =>
            {
                try
                {
                    var element = searchContext.FindElement(locator);
                    var elementText = element.Text;
                    if (string.IsNullOrEmpty(elementText))
                    {
                        return true;
                    }

                    return !elementText.Equals(text);
                }
                catch (NoSuchElementException)
                {
                    // Returns true because the element with text is not present in DOM. The
                    // try block checks if the element is present but is invisible.
                    return true;
                }
                catch (StaleElementReferenceException)
                {
                    // Returns true because stale element reference implies that element
                    // is no longer visible.
                    return true;
                }
            };
        }

        /// <summary>
        /// An expectation for checking an element is visible and enabled such that you
        /// can click it.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns>The <see cref="IWebElement"/> once it is located and clickable (visible and enabled).</returns>
        public static Func<ISearchContext, IWebElement> ElementToBeClickable(By locator)
        {
            return (searchContext) =>
            {
                var element = ElementIfVisible(searchContext.FindElement(locator));
                try
                {
                    if (element != null && element.Enabled)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// An expectation for checking an element is visible and enabled such that you
        /// can click it.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The <see cref="IWebElement"/> once it is clickable (visible and enabled).</returns>
        public static Func<ISearchContext, IWebElement> ElementToBeClickable(IWebElement element)
        {
            return (searchContext) =>
            {
                try
                {
                    if (element != null && element.Displayed && element.Enabled)
                    {
                        return element;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// Wait until an element is no longer attached to the DOM.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><see langword="false"/> is the element is still attached to the DOM; otherwise, <see langword="true"/>.</returns>
        public static Func<ISearchContext, bool> StalenessOf(IWebElement element)
        {
            return (searchContext) =>
            {
                try
                {
                    // Calling any method forces a staleness check
                    return element == null || !element.Enabled;
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            };
        }

        /// <summary>
        /// An expectation for checking if the given element is selected.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns><see langword="true"/> given element is selected.; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> ElementToBeSelected(IWebElement element)
        {
            return ElementSelectionStateToBe(element, true);
        }

        /// <summary>
        /// An expectation for checking if the given element is in correct state.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="selected">selected or not selected</param>
        /// <returns><see langword="true"/> given element is in correct state.; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> ElementToBeSelected(IWebElement element, bool selected)
        {
            return (searchContext) =>
            {
                return element.Selected == selected;
            };
        }

        /// <summary>
        /// An expectation for checking if the given element is in correct state.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="selected">selected or not selected</param>
        /// <returns><see langword="true"/> given element is in correct state.; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> ElementSelectionStateToBe(IWebElement element, bool selected)
        {
            return (searchContext) =>
            {
                return element.Selected == selected;
            };
        }

        /// <summary>
        /// An expectation for checking if the given element is selected.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <returns><see langword="true"/> given element is selected.; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> ElementToBeSelected(By locator)
        {
            return ElementSelectionStateToBe(locator, true);
        }

        /// <summary>
        /// An expectation for checking if the given element is in correct state.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="selected">selected or not selected</param>
        /// <returns><see langword="true"/> given element is in correct state.; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, bool> ElementSelectionStateToBe(By locator, bool selected)
        {
            return (searchContext) =>
            {
                try
                {
                    var element = searchContext.FindElement(locator);
                    return element.Selected == selected;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            };
        }

        /// <summary>
        /// An expectation for checking if the given element contains expected class.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="className">expectation of class to be in element</param>
        /// <returns><see langword="true"/> given element is in correct state.; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, IWebElement> ElementContainsClass(By locator, string className)
        {
            return (searchContext) =>
            {
                var element = searchContext.FindElement(locator);
                return ClassNameRegex
                    .Matches(element.GetAttribute("class"))
                    .Cast<Match>()
                    .Any(x => x.Groups[0].Value == className) ? element : null;
            };
        }

        /// <summary>
        /// An expectation for checking if the given element contains expected class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="className">expectation of class to be in element</param>
        /// <returns><see langword="true"/> given element is in correct state.; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, IWebElement> ElementContainsClass(IWebElement element, string className)
        {
            return (searchContext) =>
            {
                try
                {
                    return ClassNameRegex
                        .Matches(element.GetAttribute("class"))
                        .Cast<Match>()
                        .Any(x => x.Groups[0].Value == className) ? element : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        /// <summary>
        /// An expectation for checking if the given element not contains expected class.
        /// </summary>
        /// <param name="locator">The locator used to find the element.</param>
        /// <param name="className">expectation of class not to be in element</param>
        /// <returns><see langword="true"/> given element is in correct state.; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, IWebElement> ElementNotContainsClass(By locator, string className)
        {
            return (searchContext) =>
            {
                var element = searchContext.FindElement(locator);
                return ClassNameRegex
                    .Matches(element.GetAttribute("class"))
                    .Cast<Match>()
                    .Any(x => x.Groups[0].Value != className) ? element : null;
            };
        }

        /// <summary>
        /// An expectation for checking if the given element not contains expected class.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="className">expectation of class not to be in element</param>
        /// <returns><see langword="true"/> given element is in correct state.; otherwise, <see langword="false"/>.</returns>
        public static Func<ISearchContext, IWebElement> ElementNotContainsClass(IWebElement element, string className)
        {
            return (searchContext) =>
            {
                try
                {
                    return ClassNameRegex
                        .Matches(element.GetAttribute("class"))
                        .Cast<Match>()
                        .Any(x => x.Groups[0].Value != className) ? element : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        private static IWebElement ElementIfVisible(IWebElement element)
        {
            return element.Displayed ? element : null;
        }
    }
}
