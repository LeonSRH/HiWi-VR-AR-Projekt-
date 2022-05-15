using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHospital.Model
{
    public class Worker : Person
    {
        [JsonProperty(PropertyName = "id")] public int Employee_Id { get; set; }

        [JsonProperty(PropertyName = "cardNumber")]
        public int Staff_Id { get; set; }

        [JsonProperty(PropertyName = "function")]
        public string Function { get; set; }

        [JsonProperty(PropertyName = "department")]
        public Department Department { get; set; }

        [JsonProperty(PropertyName = "roomName")]
        public string RoomName { get; set; }

        [JsonProperty(PropertyName = "professionalGroup")]
        public Professional_Group Professional_Group { get; set; }

        [JsonProperty(PropertyName = "hasWorkspace")]
        public bool HasWorkspace { get; set; }
    }

    public class Professional_Group
    {
        [JsonProperty(PropertyName = "name")] public string Name { get; set; }

        [JsonProperty(PropertyName = "skills")]
        public List<string> Skills { get; set; }
    }
}