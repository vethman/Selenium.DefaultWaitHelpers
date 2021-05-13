# Selenium.DefaultWaitHelpers
When using selenium it can be that you have to write a lot of waits. This package will help you to make things easier.

## Original ExpectedConditions split into ExpectedConditionsSearchContext and ExpectedConditionsWebDriver
Based on [DotNetSeleniumExtras](https://github.com/DotNetSeleniumTools/DotNetSeleniumExtras/blob/master/src/WaitHelpers/ExpectedConditions.cs)
- ExpectedConditionsSearchContext contains the conditions that can be applied to both WebElement and WebDriver context
- ExpectedConditionsWebDriver contains the conditions that can only be applied to the WebDriver context

## WebDriver Extension WaitUntil
Normally you have to do the following:
```csharp
var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));
var element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("testId")));
```
Now you can do:
```csharp
var element = _webDriver.WaitUntil(ExpectedConditionsSearchContext.ElementIsVisible(By.Id("testId")));
```
Notice that the ExpectedConditions changed to ExpectedConditionsSearchContext more on that later...

## WebElementWait + WebElement Extension WaitUntil
Selenium provides the abillity to wait in the context of the WebDriver by WebDriverWait. There is no such thing to wait in de WebElement context. Maybe you want to have your focus on a specific part of the page in your PageObject. You can do this by using the DefaultWait:
```csharp
var parentElement = _webDriver.FindElement(By.Id("testId"));
var wait = new DefaultWait<IWebElement>(parentElement, new SystemClock());
wait.Timeout = TimeSpan.FromSeconds(30);
var element = wait.Until(ExpectedConditions.ElementExists(By.Id("waitForThisElement")));
```
Now you can do:
```csharp
var parentElement = _webDriver.FindElement(By.Id("testId"));
var wait = new WebElementWait(parentElement, TimeSpan.FromSeconds(30));
var element = wait.Until(ExpectedConditionsSearchContext.ElementExists(By.Id("waitForThisElement")));
```
And with the extra extension on WebElement you can even do:
```csharp
var parentElement = _webDriver.FindElement(By.Id("testId"));
var element = parentElement.WaitUntil(ExpectedConditionsSearchContext.ElementExists(By.Id("waitForThisElement")));
```

## WaitForElement(s)
Extension overload on FindElement and FindElements. You can direct pick your choice on what the state of the element you are looking for needs to be:
```csharp
_webDriver.FindElement(By.Id("testId"), WaitForElement.Visible);
_webDriver.FindElement(By.Id("testId"), WaitForElement.Exists);
_webDriver.FindElement(By.Id("testId"), WaitForElement.Clickable);

_webDriver.FindElements(By.Id("testId"), WaitForElements.Visible);
_webDriver.FindElements(By.Id("testId"), WaitForElements.Exists);
```

## Default Wait is 30 seconds
But can be altered as well as the pollingtime and exceptions to ignore:
![image](https://user-images.githubusercontent.com/50708069/118129117-11ff3300-b3fc-11eb-8de1-9356983f76dc.png)
![image](https://user-images.githubusercontent.com/50708069/118129163-217e7c00-b3fc-11eb-975d-ad130178b545.png)
![image](https://user-images.githubusercontent.com/50708069/118129202-2c391100-b3fc-11eb-841c-0db5e0e8fc17.png)
