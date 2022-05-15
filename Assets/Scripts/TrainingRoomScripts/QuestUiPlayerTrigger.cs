using System;
using System.Collections;
using UnityEngine;
namespace SmartHospital.TrainingRoom
{
    public class QuestUiPlayerTrigger : MonoBehaviour
    {
        //object withing trigger range
        private GameObject triggerObj;

        public GameObject UiManager;

        //quest objects
        private GameObject[] quest_Points;

        //distance to the triggered object
        private float dist;
#pragma warning disable CS0414 // Dem Feld "QuestUiPlayerTrigger.distToCheckTo" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        private float distToCheckTo;
#pragma warning restore CS0414 // Dem Feld "QuestUiPlayerTrigger.distToCheckTo" wurde ein Wert zugewiesen, der aber nie verwendet wird.

        //player UI
        private GameObject TalkItem;
        private GameObject QuestItem;
        private GameObject ThanksItem;
        public GameObject UiItem;
        private Transform LoadingBar;

        //trigger
        private bool enter = false;
#pragma warning disable CS0414 // Dem Feld "QuestUiPlayerTrigger.stay" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        private bool stay = false;
#pragma warning restore CS0414 // Dem Feld "QuestUiPlayerTrigger.stay" wurde ein Wert zugewiesen, der aber nie verwendet wird.
#pragma warning disable CS0414 // Dem Feld "QuestUiPlayerTrigger.exit" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        private bool exit = false;
#pragma warning restore CS0414 // Dem Feld "QuestUiPlayerTrigger.exit" wurde ein Wert zugewiesen, der aber nie verwendet wird.

        private bool talk;
        private bool onAQuest;
        private bool[] questCompleted;
        private bool inRange;
        private Status status;
        private int quest_sequence;

        private GameObject activeNPCQ;

        private Transform instructionText;
        private Transform questText;


        private void Awake()
        {
            distToCheckTo = 2f;

            QuestItem = GameObject.Find("Quest").transform.GetChild(0).gameObject;
            TalkItem = GameObject.Find("Talk").transform.GetChild(0).gameObject;
            ThanksItem = GameObject.Find("Thanks").transform.GetChild(0).gameObject;
            LoadingBar = GameObject.Find("Loadingbar").transform;
            instructionText = GameObject.Find("instructions text").transform;
            questText = GameObject.Find("quest content text").transform;

            status = Status.NOTRIGGER;
            onAQuest = false;
            inRange = false;
            talk = false;
            quest_sequence = 0;

        }

        private void Start()
        {
            talk = false;
            UiItem.SetActive(false);
            StartCoroutine(TE());
        }

        void Update()
        {
            if (enter)
            {
                dist = Vector3.Distance(triggerObj.transform.position, transform.position);

                if (dist < 3)
                {
                    inRange = true;
                    showPlayerUI();

                }
                else if (dist >= 3)
                {
                    UiItem.SetActive(false);
                    talk = false;
                    status = Status.NOTRIGGER;
                    inRange = false;
                    enter = false;
                }

            }

            if (onAQuest)
            {
                if (questReady() == true)
                {

                    setInstructionsForPlayer("Gehen Sie zurück zu dem Auftraggeber.", "Aufgabe bereit zur Abgabe.");
                }
            }


        }

        //checks all questCompleted bools and returns true when all are completed
        private bool questReady()
        {

            foreach (bool quest in questCompleted)
            {
                if (quest == false)
                {
                    return false;
                }
            }

            return true;
        }

        //shows the player UI and wait for player input
        private void showPlayerUI()
        {
            //talk interface (press E to speak to NPC)
            if (!talk && inRange)
            {

                if (status == Status.NPC || status == Status.PATIENT)
                {
                    if (Input.GetKey(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {

                        FindObjectOfType<DialogueManager>().DisplayNextSentence();
                        talk = true;

                    }
                }
                else if (status == Status.QUEST && onAQuest)
                {
                    talkItemActions("set", "Interagieren.");
                    if (LoadingBar.GetComponent<LoadingBarController>().getActive() && !LoadingBarController.getReady() && triggerObj.GetComponent<QuestTriggerer>().getEnabled())
                    {
                        talkItemActions("hide", "");
                    }
                    else if (!LoadingBar.GetComponent<LoadingBarController>().getActive() && LoadingBarController.getReady() && triggerObj.GetComponent<QuestTriggerer>().getEnabled())
                    {
                        talkItemActions("set", "Interagieren");
                        talkItemActions("show", "");
                        if (Input.GetKey(KeyCode.E) || Input.GetMouseButtonDown(0))
                        {
                            LoadingBar.GetComponent<LoadingBarController>().activateLoadingBar();
                            talkItemActions("hide", "");
                        }

                    }
                    else if (!LoadingBar.GetComponent<LoadingBarController>().getActive() && LoadingBarController.getReady() && !triggerObj.GetComponent<QuestTriggerer>().getEnabled())
                    {
                        if (triggerObj.GetComponent<QuestTriggerer>().getName() == "Wasser")
                        {
                            UiItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Wasser erhalten.";
                            UiItem.SetActive(true);
                        }
                        else if (triggerObj.GetComponent<QuestTriggerer>().getName() == "Medikament")
                        {
                            UiItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Schmerzmittel erhalten.";
                            UiItem.SetActive(true);
                        }
                        else if (triggerObj.GetComponent<QuestTriggerer>().getName() == "Brot")
                        {
                            UiItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Essen erhalten.";
                            UiItem.SetActive(true);
                        }
                        else
                        {
                            UiItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Aktion erfolgreich!";
                            UiItem.SetActive(true);
                        }



                    }




                }
                else if (status == Status.DOCTOR)
                {
                    talkItemActions("set", "Sprechen.");
                    if (LoadingBarController.getReady())
                    {
                        talkItemActions("hide", "");
                    }

                    if (Input.GetKey(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        LoadingBar.GetComponent<LoadingBarController>().activateLoadingBar();

                        triggerObj.GetComponent<GoToPatientTrigger>().setPatient(activeNPCQ);
                    }
                }
                else if (status == Status.ZOMBIE)
                {
                    UiItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Hey tanz mit.";
                    UiItem.SetActive(true);
                }

            }//quest interface (press Y to accept quest and N to decline quest)
            else if (talk && inRange && status == Status.PATIENT)
            {
                if (!onAQuest && !triggerObj.GetComponent<StartPlayerQuest>().getQuestReady() && status == Status.PATIENT)
                {

                    if (Input.GetKey(KeyCode.F) || Input.GetMouseButtonDown(0))
                    {
                        FindObjectOfType<DialogueManager>().EndDialogue();
                        talk = false;
                        activateQuest();
                    }
                    else if (Input.GetKey(KeyCode.N) || Input.GetMouseButtonDown(1))
                    {
                        FindObjectOfType<DialogueManager>().EndDialogue();
                        talk = false;
                    }


                }
                else if (onAQuest && questReady() && inRange && (triggerObj == activeNPCQ))
                {
                    triggerObj.GetComponent<StartPlayerQuest>().setQuestReady(true);
                    disableQuest();
                    if (Input.GetKey(KeyCode.E) || Input.GetMouseButtonDown(0))
                    {
                        FindObjectOfType<DialogueManager>().EndDialogue();
                        talk = false;

                    }

                }
            }

        }

        //activates all quest in the quest_points array
        private void activateQuest()
        {
            activeNPCQ = triggerObj.gameObject;
            quest_Points = triggerObj.GetComponent<StartPlayerQuest>().getQuestPoints();

            UiManager.GetComponent<FocusArrowOnNextPoint>().setPoints(quest_Points);
            UiManager.GetComponent<FocusArrowOnNextPoint>().setStartPoint(activeNPCQ);
            UiManager.GetComponent<FocusArrowOnNextPoint>().setActive(true);

            quest_sequence = 0;
            var q = triggerObj.GetComponent<StartPlayerQuest>();
            setInstructionsForPlayer(q.getplayerInstructions1(), q.getplayerInstructions2());


            triggerObj.GetComponent<StartPlayerQuest>().setOnAQuest(true);

            foreach (GameObject quest in quest_Points)
            {
                //add script quest triggerer if not assigned
                if (quest.GetComponent<QuestTriggerer>() == null)
                {
                    quest.gameObject.AddComponent<QuestTriggerer>();
                }
                else
                {
                    quest.GetComponent<QuestTriggerer>().enabled = true;
                }
                //add script draw path for training if not assigned
                if (quest.GetComponent<DrawPathForTraining>() == null)
                {
                    quest.gameObject.AddComponent<DrawPathForTraining>();
                }
                else
                {
                    quest.GetComponent<DrawPathForTraining>().enabled = true;
                }
                //add line renderer when it is not connected
                if (quest.GetComponent<LineRenderer>() == null)
                {
                    quest.AddComponent<LineRenderer>();
                }
                else
                {
                    quest.GetComponent<LineRenderer>().enabled = true;
                }
                //add a box collider when there is no boxcollider attached to the object

                quest.GetComponent<BoxCollider>().enabled = true;

                quest.GetComponent<DrawPathForTraining>().setTarget(triggerObj.GetComponent<StartPlayerQuest>().getStartpoint());

            }

            questCompleted = new bool[quest_Points.Length];

            for (int i = 0; i < questCompleted.Length; i++)
            {
                questCompleted[i] = false;
            }

            onAQuest = true;

        }

        //disables on a quest + add points in playerpoins
        public void disableQuest()
        {
            activeNPCQ.GetComponent<StartPlayerQuest>().setOnAQuest(false);
            setInstructionsForPlayer("", "");

            UiManager.GetComponent<FocusArrowOnNextPoint>().setActive(false);

            if (activeNPCQ.tag == "Patient")
            {
                GetComponent<PlayerPoints>().addZombiesHealed();
                GetComponent<PlayerPoints>().addPoints(5);
            }
            else if (activeNPCQ.tag == "NPC")
            {
                GetComponent<PlayerPoints>().addPoints(10);
                GetComponent<PlayerPoints>().addCompletedQuest();
            }


            quest_sequence = 0;
            onAQuest = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            triggerObj = other.gameObject;
            enter = true;

            if (other.tag == "NPC")
            {
                status = Status.NPC;
            }
            else if (other.tag == "Quest")
            {
                status = Status.QUEST;
            }
            else if (other.tag == "Patient")
            {
                status = Status.PATIENT;
            }
            else if (other.tag == "Doctor")
            {
                status = Status.DOCTOR;
            }
            else if (other.tag == "Zombie")
            {
                status = Status.ZOMBIE;
            }
            else
            {
                status = Status.NOTRIGGER;
            }




        }

        //status of player enter
        public enum Status
        {
            NPC,
            QUEST,
            PATIENT,
            DOCTOR,
            NOTRIGGER,
            ZOMBIE
        }

        IEnumerator TE()
        {
            while (true)
            {
                switch (status)
                {
                    case Status.NPC:
                        break;
                    case Status.QUEST:
                        break;
                    case Status.PATIENT:
                        break;
                    case Status.DOCTOR:
                        break;
                    case Status.NOTRIGGER:
                        talk = false;
                        QuestItem.SetActive(false);
                        TalkItem.SetActive(false);
                        ThanksItem.SetActive(false);
                        break;

                }
                yield return null;
            }


        }

        //actions for the talk item UI 
        public void talkItemActions(string action, string text)
        {
            switch (action)
            {
                case "hide":
                    TalkItem.SetActive(false);
                    break;
                case "show":
                    TalkItem.SetActive(true);
                    break;
                case "set":
                    TalkItem.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = text;
                    break;
            }
        }

        //actions for the quest item UI
        public void questItemActions(string action, string text)
        {
            switch (action)
            {
                case "hide":
                    QuestItem.SetActive(false);
                    break;
                case "show":
                    QuestItem.SetActive(true);
                    break;
                case "set":
                    QuestItem.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = text;
                    break;

            }
        }


        //actions for the thanks item UI
        public void thanksItemActions(string action, string text)
        {
            switch (action)
            {
                case "hide":
                    ThanksItem.SetActive(false);
                    break;
                case "show":
                    ThanksItem.SetActive(true);
                    break;
                case "set":
                    ThanksItem.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = text;
                    break;

            }
        }


        //return quest sequenz item
        public GameObject getQuestSequenze()
        {

            if (quest_sequence < quest_Points.Length)
            {
                return quest_Points[quest_sequence];

            }
            else
            {
                return quest_Points[0];
            }

        }

        //sets the quest status for the next quest item in questCompleted array
        public void setQuestCompleted(bool value)
        {
            if (quest_sequence < quest_Points.Length)
            {
                questCompleted[quest_sequence] = value;
                quest_sequence++;
            }

        }

        public bool getOnAQuest()
        {
            return onAQuest;
        }

        public bool getQuestCompleted()
        {
            if (quest_sequence < questCompleted.Length)
            {
                return questCompleted[quest_sequence];
            }
            else
            {
                return questCompleted[0];
            }

        }

        //set instructions in the standard ui
        public void setInstructionsForPlayer(string instructions, string questContent)
        {

            instructionText.gameObject.GetComponent<UnityEngine.UI.Text>().text = instructions;
            questText.gameObject.GetComponent<UnityEngine.UI.Text>().text = questContent;

        }

        public GameObject getActiveNPC()
        {
            return activeNPCQ;
        }
    }
}
