using UnityEngine;

namespace SmartHospital.Test_Debug {
    [RequireComponent(typeof(Camera))]
    public class CameraAngleTestScript : MonoBehaviour {
        public Vector3 cameraAngle = new Vector3(90, 0, 0);

        void Update() {
            transform.eulerAngles = cameraAngle;
        }
    }
}