using UnityEngine;

namespace SmartHospital.Test_Debug {
    public class TestMove : MonoBehaviour {
        float angleMultiplier;

        void Start() {
            angleMultiplier = Random.Range(10, 30);
        }

        void Update() {
            var angle = angleMultiplier * Time.deltaTime;

            transform.RotateAround(Values.CameraPositions[int.MinValue], Vector3.up, angle);
            transform.Rotate(Vector3.up, -angle);
        }
    }
}