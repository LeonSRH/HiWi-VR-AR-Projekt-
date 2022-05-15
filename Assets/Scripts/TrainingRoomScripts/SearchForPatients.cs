using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SearchForPatients : Action {
    GameObject[] patientBeds;


    public GameObject[] storedGameObjectList;

    SharedGameObject[] target;

    public override void OnStart() { }

    public override TaskStatus OnUpdate() {
        patientBeds = GameObject.FindGameObjectsWithTag("Quest");


        if (patientBeds != null && patientBeds.Length >= 0) {
            Debug.Log("Anzahl pat bed: " + patientBeds.Length);
            var init = 0;


            foreach (var bed in patientBeds) {
                Debug.Log("Patient Bed: " + patientBeds[init]);

                storedGameObjectList[init] = patientBeds[init];

                Debug.Log("Shared Bed: " + storedGameObjectList[init]);
                init++;
            }


            init = 0;
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}