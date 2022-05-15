using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBedsForPatient : MonoBehaviour
{

    private GameObject[] patientRooms;
    private int roomsCount;

    public GameObject bed;

    void Start()
    {
        roomsCount = GameObject.FindGameObjectsWithTag("PatientRoom").Length;
        patientRooms = GameObject.FindGameObjectsWithTag("PatientRoom");

        createBeds();

    }

    void createBeds()
    {
        foreach (GameObject patientRoom in patientRooms)
        {
            Instantiate(bed, new Vector3(patientRoom.transform.position.x, patientRoom.transform.position.y, patientRoom.transform.position.z), Quaternion.identity);
        }
    }

    void Update()
    {

    }
}
