using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.TrainingRoom
{
    public class DialogueManager : MonoBehaviour
    {

        public Text nameText;
        public Text dialogueText;
        private bool isPlaying;

        public Animator animator;

        private Queue<string> sentences;

        // Use this for initialization
        void Start()
        {
            isPlaying = false;
            sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            //Wenn der Spieler innerhalb der Reichweite ist, spiele den Dialog ab
            isPlaying = true;
            animator.SetBool("IsOpen", true);

            nameText.text = dialogue.name;

            sentences.Clear();

            //weise dem DialogManager die Dialogsätze des NPCs zu
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            //zeige den ersten Satz
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }


        IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        public void EndDialogue()
        {
            isPlaying = false;
            animator.SetBool("IsOpen", false);
        }

        public bool getIsPlaying()
        {
            return isPlaying;
        }

    }
}