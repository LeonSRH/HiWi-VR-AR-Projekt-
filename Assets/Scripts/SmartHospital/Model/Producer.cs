using Newtonsoft.Json;

namespace SmartHospital.Model
{
    public class Producer
    {

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

        [JsonProperty(PropertyName = "phoneNumber")]
        public string Phone_Number { get; set; }

        [JsonProperty(PropertyName = "eMail")]
        public string E_Mail { get; set; }

        [JsonProperty(PropertyName = "website")]
        public string Website { get; set; }

        [JsonProperty(PropertyName = "responsiblePerson")]
        public Person Responsible_Person { get; set; }

        [JsonProperty(PropertyName = "labelPath")]
        public string Label_Path { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "remarks")]
        public string Remarks { get; set; }

    }

    public class Phone_Number
    {
       
    }
}