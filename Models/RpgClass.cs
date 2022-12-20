using System.Text.Json.Serialization;

namespace cshap_basic_vscode.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // thay vì gửi enum dạng 1,2,3 => trả ra tên của nó
    public enum RpgClass
    {
        Knight = 1,
        Mage = 2,
        Cleric = 3
    }
}