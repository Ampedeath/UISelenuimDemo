using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UISelenuimDemo.Utilts;

namespace UISelenuimDemo.Pages
{
    public class ProductPage : BasePage
    {
        public ProductPage(IWebDriver driver) : base(driver) { }

        private By AddToFavoritesBtn => By.CssSelector("button[data-product-favorite='']"); 
        private By FavoriteSuccessIcon => By.CssSelector("div[class*='list-enter-to']"); 
        private By ProductNameLocator => By.CssSelector("h1[data-product-name='']"); 

        public string GetProductName()
        {
            return WaitUntilVisible(ProductNameLocator).Text;
        }

        public void AddToFavorites()
        {
            WaitUntilClickable(AddToFavoritesBtn).Click();
        }

        public bool IsAddedToFavorites()
        {
            try
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(3));

                var icon = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(FavoriteSuccessIcon));

                return icon.Displayed;
            }
            catch
            {
                return false;
            }
        }
    }
}
