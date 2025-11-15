using FluentAssertions;
using OpenQA.Selenium;
using UISelenuimDemo.Pages;
using UISelenuimDemo.Utilts;

namespace UISelenuimDemo.Tests
{
    public class UITests
    {
        private IWebDriver _driver;
        private HomePage _home;
        private LoginPage _login;
        private SearchResultsPage _search;
        private ProductPage _product;
        private FavoritePage _favorites;

        private string BaseUrl => Config.BaseUrl;
        private string ValidPhone => Config.ValidPhone;
        private string ValidPassword => Config.ValidPassword;

        [SetUp]
        public void SetUp()
        {
            _driver = Driver.GetDriver();
            _home = new HomePage(_driver);
            _login = new LoginPage(_driver);
            _search = new SearchResultsPage(_driver);
            _product = new ProductPage(_driver);
            _favorites = new FavoritePage(_driver);
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.Quit();
                }
                catch { }
                finally
                {
                    _driver.Dispose();
                    _driver = null;
                }
            }
        }

        [Test]
        public void LoginPositive()
        {
            _home.Open(BaseUrl);
            _home.OpenLogin();
            _login.Login(ValidPhone, ValidPassword);

            _login.IsLoggedIn().Should().BeTrue();
        }

        [TestCase("0930000000", "wrongPassword", "Невiрний логiн або пароль.")]
        [TestCase("", "12345678", "обов'язкове поле")]
        [TestCase("0", "12345678", "введіть правильний телефон")]
        [TestCase("0930000000", "", "обов'язкове поле")]
        public void Login_WithInvalidCredentials_ShouldShowCorrectValidation(string phone, string password, string expectedMessage)
        {
            _home.Open(BaseUrl);
            _home.OpenLogin();
            _login.Login(phone, password);

            var errors = _login.GetAllErrors();

            errors.Should().Contain(e => e.Contains(expectedMessage), $"очікувалось повідомлення з текстом '{expectedMessage}'");
        }

        [TestCase("Gigabyte B650 D3HP AMD B650 AM5 HDMI/2xDP MicroATX DDR5 Ryzen 7000 (582957)")] 
        public void Search_ShouldReturnSingleResult(string query)
        {
            _home.Open(BaseUrl);
            _home.Search(query);

            var count = _search.GetResultsCount();
            count.Should().Be(1, "цей пошук має повертати рівно один результат");
        }

        [TestCase("Gigabyte B650 D3HP AMD B650 AM5")]
        public void Search_ShouldReturnMultipleResults(string query)
        {
            _home.Open(BaseUrl);
            _home.Search(query);

            var count = _search.GetResultsCount();
            count.Should().BeGreaterThan(1, "очікується більше одного результату для цього запиту");
        }

        [TestCase("Gigabyte B650 D3HP AMD B650 AM5 HDMI/2xDP MicroATX DDR5 Ryzen 7000 (582957)")]
        public void Search_AddFirstResultToFavorites_ShouldBeInFavorites(string query)
        {
            _home.Open(BaseUrl);
            // Log in
            _home.Open(BaseUrl);
            _home.OpenLogin();
            _login.Login(ValidPhone, ValidPassword);
            // Search for element
            _home.Search(query);
            _search.OpenFirstProduct();
            var productName = _product.GetProductName();
            // Add to favorite
            _product.AddToFavorites();
            _product.IsAddedToFavorites().Should().BeTrue("має з'явитись підтвердження що додано");


            _home.OpenFavoritePage();

            _favorites.IsProductInFavorites(productName).Should().BeTrue($"товар '{productName}' має бути у обраному");
        }
    }
}
