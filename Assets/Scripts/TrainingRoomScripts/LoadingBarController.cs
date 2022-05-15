using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace SmartHospital.TrainingRoom
{
    public class LoadingBarController : MonoBehaviour
    {

        public Transform LoadingBar;
        public Transform TextIndicator;

        [SerializeField] private float currentAmount;
#pragma warning disable CS0649 // Dem Feld "LoadingBarController.speed" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "0".
        [SerializeField] private float speed;
#pragma warning restore CS0649 // Dem Feld "LoadingBarController.speed" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "0".

        private bool active;
        private static bool ready = true;

        // Update is called once per frame
        void Update()
        {
            if (active)
            {
                if (currentAmount < 100)
                {
                    currentAmount += speed * Time.deltaTime;
                    TextIndicator.GetComponent<Text>().text = ((int)currentAmount).ToString() + "%";
                    PlayerCameraControllerActions.disableCameraController();

                }
                else
                {
                    TextIndicator.GetComponent<Text>().text = "Fertig.";
                    ready = true;
                    PlayerCameraControllerActions.activateCameraController();
                    gameObject.SetActive(false);
                    active = false;
                    currentAmount = 0;
                }

                LoadingBar.GetComponent<Image>().fillAmount = currentAmount / 100;
            }
            else
            {
                gameObject.SetActive(false);
            }

        }

        public static bool getReady()
        {
            return ready;
        }

        public bool getActive()
        {
            return active;
        }

        public void activateLoadingBar()
        {
            active = true;
            gameObject.SetActive(true);
            ready = false;
        }
    }
}
