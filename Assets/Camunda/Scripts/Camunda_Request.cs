using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Camunda_Request
{
    protected const string ServerAddress = "http://localhost:8080/engine-rest";

    IWebProxy proxy = WebRequest.GetSystemWebProxy();
    ICredentials credentials = new NetworkCredential("demo", "demo");
    HttpClientHandler httpHandler;

    HttpClient http;

    /// <summary>
    /// Sets up the Client for Camunda
    /// </summary>
    private void setUpClient()
    {
        httpHandler = new HttpClientHandler()
        {
            Proxy = proxy,
            Credentials = credentials
        };

        http = new HttpClient(httpHandler);

    }

    /// <summary>
    /// Static Camunda Process Start
    /// </summary>
    /// <returns>DTO of the Camunda Response by starting a process</returns>
    public async static Task<Camunda_Response_Root> StartProcess(string ProcessName)
    {
        var http = new HttpClient();

        Camunda_Request_Root request = new Camunda_Request_Root();

        request.withVariablesInReturn = true;
        var objSerialized = JsonConvert.SerializeObject(request);

        var content = new System.Net.Http.StringContent(objSerialized, Encoding.UTF8, "application/json");

        var url = String.Format(ServerAddress + "/process-definition/key/" + ProcessName + "/start");
        var response = await http.PostAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<Camunda_Response_Root>(result);
        return data;
    }

    /// <summary>
    /// Sends a request to camunda service and gets the task root response
    /// </summary>
    /// <param name="taskId">taskId of the active task</param>
    /// <returns>The active task root response</returns>
    public async static Task<Camunda_Response_Variables> CompleteTask(string taskId)
    {
        var proxy = WebRequest.GetSystemWebProxy();
        var credentials = new NetworkCredential("demo", "demo");
        var httpHandler = new HttpClientHandler()
        {
            Proxy = proxy,
            Credentials = credentials
        };
        var http = new HttpClient();

        Camunda_Request_Root request = new Camunda_Request_Root();

        request.withVariablesInReturn = true;
        var objSerialized = JsonConvert.SerializeObject(request);

        var content = new System.Net.Http.StringContent(objSerialized, Encoding.UTF8, "application/json");

        //Get active task Id
        var activeTaskUrl = String.Format(ServerAddress + "/task?processDefinitionKey=Process_Waypoint");
        var activeTaksResponse = await http.GetAsync(activeTaskUrl);
        var activeResultResponse = await activeTaksResponse.Content.ReadAsStringAsync();

        List<Camunda_Response_Task_Root> response_Task_Root = JsonConvert.DeserializeObject<List<Camunda_Response_Task_Root>>(activeResultResponse);

        string activeTaskId = "";
        foreach (Camunda_Response_Task_Root taskRoot in response_Task_Root)
        {
            if (taskRoot.processInstanceId.Equals(taskId))
            {
                activeTaskId = taskRoot.id;
            }
        }

        var url = String.Format(ServerAddress + "/task/" + activeTaskId + "/complete");
        var response = await http.PostAsync(url, content);
        var result = await response.Content.ReadAsStringAsync();

        var data = JsonConvert.DeserializeObject<Camunda_Response_Variables>(result);

        return data;
    }

}
