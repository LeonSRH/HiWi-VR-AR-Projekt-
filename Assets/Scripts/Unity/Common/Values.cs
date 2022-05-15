using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital
{
    public class Values : MonoBehaviour
    {
        public enum Platform
        {
            Editor,
            Standalone,
            WebGL,
            Other
        }

        static Values()
        {
#if (UNITY_EDITOR)
            CurrentPlatform = Platform.Editor;
#elif (UNITY_WEBGL)
        CurrentPlaftorm = Platform.WebGL;
#elif (UNITY_STANDALONE || UNITY_ANDROID || UNITY_IOS)
        CurrentPlatform = Platform.Standalone;
#else
        CurrentPlatform = Platform.Other;
#endif
        }

        public static Dictionary<int, Vector3> CameraPositions { get; } = new Dictionary<int, Vector3> {
            {int.MinValue, new Vector3(0f, 20f, -85.6f)},
            {-2, new Vector3(-7.5f, 144.7f, -80.5f)},
            {-1, new Vector3(-7.5f, 144.7f, -80.5f)},
            {0, new Vector3(-7.5f, 144.7f, -80.5f)},
            {1, new Vector3(-7.5f, 141f, -84.1f)},
            {2, new Vector3(-3.37f, 125f, -83f)},
            {3, new Vector3(-3.37f, 83f, -121.61f)}
        };

        public static Dictionary<int, string> FloorSceneNames { get; } = new Dictionary<int, string> {
            {int.MinValue, "TestScene"},
            {-2, "98_Floor"},
            {-1, "99_Floor"},
            {0, "0_Floor"},
            {1, "1_Floor_New"},
            {2, "2_Floor_New"},
            {3, "3_Floor_New"}
        };

        public static Vector3 CameraNorthRotation { get; } = new Vector3(90, 0, 0);

        public static float LightIntensity3D { get; } = 0.8f;

        public static float LightIntensity2D { get; } = 0.6f;

        public static Vector3 LightRotation3D { get; } = new Vector3(60, 45, 0);

        public static Vector3 LightRotation2D { get; } = new Vector3(90, 0, 0);

        public static Platform CurrentPlatform { get; }
    }
}