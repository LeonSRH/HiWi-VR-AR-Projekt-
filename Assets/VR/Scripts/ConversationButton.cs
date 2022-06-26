using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ConversationButton : MonoBehaviour
{

    [SerializeField] private GameObject Exit;
    [SerializeField] private GameObject TriageInterior;
    [SerializeField] private ComputerRegistration Computer;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private Stopwatch stopwatch;
    [SerializeField] private GameObject Patient;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetButton(int status)
    {
        switch (status)
        {
            case 1:
                Computer.SetBool(true);
                break;

            case 2:
                Patient.GetComponent<NavMeshAgent>().SetDestination(TriageInterior.transform.position);
                Patient.GetComponent<NavMeshAgent>().isStopped = false;
                gameObject.GetComponent<Animator>().SetFloat("Walking", 1);
                Canvas.SetActive(false);
                break;

            case 3:
                Patient.GetComponent<NavMeshAgent>().SetDestination(Exit.transform.position);
                Patient.GetComponent<NavMeshAgent>().isStopped = false;
                Canvas.SetActive(false);
                gameObject.GetComponent<Animator>().SetFloat("Walking", 1);
                stopwatch.StopStopWatch();
                break;
        }
    }
}
