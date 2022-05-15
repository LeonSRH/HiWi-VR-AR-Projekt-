using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreatePatients : MonoBehaviour
{
    /**
     * Day time for the change in Coroutine
     * */
    public enum DayTime
    {
        MORNING,
        DAY,
        NIGHT,
        NOTIME
    }

    //all isActive patients in scene
    GameObject[] activePatients;

    [Tooltip("The main light of the scene.")]
    public Light dayLight;

    DayTime dayTime;

    //time to update time of the day
    float dayTimeChange;

    //initialize the next day time || 0: Morning, 1: Day, 2: Night
    int nextDayTimeChange;

    [Tooltip("Models for nurse models. (Prefab(s))")]
    public Transform[] nurse;

    //number of nurses which should be spawned
    int nurseNumber;

    [Tooltip("Model for patient bed. (Prefab)")]
    public Transform patientBed;

    //all patient rooms in the scene found by tag
    GameObject[] patientRooms;

    //Prefabs for patient Models
    [Tooltip("Models for patient models. (Prefab(s))")]
    public Transform[] patients;

    //all spanwpoints in scene where the models should spawn
    GameObject[] spawnPoints;

    //timer time for the change of the day time
    readonly float timeScale = 240.0f;

    void Start()
    {
        patientRooms = GameObject.FindGameObjectsWithTag("PatientRoom");
        spawnPoints = GameObject.FindGameObjectsWithTag("Waypoint");

        //set Timer
        dayTimeChange = timeScale;

        //If beds are already placed in the building we dont need to place them
        //createBeds();
        dayTime = DayTime.MORNING;

        nurseNumber = patientRooms.Length / 4;
        spawnModelsOnSpawnpoints(nurse, false, nurseNumber);
        spawnModelsOnSpawnpoints(patients, true, patientRooms.Length);

        StartCoroutine(DTM());
    }


    void createBeds()
    {
        var init = 0;
        //for the room position
        float[] x;
        float[] y;
        float[] z;

        //spawn patient beds in patient rooms
        if (patientRooms != null && patientRooms.Length > init)
        {
            x = new float[patientRooms.Length];
            y = new float[patientRooms.Length];
            z = new float[patientRooms.Length];

            foreach (var go in patientRooms)
            {
                x[init] = go.transform.position.x;
                y[init] = go.transform.position.y;
                z[init] = go.transform.position.z;

                Instantiate(patientBed, new Vector3(x[init], y[init], z[init]), Quaternion.identity);
                init++;
            }
        }
    }

    void Update()
    {
        dayTimeTimer();
    }

    void dayTimeTimer()
    {
        dayTimeChange -= Time.deltaTime;

        if (dayTimeChange < 0)
        {
            if (nextDayTimeChange == 0)
            {
                morning();
                dayTimeChange = timeScale;
            }
            else if (nextDayTimeChange == 1)
            {
                day();
                dayTimeChange = timeScale;
            }
            else if (nextDayTimeChange == 2)
            {
                night();
                dayTimeChange = timeScale;
            }
            else
            {
                NoTimeSet();
                dayTimeChange = timeScale;
            }
        }
    }

    //spawn models on busstops
    void spawnModelsOnSpawnpoints(Transform[] model, bool patient, int anzahl)
    {
        int rndModel;
        var busInd = 0;

        if (patient && anzahl > 0)
        {
            //get random number of patients that should be spawned
            float rndNumber = (int)Random.Range(0f, anzahl);
            //get a random patient model in array
            rndModel = (int)Random.Range(0f, patients.Length);
            var patientToSpawn = patients[rndModel];

            for (var i = 0; i < rndNumber; i++)
            {
                busInd = (int)Random.Range(0f, spawnPoints.Length);
                Instantiate(patientToSpawn,
                    new Vector3(spawnPoints[busInd].transform.position.x, spawnPoints[busInd].transform.position.y,
                        spawnPoints[busInd].transform.position.z), Quaternion.identity);
            }
        }
        else
        {
            //get a random nurse model in array
            rndModel = (int)Random.Range(0f, nurse.Length);
            for (var count = 0; count < nurseNumber; count++)
            {
                busInd = (int)Random.Range(0f, spawnPoints.Length);
                Instantiate(nurse[rndModel],
                    new Vector3(spawnPoints[busInd].transform.position.x, spawnPoints[busInd].transform.position.y,
                        spawnPoints[busInd].transform.position.z), Quaternion.identity);
            }
        }
    }

    void night()
    {
        dayLight.intensity = 0f;
        spawnModelsOnSpawnpoints(patients, true, 2);
        dayTime = DayTime.NIGHT;
    }

    void day()
    {
        activePatients = GameObject.FindGameObjectsWithTag("Patient");
        dayLight.intensity = 2f;
        var patientsForFreeRooms = patientRooms.Length - activePatients.Length;

        if (patientsForFreeRooms >= 0)
        {
            spawnModelsOnSpawnpoints(patients, true, patientsForFreeRooms + 1);
        }

        dayTime = DayTime.DAY;
    }

    void morning()
    {
        activePatients = GameObject.FindGameObjectsWithTag("Patient");
        dayLight.intensity = 0.9f;
        var patientsForFreeRooms = patientRooms.Length - activePatients.Length;

        if (patientsForFreeRooms >= 0)
        {
            spawnModelsOnSpawnpoints(patients, true, patientsForFreeRooms + 1);
        }

        dayTime = DayTime.MORNING;
    }

    void NoTimeSet()
    {
        throw new NotImplementedException();
    }

    public static GameObject getRoomWP(String tag)
    {
        GameObject wp = GameObject.Find(tag);
        return wp;
    }

    IEnumerator DTM()
    {
        //TODO
        //While Scene isActive or Scene playing
        while (true)
        {
            switch (dayTime)
            {
                case DayTime.MORNING:
                    //next day
                    nextDayTimeChange = 1;
                    break;
                case DayTime.DAY:
                    nextDayTimeChange = 2;
                    break;
                case DayTime.NIGHT:
                    nextDayTimeChange = 0;
                    break;
                case DayTime.NOTIME:
                    NoTimeSet();
                    break;
            }

            yield return null;
        }
    }
}