using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteriorTrigger : MonoBehaviour
{

    [SerializeField] private GameObject Vitalparameter;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<ConversationManager>() != null)
        {
            other.GetComponent<NavMeshAgent>().isStopped = true;
            other.GetComponent<Animator>().SetFloat("Walking", 0);
            /* other.gameObject.transform.rotation = this.gameObject.transform.rotation; */
            StartCoroutine(DelayedSetRotation(other));
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    IEnumerator DelayedSetRotation (Collider other)
    {
        yield return new WaitForSeconds(0.5f);
        other.gameObject.transform.rotation = this.gameObject.transform.rotation;
        Vitalparameter.SetActive(true);
    }
}
