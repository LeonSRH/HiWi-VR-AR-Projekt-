using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital.TrainingRoom
{
    public class PauseOnPlayerEnter : MonoBehaviour
    {
        private bool enter;
        private bool inRange;

        private float dist;
        private GameObject player;

        private void Awake()
        {
            enter = false;
            player = GameObject.Find("3rdPersonPlayer");
        }
        private void Update()
        {
            if (enter)
            {
                dist = Vector3.Distance(transform.position, player.transform.position);
                if (dist > 3)
                {
                    FindObjectOfType<DialogueManager>().EndDialogue();
                    enter = false;
                }

            }

        }


        private void OnTriggerEnter(Collider coll)
        {

            if (coll.tag == "Player" && !FindObjectOfType<DialogueManager>().getIsPlaying())
            {
                enter = true;

                if (tag == "NPC")
                {
                    GetComponent<DialogueTrigger>().TriggerDialogue();
                }
                else if (tag == "Patient" && !GetComponent<StartPlayerQuest>().getQuestReady())
                {
                    if (!player.GetComponent<QuestUiPlayerTrigger>().getOnAQuest())
                    {
                        GetComponent<DialogueTrigger>().TriggerquestDialogue();
                    }
                    else if (player.GetComponent<QuestUiPlayerTrigger>().getOnAQuest() && !player.GetComponent<QuestUiPlayerTrigger>().getQuestCompleted() && player.GetComponent<QuestUiPlayerTrigger>().getActiveNPC() == gameObject)
                    {
                        GetComponent<DialogueTrigger>().TriggerOnAQUestDialoge();
                    }
                    else if (player.GetComponent<QuestUiPlayerTrigger>().getOnAQuest() && player.GetComponent<QuestUiPlayerTrigger>().getQuestCompleted() && player.GetComponent<QuestUiPlayerTrigger>().getActiveNPC() == gameObject)
                    {
                        GetComponent<DialogueTrigger>().TriggerExitDialoge();
                    }
                    else
                    {
                        GetComponent<DialogueTrigger>().TriggerDialogue();
                    }
                }
                else if (tag == "Patient" && GetComponent<StartPlayerQuest>().getQuestReady())
                {
                    GetComponent<DialogueTrigger>().TriggerDialogue();
                }


            }
        }
    }
}
