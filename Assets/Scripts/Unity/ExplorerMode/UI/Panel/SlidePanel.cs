using System.Collections;
using SmartHospital.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SmartHospital.UI {

    /// <inheritdoc />
    /// <summary>
    ///     Class defines a panel that can slide in and out from and to specified points of the UI.
    /// </summary>
    public class SlidePanel : UIBehaviour {
#pragma warning disable CS0414 // Dem Feld "SlidePanel._duration" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        float _duration = 2f;
#pragma warning restore CS0414 // Dem Feld "SlidePanel._duration" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        RectTransform _rectTransform;

        bool _shown;
        RectTransform _triggerRectTransform;
        public float AnimationSpeed = 1.5f;
        public Vector2 DisabledPosition;
        public Vector2 NotShownPosition;
        public Vector2 ShownPosition;
        public PointerDownButton Trigger;
        public float TriggerInRotation;
        public float TriggerOutRotation = 180f;

        protected override void Awake() {
            _rectTransform = GetComponent<RectTransform>();

            if (Trigger) {
                RegisterTrigger(Trigger);
            }
            else {
                var buttons = GetComponentsInChildren<PointerDownButton>();

                for (var i = 0; i < buttons.Length; i++) {
                    var button = buttons[i];
                    if (button.name == "TriggerButton") {
                        RegisterTrigger(button);
                    }
                }
            }
        }

        protected override void Start() {
            if (_triggerRectTransform) {
                _triggerRectTransform.localEulerAngles =
                    _shown ? new Vector3(0, 0, TriggerInRotation) : new Vector3(0, 0, TriggerOutRotation);
            }
        }

        public void RegisterTrigger(PointerDownButton trigger) {
            if (!trigger) {
                return;
            }

            _triggerRectTransform = trigger.GetComponent<RectTransform>();
            trigger.OnPointerDownEvent.AddListener(eventData => Show(!_shown));
        }

        public void TogglePosition() {
            Show(!_shown);
        }

        /// <summary>
        ///     This method toggles the isActive state of the gameObject based on the passed boolean.
        /// </summary>
        /// <param name="toggle">The state in which the gamobject is set.</param>
        public void Toggle(bool toggle) {
            gameObject.SetActive(toggle);
        }

        void Show(bool show) {
            if (_shown == show) {
                return;
            }

            _shown = show;
            StopAllCoroutines();
            StartCoroutine(ShowAnimation(show));
        }

        IEnumerator ToggleAnimation(bool toggle) {
            var endPosition = toggle ? NotShownPosition : DisabledPosition;

            while (!_rectTransform.anchoredPosition.CheckInRange(endPosition, 0.3f)) {
                var interpolant = Time.deltaTime * AnimationSpeed;
                _rectTransform.anchoredPosition =
                    Vector2.Lerp(_rectTransform.anchoredPosition, endPosition, interpolant);

                yield return new WaitForEndOfFrame();
            }
        }

        IEnumerator ShowAnimation(bool show) {
            Vector2 endPosition;
            float endRotation;

            if (show) {
                endPosition = ShownPosition;
                endRotation = TriggerInRotation;
            }
            else {
                endPosition = NotShownPosition;
                endRotation = TriggerOutRotation;
            }

            while (!_rectTransform.anchoredPosition.CheckInRange(endPosition, 0.3f)) {
                var interpolant = Time.deltaTime * AnimationSpeed;
                _rectTransform.anchoredPosition =
                    Vector2.Lerp(_rectTransform.anchoredPosition, endPosition, interpolant);

                var currentRotation = _triggerRectTransform.localEulerAngles;
                currentRotation.z = Mathf.LerpAngle(currentRotation.z, endRotation, interpolant);
                _triggerRectTransform.localEulerAngles = currentRotation;

                yield return new WaitForEndOfFrame();
            }
        }
    }

}