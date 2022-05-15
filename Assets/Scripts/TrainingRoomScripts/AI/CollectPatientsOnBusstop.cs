using UnityEngine;
using UnityEngine.AI;

public class CollectPatientsOnBusstop : MonoBehaviour {
    NavMeshAgent agent;
    int init;
    bool onShBusstop;
    readonly GameObject[] passengers = new GameObject[10];
    GameObject sh_Busstop;
    Vector3 spawnPointOnShBusstop;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        sh_Busstop = GameObject.Find("0 sh_Busstop");

        spawnPointOnShBusstop = sh_Busstop.transform.GetChild(4).position;
    }

    void OnTriggerStay(Collider collider) {
        if (agent.velocity.magnitude == 0) {
            if (collider.gameObject.tag == "Patient") {
                checkForCollectingPassengers(collider);
            }
            else if (onShBusstop && init > 0) {
                //bus is on the endstation + not driving
                letOutThePassengers();
            }
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (agent.velocity.magnitude == 0 && !onShBusstop) {
            if (collider.gameObject.tag == "Patient") {
                checkForCollectingPassengers(collider);
            }
        }
        else if (agent.velocity.magnitude == 0 && onShBusstop && init > 0) {
            //bus is on the endstation + not driving
            letOutThePassengers();
        }
    }

    void checkForCollectingPassengers(Collider other) {
        //bus is on a busstop + not driving + not on the endstation
        //collect the passengers on this busstop
        if (!onShBusstop && init < passengers.Length) {
            other.gameObject.SetActive(false);
            passengers[init] = other.gameObject;
            init++;
        }
    }

    void letOutThePassengers() {
        for (var i = 0; i < init; i++) {
            passengers[i].transform.position = spawnPointOnShBusstop;
            passengers[i].gameObject.SetActive(true);
        }

        init = 0;
    }

    void Update() {
        isOnShBusstop();
    }

    void isOnShBusstop() {
        var dist = Vector3.Distance(transform.position, sh_Busstop.transform.position);

        if (dist <= 10) {
            onShBusstop = true;
        }
        else if (dist > 2) {
            onShBusstop = false;
        }
    }
}