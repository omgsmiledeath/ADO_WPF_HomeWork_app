using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ADO_WPF_HomeWork_app.Models
{
    internal class SettingsSave
    {
        [JsonProperty("MssqlDataSource")]
        public string MssqlDataSource { get; set; } = string.Empty;
        [JsonProperty("MssqlInitialCatalog")]
        public string MssqlInitialCatalog { get; set; } = string.Empty;

        [JsonProperty("OledbDataSource")]
        public string OledbDataSource { get; set; } = string.Empty;
    }
}
