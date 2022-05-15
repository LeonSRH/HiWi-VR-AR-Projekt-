using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.UI.Compass {
    public class CompassBar : MonoBehaviour {
        public const string BarName = "Bar";
        public const string TextName = "Text";

        float angle;
        RectTransform bar;
        RectTransform barImage;
        public float barWidthHighlighted = 3f;
        public float barWidthNotHighlighted = 1.5f;
        public Color colorHighlighted; //= new Color(68, 138, 255);
        public Color colorNotHighlighted = Color.white;

        Vector2 position;

        Text text;
        public int textSizeHighlighted = 12;
        public int textSizeNotHighlighted = 9;

        public float Angle {
            get { return angle; }
            set {
                angle = value;
                text.text = value.ToString(CultureInfo.CurrentCulture);
            }
        }

        public float BarWidth {
            get { return barImage.sizeDelta.x; }
            set { barImage.sizeDelta = new Vector2(value, barImage.sizeDelta.y); }
        }

        public float BarHeight {
            get { return barImage.sizeDelta.y; }
            set { barImage.sizeDelta = new Vector2(barImage.sizeDelta.x, value); }
        }

        public float Position => position.x;

        string Text {
            get { return text.text; }
            set { text.text = value; }
        }

        void Awake() {
            bar = transform.GetComponent<RectTransform>();
            position = bar.anchoredPosition;

            foreach (Transform t in transform) {
                if (t.name == TextName) {
                    text = t.GetComponent<Text>();
                }
                else if (t.name == BarName) {
                    barImage = t.GetComponent<RectTransform>();
                }
            }
        }


        void SetHighlighted(bool highlighted) {
            if (highlighted) {
                var size = barImage.sizeDelta;
                size.x = barWidthHighlighted;
                barImage.sizeDelta = size;
                barImage.GetComponent<RawImage>().color = colorHighlighted;

                text.fontStyle = FontStyle.Normal;
                text.fontSize = textSizeHighlighted;
                text.color = colorHighlighted;
            }
            else {
                var size = barImage.sizeDelta;
                size.x = barWidthNotHighlighted;
                barImage.sizeDelta = size;
                barImage.GetComponent<RawImage>().color = colorNotHighlighted;

                text.fontStyle = FontStyle.Normal;
                text.fontSize = textSizeNotHighlighted;
                text.color = colorNotHighlighted;
            }
        }

        public void SetPosition(float x) {
            position.x = x;
            bar.anchoredPosition = position;
        }

        public void SetVisible(bool visible) {
            barImage.gameObject.SetActive(visible);
            text.gameObject.SetActive(visible);
        }

        public void ApplyCompassMode(CompassMode mode) {
            foreach (var label in mode.Labels) {
                if (Angle == label.Key) {
                    Text = label.Value;
                    SetHighlighted(true);
                    break;
                }

                SetHighlighted(false);
            }
        }
    }
}