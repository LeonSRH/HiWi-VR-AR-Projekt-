using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.StartMenu {
    public class ButtonHandler : MonoBehaviour {
        public Button button00;
        public Button button01;
        public Button button02;
        public Button button03;

        public Button button98;
        public Button button99;

        void Start() {
            button98.onClick.AddListener(() => HandleClick(-2));
            button99.onClick.AddListener(() => HandleClick(-1));
            button00.onClick.AddListener(() => HandleClick(0));
            button01.onClick.AddListener(() => HandleClick(1));
            button02.onClick.AddListener(() => HandleClick(2));
            button03.onClick.AddListener(() => HandleClick(3));
        }

        void HandleClick(int floor) {
            GameObject.Find("Setup").GetComponent<SetupModule>().TakeUserInput(floor);
        }
    }
}