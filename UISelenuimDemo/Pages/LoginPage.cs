using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenuimDemo.Pages
{
    public class LoginPage : BasePage
    {
        private readonly WebDriverWait _wait;

        public LoginPage(IWebDriver driver) : base(driver) 
        {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private By PhoneInputLocator => By.CssSelector("input[name='login']"); 
        private By PasswordInputLocator => By.CssSelector("input[name='password']"); 
        private By SubmitBtnLocator => By.CssSelector("button[data-auth-type='login']"); 
        private By AccountNameLocator => By.CssSelector("button[data-testid='login']"); 

        // Error Message Locators
        private By ErrorXPathContains(string text) => By.XPath($"//span[contains(text(), '{text}')]");


        // Login method
        public void Login(string phone, string password)
        {
            EnterPhone(phone);
            EnterPassword(password);
            ClickSubmit();
        }

        // Enter Phone
        private void EnterPhone(string phone)
        {
            var phoneInput = _wait.Until(ExpectedConditions.ElementIsVisible(PhoneInputLocator));
            phoneInput.Clear();
            phoneInput.SendKeys(phone);
        }

        // Enter Pass
        public void EnterPassword(string pass)
        {
            var passwordInput = _wait.Until(ExpectedConditions.ElementIsVisible(PasswordInputLocator));
            passwordInput.Clear();
            passwordInput.SendKeys(pass);
        }

        // Submit btn
        public void ClickSubmit()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(SubmitBtnLocator)).Click();
        }

        // Get Error text by text part

        public List<string> GetAllErrors()
        {
            var errors = new List<string>();

            // 1) Top general error
            var topError = GetErrorTextIfPresent(By.XPath("//div[contains(text(), 'Невірний логін') or contains(text(), 'пароль')]"));
            if (topError != null)
                errors.Add(topError);

            // 2) Phone field errors
            var phoneErrors = Driver.FindElements(By.XPath("//input[@name='login']/following-sibling::span"));
            if (phoneErrors.Any())
                errors.AddRange(phoneErrors.Select(e => e.Text));

            // 3) Password field errors
            var passErrors = Driver.FindElements(By.XPath("//input[@name='password']/following-sibling::span[@data-error]"));
            if (passErrors.Any())
                errors.AddRange(passErrors.Select(e => e.Text));

            return errors;
        }
        private string GetErrorTextIfPresent(By locator)
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(locator)).Text;
            }
            catch
            {
                return null;
            }
        }












        public string GetErrorByText(string text)
        {
            try
            {
                return _wait.Until(ExpectedConditions.ElementIsVisible(ErrorXPathContains(text))).Text;
            }
            catch
            {
                return null; 
            }
        }

        // Check is Logged in
        public bool IsLoggedIn()
        {
            try
            {
                var btn = _wait.Until(ExpectedConditions.ElementIsVisible(AccountNameLocator));

                return !btn.Text.Contains("Увійти", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

    }
}
