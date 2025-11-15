using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenuimDemo.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected IWebElement WaitUntilClickable(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
        }

        protected IWebElement WaitUntilVisible(By locator)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(locator));
        }

        protected bool WaitUntilElementContainsText(By locator, string text)
        {
            return Wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);
                    return element.Text.Contains(text);
                }
                catch
                {
                    return false;
                }
            });
        }
    }
}
