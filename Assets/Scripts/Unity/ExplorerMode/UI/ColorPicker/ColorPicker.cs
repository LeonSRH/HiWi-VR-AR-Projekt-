using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SmartHospital.UI.ColorPicker {
    public sealed class ColorPicker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler,
        IDragHandler, IEndDragHandler, IScrollHandler {
        
        public delegate void ChangeColor(Color newColor);

        public delegate void EndColorChange(Color finalColor);
        
        public event ChangeColor OnColorChange;

        public event EndColorChange OnEndColorChange;

        ColorWheel colorWheel;
        RectTransform colorWheelRectTransform;
        Vector2 currentPosition;
        bool isDraggable;
        Color pickedColor;
        Picker picker;
        RectTransform pickerRectTransform;

        Slider valueSlider;

        public Color Color {
            get { return pickedColor; }
            set { SetColor(value); }
        }

        public void OnDrag(PointerEventData eventData) {
            HandleClickOrDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData) {
            HandleEndDrag(eventData);
        }

        public void OnPointerClick(PointerEventData eventData) {
            HandleClickOrDrag(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            isDraggable = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            isDraggable = false;
        }

        public void OnScroll(PointerEventData eventData) {
            throw new System.NotImplementedException();
        }

        void Awake() {
            valueSlider = GetComponentInChildren<Slider>();
            colorWheel = GetComponentInChildren<ColorWheel>();
            colorWheelRectTransform = colorWheel.GetComponent<RectTransform>();
            picker = GetComponentInChildren<Picker>();
            pickerRectTransform = picker.GetComponent<RectTransform>();
        }
        
        void Start() {
            valueSlider.onValueChanged.AddListener(value => {
                colorWheel.Value = value;
                OnColorChange?.Invoke(colorWheel.GetColorAt(currentPosition.x, currentPosition.y));
            });
            colorWheel.Value = valueSlider.value;

            var color = Color.red;
            float hue, saturation, v;
            
            Color.RGBToHSV(color, out hue, out saturation, out v);

            print(colorWheel.GetColorAt(colorWheel.GetCoordinatesOfColor(hue, saturation)));
            print(color);
            
            style = new GUIStyle {
                fontSize = 50
            };
            rect = new Rect(Screen.width / 2 - 100, Screen.height / 2 - 12.5f, 200, 25);
        }

        GUIStyle style;
        Rect rect;
        
        void OnGUI() {
            style.normal.textColor = pickedColor;
            GUI.Label(rect, "Rainbow", style);
        }

        void HandleClickOrDrag(PointerEventData eventData) {
           
            CalculateColor(eventData);

            if (pickedColor == Color.clear) {
                return;
            }
            
            picker.transform.position = Input.mousePosition;
            OnColorChange?.Invoke(pickedColor);
        }
        
        void HandleEndDrag(PointerEventData eventData) {
            CalculateColor(eventData);

            if (pickedColor == Color.clear) {
                Debug.LogError("Invalid color in color picker");
                return;
            }
            
            picker.transform.position = colorWheel.GetCoordinatesOfColor(pickedColor);
            
            OnEndColorChange?.Invoke(pickedColor);
        }

        void CalculateColor(PointerEventData eventData) {
            if (!isDraggable) {
                return;
            }

            currentPosition = eventData.position - (Vector2) colorWheelRectTransform.position;
            currentPosition.y += colorWheelRectTransform.rect.height / 2;

            var localScale = (Vector2) colorWheelRectTransform.localScale;
            var canvasScale = GetComponentInParent<Canvas>().scaleFactor;
            
            currentPosition = Vector2.Scale(currentPosition, localScale);
            currentPosition *= canvasScale;

            if (currentPosition.x < 0 || currentPosition.y < 0) {
                return;
            }

            var color = colorWheel.GetColorAt(currentPosition.x, currentPosition.y);

            pickedColor = color;
        }

        void SetColor(Color color) {
            pickedColor = color;

            float hue, saturation, value;

            Color.RGBToHSV(color, out hue, out saturation, out value);

            pickerRectTransform.anchoredPosition = colorWheel.GetCoordinatesOfColor(hue, saturation) -
                                                    colorWheelRectTransform.rect.size / 2;
            valueSlider.value = value;
        }
    }
}