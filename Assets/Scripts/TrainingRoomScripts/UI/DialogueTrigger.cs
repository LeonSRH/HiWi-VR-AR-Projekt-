using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital.TrainingRoom
{
    public class DialogueTrigger : MonoBehaviour
    {

        public Dialogue questDialogue;

        public Dialogue dialogue;

        public Dialogue exitDialogue;

        public Dialogue onAQuestDialogue;

        public void TriggerquestDialogue()
        {

            FindObjectOfType<DialogueManager>().StartDialogue(questDialogue);
        }

        public void TriggerDialogue()
        {

            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }

        public void TriggerExitDialoge()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(exitDialogue);
        }

        public void TriggerOnAQUestDialoge()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(onAQuestDialogue);
        }
    }
}