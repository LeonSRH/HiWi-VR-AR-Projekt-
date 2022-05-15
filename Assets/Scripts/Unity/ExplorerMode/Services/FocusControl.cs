using System.Collections;
using SmartHospital.Common;
using SmartHospital.ExplorerMode.CameraControl;
using SmartHospital.Extensions;
using SmartHospital.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SmartHospital.ExplorerMode.Services {
    public class FocusControl : BaseController {
        const uint Delay = 1;
        const float MaxDarkness = 0.8f;
        const float MinDarkness = 0f;
        const float DarkenerBoost = 1.1f;

        public PointerDownButton activateButton;
        public CameraManager cameraControl;
        public SpriteRenderer darkener;
        bool currentlyPaused;
        float delayCounter;
        bool delayed;

        DimensionToggle dToggle;
        bool hasFocus;
        CursorLockMode pastCursorLM = CursorLockMode.None;

        void Awake() {
            dToggle = transform.parent.GetComponentInChildren<DimensionToggle>();

            activateButton.OnPointerDownEvent.AddListener(FocusCanvasOn);
        }

        void Start() {
            ToggleCursorKeyboardCamera(false);
        }

        void Update() {
            if (delayed) {
                delayCounter += Time.deltaTime;

                if (delayCounter >= Delay) {
                    delayCounter = 0;
                    delayed = false;
                }
            }

            if (!dToggle.Is3D || currentlyPaused) {
                return;
            }

            if (Cursor.lockState == CursorLockMode.None && pastCursorLM == CursorLockMode.Locked) {
                FocusCanvas(false, "CursorLock removed", true);
            }

            pastCursorLM = Cursor.lockState;
        }

        void FocusCanvasOn(PointerEventData eventData) {
            FocusCanvas(true, "ActivateButton");
        }

        IEnumerator Darken(bool darken) {
            var endDarkness = darken ? MaxDarkness : MinDarkness;

            while (!darkener.color.a.DeltaInRange(endDarkness, 0.03f)) {
                var currentColor = darkener.color;
                currentColor.a = Mathf.Lerp(darkener.color.a, endDarkness, Time.deltaTime * DarkenerBoost);
                darkener.color = currentColor;

                yield return new WaitForEndOfFrame();
            }
        }

        public void FocusCanvas(bool focusEnabled, string caller, bool paused = false) {
            Debug.Log("Unity: FocusCanvas(" + focusEnabled + ") called by " + caller + ", paused: " + paused);

            hasFocus = focusEnabled;

            currentlyPaused = paused;

            activateButton.gameObject.SetActive(currentlyPaused);
            StopAllCoroutines();
            StartCoroutine(Darken(currentlyPaused));

            ToggleCursorKeyboardCamera(hasFocus);
        }

        void ToggleCursorKeyboardCamera(bool on) {
            LockCursor(on);
            CaptureAllKeyboardInput(on);
            cameraControl.MainCamera.AllowInteraction = on;
        }

        static void LockCursor(bool locked) {
            Cursor.visible = !locked;

            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        }

        static void CaptureAllKeyboardInput(bool on) {
            #if !UNITY_EDITOR && UNITY_WEBGL
        WebGLInput.captureAllKeyboardInput = on;
            #endif
        }
    }
}