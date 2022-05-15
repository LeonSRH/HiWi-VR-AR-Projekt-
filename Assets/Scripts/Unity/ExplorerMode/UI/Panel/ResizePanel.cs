using UnityEngine;
using UnityEngine.EventSystems;

namespace SmartHospital.UI {
    public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler {
        public Vector2 maxSize = new Vector2(400, 400);

        public Vector2 minSize = new Vector2(100, 100);

        Vector2 originalLocalPointerPosition;
        Vector2 originalSizeDelta;

        public RectTransform panelRectTransform;

        public void OnDrag(PointerEventData data) {
            if (!panelRectTransform) {
                return;
            }

            Vector2 localPointerPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position,
                data.pressEventCamera, out localPointerPosition);
            Vector3 offsetToOriginal = localPointerPosition - originalLocalPointerPosition;

            var sizeDelta = originalSizeDelta + new Vector2(-offsetToOriginal.x, -offsetToOriginal.y);

            sizeDelta = new Vector2(
                Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
                Mathf.Clamp(sizeDelta.x, minSize.y, maxSize.y)
            );

            panelRectTransform.sizeDelta = sizeDelta;
        }

        public void OnPointerDown(PointerEventData data) {
            originalSizeDelta = panelRectTransform.sizeDelta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(panelRectTransform, data.position,
                data.pressEventCamera, out originalLocalPointerPosition);
        }

        void Awake() {
            //panelRectTransform = transform.parent.GetComponent<RectTransform>();
        }
    }
}