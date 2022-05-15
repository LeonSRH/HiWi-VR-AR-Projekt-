using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;

#region Request
public class Camunda_Request_AVariable
{
    public string value { get; set; }
    public string type { get; set; }
}

public class Camunda_Request_ValueInfo
{
    public bool transient { get; set; }
}

public class Camunda_Request_AnotherVariable
{
    public bool value { get; set; }
    public string type { get; set; }
    public Camunda_Request_ValueInfo valueInfo { get; set; }
}

public class Camunda_Request_Variables
{
    public Camunda_Request_AVariable aVariable { get; set; }
    public Camunda_Request_AnotherVariable anotherVariable { get; set; }
}

public class Camunda_Request_Root
{
    public Camunda_Request_Variables variables { get; set; }
    public string businessKey { get; set; }
    public bool withVariablesInReturn { get; set; }
}

#endregion

#region Response


public class Camunda_Response_Link
{
    [JsonProperty("method")]
    public string method { get; set; }

    [JsonProperty("href")]

    public string href { get; set; }

    [JsonProperty("rel")]

    public string rel { get; set; }
}


public class Camunda_Response_AnotherVariable
{
    [JsonProperty("type")]
    public string type { get; set; }

    [JsonProperty("value")]
    public bool value { get; set; }

    [JsonProperty("valueInfo")]
    public Camunda_Response_ValueInfo valueInfo { get; set; }
}

public class Camunda_Response_ValueInfo
{
}

public class Camunda_Response_AVariable
{
    [JsonProperty("type")]
    public string type { get; set; }

    [JsonProperty("value")]
    public string value { get; set; }

    [JsonProperty("valueInfo")]
    public Camunda_Response_ValueInfo valueInfo { get; set; }
}

public class Camunda_Response_Variables
{
    [JsonProperty("anotherVariable")]
    public Camunda_Response_AnotherVariable anotherVariable { get; set; }

    [JsonProperty("aVariable")]
    public Camunda_Response_AVariable aVariable { get; set; }
}

public class Camunda_Response_Root
{
    [JsonProperty("links")]
    public List<Camunda_Response_Link> links { get; set; }

    [JsonProperty("id")]
    public string id { get; set; }

    [JsonProperty("definitionId")]
    public string definitionId { get; set; }

    [JsonProperty("businessKey")]
    public string businessKey { get; set; }

    [JsonProperty("ended")]
    public bool ended { get; set; }

    [JsonProperty("suspended")]
    public bool suspended { get; set; }

    [JsonProperty("tenantId")]
    public object tenantId { get; set; }

    [JsonProperty("variables")]
    public Camunda_Response_Variables variables { get; set; }
}

#endregion

#region Response_Task

public class Camunda_Response_Task_Root
{
    [JsonProperty("id")]
    public string id { get; set; }

    [JsonProperty("name")]
    public string name { get; set; }

    [JsonProperty("assignee")]
    public object assignee { get; set; }

    [JsonProperty("created")]
    public DateTime created { get; set; }

    [JsonProperty("due")]
    public object due { get; set; }

    [JsonProperty("followUp")]
    public object followUp { get; set; }

    [JsonProperty("delegationState")]
    public object delegationState { get; set; }

    [JsonProperty("description")]
    public object description { get; set; }

    [JsonProperty("executionId")]
    public string executionId { get; set; }

    [JsonProperty("owner")]
    public object owner { get; set; }

    [JsonProperty("parentTaskId")]
    public object parentTaskId { get; set; }

    [JsonProperty("priority")]
    public int priority { get; set; }

    [JsonProperty("processDefinitionId")]
    public string processDefinitionId { get; set; }

    [JsonProperty("processInstanceId")]
    public string processInstanceId { get; set; }

    [JsonProperty("taskDefinitionKey")]
    public string taskDefinitionKey { get; set; }

    [JsonProperty("caseExecutionId")]
    public object caseExecutionId { get; set; }

    [JsonProperty("caseInstanceId")]
    public object caseInstanceId { get; set; }

    [JsonProperty("caseDefinitionId")]
    public object caseDefinitionId { get; set; }

    [JsonProperty("suspended")]
    public bool suspended { get; set; }

    [JsonProperty("formKey")]
    public object formKey { get; set; }

    [JsonProperty("tenantId")]
    public object tenantId { get; set; }
}
#endregion
