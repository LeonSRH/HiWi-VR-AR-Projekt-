using UnityEngine;

namespace SmartHospital.Test_Debug {
    public class WTSP_Test : MonoBehaviour {
        public Camera testCamera;
        public GameObject testGameObject;

        void OnGUI() {
            Vector2 position = testCamera.WorldToScreenPoint(testGameObject.transform.position);
            GUI.Label(new Rect(position.x, position.y, 100, 25), position.ToString());
        }
    }
}