using System.Text.Json.Serialization;

namespace AcademiaAtos_ApiWeb.DataModels
{
    public class Pessoa
    {
        public int id { get; set; }
        public string nome { get; set; }
        [JsonIgnore]
        public virtual ICollection<Email>? emails { get; set; }

    }
}
