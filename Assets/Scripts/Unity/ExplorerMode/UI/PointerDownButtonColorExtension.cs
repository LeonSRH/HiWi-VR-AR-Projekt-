using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.UI {

    [RequireComponent(typeof(PointerDownButton))]
    [RequireComponent(typeof(Image))]
    public class PointerDownButtonColorExtension : MonoBehaviour {
        public Color PrimaryColor;
        public Color HoverColor;
        public Color SelectedColor;

        PointerDownButton _button;
        Image _background;

        void Awake() {
            _button = GetComponent<PointerDownButton>();
            _background = GetComponent<Image>();
        }
    }

}