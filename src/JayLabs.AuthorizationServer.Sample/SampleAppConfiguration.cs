using System.Configuration;

namespace JayLabs.AuthorizationServer.Sample
{
    public class SampleAppConfiguration
    {
        public bool AllowInsecureHttp { get; set; }
        public string OpenIdClientId { get; set; }
        public string OpenIdAuthority { get; set; }

        public static SampleAppConfiguration CreateFromConfigFile(string prefix)
        {
            var fromConfigFile = new SampleAppConfiguration
            {
                AllowInsecureHttp = ParseOrDefault(prefix, "AllowInsecureHttp", true),
                OpenIdClientId = GetOrDefault(prefix, "OpenIdClientId", ""),
                OpenIdAuthority = GetOrDefault(prefix, "OpenIdAuthority", "")
            };

            return fromConfigFile;
        }

        static string GetOrDefault(string prefix, string key, string defaultValue)
        {
            var value = ConfigurationManager.AppSettings[string.Format("{0}:{1}", prefix, key)];

            return string.IsNullOrWhiteSpace(value) ? defaultValue : value;
        }

        static bool ParseOrDefault(string prefix, string key, bool defaultValue)
        {
            var boolean = ConfigurationManager.AppSettings[string.Format("{0}:{1}", prefix, key)];

            bool parsed;
            if (bool.TryParse(boolean, out parsed))
            {
                return parsed;
            }
            return defaultValue;
        }

        public bool IsValid()
        {
            return
                !string.IsNullOrWhiteSpace(OpenIdClientId) &&
                !string.IsNullOrWhiteSpace(OpenIdAuthority);
        }
    }
}