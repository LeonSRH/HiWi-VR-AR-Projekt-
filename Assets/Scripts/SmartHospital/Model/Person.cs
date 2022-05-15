using Newtonsoft.Json;

namespace SmartHospital.Model
{
    public class Person
    {
        [JsonProperty(PropertyName = "givenName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "familyName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "formOfAddress")]
        public string FormOfAdress { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string E_Mail { get; set; }

        [JsonProperty(PropertyName = "address")]
        public Address Address { get; set; }

    }

    public class Address
    {
        [JsonProperty(PropertyName = "street")]
        public string Street { get; set; }

        [JsonProperty(PropertyName = "streetNumber")]
        public string Street_Number { get; set; }

        [JsonProperty(PropertyName = "zipCode")]
        public int Zip_Code { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string City { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        [JsonProperty(PropertyName = "pobAddress")]
        public int POB_Address { get; set; }

        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

    }
}
