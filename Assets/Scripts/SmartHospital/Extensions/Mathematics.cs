using UnityEngine;

namespace SmartHospital.Extensions {
    public static class Mathematics {
        public const float PiBy4 = Mathf.PI / 4;
        public const float PiBy2 = Mathf.PI / 2;
        public const float PiTimes2 = Mathf.PI * 2;

        /// <summary>
        ///     Calculates the polar coordinates as a Vector2 with the given parameters.
        /// </summary>
        /// <param name="radius">Radius of the circle. Can be seen as the distance between origin and desired point.</param>
        /// <param name="radianAngle">Angle in radians to the desired point.</param>
        /// <returns>Vector2 containing the polar coordinates.</returns>
        public static Vector2 CalculatePolarCoordinates(float radius, float radianAngle) =>
            new Vector2(radius * Mathf.Cos(radianAngle), radius * Mathf.Sin(radianAngle));

        /// <summary>
        /// </summary>
        /// <param name="angleInRadian"></param>
        /// <returns></returns>
        public static float ConvertTo2Pi(float angleInRadian) {
            if (angleInRadian > Mathf.PI || angleInRadian > 0) {
                return angleInRadian;
            }

            return 2 * Mathf.PI + angleInRadian;
        }

        /// <summary>
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static float CalculateDistance(int x1, int y1, int x2, int y2) =>
            Mathf.Abs(Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2)));

        /// <summary>
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static float CalculateDistance(float x1, float y1, float x2, float y2) =>
            Mathf.Abs(Mathf.Sqrt(Mathf.Pow(x2 - x1, 2) + Mathf.Pow(y2 - y1, 2)));

        /// <summary>
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <returns></returns>
        public static float CalculateDeltaAngle(Vector3 pos1, Vector3 pos2) =>
            CalculateDeltaAngle(pos1.x, pos1.z, pos2.x, pos2.z);

        /// <summary>
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public static float CalculateDeltaAngle(float x1, float y1, float x2, float y2) =>
            Mathf.Atan2(y2 - y1, x2 - x1);
    }
}