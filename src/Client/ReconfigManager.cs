using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace Reconfig.Configuration
{
    public static class ReconfigManager
    {
        public const string CacheDependencyFile = "SecConfiguration.cache";
        static readonly object Locker = new object();
        static Configuration _configuration;

        static Configuration Configuration
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    return (Configuration)HttpRuntime.Cache["SecConfig.Configuration"];
                }
                return _configuration;
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpRuntime.Cache.Insert("SecConfig.Configuration", value, null, DateTime.Now + TimeSpan.FromMinutes(5), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
                }
                else
                {
                    _configuration = value;
                }
            }
        }

        public static NameValueCollection AppSettings
        {
            get
            {
                return GetSection("appSettings");
            }
        }

        public static ConnectionStringSettingsCollection ConnectionStrings
        {
            get
            {
                var connectionStrings = new ConnectionStringSettingsCollection();
                var section = GetSectionInternal("connectionStrings");
                foreach (var setting in section.Settings)
                {
                    connectionStrings.Add(new ConnectionStringSettings
                    {
                        Name = setting.Key,
                        ConnectionString = setting.Value
                    });
                }
                return connectionStrings;
            }
        }

        public static NameValueCollection GetSection(string sectionName)
        {
            var section = GetSectionInternal(sectionName);
            var result = new NameValueCollection(section.Settings.Count);
            foreach (var setting in section.Settings)
            {
                result.Add(setting.Key, setting.Value);
            }

            return result;
        }

        static ConfigurationSection GetSectionInternal(string sectionName)
        {
            LoadConfiguration(2);

            ConfigurationSection section = null;

            foreach (var sect in Configuration.Sections)
            {
                if (sect.Name == sectionName)
                {
                    section = sect;
                    break;
                }
            }

            return section;
        }

        static void LoadConfiguration(int attempts = 1)
        {
            if (Configuration != null)
            {
                return;
            }

            lock (Locker)
            {
                if (Configuration != null)
                {
                    return;
                }

                attempts--;

                if (attempts < 0)
                {
                    throw new ReconfigException(new ApplicationException("Number of attempts exceeded"));
                }

                string jsonConfig = null;

                if (IsFileOld())
                {
                    jsonConfig = DownloadFromServer();
                }
                if (string.IsNullOrEmpty(jsonConfig))
                {
                    jsonConfig = LoadFromFile();
                }
                if (string.IsNullOrEmpty(jsonConfig))
                {
                    LoadConfiguration(attempts);
                }
                try
                {
                    Configuration = JsonConvert.DeserializeObject<Configuration>(jsonConfig);
                }
                catch (Exception exc)
                {
                    if (attempts <= 0) throw new ReconfigException(exc);
                    LoadConfiguration(attempts);
                }
            }
        }

        static string LoadFromFile()
        {
            try
            {
                var cacheFile = ResolveCacheFilePath();
                if (File.Exists(cacheFile))
                {
                    using (var stream = File.OpenRead(cacheFile))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
        }

        static string DownloadFromServer()
        {
            try
            {
                var requestUrl = ResolveConfigUrl();
                var request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);
                request.UseDefaultCredentials = false;
                request.Accept = "application/json";
                request.Credentials = null;
                request.UserAgent = typeof(ReconfigManager).FullName;

                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var config = reader.ReadToEnd();
                            TrySaveToFile(config);
                            return config;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
            }
            return null;
        }

        static void TrySaveToFile(string config)
        {
            try
            {
                using (var file = File.Create(ResolveCacheFilePath()))
                {
                    using (var writer = new StreamWriter(file, Encoding.UTF8))
                    {
                        writer.Write(config);
                        writer.Close();
                        file.Close();
                    }
                }
            }
            catch
            {
            }
        }

        static bool IsFileOld()
        {
            try
            {
                var info = new FileInfo(ResolveCacheFilePath());
                return DateTime.Now > info.LastWriteTime.AddMinutes(10);
            }
            catch
            {
            }
            return true;
        }

        static string ResolveConfigUrl()
        {
            var apiUrl = ResolveApiUrl();

            var context = HttpContext.Current;
            if (context != null)
            {
                var uri = context.Request.Url;
                var hostName = uri.Host.ToLower();
                if (hostName == "localhost" || hostName == "127.0.0.1")
                {
                    hostName = HttpContext.Current.Server.MachineName;
                }

                var siteName = HostingEnvironment.ApplicationVirtualPath;

                apiUrl = string.Format("{0}/resolve?url={1}://{2}{3}{4}",
                    apiUrl, uri.Scheme, hostName, (uri.IsDefaultPort ? "" : ":" + uri.Port), siteName);
            }
            else
            {
                var appName = Path.GetFileNameWithoutExtension(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                var configName = ConfigurationManager.AppSettings["SecConfig.ConfigName"];
                if (configName == null)
                {
                    throw new ReconfigException(new ApplicationException("'SecConfig.ConfigName' not found in app.config file"));
                }
                apiUrl = string.Format("{0}/resolve?name={1}&appName={2}", apiUrl, configName, appName);
            }

            return apiUrl;
        }

        static string ResolveApiUrl()
        {
            var apiUrl = ConfigurationManager.AppSettings["SecConfig.ApiUrl"];
            if (apiUrl == null)
            {
                apiUrl = (string)Registry.GetValue(string.Format("{0}\\Software\\{1}", Registry.LocalMachine, "SecConfig"), "ApiUrl", null);
            }

            if (apiUrl == null)
            {
                throw new ReconfigException(new ApplicationException("'SecConfig.ApiUrl' not found in app.config file nor in Registry"));
            }

            return apiUrl + "/api/config";
        }

        static string ResolveCacheFilePath()
        {
            if (HttpContext.Current != null)
            {
                return Path.Combine(HttpContext.Current.Server.MapPath("~/bin/"), CacheDependencyFile);
            }

            return Path.Combine(System.Reflection.Assembly.GetExecutingAssembly().Location, CacheDependencyFile);
        }
    }
}