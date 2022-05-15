using UnityEngine;
using UnityEditor;
using SmartHospital.UI;

namespace SmartHospital.Inspectors {

    [CustomEditor(typeof(SwitcherPanel))]
    public sealed class SwitcherPanelInspector : Editor {
        // ReSharper disable once UnusedMember.Local
        void CustomUI() {
            var switcherPanel = (SwitcherPanel) target;

            switcherPanel.ButtonSprite = EditorGUILayout.ObjectField("Sprite", switcherPanel.ButtonSprite,
                typeof(Sprite),
                false, GUILayout.MaxHeight(EditorGUIUtility.singleLineHeight)) as Sprite;
            switcherPanel.ButtonFont =
                EditorGUILayout.ObjectField("Font", switcherPanel.ButtonFont, typeof(Font), false) as Font;

            EditorGUILayout.Space();

            switcherPanel.DeselectedColor =
                EditorGUILayout.ColorField("Deselected Color", switcherPanel.DeselectedColor);
            switcherPanel.DeselectedFontColor =
                EditorGUILayout.ColorField("Deselected Font Color", switcherPanel.DeselectedFontColor);

            EditorGUILayout.Space();

            switcherPanel.SelectedColor = EditorGUILayout.ColorField("Selected Color", switcherPanel.SelectedColor);
            switcherPanel.SelectedFontColor =
                EditorGUILayout.ColorField("Selected Font Color", switcherPanel.SelectedFontColor);

            EditorGUILayout.Space();

            GUILayout.Label("Tab List:");
        }
    }
}