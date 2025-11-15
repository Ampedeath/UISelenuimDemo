using System.Text.Json;

namespace UISelenuimDemo.Utilts
{
    internal static class Config
    {
        public static string BaseUrl { get; private set; }
        public static string ValidPhone { get; private set; }
        public static string ValidPassword { get; private set; }

        static Config()
        {
            var json = File.ReadAllText("appsettings.json");
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            BaseUrl = data["BaseUrl"];
            ValidPhone = data["ValidPhone"];
            ValidPassword = data["ValidPassword"];
        }
    }
}
