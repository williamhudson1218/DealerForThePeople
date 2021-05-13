using Microsoft.Extensions.Configuration;
using System.Linq;

namespace DealerForThePeople.Controller
{
    public static class SettingsLogic
    {
        public static string GetURL()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["url"];
        }

        public static string[] GetPositiveWords()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("AppSettings:positiveWords")
                .GetChildren()
                .Select(x =>x.Value)
                .ToArray();
        }
        public static string[] GetNegativeWords()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("AppSettings:negativeWords")
                .GetChildren()
                .Select(x => x.Value)
                .ToArray();
        }

    }
}
