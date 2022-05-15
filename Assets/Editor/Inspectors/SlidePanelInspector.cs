using UnityEditor;
using UnityEngine;
using SmartHospital.UI;

namespace SmartHospital.Inspectors {

    [CustomEditor(typeof(SlidePanel))]
    public class SlidePanelInspector : Editor {
        
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            var panel = (SlidePanel) target;

            if (GUILayout.Button("Set Current position as 'Shown' position")) {
                panel.ShownPosition = panel.GetComponent<RectTransform>().anchoredPosition;
            }

            if (GUILayout.Button("Set Current position as 'Not Shown' position")) {
                panel.NotShownPosition = panel.GetComponent<RectTransform>().anchoredPosition;
            }

            if (GUILayout.Button("Set Current position as 'Disabled' position")) {
                panel.DisabledPosition = panel.GetComponent<RectTransform>().anchoredPosition;
            }
        }
    }
}