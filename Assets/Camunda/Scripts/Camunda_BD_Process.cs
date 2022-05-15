using System.Threading.Tasks;
using UnityEngine;

public class Camunda_BD_Process
{
    string Process;

    public Camunda_BD_Process(string Process)
    {
        this.Process = Process;
    }

    public string ActiveVariable { get; set; }

    public bool Running { get; set; }

    public string ProcessName { get; set; }

    public async Task StartProcessAsync()
    {
        Camunda_Response_Root rootObj = await Camunda_Request.StartProcess(Process);
        ProcessName = rootObj.id;
        ActiveVariable = rootObj.variables.aVariable.value;
        Running = !rootObj.ended;

        Debug.Log("Process with Id: " + rootObj.id + " \n Running: " + Running);


    }

    public async Task StartNextTaskAsync()
    {

        Camunda_Response_Variables rootObj = await Camunda_Request.CompleteTask(ProcessName);

        if (rootObj != null)
        {
            ActiveVariable = rootObj.aVariable.value;
            Debug.Log(ActiveVariable);
            Debug.Log("Completed task of Process: " + ProcessName + "\n Returned value A: " + rootObj.aVariable.value);
        }
        else
        {
            Debug.Log("No active variables");
        }


    }

    public void StartProcess()
    {
        StartProcessAsync();
    }

    public void StartNextTask()
    {
        StartNextTaskAsync();
    }


}
