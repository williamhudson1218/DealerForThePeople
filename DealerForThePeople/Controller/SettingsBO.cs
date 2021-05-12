using Microsoft.Extensions.Configuration;

namespace DealerForThePeople.Controller
{
    public static class SettingsBO
    {
        public static string GetURL()
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")["URL"];
        }
    }
}
