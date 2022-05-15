using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToPatientTrigger : MonoBehaviour
{
    //patient for emergency call
    private GameObject patient;

    //status of the doctor
    private WorkingStatus status;
    private Animator anim;

    //Doctor components
    private NavMeshAgent agent;
    private Transform activeWP;
    private int active_Waypoint_number;
    //waypoints for navmesh
    public Transform[] wp_ForDoctor;

    private bool playerEnter;

    private GameObject player;

    //START
    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("3rdPersonPlayer");
        playerEnter = false;

        active_Waypoint_number = 0;
        if (wp_ForDoctor.Length > 0)
        {
            activeWP = wp_ForDoctor[active_Waypoint_number];
        }

        status = WorkingStatus.WORKING;
        StartCoroutine(ENUM());
    }

    
    private void Update()
    {
        
        if (!playerEnter)
        {
            //wenn Status = arbeiten dann
            if (status == WorkingStatus.WORKING)
            {
                //prüfe die Distanz zum nächsten zugeteilten Wegpunkt
                if (Vector3.Distance(transform.position, activeWP.transform.position) > 1)
                {
                    agent.destination = new Vector3(activeWP.transform.position.x, activeWP.transform.position.y, activeWP.transform.position.z);
                }//falls die Distanz kleiner als 1 ist, dann setze den Status auf Pause und suche den nächsten Arbeitspunkt
                else
                {
                    status = WorkingStatus.PAUSE;
                    active_Waypoint_number++;

                    
                    if (active_Waypoint_number < wp_ForDoctor.Length)
                    {
                        activeWP = wp_ForDoctor[active_Waypoint_number];
                    }
                    else //wenn alle Wegpunkte abgelaufen wurden, dann gehe zum Startpunkt
                    {
                        active_Waypoint_number = 0;
                        activeWP = wp_ForDoctor[active_Waypoint_number];
                    }
                }
            }
            else if (status == WorkingStatus.EMERGENCY)
            {
                if (patient != null)
                {
                    if (Vector3.Distance(transform.position, patient.transform.position) > 3)
                    {
                        agent.destination = new Vector3(patient.transform.position.x, patient.transform.position.y, patient.transform.position.z);
                    }
                    else
                    {
                        status = WorkingStatus.TALKING;
                    }
                }
            }
        }
        else if (playerEnter)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 2)
            {
                agent.destination = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                anim.SetBool("Talking", true);
                status = WorkingStatus.EMERGENCY;
                playerEnter = false;

            }

        }

    }

    //status of player enter
    public enum WorkingStatus
    {
        WORKING,
        PAUSE,
        EMERGENCY,
        TALKING
    }

    public void continueWorking()
    {
        status = WorkingStatus.WORKING;
    }

    IEnumerator ENUM()
    {
        while (true)
        {
            switch (status)
            {
                case WorkingStatus.WORKING:
                    anim.SetFloat("Forward", 1f);
                    break;
                case WorkingStatus.PAUSE:
                    anim.SetFloat("Forward", 0f);
                    yield return new WaitForSecondsRealtime(10);
                    status = WorkingStatus.WORKING;
                    break;
                case WorkingStatus.EMERGENCY:
                    yield return new WaitForSecondsRealtime(2);
                    anim.SetBool("Talking", false);
                    anim.SetFloat("Forward", 1f);
                    GetComponent<BoxCollider>().enabled = false;
                    playerEnter = false;
                    break;
                case WorkingStatus.TALKING:
                    anim.SetFloat("Forward", 0f);
                    anim.SetBool("Talking", true);
                    break;

            }
            yield return null;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //playerEnter = true;
        }
    }

    //set patient for emergency
    public void setPatient(GameObject pat)
    {
        patient = pat;
    }
}
