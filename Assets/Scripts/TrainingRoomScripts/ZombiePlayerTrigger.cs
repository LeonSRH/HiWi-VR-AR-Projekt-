using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombiePlayerTrigger : MonoBehaviour
{
    public GameObject frederikTrigger;

#pragma warning disable CS0414 // Dem Feld "ZombiePlayerTrigger.entered" wurde ein Wert zugewiesen, der aber nie verwendet wird.
    private bool entered;
#pragma warning restore CS0414 // Dem Feld "ZombiePlayerTrigger.entered" wurde ein Wert zugewiesen, der aber nie verwendet wird.

    private void Start()
    {
        entered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && GetComponent<Animator>().GetBool("following"))
        {
            entered = true;
            this.gameObject.GetComponent<NavMeshAgent>().SetDestination(this.transform.position);
            this.gameObject.GetComponent<Animator>().SetBool("playerReached", true);
            frederikTrigger.SetActive(false);

        }
    }

}
