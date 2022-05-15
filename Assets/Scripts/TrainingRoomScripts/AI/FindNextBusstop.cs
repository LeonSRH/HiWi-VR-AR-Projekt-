using UnityEngine;
using UnityEngine.AI;

namespace SmartHospital.TrainingRoom
{
    public class FindNextBusstop : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator anim;

        GameObject[] busstops;
        float lastdistance = 5000;
        Transform waypointposition;

        // Use this for initialization
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            busstops = GameObject.FindGameObjectsWithTag("Busstop");
            anim = GetComponent<Animator>();

            if (busstops[0] != null)
            {
                foreach (var el in busstops)
                {
                    var dist = Vector3.Distance(transform.position, el.transform.position);

                    if (lastdistance < dist)
                    {
                        //do nothing, already the smallest distance
                    }
                    else if (lastdistance >= dist)
                    {
                        //change lastdistance to the smaller distance
                        lastdistance = dist;
                        waypointposition = el.transform;
                    }
                    else
                    {
                        lastdistance = dist;
                        waypointposition = el.transform;
                    }
                }

                anim.SetFloat("Forward", 1);
                agent.destination = waypointposition.GetChild(0).transform.position;
            }

        }

        void OnTriggerEnter(Collider other)
        {
            if (busstops[0] != null)
            {
                if (other == waypointposition.GetChild(0).GetComponent<BoxCollider>())
                {
                    anim.SetFloat("Forward", 0);
                    Destroy(this);
                }
            }

        }
    }
}