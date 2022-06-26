using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class ConversationManager : MonoBehaviour
{
    [Header("AusgabeObjekte")]
    [SerializeField] private TextMeshProUGUI Patienttext;
    [SerializeField] private TextMeshProUGUI Antwortitel;
    [SerializeField] private TextMeshProUGUI VitalParameter1,VitalParameter2,VitalParameter3,VitalParameter4, Vitalparameter5;

    [Header("Input von Patient")]
    [SerializeField] private string Frage1;
    [SerializeField] private string Frage2;
    [SerializeField] private string Frage3;

    [Header("Antwortmöglichkeiten für den Spieler")]
    [SerializeField] private string Blutdruck;
    [SerializeField] private string Herzfrequenz;
    [SerializeField] private string SpO2;
    [SerializeField] private string Atemfrequenz;
    [SerializeField] private string Temperatur;

    [Header("Sonstige Variablen")]
    [SerializeField] private GameObject Exit;
    [SerializeField] private GameObject TriageInterior;
    [SerializeField] private ComputerRegistration Computer;
    [SerializeField] private GameObject PatientQuestionCanvas;
    [SerializeField] private GameObject VitalparameterCanvas;
    [SerializeField] private Stopwatch stopwatch;
    [SerializeField] private PatientSpawnManager patientmanager;


    public void SetConversation(int status)
    {

        switch (status)
        {
            case 1:
                Patienttext.text = Frage1;
                break;
            case 2:
                Patienttext.text = Frage2;
                break;

            case 3:
                Patienttext.text = Frage3;
            break;

            default:
                break;
        }
    }

    public void SetButton(int status)
    {
        switch (status)
        {
            case 1:
            //Anmelden des Patienten
                Computer.SetBool(true);
                break;

            case 2:
                this.gameObject.GetComponent<NavMeshAgent>().SetDestination(TriageInterior.transform.position);
                this.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                gameObject.GetComponent<Animator>().SetFloat("Walking", 1);
                PatientQuestionCanvas.SetActive(false);
                break;

            case 3:
                this.gameObject.GetComponent<NavMeshAgent>().SetDestination(Exit.transform.position);
                this.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                PatientQuestionCanvas.SetActive(false);
                gameObject.GetComponent<Animator>().SetFloat("Walking", 1);
                stopwatch.StopStopWatch();
                patientmanager.Invoke("ResetPatient", 3);
                break;
        }
    }


    public void SetParameter(int status)
    {
        switch (status)
        {
            case 1:
            VitalParameter1.text = Blutdruck;
            break;
            case 2:
            VitalParameter2.text = Herzfrequenz;
            break;
            case 3:
            VitalParameter3.text = SpO2;
            break;
            case 4:
            VitalParameter4.text = Atemfrequenz;
            break;
            case 5:
            Vitalparameter5.text = Temperatur;
            break;
        } 
    }
}

