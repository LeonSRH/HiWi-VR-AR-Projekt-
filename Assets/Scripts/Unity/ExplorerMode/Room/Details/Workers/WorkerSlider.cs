using System.Linq;
using SmartHospital.UI;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.Controller.ExplorerMode.Rooms.Details.Workers {

    public class WorkerSlider : MonoBehaviour {
        Slider _workerSlider;
        Image[] _workerIcons;

        // Use this for initialization
        void Awake() {
            SetupIcons();
            SetupSlider();
        }


        void SetupIcons() {
            _workerIcons = GetComponentsInChildren<Image>().Where(i => i.name == "Icon").ToArray();
        }

        void SetupSlider() {
            _workerSlider = GetComponentInChildren<Slider>();
            _workerSlider.onValueChanged.AddListener(newValue => {
                for (var i = 0; i < newValue; i++) {
                    _workerIcons[i].enabled = true;
                }

                for (var i = (int) newValue; i < _workerIcons.Length; i++) {
                    _workerIcons[i].enabled = false;
                }
            });
            _workerSlider.value = 2f;
        }

        public PointerDownButton[] Buttons =>
            _workerIcons.Select(icon => icon.GetComponent<PointerDownButton>()).ToArray();

        public int Value {
            get { return (int) _workerSlider.value; }
            set { _workerSlider.value = value; }
        }

        public void EnableButtons() {
            foreach (var workerSliderButton in Buttons) {
                workerSliderButton.enabled = true;
            }
        }

        public void DisableButtons() {
            foreach (var workerSliderButton in Buttons) {
                workerSliderButton.enabled = true;
            }
        }
    }

}