using UnityEngine;

namespace SmartHospital.UI {
    public class GuiUtility {
        public static Rect GetGuiCenterRect(float width, float height) => new Rect(Screen.width / 2 - width / 2,
            Screen.height / 2 - height / 2, width, height);
    }
}