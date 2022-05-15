using System.Net.Mime;
using SmartHospital.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ExplorerMode.UI.View
{
    public class DimensionToggleView : MonoBehaviour
    {
#pragma warning disable CS0649 // Dem Feld "DimensionToggleView.button2D" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
        [SerializeField] PointerDownButton button2D;
#pragma warning restore CS0649 // Dem Feld "DimensionToggleView.button2D" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
#pragma warning disable CS0649 // Dem Feld "DimensionToggleView.button3D" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
        [SerializeField] PointerDownButton button3D;
#pragma warning restore CS0649 // Dem Feld "DimensionToggleView.button3D" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".

        Text button2DText;
        Text button3DText;

        public delegate void Button2DClick(PointerEventData ped);

        public delegate void Button3DClick(PointerEventData ped);

        public event Button2DClick OnButton2DClick;
        public event Button3DClick OnButton3DClick;

        void Start()
        {
            button2DText = button2D.GetComponentInChildren<Text>();
            button3DText = button3D.GetComponentInChildren<Text>();

            SetHighlighted(button2DText);
            SetNormal(button3DText);
            
            button2D.OnPointerDownEvent.AddListener(data =>
            {
                OnButton2DClick?.Invoke(data);
                SetHighlighted(button2DText);
                SetNormal(button3DText);
            });

            button3D.OnPointerDownEvent.AddListener(data =>
            {
                OnButton3DClick?.Invoke(data);
                SetHighlighted(button3DText);
                SetNormal(button2DText);
            });
        }

        void SetHighlighted(Text textToHighlight)
        {
            textToHighlight.GetComponent<Outline>().enabled = true;
            textToHighlight.color = Color.white;
        }

        void SetNormal(Text textToNormal)
        {
            textToNormal.GetComponent<Outline>().enabled = false;
            textToNormal.color = Color.black;
        }
    }
}