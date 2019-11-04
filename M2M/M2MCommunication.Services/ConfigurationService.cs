﻿using CommonServiceLocator;
using M2MCommunication.Core;

namespace M2MCommunication.Services
{
    public class ConfigurationService
    {
        public IConfiguration Configuration { get; }

        public ConfigurationService()
        {
            Configuration = ServiceLocator.Current.GetInstance<IConfiguration>();
        }
    }
}