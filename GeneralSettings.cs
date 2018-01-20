using Loki;
using Loki.Common;
using Newtonsoft.Json;

namespace CraftBot {
    public class GeneralSettings : JsonSettings {

        private static GeneralSettings _instance;
        public static GeneralSettings Instance => _instance ?? (_instance = new GeneralSettings());

        private GeneralSettings() : base(GetSettingsFilePath(Configuration.Instance.Name, "CraftBot", "GeneralSettings.json")) {

        }
    }
}