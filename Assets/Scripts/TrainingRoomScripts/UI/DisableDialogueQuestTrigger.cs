using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SmartHospital.TrainingRoom
{

    public class DisableDialogueQuestTrigger : MonoBehaviour
    {

        GameObject player;


        private void Start()
        {
            player = GameObject.Find("3rdPersonPlayer");
        }
        // Update is called once per frame
        void Update()
        {
            if (player.GetComponent<QuestUiPlayerTrigger>().getOnAQuest())
            {
                GetComponent<DialogueTrigger>().enabled = false;
            }

        }
    }
}