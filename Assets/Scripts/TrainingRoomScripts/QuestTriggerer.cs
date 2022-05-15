using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital.TrainingRoom
{
    public class QuestTriggerer : MonoBehaviour
    {

        private bool enter = false;
        private float dist;
        private float distToCheck = 2;
        private GameObject player;
        private QuestUiPlayerTrigger quest;
        public string triggerName;

        public string getName()
        {
            return triggerName;
        }

        private void Awake()
        {
            player = GameObject.Find("3rdPersonPlayer");
            quest = player.GetComponent<QuestUiPlayerTrigger>();

        }

        private void Update()
        {
            dist = Vector3.Distance(player.transform.position, transform.position);
            if (dist < distToCheck && enter)
            {
                waitForPlayer();
            }

        }

        private void waitForPlayer()
        {
            //wenn spieler sich auf einer quest befindet und in im bereich des colliders ist, warte auf das Input
            if (quest.getOnAQuest() && (Input.GetKey(KeyCode.E) || Input.GetMouseButtonDown(0)) && !quest.getQuestCompleted())
            {
                quest.setQuestCompleted(true);
                enter = false;
                GetComponent<DrawPathForTraining>().enabled = false;
                GetComponent<LineRenderer>().enabled = false;
                GetComponent<BoxCollider>().enabled = false;

                enabled = false;
            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                enter = true;
            }
        }

        public bool getEnabled()
        {
            return enabled;
        }

    }
}
