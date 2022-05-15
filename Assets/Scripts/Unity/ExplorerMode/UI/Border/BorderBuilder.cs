using UnityEngine;

namespace SmartHospital.UI {
    public class BorderBuilder : MonoBehaviour {
        GameObject border;

        [SerializeField]
#pragma warning disable CS0649 // Dem Feld "BorderBuilder.BorderPrefab" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
        GameObject BorderPrefab;
#pragma warning restore CS0649 // Dem Feld "BorderBuilder.BorderPrefab" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".

        readonly float scale = 0.7f;

        void Start() {
            CreateBorderRect();
            ScalePrefab();

            AddTopLeftBorder();
            AddTopRightBorder();
            AddBottomLeftBorder();
            AddBottomRightBorder();
        }

        void CreateBorderRect() {
            border = new GameObject {
                name = "Border"
            };

            border.transform.parent = transform;
            border.transform.SetAsFirstSibling();
            var rectTransform = border.AddComponent(typeof(RectTransform)) as RectTransform;
            
            if (rectTransform == null) {
                return;
            }

            rectTransform.anchorMin = new Vector2Int(0, 0);
            rectTransform.anchorMax = new Vector2Int(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.offsetMin = new Vector2Int(0, 0);
            rectTransform.offsetMax = new Vector2Int(0, 0);
        }

        void ScalePrefab() {
            var prefabTransform = BorderPrefab.GetComponent<RectTransform>();
            prefabTransform.localScale = new Vector3(scale, scale, scale);
        }

        void AddTopLeftBorder() {
            var topLeftBorder = Instantiate(BorderPrefab, border.transform);
            topLeftBorder.name = "top_left_border";
        }

        void AddTopRightBorder() {
            var topRightBorder = Instantiate(BorderPrefab, border.transform);
            topRightBorder.name = "top_right_border";
            var rectTransform = topRightBorder.GetComponent<RectTransform>();
            rectTransform.eulerAngles = new Vector3Int(0, 0, -45);
            rectTransform.anchorMin = new Vector2Int(1, 1);
            rectTransform.anchorMax = new Vector2Int(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
        }

        void AddBottomLeftBorder() {
            var bottomLeftBorder = Instantiate(BorderPrefab, border.transform);
            bottomLeftBorder.name = "bottom_left_border";
            var rectTransform = bottomLeftBorder.GetComponent<RectTransform>();
            rectTransform.eulerAngles = new Vector3(0, 0, 135);
            rectTransform.anchorMin = new Vector2Int(0, 0);
            rectTransform.anchorMax = new Vector2Int(0, 0);
            rectTransform.pivot = new Vector2Int(0, 0);
            rectTransform.position += new Vector3(rectTransform.rect.width / 2, 0, 0);
        }

        void AddBottomRightBorder() {
            var bottomRightBorder = Instantiate(BorderPrefab, border.transform);
            bottomRightBorder.name = "bottom_right_border";
            var rectTransform = bottomRightBorder.GetComponent<RectTransform>();
            rectTransform.eulerAngles = new Vector3(0, 0, -135);
            rectTransform.anchorMin = new Vector2Int(1, 0);
            rectTransform.anchorMax = new Vector2Int(1, 0);
            rectTransform.pivot = new Vector2Int(1, 0);
            rectTransform.position -= new Vector3(rectTransform.rect.width / 2, 0, 0);
        }
    }
}