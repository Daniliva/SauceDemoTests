using System.Text.Json;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace SauceDemoTests.Configuration
{
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot Config;

        static ConfigReader()
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }

        public static string BaseUrl => Config["BaseUrl"]!;

        public static string[] GetBrowsers()
        {
            return Config.GetSection("Browsers").Get<string[]>()!;
        }

        public static IEnumerable<TestCaseData> GetUsers()
        {
            var path = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "users.json");
            var json = File.ReadAllText(path);
            var users = JsonSerializer.Deserialize<List<UserModel>>(json);

            foreach (var user in users!)
            {
                yield return new TestCaseData(user).SetName($"{{m}}(\"{user.Username}\")");
            }
        }
    }
}