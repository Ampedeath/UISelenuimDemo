using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenuimDemo.Pages
{
    public class FavoritePage : BasePage
    {
        public FavoritePage(IWebDriver driver) : base(driver) { }

        private By FavoritesListLocator => By.CssSelector("li[data-test-small-card]"); 
        private By FavoriteItemTitle => By.CssSelector("li[data-test-small-card] p[itemprop='name'] a"); 

        public bool IsProductInFavorites(string productName)
        {
            try
            {
                var elems = Driver.FindElements(FavoritesListLocator);
                return elems.Any(e => e.FindElement(FavoriteItemTitle).Text.Contains(productName));
            }
            catch
            {
                return false;
            }
        }
    }
}
