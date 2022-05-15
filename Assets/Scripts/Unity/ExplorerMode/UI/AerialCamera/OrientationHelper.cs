using SmartHospital.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.UI.TopCameraPanel {
    public class OrientationHelper : MonoBehaviour {
        Image arrowImage;

        RectTransform arrowRectTransform;
        public RectTransform secondCameraView;
        public Camera targetCamera;
        public Transform targetObject;


        void Awake() {
            arrowRectTransform = GetComponent<RectTransform>();
            arrowImage = GetComponentInChildren<Image>();
        }

        void Update() {
            //Vector3 viewPortPoint = targetCamera.WorldToViewportPoint(targetObject.position);

            //if (!(viewPortPoint.x.InClosedInterval(0, 1) && viewPortPoint.y.InClosedInterval(0, 1))) {

            arrowImage.enabled = true;

            var deltaAngle = CalculateDeltaAngle();

            arrowRectTransform.anchoredPosition = CalculateArrowPosition(deltaAngle);
            ApplyRotation(deltaAngle);
            //}
            //else {

            //    arrowImage.enabled = false;
            //}
        }

        void ApplyRotation(float deltaAngle) {
            var rotation = arrowRectTransform.eulerAngles;
            rotation.z = deltaAngle * Mathf.Rad2Deg - 90;
            arrowRectTransform.eulerAngles = rotation;
        }

        float CalculateDeltaAngle() =>
            Mathematics.ConvertTo2Pi(Mathematics.CalculateDeltaAngle(targetCamera.transform.position,
                targetObject.position));

        Vector2 CalculateArrowPosition(float deltaAngleRad) =>
            Mathematics.CalculatePolarCoordinates(CalculateHypothenuse(deltaAngleRad), deltaAngleRad);

        float CalculateHypothenuse(float deltaAngleRad) {
            float len;

            if (deltaAngleRad.InOpenInterval(Mathematics.PiBy4, Mathematics.PiBy4 * 3) ||
                deltaAngleRad.InOpenInterval(Mathematics.PiBy4 * 5, Mathematics.PiBy4 * 7)) {
                len = secondCameraView.rect.height / 2;
            }
            else {
                len = secondCameraView.rect.width / 2;
            }

            if (deltaAngleRad.InOpenInterval(Mathematics.PiBy4, Mathematics.PiBy4 * 2) ||
                deltaAngleRad.InOpenInterval(Mathematics.PiBy4 * 3, Mathf.PI) ||
                deltaAngleRad.InOpenInterval(Mathematics.PiBy4 * 5, Mathematics.PiBy4 * 6) ||
                deltaAngleRad.InOpenInterval(Mathematics.PiBy4 * 7, Mathf.PI * 2)) {
                deltaAngleRad = Mathematics.PiBy4 - deltaAngleRad % Mathematics.PiBy4;
            }
            else {
                deltaAngleRad %= Mathematics.PiBy4;
            }

            return len / Mathf.Cos(deltaAngleRad);
        }
    }
}