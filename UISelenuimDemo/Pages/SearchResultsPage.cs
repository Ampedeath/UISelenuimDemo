using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UISelenuimDemo.Pages
{
    public class SearchResultsPage : BasePage
    {
        public SearchResultsPage(IWebDriver driver) : base(driver) { }

        private By ProductItemsLocator => By.CssSelector("li[data-test-small-card]"); 
        private By ProductTitleLocator => By.CssSelector("li[data-test-small-card] p[itemprop='name'] a"); 

        public int GetResultsCount()
        {
            try
            {
                var elems = Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(ProductItemsLocator));
                return elems.Count;
            }
            catch
            {
                return 0;
            }
        }

        public List<string> GetAllTitles()
        {
            try
            {
                var elems = Wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(ProductTitleLocator));
                return elems.Select(e => e.Text).ToList();
            }
            catch
            {
                return new List<string>();
            }
        }

        public void OpenFirstProduct()
        {
            var first = Wait.Until(ExpectedConditions.ElementToBeClickable(ProductTitleLocator));
            first.Click();
        }
       
    }
}
