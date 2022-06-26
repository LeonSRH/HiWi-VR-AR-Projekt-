using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSpawnManager : MonoBehaviour
{

    private int spawninterval;
    private float time; //used to track time 
    private int seconds; //used to check when spawninterval is equal
    [SerializeField] private GameObject Patient;
    [SerializeField] private Stopwatch Stopwatch;
    [SerializeField] private Transform Spawnpoint;
    // Start is called before the first frame update
    void Start()
    {
        spawninterval = Random.Range(4, 7);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        seconds = (int) (time % 60);
        if(spawninterval == seconds)
        {
            HandleSpawn();
        }
    }

    private void HandleSpawn()
    {
        //time = 0;
        spawninterval = Random.Range(5, 7);
        Patient.SetActive(true);
        Stopwatch.StartStopWatch();
    }

    public void ResetPatient()
    {
        Patient.SetActive(false);
        time = 0;
        Patient.transform.position = Spawnpoint.position;
        Stopwatch.currentTime = 0;
    }
}
