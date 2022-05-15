using UnityEngine;

namespace SmartHospital.TrainingRoom {
    public class FirstCameraScript : MonoBehaviour {
        int cam_length;

        public GameObject[] cameras;
        int init;

        // Use this for initialization
        void Start() {
            init = 0;
            cameras[0].GetComponent<Camera>().enabled = true;
            cam_length = cameras.Length;
        }

        void switchCamera() {
            if (init < cam_length - 1) {
                cameras[init].GetComponent<Camera>().enabled = false;
                init++;
                cameras[init].GetComponent<Camera>().enabled = true;
            }
            else {
                cameras[init].GetComponent<Camera>().enabled = false;
                init = 0;
                cameras[init].GetComponent<Camera>().enabled = true;
            }
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Z)) {
                switchCamera();
            }

            /** if (Input.GetKeyDown(KeyCode.Z))
             {
                 ShowOverheadView();
             }



             if (Input.GetKeyUp(KeyCode.Y))
             {
                 ShowFirstPersonView();

             }**/
        }
    }
}