using SmartHospital.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.UI.ColorPicker {
    [RequireComponent(typeof(Image))]
    public sealed class ColorWheel : MonoBehaviour {
        Vector2 center;
        Image image;
        RectTransform imageRectTransform;
        int resolution = 512;

        float v;

        public float Value {
            get { return v; }
            set {
                v = Mathf.Clamp01(value);
                UpdateWheel();
            }
        }

        public int Resolution {
            get { return resolution; }
            set {
                resolution = value;
                center = new Vector2(resolution / 2f, resolution / 2f);
                BuildWheel();
            }
        }

        void Awake() {
            image = GetComponent<Image>();
            imageRectTransform = image.GetComponent<RectTransform>();
        }

        void Start() {
            center = new Vector2(resolution / 2f, resolution / 2f);
            BuildWheel();
        }

        Texture2D GenerateTexture(int edgeLength) {
            var texture = new Texture2D(edgeLength, edgeLength);

            for (var i = texture.width - 1; i >= 0; i--) {
                for (var j = texture.height - 1; j >= 0; j--) {
                    texture.SetPixel(j, i, GetColorAt(j, i));
                }
            }

            texture.Apply();

            return texture;
        }

        public Color GetColorAtTransformedCoordinates(float x, float y, float? value = null) {
            return GetColorAt(TransformCoordinatesToTextureResolution(x, TransformMode.X),
                TransformCoordinatesToTextureResolution(y, TransformMode.Y), value);
        }

        public Color GetColorAt(Vector2 coordinates, float? value = null) {
            return GetColorAt(coordinates.x, coordinates.y, value);
        }

        public Color GetColorAt(float x, float y, float? value = null) {
            var center = resolution / 2;

            if (Mathematics.CalculateDistance(x, y, center, center) > center) {
                return Color.clear;
            }

            print(Mathematics.ConvertTo2Pi(Mathematics.CalculateDeltaAngle(center, center, x, y)));
            return Color.HSVToRGB(
                Mathematics.ConvertTo2Pi(Mathematics.CalculateDeltaAngle(center, center, x, y)) / Mathematics.PiTimes2,
                Mathematics.CalculateDistance(x, y, center, center) / center, value ?? v);
        }

        public float TransformCoordinatesToTextureResolution(float number, TransformMode mode) {
            switch (mode) {
                case TransformMode.X: return number * (resolution / imageRectTransform.rect.width);
                case TransformMode.Y: return number * (resolution / imageRectTransform.rect.height);
                default:
                    Debug.LogError("Fatal Error in ColorWheel");
                    return -1f;
            }
        }

        public Vector2 GetCoordinatesOfColor(Color color) {
            float hue, saturation, value;

            Color.RGBToHSV(color, out hue, out saturation, out value);

            return GetCoordinatesOfColor(hue, saturation);
        }

        public Vector2 GetCoordinatesOfColor(float hue, float saturation) =>
            (Mathematics.CalculatePolarCoordinates(saturation * (resolution / 2f), hue * 360f * Mathf.Deg2Rad) +
             center) * (imageRectTransform.rect.width / resolution);

        public void BuildWheel() {
            image.sprite = Sprite.Create(GenerateTexture(resolution),
                new Rect(0f, 0f, resolution, resolution), new Vector2(0.5f, 0.5f));
        }

        void UpdateWheel() {
            image.color = Color.HSVToRGB(0, 0, v);
        }

        public enum TransformMode {
            X,
            Y
        }
    }
}