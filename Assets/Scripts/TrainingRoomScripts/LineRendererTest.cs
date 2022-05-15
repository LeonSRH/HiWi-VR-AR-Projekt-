using UnityEngine;

public class LineRendererTest : MonoBehaviour {
    LineRenderer lineRenderer;
    GameObject[] spawnpoints;

    Vector3[] wayPoints; //You have to set these waypoints before you start to use LineRenderer.

    void Start() {
        spawnpoints = GameObject.FindGameObjectsWithTag("Waypoint");
        spawnpoints = sortArrayByFirstSymbolInName(spawnpoints);
        wayPoints = new Vector3[spawnpoints.Length];
        var init = 0;


        foreach (var wp in spawnpoints) {
            wayPoints[init] = wp.transform.position;
            init++;
        }


        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = wayPoints.Length;
        for (var i = 0; i < wayPoints.Length; i++) {
            lineRenderer.SetPosition(i, wayPoints[i]);
        }
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
}