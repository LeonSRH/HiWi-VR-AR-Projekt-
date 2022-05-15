using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital.TrainingRoom
{
    public class PlayerCameraControllerActions : MonoBehaviour
    {

        private void Start()
        {
            UnityEngine.Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public static void disableCameraController()
        {
            GameObject.Find("vThirdPersonCamera").GetComponent<vThirdPersonCamera>().enabled = false;
            //GameObject.Find("3rdPersonPlayer").GetComponent<Invector.CharacterController.vThirdPersonController>().enabled = false;
        }

        public static void activateCameraController()
        {
            GameObject.Find("vThirdPersonCamera").GetComponent<vThirdPersonCamera>().enabled = true;
            //GameObject.Find("3rdPersonPlayer").GetComponent<Invector.CharacterController.vThirdPersonController>().enabled = true;
        }
    }
}
