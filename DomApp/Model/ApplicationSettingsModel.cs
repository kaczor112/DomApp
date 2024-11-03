using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ApplicationSettingsModel
    {
        public bool RefreshSettings { get; set; }
        public bool Debug { get; set; }
        public string KeyID { get; set; }
        public string MyWebsite { get; set; }

        public static ApplicationSettingsModel Default
        {
            get
            {
                return new ApplicationSettingsModel()
                {
                    RefreshSettings = true,
                    Debug = true,
                    KeyID = "MojeID",
                    MyWebsite = @"https://example.com/"
                };
            }
        }
    }
}