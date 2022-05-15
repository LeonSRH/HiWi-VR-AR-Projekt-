
using System.Collections;
using UnityEngine;
namespace SmartHospital.TrainingRoom
{
    public class OpenDoorTrigger : MonoBehaviour
    {

        private Animator triggerObjectAnim;
        private GameObject UiOpenDoor;
        private GameObject UiItem;

        private GameObject triggerObj;

        private Transform LoadingBar;

        private float dist;
        private bool inRange;

#pragma warning disable CS0414 // Dem Feld "OpenDoorTrigger.open" wurde ein Wert zugewiesen, der aber nie verwendet wird.
        private bool open;
#pragma warning restore CS0414 // Dem Feld "OpenDoorTrigger.open" wurde ein Wert zugewiesen, der aber nie verwendet wird.

        private Status status;

        bool enter;

        void Awake()
        {
            enter = false;
            open = false;
            status = Status.NOTRIGGER;
            UiItem = GameObject.Find("ItemUI");
            UiOpenDoor = GameObject.Find("OpenDoorImage");
            LoadingBar = GameObject.Find("Loadingbar").transform;

        }

        private void Start()
        {
            StartCoroutine(TE());
        }

        private void Update()
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
                    uiOpenDoorItemActions("hide");
                    UiItem.SetActive(false);
                    if (status == Status.ITEM)
                    {
                        triggerObj.GetComponent<ItemTriggerer>().setpointsAdded(false);
                        triggerObj.GetComponent<ItemTriggerer>().setActive(true);
                    }

                    status = Status.NOTRIGGER;
                    inRange = false;
                    enter = false;
                }

            }

        }

        private void showPlayerUI()
        {
            if (inRange)
            {
                if (status == Status.DOOR || status == Status.SMALLDOOR)
                {
                    UiItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Drücke F zum Öffnen der Tür.";
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        triggerObjectAnim.SetBool("open", !triggerObjectAnim.GetBool("open"));
                    }
                }
                else if (status == Status.ITEM)
                {
                    if (LoadingBar.GetComponent<LoadingBarController>().getActive() && !LoadingBarController.getReady())
                    {
                        UiItem.SetActive(false);
                    }
                    else if (!LoadingBar.GetComponent<LoadingBarController>().getActive() && LoadingBarController.getReady() && triggerObj.GetComponent<ItemTriggerer>().getActive()
                        && !triggerObj.GetComponent<ItemTriggerer>().getpointsAdded())
                    {
                        UiItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Drücke F zum Benutzen.";
                        UiItem.SetActive(true);

                        if (Input.GetKey(KeyCode.F) || Input.GetMouseButtonDown(0))
                        {
                            LoadingBar.GetComponent<LoadingBarController>().activateLoadingBar();
                            UiItem.SetActive(false);
                            triggerObj.GetComponent<ItemTriggerer>().setActive(false);
                        }

                    }
                    else if (!triggerObj.GetComponent<ItemTriggerer>().getActive() && !triggerObj.GetComponent<ItemTriggerer>().getpointsAdded())
                    {
                        if (triggerObj.GetComponent<ItemTriggerer>().getName() == "Hygiene")
                        {
                            UiItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Du hast dir die Hände desinfiziert";
                        }
                        else if (triggerObj.GetComponent<ItemTriggerer>().getName() == "Kaffee")
                        {
                            UiItem.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "Du hast dir einen Kaffee geholt!";
                        }
                        UiItem.SetActive(true);
                        GetComponent<PlayerPoints>().addPoints(1);
                        triggerObj.GetComponent<ItemTriggerer>().setpointsAdded(true);

                    }


                }


            }
        }

        private void OnTriggerEnter(Collider other)
        {
            triggerObj = other.gameObject;
            enter = true;

            if (other.tag == "SmallDoor")
            {
                status = Status.SMALLDOOR;
                triggerObjectAnim = triggerObj.GetComponent<Animator>();
            }
            else if (other.tag == "Door")
            {
                status = Status.DOOR;
                triggerObjectAnim = triggerObj.GetComponent<Animator>();
            }
            else if (other.tag == "Item")
            {
                status = Status.ITEM;
            }
            else
            {
                status = Status.NOTRIGGER;
            }

        }

        //actions for the Door UI Interaction 
        private void uiOpenDoorItemActions(string action)
        {
            switch (action)
            {
                case "hide":
                    UiOpenDoor.SetActive(false);
                    break;
                case "show":
                    UiOpenDoor.SetActive(true);
                    break;
            }
        }

        //actions for the UI Item Interaction 
        private void uiItemActions(string action)
        {
            switch (action)
            {
                case "hide":
                    UiItem.SetActive(false);
                    break;
                case "show":
                    UiItem.SetActive(true);
                    break;
            }
        }

        //status of player enter
        public enum Status
        {
            SMALLDOOR,
            DOOR,
            ITEM,
            NOTRIGGER
        }

        IEnumerator TE()
        {
            while (true)
            {
                switch (status)
                {
                    case Status.DOOR:
                        UiOpenDoor.SetActive(true);
                        break;
                    case Status.SMALLDOOR:
                        UiOpenDoor.SetActive(true);
                        break;
                    case Status.ITEM:

                        break;
                    case Status.NOTRIGGER:
                        UiOpenDoor.SetActive(false);

                        break;

                }
                yield return null;
            }


        }
    }
}
