using UnityEngine.AI;

namespace SmartHospital.Extensions {
    public static class NavMeshPathExtensions {
        public static float CalculateLength(this NavMeshPath path) => path.corners.CalculatePathLength();
    }
}