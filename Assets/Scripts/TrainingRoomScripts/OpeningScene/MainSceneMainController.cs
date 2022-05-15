using SmartHospital.TrainingRoom;
using UnityEngine;


namespace SmartHospital.Controller
{
    /// <summary>
    /// Main Scene controller: gameObjects, cameras
    /// </summary>
    public class MainSceneMainController : MonoBehaviour
    {
        [Tooltip("Ebene 98: 0, Ebene 99: 1, ... , Ebene 04: 6")]
        public GameObject[] ebenen;

        public GameObject ground;
        public Camera overviewCamera;
        public Camera mainCamera;

        public float startPositionStandalone = -4;
        public float incrementPositionStandalone = 2;
        public float startPositionMobile = -0.5f;
        public float incrementPositionMobile = 0.5f;

        private Transform mainCameraStartPosition;
        private Vector3[] ebenenStartPosition;


        MainSceneUIController uIController;

        /// <summary>
        /// Enables the one floor overview Mode - shows the selected floor with the overview camera
        /// </summary>
        /// <param name="ebene">floor which should be shown</param>
        public void EnableFloorOverviewMode(int ebene)
        {
            overviewCamera.enabled = true;
            mainCamera.enabled = false;

            buildBuilding();

            uIController.SetMainPerspective(false);

            mainCamera.GetComponent<CameraOrbit>().enabled = false;


            foreach (GameObject eb in ebenen)                                   // Tauscht die 3D Ebene aus bei Klick auf der Ebenen Map
            {
                eb.SetActive(false);

            }

            ebenen[6].SetActive(false);
            ebenen[ebene].SetActive(true);
            if (ground != null)
                ground.SetActive(false);

        }


        public void CameraModes(int mode)
        {
            if (mode == 0) // Von Oben
            {

                uIController.SetMainPerspective(false);
                overviewCamera.enabled = true;
                mainCamera.enabled = false;
            }

            if (mode == 1) // Normaler Modus
            {
                uIController.SetMainPerspective(true);

                overviewCamera.enabled = false;
                mainCamera.enabled = true;
                mainCamera.GetComponent<CameraOrbit>().enabled = true;
            }

        }


        public void enableFloorMode()
        {
            buildBuilding();
            //camera change
            overviewCamera.enabled = true;
            mainCamera.enabled = false;

            //disable main camera rotation
            mainCamera.GetComponent<CameraOrbit>().enabled = false;

            //disable all unnacessay game Objects
            if (ground != null)
                ground.SetActive(false);
            foreach (GameObject eb in ebenen)
            {
                eb.SetActive(false);
            }
            ebenen[6].SetActive(false);
        }

        public void startFloorMode(int ebene)
        {
            enableFloorMode();
            ebenen[ebene].SetActive(true);
        }

        //enable rotate and view all mode
        public void showAllGameObjects()
        {
            mainCamera.GetComponent<CameraOrbit>().enabled = true;
            mainCamera.transform.position = mainCameraStartPosition.position;

            for (int i = 0; i < ebenen.Length; i++)
            {
                ebenen[i].SetActive(true);
                ebenen[i].transform.position = ebenenStartPosition[i];

            }


            mainCamera.enabled = true;
            overviewCamera.enabled = false;
            buildBuilding();
        }

        private void Start()
        {
            mainCameraStartPosition = mainCamera.transform;
            uIController = GameObject.FindObjectOfType<MainSceneUIController>();



            ebenenStartPosition = new Vector3[ebenen.Length];
            for (int i = 0; i < ebenen.Length; i++)
            {
                ebenenStartPosition[i] = ebenen[i].transform.position;
            }

            showAllGameObjects();
        }

        //splits the building into the floors
        public void splitBuilding()
        {
            var i = startPositionStandalone;
            foreach (GameObject ebene in ebenen)
            {
                iTween.MoveTo(ebene, new Vector3(ebene.transform.position.x, ebene.transform.position.y + i, ebene.transform.position.z), 10);
                i += incrementPositionStandalone;
            }

        }


        //splits the building into the floors
        public void splitMobileBuilding()
        {
            var i = startPositionMobile;
            foreach (GameObject ebene in ebenen)
            {
                iTween.MoveTo(ebene, new Vector3(ebene.transform.position.x, ebene.transform.position.y + i, ebene.transform.position.z), 10);
                i += incrementPositionMobile;
            }
        }

        //builds the building into the start position
        public void buildBuilding()
        {
            foreach (GameObject ebene in ebenen)
            {
                iTween.Stop(ebene);
            }

            for (int i = 0; i < ebenen.Length; i++)
            {
                ebenen[i].SetActive(true);
                ebenen[i].transform.position = ebenenStartPosition[i];
            }
        }
    }
}
