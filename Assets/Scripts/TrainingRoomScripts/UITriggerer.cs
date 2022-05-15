using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital.TrainingRoom
{
    public class UITriggerer : MonoBehaviour
    {
        public GameObject hidingUI;
        public GameObject standardUI;
        public GameObject SettingsUI;
        public GameObject FAQUI;
        public GameObject quitUI;
        public GameObject badgesUI;
        public GameObject bigMapUI;
        public GameObject endUI;
        private bool showing;

        public GameObject[] questFlags;

        private bool checkQuestFlags()
        {
            bool ready = true;

            foreach (GameObject quest in questFlags)
            {
                if (!quest.GetComponent<FindLocationQuest>().getVisited())
                {
                    ready = false;
                }

            }

            return ready;
        }

        private void Start()
        {
            PlayerCameraControllerActions.disableCameraController();
            showing = false;
            startGame();
        }

        // Update is called once per frame
        void Update()
        {


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                showHidingUI();

                PlayerCameraControllerActions.disableCameraController();

            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                showing = !showing;

                bigMapUI.SetActive(showing);

            }




            if (checkQuestFlags())
            {
                PlayerCameraControllerActions.disableCameraController();
                showEndUI();
            }
            else
            {
                endUI.SetActive(false);
            }

        }

        private void showEndUI()
        {
            endUI.SetActive(true);

            standardUI.SetActive(false);
            hideBigMapUI();
            hidingUI.SetActive(false);
            SettingsUI.SetActive(false);
            FAQUI.SetActive(false);
            quitUI.SetActive(false);
            badgesUI.SetActive(false);
        }

        public void hideBigMapUI()
        {
            bigMapUI.SetActive(false);
        }

        public void showBadgesUI()
        {
            badgesUI.SetActive(true);

            standardUI.SetActive(false);
            PlayerCameraControllerActions.disableCameraController();
        }

        public void hideBadgesUI()
        {
            badgesUI.SetActive(false);
        }

        public void quitGame()
        {

        }

        public void startGame()
        {
            PlayerCameraControllerActions.disableCameraController();
            standardUI.SetActive(true);

            hideBigMapUI();
            hidingUI.SetActive(false);
            SettingsUI.SetActive(false);
            FAQUI.SetActive(true);
            quitUI.SetActive(false);
            badgesUI.SetActive(false);
        }

        public void continueGame()
        {
            standardUI.SetActive(true);

            hideBigMapUI();
            hidingUI.SetActive(false);
            SettingsUI.SetActive(false);
            FAQUI.SetActive(false);
            quitUI.SetActive(false);
            badgesUI.SetActive(false);
            PlayerCameraControllerActions.activateCameraController();
        }

        public void showHidingUI()
        {
            hidingUI.SetActive(true);

            standardUI.SetActive(false);
            SettingsUI.SetActive(false);
            FAQUI.SetActive(false);
            quitUI.SetActive(false);
            badgesUI.SetActive(false);
            PlayerCameraControllerActions.disableCameraController();

        }


        public void showSettings()
        {
            SettingsUI.SetActive(true);
            hidingUI.SetActive(false);
            PlayerCameraControllerActions.disableCameraController();

        }

        public void showFAQUI()
        {
            FAQUI.SetActive(true);

            hidingUI.SetActive(false);
            standardUI.SetActive(false);
            PlayerCameraControllerActions.disableCameraController();

        }

        public void showQuitUI()
        {
            PlayerCameraControllerActions.disableCameraController();
            quitUI.SetActive(true);

            standardUI.SetActive(false);
            hidingUI.SetActive(false);
        }
    }
}
