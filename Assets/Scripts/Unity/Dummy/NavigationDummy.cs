using SmartHospital.Controller;
using SmartHospital.Model;
using UnityEngine;
using UnityEngine.AI;

public class NavigationDummy : MonoBehaviour
{
    [SerializeField] NavigationDummyView view;
    [SerializeField] MainSceneMainController builder;

    LineRenderer lineRenderer;
    string start;
    string dest;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;

        view.OnStartChosen += delegate (string s)
        {
            //builder.buildBuilding();
            start = s;
            ShowPath();
        };
        view.OnDestChosen += delegate (string s)
        {
            //builder.buildBuilding();
            dest = s;
            ShowPath();
        };
    }

    void ShowPath()
    {
        if (start == null || dest == null)
        {
            return;
        }

        var gameObjects = GameObject.FindGameObjectsWithTag("RoomCollider");
        Transform startObject = null;
        Transform destObject = null;

        for (var i = 0; i < gameObjects.Length; i++)
        {
            var room = gameObjects[i].GetComponent<ClientRoom>();

            if (!room) continue;
            if (room.RoomName == start)
            {
                startObject = room.transform;

            }
            else if (room.RoomName == dest)
            {
                destObject = room.transform;

            }
        }

        if (startObject == null || destObject == null)
        {
            print("CRITICAL ERROR ON FINDING ROOMS");
            return;
        }

        var startHitFound = NavMesh.SamplePosition(startObject.position, out var startHit, 10f, NavMesh.AllAreas);
        var destHitFound = NavMesh.SamplePosition(destObject.position, out var destHit, 10f, NavMesh.AllAreas);

        if (!startHitFound)
        {
            print("No StartHit");
        }

        if (!destHitFound)
        {
            print("No DestHit");
        }

        print($"Start: {startObject.name} : {startObject.position} -> {startHit.position}");
        print($"End: {destObject.name} : {destObject.position} -> {destHit.position}");

        var path = new NavMeshPath();
        var pathAvailable = NavMesh.CalculatePath(startHit.position, destHit.position,
            NavMesh.AllAreas, path);

        if (!pathAvailable)
        {
            print("No Path Found");
            return;
        }

        lineRenderer.positionCount = path.corners.Length;

        for (var i = 0; i < path.corners.Length; i++)
        {
            var current = path.corners[i];
            current.y += 20f;
            lineRenderer.SetPosition(i, current);
        }
    }
}