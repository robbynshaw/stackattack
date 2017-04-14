using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace stackattack.Core
{
    public class Config : IConfig
    {
        public int MaxQuestions { get; private set; }

        public string SQLiteDatabasePath { get; private set; }

        public Config()
        {
            // Load values from web.config
            this.MaxQuestions = GetValueOrDefault("MaxQuestions", 20);
            this.SQLiteDatabasePath = GetValueOrDefault("SQLiteDatabasePath",
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "stackattack.sqlite"));
        }

        private int GetValueOrDefault(string key, int defaultValue)
        {
            int i;
            string val = WebConfigurationManager.AppSettings[key];
            if (int.TryParse(val, out i))
            {
                return i;
            }
            return defaultValue;
        }

        private string GetValueOrDefault(string key, string defaultValue)
        {
            string val = WebConfigurationManager.AppSettings[key];
            return string.IsNullOrEmpty(val) ? defaultValue : val;
        }
    }
}