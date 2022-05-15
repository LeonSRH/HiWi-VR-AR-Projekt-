using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHospital.Model
{
    public partial class Department
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("costCentreAssignment")] public int CostCentre { get; set; }

        [JsonProperty("functionalAreas")] public List<FunctionalArea> FunctionalAreas { get; set; }

        [JsonIgnore] public Worker MedicalDirector { get; set; }

        [JsonIgnore] public Worker AccessDirector { get; set; }
    }

    public partial class FunctionalArea
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("type")] public string Type { get; set; }
    }
}