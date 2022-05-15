using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.UI {
    public class LoadingPanel : MonoBehaviour {
        Animator loadingAnimator;
        Slider loadingSlider;

        Text loadingText;

        void Awake() {
            loadingText = GetComponentInChildren<Text>();
            loadingSlider = GetComponentInChildren<Slider>();
            loadingAnimator = GetComponentInChildren<Animator>();
        }

        public void SetEnabled(bool panelEnabled) {
            gameObject.SetActive(panelEnabled);
        }

        public void SetText(string text) {
            loadingText.text = text;
        }

        public void SetProgress(float value) {
            loadingSlider.value = value;
        }

        public void EnableAnimation(bool enabled) {
            loadingAnimator.SetBool("Loading", enabled);
        }
    }
}