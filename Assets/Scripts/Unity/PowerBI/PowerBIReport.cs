using Newtonsoft.Json;
using System;

public class PowerBIReport
{

    [JsonProperty("Date")]
    public DateTime Date { get; set; }

    [JsonProperty("UserName")]
    public string UserName { get; set; }

    [JsonProperty("FoundRooms")]
    public decimal FoundRooms { get; set; }

    [JsonProperty("FoundWorker")]
    public decimal FoundWorker { get; set; }

    [JsonProperty("FoundDepartments")]
    public decimal FoundDepartments { get; set; }

    [JsonProperty("FoundProfessionalGroups")]
    public decimal FoundProfessionalGroups { get; set; }

    [JsonProperty("FoundWorkspaces")]
    public decimal FoundWorkspaces { get; set; }

}

