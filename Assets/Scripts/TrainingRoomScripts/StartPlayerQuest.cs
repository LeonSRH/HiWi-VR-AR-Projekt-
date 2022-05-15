using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital.TrainingRoom
{
    public class StartPlayerQuest : MonoBehaviour
    {
        //quest objects
        public GameObject[] quest_Points;
        public String playerInstruction1;
        public String playerInstruction2;
        public String QuestItemText;
        public String questPlayerInstructions;
        public Transform startPoint;

        public GameObject blinkLampe;

        private bool onAQuest;

        private bool questReady;

        private void Start()
        {
            onAQuest = false;
            questReady = false;
        }

        private void Update()
        {
            if (questReady)
            {
                blinkLampe.GetComponent<BlinkLampe>().SetStateNumber(0);
            }
            else
            {
                blinkLampe.GetComponent<BlinkLampe>().SetStateNumber(2);
            }
        }

        public void setOnAQuest(bool value)
        {
            onAQuest = value;
        }

        public void setQuestReady(bool value)
        {
            questReady = value;
        }

        public bool getQuestReady()
        {
            return questReady;
        }

        public Transform getStartpoint()
        {
            if (startPoint == null)
            {
                return transform;
            }
            else
            {
                return startPoint;
            }

        }

        public bool getOnAQuest()
        {
            return onAQuest;
        }

        public GameObject[] getQuestPoints()
        {
            return quest_Points;


        }

        public String getplayerInstructions1()
        {
            return playerInstruction1;


        }

        public String getQuestItemText()
        {
            return QuestItemText;
        }

        public String getplayerInstructions2()
        {
            return playerInstruction2;

        }

        public String getQuestContentInstructions()
        {
            return questPlayerInstructions;


        }
    }
}
