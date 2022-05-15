using System.Collections.Generic;
using SmartHospital.Extensions;
using UnityEngine;

namespace SmartHospital.UI.Compass {
    public class CompassController : MonoBehaviour {
        List<CompassBar> barList;
        public float fullSizeBorder = 0.9f;

        public Camera mainCamera;
        public Vector3 northRotation;
        public GameObject prefab;

        RectTransform rectTransform;

        void Start() {
            rectTransform = GetComponent<RectTransform>();

            var cb = GetComponent<CompassBuilder>();
            barList = cb.BuildCompass(GetComponentInChildren<CompassBar>(), transform, CompassMode.ADVANCEDLABELS, 10f,
                20f);
            Destroy(prefab);
        }

        void Update() {
            var currentRotation = mainCamera.transform.eulerAngles;
            var deltaAngle = Mathf.DeltaAngle(northRotation.y, currentRotation.y);

            //float deltaAngle = Mathf.Atan2(MainCamera.transform.right.z, MainCamera.transform.right.x) * Mathf.Rad2Deg;
            //float pixelPerAngle = (Screen.width * 6) / 360f;

            foreach (var cb in barList) {
                //cb.SetPosition(-(deltaAngle * pixelPerAngle * 3) + (pixelPerAngle * cb.Angle));
                // ReSharper disable once PossibleLossOfFraction
                var polarCoordinates = Mathematics.CalculatePolarCoordinates(rectTransform.rect.width * 0.8f,
                    (deltaAngle + cb.Angle) * Mathf.Deg2Rad);

                if (polarCoordinates.x < 0) {
                    cb.SetVisible(false);
                }
                else {
                    cb.SetVisible(true);
                    cb.SetPosition(-polarCoordinates.y);

                    if (cb.Position > fullSizeBorder * (rectTransform.sizeDelta.x / 2)) { }
                }

                //if (cb.Angle == 0f) {
                //print((deltaAngle + cb.Angle) * Mathf.Deg2Rad);
                //}
            }
        }
    }
}