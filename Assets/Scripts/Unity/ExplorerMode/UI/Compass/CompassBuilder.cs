using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital.UI.Compass {
    public class CompassBuilder : MonoBehaviour {
        public List<CompassBar> BuildCompass(CompassBar prefab, Transform parent, CompassMode mode, float angleBetween,
            float angleBetweenLabels = 10f) => BuildCompass(prefab, parent, mode, (int) (360f / angleBetween),
            angleBetween, angleBetweenLabels);

        public List<CompassBar> BuildCompass(CompassBar prefab, Transform parent, CompassMode mode, int amount,
            float angleBetweenLabels = 10f) =>
            BuildCompass(prefab, parent, mode, amount, 360f / amount, angleBetweenLabels);

        List<CompassBar> BuildCompass(CompassBar prefab, Transform parent, CompassMode mode, int amount,
            float angleBetween, float angleBetweenLabels) {
            var list = new List<CompassBar>();

            for (var i = 0; i < amount; i++) {
                var current = Instantiate(prefab, parent);
                var currentAngle = i * angleBetween;

                current.gameObject.name = "CompassBar_" + currentAngle;
                current.Angle = currentAngle;
                current.ApplyCompassMode(mode);

                list.Add(current);
            }

            return list;
        }
    }

    public class CompassMode {
        public static readonly CompassMode NOLABELS = new CompassMode(new Dictionary<float, string>());

        public static readonly CompassMode BASICLABELS = new CompassMode(new Dictionary<float, string> {
            {0f, "N"},
            {90f, "O"},
            {180f, "S"},
            {360f, "W"}
        });

        public static readonly CompassMode ADVANCEDLABELS = new CompassMode(new Dictionary<float, string> {
            {0f, "N"},
            {45f, "NO"},
            {90f, "O"},
            {135f, "SO"},
            {180f, "S"},
            {225f, "SW"},
            {270f, "W"},
            {315f, "NW"}
        });

        public static readonly CompassMode MOSTLABELS = new CompassMode(new Dictionary<float, string> {
            {0f, "N"},
            {22.5f, "NNO"},
            {45f, "NO"},
            {67.5f, "ONO"},
            {90f, "O"},
            {112.5f, "OSO"},
            {135f, "SO"},
            {157.5f, "SSO"},
            {180f, "S"},
            {202.5f, "SSW"},
            {225f, "SW"},
            {247.5f, "WSW"},
            {270f, "W"},
            {292.5f, "WNW"},
            {315f, "NW"},
            {337.5f, "NNW"}
        });

        CompassMode(Dictionary<float, string> labels) {
            Labels = labels;
        }

        public Dictionary<float, string> Labels { get; }
    }
}