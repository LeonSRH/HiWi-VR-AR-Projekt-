using UnityEngine;

namespace SmartHospital.Extensions {
    public static class FloatExtension {
        public static bool InClosedInterval(this float actual, float min, float max) => actual >= min && actual <= max;

        public static bool InOpenInterval(this float actual, float min, float max) => actual > min && actual < max;

        public static bool InHalfOpenIntervalL(this float actual, float min, float max) =>
            actual > min && actual <= max;

        public static bool InHalfOpenIntervalR(this float actual, float min, float max) =>
            actual >= min && actual < max;

        public static bool DeltaInRange(this float first, float second, float range) =>
            Mathf.Abs(second - first) <= range;
    }
}