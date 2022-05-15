using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.TrainingRoom
{
    public class FindLocationQuest : MonoBehaviour
    {
        public TextMeshProUGUI uiText;
        public GameObject image;
        public Color finishedMaterial;
        private bool visited;

        public GameObject moveObject;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && !visited)
            {
                iTween.ScaleFrom(moveObject, new Vector3(3, 3, 0), 3);
                iTween.MoveFrom(moveObject, new Vector3(3, moveObject.transform.position.y, 0), 5);
                visited = true;
                image.GetComponent<Image>().color = finishedMaterial;
            }
        }

        public bool getVisited()
        {
            return visited;
        }

    }
}
