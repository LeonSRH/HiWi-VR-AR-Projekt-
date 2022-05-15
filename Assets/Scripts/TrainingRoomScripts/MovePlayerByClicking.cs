using UnityEngine;
using UnityEngine.AI;

namespace SmartHospital {
    public class RoomSelected : MonoBehaviour {
        public Camera firstPersonCamera;
        NavMeshAgent nav;

        public Camera overheadCamera;
        public GameObject player;

        // Use this for initialization
        void Start() {
            nav = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update() {
            if (Input.GetMouseButtonDown(0)) {
                // if left button pressed...
                var ray = overheadCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    nav.SetDestination(hit.point);
                    OnMouseDown();
                }
            }
            else if (Input.GetMouseButtonDown(1)) {
                OnRightMouseClicked();
            }
        }

        void OnRightMouseClicked() {
            firstPersonCamera.enabled = false;
            overheadCamera.enabled = true;
        }

        void OnMouseDown() {
            // firstPersonCamera.enabled = true;
            // overheadCamera.enabled = false;

            Debug.Log("Player moved to position: " + Input.mousePosition);
        }
    }
}