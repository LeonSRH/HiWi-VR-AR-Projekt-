using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;


public class ConversationManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Patienttext;
    [SerializeField] private TextMeshProUGUI Antwortitel;
    [SerializeField] private TextMeshProUGUI Antwort1, Antwort2, Antwort3;

    [Header("Input von Patient")]
    [SerializeField] private string Frage1;
    [SerializeField] private string Frage2;
    [SerializeField] private string Frage3;

    [Header("Antwortmöglichkeiten für den Spieler")]
    [SerializeField] private string Antwortitel1;
    [SerializeField] private string A1;
    [SerializeField] private string B1;
    [SerializeField] private string C1;
    [Space(30)]
    [SerializeField] private string Antwortitel2;
    [SerializeField] private string A2;
    [SerializeField] private string B2;
    [SerializeField] private string C2;

    public int statusglobal;
    [SerializeField] private GameObject Exit;
    [SerializeField] private ComputerRegistration Computer;
    [SerializeField] private GameObject Canvas;
    [SerializeField] private Stopwatch stopwatch;


    public void SetConversation(int status)
    {
        statusglobal = status;
        switch (status)
        {
            case 1:
                Patienttext.text = Frage1;
                Antwortitel.text = Antwortitel1;
                Antwort1.text = A1;
                Antwort2.text = B1;
                Antwort3.text = C1;
                break;
            case 2:
                Patienttext.text = Frage2;
                Antwortitel.text = Antwortitel2;
                Antwort1.text = A2;
                Antwort2.text = B2;
                Antwort3.text = C2;
                break;
            default:
                break;
        }
    }

    public void SetButton()
    {
        switch (statusglobal)
        {
            case 1:
                Computer.SetBool(true);
                break;
            case 2:
                Debug.Log("lol");
                this.gameObject.GetComponent<NavMeshAgent>().SetDestination(Exit.transform.position);
                this.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                Canvas.SetActive(false);
                gameObject.GetComponent<Animator>().SetFloat("Walking", 1);
                stopwatch.StopStopWatch();
                break;
        }
    }
}
