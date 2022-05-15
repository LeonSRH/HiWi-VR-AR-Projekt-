using UnityEngine;
using UnityEngine.AI;

public class DrawPathForTraining : MonoBehaviour
{
    float elapsed;
    LineRenderer lineRenderer;
    NavMeshPath path;

    private Transform target;
    public GameObject tg;

    void Start()
    {
        path = new NavMeshPath();
        elapsed = 0.0f;

        lineRenderer = GetComponent<LineRenderer>();
        setTarget(tg.transform);
    }

    void Update()
    {
        // Update the way to the goal every second.
        elapsed += Time.deltaTime;
        if (elapsed > 1.0f && target != null)
        {
            elapsed -= 1.0f;
            NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
        }

        lineRenderer.positionCount = path.corners.Length;


        for (var i = 0; i < path.corners.Length; i++)
        {
            lineRenderer.SetPosition(i, path.corners[i]);
        }
    }

    public void setTarget(Transform waypoint)
    {
        target = waypoint;
    }
}