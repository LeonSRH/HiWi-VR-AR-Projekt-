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
        time = 0;
        //spawninterval = Random.Range(10, 20);
        Patient.SetActive(true);
        Stopwatch.StartStopWatch();

    }
}
