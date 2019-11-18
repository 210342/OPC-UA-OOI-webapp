using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceModel.Configuration
{
    public class RepositoryConfiguration
    {
        public string Directory { get; set; }
        public string PropertiesFileName { get; set; }
        public RepositoryMapping[] Mappings { get; set; }
    }
}
