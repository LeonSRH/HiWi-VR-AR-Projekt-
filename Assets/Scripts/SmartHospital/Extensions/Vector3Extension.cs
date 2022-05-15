using UnityEngine;

namespace SmartHospital.Extensions {
    public static class Vector3Extension {
        public static bool CheckInRange(this Vector3 current, Vector3 target, float distance,
            Vector3ApproximatelyMode mode = Vector3ApproximatelyMode.All) {
            if (mode == Vector3ApproximatelyMode.All || mode == Vector3ApproximatelyMode.XY ||
                mode == Vector3ApproximatelyMode.XZ || mode == Vector3ApproximatelyMode.X) {
                if (Mathf.Abs(target.x - current.x) > distance) {
                    return false;
                }
            }

            if (mode == Vector3ApproximatelyMode.All || mode == Vector3ApproximatelyMode.XY ||
                mode == Vector3ApproximatelyMode.YZ || mode == Vector3ApproximatelyMode.Y) {
                if (Mathf.Abs(target.y - current.y) > distance) {
                    return false;
                }
            }

            if (mode != Vector3ApproximatelyMode.All && mode != Vector3ApproximatelyMode.XZ &&
                mode != Vector3ApproximatelyMode.YZ && mode != Vector3ApproximatelyMode.Z) {
                return true;
            }

            return !(Mathf.Abs(target.z - current.z) > distance);
        }

        public static Vector3 Clamp(this Vector3 value, Vector3 min, Vector3 max) {
            value.x = Mathf.Clamp(value.x, min.x, max.x);
            value.y = Mathf.Clamp(value.y, min.y, max.y);
            value.z = Mathf.Clamp(value.z, min.z, max.z);

            return value;
        }

        public static Vector3 LerpRotation(this Vector3 a, Vector3 b, float interpolant) =>
            Clamp(CalculateDeltaVector(a, b) * interpolant + a, a, b);

        static Vector3 CalculateDeltaVector(Vector3 a, Vector3 b) {
            a.x = Mathf.DeltaAngle(a.x, b.x);
            a.y = Mathf.DeltaAngle(a.y, b.y);
            a.z = Mathf.DeltaAngle(a.z, b.z);

            return a;
        }

        public static float CalculatePathLength(this Vector3[] vectors) {
            var combinedLength = 0f;

            for (var i = 1; i < vectors.Length; i++) {
                combinedLength += (vectors[i] - vectors[i - 1]).magnitude;
            }

            return combinedLength;
        }
    }

    public enum Vector3ApproximatelyMode {
        All,
        XY,
        XZ,
        YZ,
        X,
        Y,
        Z
    }
}