using System;
using System.Collections.Generic;
using RoomSizeCalculation;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Model;
using SmartHospital.Controller;
using UnityEngine;
using SmartHospital.Controller.ExplorerMode.Rooms;
using TMPro;

public class SelectedRoomStatus : MonoBehaviour
{
    public static RoomDetailsView view;
    public event Action<string> OnRoomSelected;

    public static InventoryMainUIController inventoryMainUi;

    [SerializeField]
    TMP_InputField bezeichnung1, bezeichnung2, bezeichnung3, ltRaumnr, klRaumnr, pictoSchild, pictoTuer;

    [SerializeField]
    TMP_Dropdown style;

    public static event EventHandler _selectionEvent
    {
        add
        { //RoomDetailsController.roomRequest.GetById(selectedObject.GetComponent<ClientRoom>().RoomName); 
        }
        remove { }
    }

    private static bool statusLocked = false;

    public static GameObject selectedObject { get; set; }

    public static GameObject[] roomColliderObjects { get; set; }


    private void Start()
    {
        view = FindObjectOfType<RoomDetailsView>();
        inventoryMainUi = GameObject.FindObjectOfType<InventoryMainUIController>();
        roomColliderObjects = GameObject.FindGameObjectsWithTag("RoomCollider");
    }

    //lock roominfo panel
    public static void setLockedStatus(bool value)
    {
        statusLocked = value;
    }

    //get roominfo locked status
    public static bool getLockStatus()
    {
        return statusLocked;
    }

    private static void ASRuleVizualisation(string roomSize, int workerCount)
    {
        //Rules
        var calculatedColor =
            RoomSizeCalculator.CalculateTrafficLightColor(roomSize, workerCount);

        switch (calculatedColor)
        {
            case TrafficLightColor.RED:
                view.WorkplaceDirectiveImage.color = Color.red;
                break;
            case TrafficLightColor.YELLOW:
                view.WorkplaceDirectiveImage.color = Color.yellow;
                break;
            case TrafficLightColor.GREEN:
                view.WorkplaceDirectiveImage.color = Color.green;
                break;
            default:
                view.WorkplaceDirectiveImage.color = Color.gray;
                break;
        }
    }

    /// <summary>
    /// Shows the information on the room details view for a specific gameObject
    /// </summary>
    /// <param name="gameObject"></param>
    private static void SetViewInformation(GameObject gameObject)
    {
        var selectedClientRoom = gameObject.GetComponent<ClientRoom>().MyRoom;

        if (selectedClientRoom != null)
        {
            HashSet<string> costCentres = new HashSet<string>();
            string costCentreString = "";

            view.RoomId = selectedClientRoom.RoomName;
            view.NumberOfWorkspaces = selectedClientRoom.NumberOfWorkspaces;
            view.Designation = selectedClientRoom.Designation;
            view.RoomSize = selectedClientRoom.Size;
            if (selectedClientRoom.Department != null)
                view.Department = selectedClientRoom.Department.Name;
            else
            {
                view.Department = "Leer";
            }
            if (selectedClientRoom.WorkersWithAccess != null)
            {
                foreach (Worker worker in selectedClientRoom.WorkersWithAccess)
                {
                    if (worker.Department != null)
                        costCentres.Add(worker.Department.CostCentre.ToString());
                }
            }


            var costCentreNumber = 0;
            foreach (string cs in costCentres)
            {
                if (costCentreNumber == 0)
                {
                    costCentreString += cs;
                    costCentreNumber++;
                }
                else
                {
                    costCentreString += ", " + cs;
                }
            }

            view.CostCentre = costCentreString;

            if (selectedClientRoom.NamePlate != null)
            {
                view.VisibleRoomName = selectedClientRoom.NamePlate.VisibleRoomName;
                view.BuildingSection = selectedClientRoom.NamePlate.BuildingSection;
            }
            if (selectedClientRoom.AccessStyle != null)
            {
                if (!selectedClientRoom.AccessStyle.Equals("NONE"))
                {
                    view.AccessControlled = true;
                }
                else
                {
                    view.AccessControlled = false;
                }
            }

            view.Comments = selectedClientRoom.Comments;
        }
        if (selectedClientRoom.Size != null)
            ASRuleVizualisation(selectedClientRoom.Size, selectedClientRoom.WorkersWithAccess.Count);

        _selectionEvent -= null;
    }


    static void SelectRoom(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Shows selected room info
    /// </summary>
    /// <param name="room"></param>
    public static void showRoomInfo(GameObject room)
    {
        selectedObject = room;
        inventoryMainUi.CreateInventoryUIForRoom(room);
        //_selectionEvent += SelectRoom;
        SetViewInformation(room);
    }

    public static void hoverRoomInfo(GameObject room)
    {
        selectedObject = room;
        _selectionEvent += SelectRoom;
        SetViewInformation(room);
    }

    public void ChangeNamePlateDetails()
    {
        MainSceneUIController mainSceneMainController = FindObjectOfType<MainSceneUIController>();
        var roomSelected = mainSceneMainController.GetSelectedRoom();

        if (roomSelected != null)
        {
            if (roomSelected.MyRoom.NamePlate != null)
            {
                NamePlate newNamePlate = new NamePlate()
                {
                    Designation = new List<string> { bezeichnung1.text + "\n", bezeichnung2.text + "\n", bezeichnung3.text },
                    Style = style.value.ToString(),
                    VisibleRoomName = klRaumnr.text,
                    SignPictogram = pictoSchild.text,
                    DoorPictogram = pictoTuer.text,
                    BuildingSection = roomSelected.MyRoom.NamePlate.BuildingSection,
                    Floor = roomSelected.MyRoom.NamePlate.Floor,


                };

                roomSelected.MyRoom.NamePlate = newNamePlate;
            }


        }
    }

    /// <summary>
    /// returns found game object with the given room id, return null if no gameobject is found
    /// </summary>
    /// <param name="roomId">the room id of the clientroom</param>
    /// <returns>gameobject with the roomId and CLientRoom script</returns>
    public static GameObject getRoomGOByRoomId(string roomId)
    {
        foreach (var obj in roomColliderObjects)
        {
            var serverRoom = obj.GetComponent<ClientRoom>().MyRoom;

            if (serverRoom.RoomName.Equals(roomId))
                return obj;
        }

        Debug.LogError($"No room with roomId: {roomId} found.");
        return null;
    }
}