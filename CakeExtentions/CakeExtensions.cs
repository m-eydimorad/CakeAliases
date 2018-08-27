using Cake.Core;
using Cake.Core.Annotations;
using Newtonsoft.Json;
using CakeExtentions;

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
}
