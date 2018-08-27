using System.Collections.Generic;
using System.ComponentModel;

namespace CakeExtentions
{
    public enum Environment
    {
        [Description("محیط تستی")]
        Test,
        [Description("محیط عملیاتی")]
        Operational
    }

    public class ProjectModel
    {
        public string Name { get; set; }
        public bool IsEnabled { get; set; }
    }

    public class EnvironmentModel
    {
        public Environment Type { get; set; }
        public bool IsEnabled { get; set; }
        public List<ProjectModel> Projects { get; set; }
    }

    public class ConfigurationModel
    {
        public string SolutionPath { get; set; }
        public List<EnvironmentModel> Environments { get; set; }

    }
}
