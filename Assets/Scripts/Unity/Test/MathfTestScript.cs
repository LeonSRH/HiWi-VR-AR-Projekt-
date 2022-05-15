using SmartHospital.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.Test_Debug {
    public class MathfTestScript : MonoBehaviour {
        public float angleA;
        public float angleB;
        public Text approximateResult;
        public float clampFMax;
        public float clampFMin;

        public float clampFValue;
        public int clampIMax;
        public int clampIMin;
        public int clampIValue;

        public Text clampResult;

        public Vector3 clampVector;
        public Vector3 clampVectorMax;
        public Vector3 clampVectorMin;
        public Text deltaAngleResult;

        public float floatA;
        public float floatB;
        public float lerpInterpolant;

        public Vector3 lerpVectorA;
        public Vector3 lerpVectorB;
        public Text vectorClampResult;
        public Text vectorLerpResult;

        void Update() {
            clampResult.text = "Clamp: Float: " + Mathf.Clamp(clampFValue, clampFMin, clampFMax) + ", Int: " +
                               Mathf.Clamp(clampIValue, clampIMin, clampIMax);

            vectorLerpResult.text = "Lerp: Actual: " + Vector3.Lerp(lerpVectorA, lerpVectorB, lerpInterpolant) +
                                    ", Anticipated: " + AnticipatedLerp(lerpVectorA, lerpVectorB, lerpInterpolant);

            deltaAngleResult.text = "DeltaAngle between " + angleA + " and " + angleB + " = (Actual: " +
                                    Mathf.DeltaAngle(angleA, angleB) + ", Anticipated: " +
                                    AnticipatedDeltaAngle(angleA, angleB) + ")";

            approximateResult.text = "Mathf.Approximately(" + floatA + ", " + floatB + ") = " +
                                     Mathf.Approximately(floatA, floatB);

            vectorClampResult.text = "Vector3.Clamp(a, b) = " + clampVector.Clamp(clampVectorMin, clampVectorMax);
        }

        Vector3 AnticipatedLerp(Vector3 a, Vector3 b, float t) => (b - a) * t + a;

        float AnticipatedDeltaAngle(float a, float b) => a % 360 + b % 360;
    }
}