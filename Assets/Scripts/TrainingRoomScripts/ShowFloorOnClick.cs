using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SmartHospital.TrainingRoom
{
    public class ShowFloorOnClick : MonoBehaviour
    {
        [Tooltip("Ebene 98: 0, Ebene 99: 1, ... , Ebene 04: 6")]
        public GameObject[] ebenen;

        [Tooltip("floor text heading")]

        public TextMeshProUGUI floorText;
        public GameObject floorUI;

        public GameObject overviewButton;
        public GameObject floorButton;

        public GameObject ground;
        public Camera overviewCamera;
        public Camera mainCamera;

        public int ebene;

        private bool floorMode;

        private void enableFloorMode()
        {
            //camera change
            overviewCamera.enabled = true;
            mainCamera.enabled = false;

            //button change
            overviewButton.SetActive(true);
            floorButton.SetActive(false);

            //enable floor buttons
            floorUI.SetActive(true);

            //disable main camera rotation
            mainCamera.GetComponent<CameraOrbit>().enabled = false;

            //disable all unnacessay game Objects
            ground.SetActive(false);
            foreach (GameObject eb in ebenen)
            {
                iTween.Stop(eb);
                eb.SetActive(false);
            }
            ebenen[6].SetActive(false);
        }


        private void OnMouseDown()
        {
            if (floorMode)
            {
                enableFloorMode();
                ebenen[ebene].SetActive(true);
                floorText.SetText("/ " + ebenen[ebene].gameObject.name);
            }

        }

        public void setFloorMode(bool floor)
        {
            floorMode = floor;
        }
    }
}
