using System.Collections.Generic;

namespace Neo.Extensions.Api
{
    public class ApplicationSettings
    {
        public string ApplicationName { get; set; }
        public List<string> Assemblies { get; set; }
    }
}
