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
        [JsonProperty("MssqlConStr")]
        public string MssqlConStr { get; set; } = string.Empty;
        
        [JsonProperty("OledbConStr")]
        public string OledbConStr { get; set; } = string.Empty;
    }
}
