using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class InitializeBusstop : Conditional {
    int busInit;

    //inializer for the bus array; isActive bus in array
    [BehaviorDesigner.Runtime.Tasks.Tooltip("The inizializer in the busstops array.")]
    public SharedInt busInt;

    //all busstops in scene
    GameObject[] busstops;

    //isActive busstop in array
    [BehaviorDesigner.Runtime.Tasks.Tooltip("The first bus stop that the bus heads to.")]
    public SharedGameObject nextBusPoint;

    //isActive busstop in array
    [BehaviorDesigner.Runtime.Tasks.Tooltip("The first and the last busstop.")]
    public SharedGameObject startBusstop;

    // The tag of the targets
    public string targetTag;

    public override void OnAwake() {
        busstops = GameObject.FindGameObjectsWithTag(targetTag);

        busstops = sortArrayByFirstSymbolInName(busstops);

        startBusstop.Value = busstops[0].gameObject;

        busInt = 0;
    }

    //sorting the busstop array
    public GameObject[] sortArrayByFirstSymbolInName(GameObject[] array) {
        string firstName;
        string secondName;

        for (var i = 0; i < 100; i++) {
            for (var z = 0; z < array.Length - 1; z++) {
                firstName = array[z].name;
                secondName = array[z + 1].name;

                if (firstName[0] > secondName[0]) {
                    var dummy = array[z];
                    array[z] = array[z + 1];
                    array[z + 1] = dummy;
                }
            }
        }

        return array;
    }

    public override TaskStatus OnUpdate() {
        if (busstops != null && busInt != null) {
            busInit = busInt.Value;
            if (busInit < busstops.Length) {
                nextBusPoint.Value = busstops[busInit].transform.GetChild(3).gameObject;
                busInt.Value = busInt.Value + 1;
                return TaskStatus.Success;
            }

            busInit = 0;
            busInt = 0;
            nextBusPoint.Value = busstops[busInit].transform.GetChild(3).gameObject;
            busInt.Value = busInt.Value + 1;
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}