using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SmartHospital.UI {
    public class PointerDownButton : UIBehaviour, IPointerDownHandler {
        public UnityEvent OnClick;

        public PointerDownButton() {
            OnPointerDownEvent = new PointerDownEvent();
        }

        public PointerDownEvent OnPointerDownEvent { get; }

        public void OnPointerDown(PointerEventData eventData) {
            OnPointerDownEvent.Invoke(eventData);
            OnClick.Invoke();
        }

        public class PointerDownEvent : UnityEvent<PointerEventData> { }
    }
}