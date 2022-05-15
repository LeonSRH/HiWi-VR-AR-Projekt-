using UnityEngine;

namespace SmartHospital.Common {
    public class BaseController : MonoBehaviour {
        void OnDrawGizmos() {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, 5);
            Gizmos.DrawIcon(transform.position, "Controller.png", false);
        }
    }
}