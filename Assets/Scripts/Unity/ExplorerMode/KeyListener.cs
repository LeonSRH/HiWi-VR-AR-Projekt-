using SmartHospital.Common;
using UnityEngine;

namespace SmartHospital.ExplorerMode
{
    public class KeyListener : BaseController {
        uint counter;
        bool testAllowed = true;
        public uint testDelay = 150;


        public KeyCode testKey = KeyCode.T;

        void Start() {
        }

        void Update() {
            if (testAllowed) {
                if (!Input.GetKey(testKey)) {
                    return;
                }
                
                testAllowed = false;
            }
            else {
                if (counter <= 0) {
                    counter = testDelay;
                    testAllowed = true;
                }
                else {
                    counter--;
                }
            }
        }
    }
}