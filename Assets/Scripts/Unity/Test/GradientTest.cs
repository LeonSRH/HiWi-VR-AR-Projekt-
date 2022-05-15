using UnityEngine;

namespace SmartHospital.Test_Debug {
    public class GradientTest : MonoBehaviour {
        Renderer _gradientRenderer;
        public Gradient gradient;
        public float slower;

        void Start() {
            _gradientRenderer = GetComponent<Renderer>();
        }

        void Update() {
            _gradientRenderer.material.color = gradient.Evaluate(Mathf.Abs(Mathf.Sin(Time.time / slower)));
        }
    }
}