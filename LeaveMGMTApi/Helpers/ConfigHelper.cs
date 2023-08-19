namespace LeaveMGMTApi.Helpers
{

    public class ConfigHelper
    {
        private static readonly IConfigurationRoot _Configuration;

        static ConfigHelper()
        {
            _Configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

        }

        public static TDefValType GetConfig<TDefValType>(string key, TDefValType defaultVal)
        {
            try
            {

                var reslt = _Configuration[key];
                if (reslt != null)
                {
                    return (TDefValType)Convert.ChangeType(reslt, typeof(TDefValType));
                }
                if (defaultVal != null)
                {
                    return defaultVal;
                }
                return default(TDefValType);
            }
            catch
            {
                if (defaultVal != null)
                {
                    return defaultVal;
                }
                return default(TDefValType);
            }

        }
        public static string GetConfigStr(string key, string defaultVal = "")
        {
            try
            {

                return _Configuration[key];
            }
            catch { }
            return defaultVal;
        }

        public static List<string> GetArraySection(string key)
        {
            try
            {
                return _Configuration.GetSection(key).Get<List<string>>();
            }
            catch { }
            return null;
        }

    }
}
