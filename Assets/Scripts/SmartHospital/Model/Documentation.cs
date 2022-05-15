using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartHospital.Model
{
    public class Documentation
    {
        [JsonProperty(PropertyName = "instructionsPath")]
        public string Instructions_Path { get; set; }

        [JsonProperty(PropertyName = "intendedUse")]
        public string Intended_Use { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "sideEffects")]
        public List<string> Side_Effects { get; set; }

        [JsonProperty(PropertyName = "packagingInformation")]
        public string Packaging_Definition { get; set; }

        [JsonProperty(PropertyName = "uiPath")]
        public string UI_Path { get; set; }
    }

    public class Technical_Documentation : Documentation
    {
        [JsonProperty(PropertyName = "medicalClass")]
        public Medical_Class Class { get; set; }

        [JsonProperty(PropertyName = "labeling")]
        public List<string> Labeling { get; set; }

        [JsonProperty(PropertyName = "essentialRequirements")]
        public List<string> Essential_Requirements { get; set; }

        [JsonProperty(PropertyName = "technicalData")]
        public string Technical_Data { get; set; }

        [JsonProperty(PropertyName = "appliedStandards")]
        public List<string> Applied_Standards { get; set; }

        [JsonProperty(PropertyName = "riskManagementPath")]
        public string Rist_Management_Path { get; set; }

        [JsonProperty(PropertyName = "specificationPath")]
        public string Specification_Path { get; set; }

        [JsonProperty(PropertyName = "clinicalExaminationPath")]
        public string Clinical_Examination_Path { get; set; }
    }

    public enum Medical_Class
    {
    }



}