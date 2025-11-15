using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenuimDemo.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver) { }

        private By LoginButtonLocator => By.CssSelector("[data-testid='login']"); 
        private By SearchInputLocator => By.CssSelector("input[type='search']"); 
        private By SearchSubmitLocator => By.CssSelector("button[data-testid='search-button']");
        private By FavoritebPageBtnLocator => By.CssSelector("[data-testid='product-favorite-button']");

        public void Open(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void OpenLogin() 
        {
            WaitUntilClickable(LoginButtonLocator).Click();
        }

        public void Search(string query)
        {
            var input = WaitUntilVisible(SearchInputLocator);
            input.Clear();
            input.SendKeys(query);
            WaitUntilClickable(SearchSubmitLocator).Click();
        }

        public void OpenFavoritePage()
        {
            WaitUntilClickable(FavoritebPageBtnLocator).Click();
        }
    }
}
