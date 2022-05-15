using UnityEngine;

namespace SmartHospital.Extensions {
    public static class Vector2Extension {
        public static bool CheckInRange(this Vector2 current, Vector2 target, float distance,
            Vector2ApproximatelyMode mode = Vector2ApproximatelyMode.All) {
            if (mode == Vector2ApproximatelyMode.All || mode == Vector2ApproximatelyMode.X) {
                if (Mathf.Abs(target.x - current.x) > distance) {
                    return false;
                }
            }

            if (mode != Vector2ApproximatelyMode.All && mode != Vector2ApproximatelyMode.Y) {
                return true;
            }

            return !(Mathf.Abs(target.y - current.y) > distance);
        }
    }

    public enum Vector2ApproximatelyMode {
        All,
        X,
        Y
    }
}