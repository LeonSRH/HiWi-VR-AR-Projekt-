using UnityEngine;

namespace SmartHospital.Test_Debug {
    [ExecuteInEditMode]
    public class EditorTestScript : MonoBehaviour {
        public string result;

        public int x;
        public int y;

        void Update() {
            result = $"{Mathf.Atan(y / x) * Mathf.Rad2Deg} {Mathf.Atan2(y, x) * Mathf.Rad2Deg}";
        }
    }
}