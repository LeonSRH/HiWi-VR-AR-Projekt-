using UnityEngine;

namespace Assets.Plugins.Lomenu_UI.Scripts {
    public class MenuCamControl : MonoBehaviour {

        //public Transform currentMount;
        public float speedFactor = 0.1f;
        public float zoomFactor = 1.0f;
        // For better results on 3D mode, you can enable this
        public Camera cameraComponent;

        //private Vector3 lastPosition;

        void Start() {
            //lastPosition = transform.position;
        }

        void Update() {
            //transform.position = Vector3.Lerp(transform.position, currentMount.position, 0.1f);
            //transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speedFactor);


            // For better results on 3D mode, you can enable this
            cameraComponent.fieldOfView = 60 + zoomFactor;

            //lastPosition = transform.position;
        }

        //public void setMount(Transform newMount) {
        //    currentMount = newMount;
        //}
    }
}