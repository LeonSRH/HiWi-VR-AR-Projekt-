using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace SmartHospital.TrainingRoom
{
    public class CreateDoorSignNumber : MonoBehaviour
    {

        private GameObject[] DoorSigns;
        private int randomNumber;
        // Use this for initialization
        void Start()
        {
            randomNumber = 0;
            DoorSigns = GameObject.FindGameObjectsWithTag("DoorSign");

            foreach (GameObject doorsign in DoorSigns)
            {
                doorsign.GetComponentInChildren<TextMeshPro>().text = "" + randomNumber;
                randomNumber++;
            }
        }

    }
}
