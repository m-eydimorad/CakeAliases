using Cake.Core;
using Cake.Core.Annotations;
using Newtonsoft.Json;
using System.Net;

namespace CakeExtensions
{
    public static class CakeExtensions
    {
        [CakeMethodAlias]
        public static ConfigurationModel GetConfiguration(this ICakeContext context, string jsonPath)
        {
            if (System.IO.File.Exists(jsonPath) == false)
                return null;

            var json = System.IO.File.ReadAllText(jsonPath);
            ConfigurationModel configuration = JsonConvert.DeserializeObject<ConfigurationModel>(json);
            return configuration;
        }

        [CakeMethodAlias]
        public static void FtpUploadFile(this ICakeContext context, string file, string hostDestination, NetworkCredential ftpCredentials)
        {
            if (FTPTasks.FtpCredentials == null)
            {
                FTPTasks.FtpCredentials = ftpCredentials;
            }

            FTPTasks.FtpUploadFileInternal(file, hostDestination);
        }
    }
}
