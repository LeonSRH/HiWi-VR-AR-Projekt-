using BehaviorDesigner.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Camunda NavMesh + Behavior Designer Controller (Process Start and Complete Task)
/// 
/// @author: KS
/// </summary>
public class Camunda_BehaviorDesigner_Controller : MonoBehaviour
{
    Camunda_BD_Process processAttached;

    public string Process;
    public BehaviorTree seekTree;
    public TextMeshProUGUI processText; //active process Text

    string nextDestinationPoint = "";   //Next destination point of the navMesh

    async void Awake()
    {
        processAttached = new Camunda_BD_Process(Process);
        await processAttached.StartProcessAsync();
        nextDestinationPoint = processAttached.ActiveVariable;
        StartSeekBehaviorTreeWithDestination(nextDestinationPoint);

        processText.text = "Next Waypoint: " + nextDestinationPoint;
    }

    /// <summary>
    /// Sets the destination of the seek behavior tree and enabled the behavior
    /// </summary>
    /// <param name="destination"></param>
    private void StartSeekBehaviorTreeWithDestination(string destination)
    {
        List<SharedVariable> variablesOfTree = GlobalVariables.Instance.GetAllVariables();
        foreach (SharedVariable v in variablesOfTree)
        {
            if (v.Name.Equals("Destination"))
            {
                v.SetValue(GameObject.Find(destination));
                seekTree.EnableBehavior();
            }
        }
    }

    /// <summary>
    /// When trigger of a GameObject entered checks for collider name; If name of collider is equals to the destination point starts next task.
    /// </summary>
    /// <param name="collider">Collider entered</param>
    private async void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.name.Equals(nextDestinationPoint))
        {
            await processAttached.StartNextTaskAsync(); //Starts next task
            nextDestinationPoint = processAttached.ActiveVariable;
            StartSeekBehaviorTreeWithDestination(nextDestinationPoint);
            processText.text = "Next Waypoint: " + nextDestinationPoint;

        }
    }
}
