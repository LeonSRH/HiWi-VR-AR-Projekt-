using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SmartHospital.TrainingRoom
{
    [RequireComponent(typeof(MeshRenderer))]
    public class HighlightGameObject : MonoBehaviour
    {
        private Color startcolor;
        private Color selectedColor;
        private Material material;

        public float animationTime = 0.2f;
        public float threshhold = 1.5f;

        private void Start()
        {

            material = GetComponent<Renderer>().material;
            startcolor = material.color;
            selectedColor = new Color(
            Mathf.Clamp01(startcolor.r * threshhold),
            Mathf.Clamp01(startcolor.g * threshhold),
            Mathf.Clamp01(startcolor.b * threshhold));
        }

        void OnMouseEnter()
        {
            /**
            iTween.ColorTo(gameObject, iTween.Hash("color", selectedColor, "time",
                animationTime, "easytype", iTween.EaseType.linear,
                "looptype", iTween.LoopType.none));**/

        }

        void OnMouseExit()
        {
            /**
            iTween.Stop(gameObject);
            material.color = startcolor;**/
        }
    }
}
