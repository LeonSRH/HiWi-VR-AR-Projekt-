using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Model;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace SmartHospital.Controller
{
    /// <summary>
    /// Control of the UI Elements in the Main Scene 
    /// @Author: KS
    /// </summary>
    public class MainSceneUIController : MonoBehaviour
    {
        [Header("Cameras \n")]
        [SerializeField]
        private Camera mainCamera, overViewCamera, roomCamera;

        //Perspective of the camera (MainPerspective: 3D/ mainCamera on)
        public bool mainPerspectiveOn = true;

        //Selected Room
        private GameObject lastSelectedRoom;

        private GameObject[] inventoryModeElements, roomModeElements, digitalSignageModeElements;

        public GameObject signage;

        private bool signageactivated = false;

        //click controller variables
        bool one_click = false;
        float timer_for_double_click;

        //this is how long in seconds to allow for a double click
        float delay = 0.2f;

        [Header("Room UI Materials \n")]
        public static Material unselectedMaterial;
        [SerializeField]
        private Material selectedMaterial, hoverMaterial, searchMaterial;

        #region Analyse Init
        public TextMeshProUGUI roomSizeInputAnalyse { get; set; }
        public TextMeshProUGUI workspacesInputAnalyse { get; set; }
        public TextMeshProUGUI roomsInputAnalyse { get; set; }

        private Analyse analyse;

        #endregion

        public static ApplicationMode mode;

        private void Start()
        {
            analyse = FindObjectOfType<Analyse>();
            unselectedMaterial = (Material)Resources.Load("UnselectedMaterial", typeof(Material));

            //setsS the application mode
            mode = ApplicationMode.NONE;
        }

        /// <summary>
        /// Adds all workspaces of the given rooms
        /// </summary>
        /// <param name="rooms"></param>
        /// <returns>Number of workspaces of the rooms</returns>
        private int AddAllRoomsWorkspaces(HashSet<GameObject> rooms)
        {
            int workspaces = 0;
            foreach (GameObject room in rooms)
            {
                var roomDetails = room.GetComponent<ClientRoom>();

                workspaces += roomDetails.MyRoom.NumberOfWorkspaces;
            }

            return workspaces;
        }

        /// <summary>
        /// Adds all sizes of the given rooms into one float variable
        /// </summary>
        /// <param name="rooms"></param>
        /// <returns>roomsize of all rooms as float</returns>
        private float AddAllRoomSizes(HashSet<GameObject> rooms)
        {
            float roomSize = 0;

            foreach (GameObject room in rooms)
            {

                var roomDetails = room.GetComponent<ClientRoom>();

                if (roomDetails.MyRoom.Size != null && !roomDetails.MyRoom.Size.Equals(""))
                {
                    try
                    {
                        roomSize += float.Parse(roomDetails.MyRoom.Size, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    catch
                    {
                        Debug.LogError($"Can't parse room size of room with id: ({roomDetails.MyRoom.RoomName}). Roomsize: ({roomDetails.MyRoom.Size})");
                    }
                }

            }

            return roomSize;
        }

        #region Analyse function

        /// <summary>
        /// Analyse mark one or all empty workspaces
        /// </summary>
        public void markEmptyOneOrAllWorkspaces()
        {
            ResetAllRoomColliderMaterials();
            var empty = analyse.getEmptyOneOrAllWorkspacesRooms();

            foreach (GameObject g in empty)
            {
                var roomColliderHover = g.GetComponent<RoomColliderMaterialController>();
                if (roomColliderHover != null)
                {
                    roomColliderHover.setCurrentMaterial(searchMaterial);
                    roomColliderHover.setSearchResultActive(true);
                    g.GetComponent<Renderer>().material = searchMaterial;
                }
            }

            if (roomSizeInputAnalyse != null)
            {
                roomSizeInputAnalyse.SetText("" + AddAllRoomSizes(empty).ToString("0.00") + " m²");
                workspacesInputAnalyse.SetText("" + AddAllRoomsWorkspaces(empty) + " Arbeitsplätze");
                roomsInputAnalyse.SetText("" + (empty.Count + 1) + " Räume");
            }


        }

        /// <summary>
        /// Analyse mark all rooms
        /// </summary>
        public void markAllAllocatedRooms()
        {
            ResetAllRoomColliderMaterials();
            var empty = analyse.getAllAllocatedRooms();

            foreach (GameObject g in empty)
            {
                var roomColliderHover = g.GetComponent<RoomColliderMaterialController>();
                if (roomColliderHover != null)
                {
                    roomColliderHover.setCurrentMaterial(searchMaterial);
                    roomColliderHover.setSearchResultActive(true);
                    g.GetComponent<Renderer>().material = searchMaterial;
                }
            }
            if (roomSizeInputAnalyse != null)
            {
                roomSizeInputAnalyse.SetText("" + AddAllRoomSizes(empty).ToString("0.00") + " m²");
                workspacesInputAnalyse.SetText("" + AddAllRoomsWorkspaces(empty) + " Arbeitsplätze");
                roomsInputAnalyse.SetText("" + (empty.Count + 1) + " Räume");
            }
        }

        /// <summary>
        /// Analyse mark all empty workspaces
        /// </summary>
        public void markEmptyAllWorkspaces()
        {
            ResetAllRoomColliderMaterials();
            var empty = analyse.getEmptyAllWorkspacesRooms();

            foreach (GameObject g in empty)
            {
                var roomColliderHover = g.GetComponent<RoomColliderMaterialController>();
                if (roomColliderHover != null)
                {
                    roomColliderHover.setCurrentMaterial(searchMaterial);
                    roomColliderHover.setSearchResultActive(true);
                    g.GetComponent<Renderer>().material = searchMaterial;
                }
            }
            if (roomSizeInputAnalyse != null)
            {
                roomSizeInputAnalyse.SetText("" + AddAllRoomSizes(empty).ToString("0.00") + " m²");
                workspacesInputAnalyse.SetText("" + AddAllRoomsWorkspaces(empty) + " Arbeitsplätze");
                roomsInputAnalyse.SetText("" + (empty.Count + 1) + " Räume");
            }
        }

        /// <summary>
        /// Analyse mark all complete workspaces
        /// </summary>
        public void markCompleteWorkspaces()
        {
            ResetAllRoomColliderMaterials();
            var empty = analyse.getCompleteWorkspacesRooms();

            foreach (GameObject g in empty)
            {
                var roomColliderHover = g.GetComponent<RoomColliderMaterialController>();
                if (roomColliderHover != null)
                {
                    roomColliderHover.setCurrentMaterial(searchMaterial);
                    roomColliderHover.setSearchResultActive(true);
                    g.GetComponent<Renderer>().material = searchMaterial;
                }
            }
            if (roomSizeInputAnalyse != null)
            {
                roomSizeInputAnalyse.SetText("" + AddAllRoomSizes(empty).ToString("0.00") + " m²");
                workspacesInputAnalyse.SetText("" + AddAllRoomsWorkspaces(empty) + " Arbeitsplätze");
                roomsInputAnalyse.SetText("" + (empty.Count + 1) + " Räume");

            }
        }

        #endregion

        /// <summary>
        /// Resets the collider colors of all rooms to default (unselected)
        /// </summary>
        public static void ResetAllRoomColliderMaterials()
        {
            var rooms = GameObject.FindGameObjectsWithTag("RoomCollider");
            foreach (GameObject g in rooms)
            {
                var roomColliderHover = g.GetComponent<RoomColliderMaterialController>();
                if (roomColliderHover != null)
                {
                    g.GetComponent<Renderer>().material = unselectedMaterial;
                    roomColliderHover.setSelected(false);
                    roomColliderHover.setSearchResultActive(false);
                    roomColliderHover.setCurrentMaterial(unselectedMaterial);
                }
            }
        }

        public void Reset()
        {
            MainSceneUIController.ResetAllRoomColliderMaterials();
            roomSizeInputAnalyse.SetText("");
            workspacesInputAnalyse.SetText("");
            roomsInputAnalyse.SetText("");
        }

        private void Update()
        {
            //mouse click left
            if (Input.GetMouseButtonDown(0))
            {

                if (!one_click) // first click no previous clicks
                {
                    one_click = true;

                    timer_for_double_click = Time.time; // save the current time
                    SelectRoomOnPerspective();
                }
                else
                {
                    //do double click things
                    one_click = false; // found a double click, now reset

                }
            }

            //reset one click if delay of the timer for double click is lower than delay
            if (one_click)
            {
                if ((Time.time - timer_for_double_click) > delay)
                {
                    one_click = false;
                }
            }

            //mouse click right
            if (Input.GetMouseButtonDown(1))
            {
                SetCameraToRoomUI.disableCameraMove();
                SetCameraToRoomUI.setCameraToStartPosition();
                ResetAllRoomColliderMaterials();
            }
        }

        /// <summary>
        /// Selects the room collider depends on the camera perspective
        /// </summary>
        private void SelectRoomOnPerspective()
        {
            if (mainPerspectiveOn)
            {
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;

                    SetLastSelectedRoom(objectHit);
                }
            }
            else
            {
                RaycastHit hit;
                Ray ray = overViewCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;

                    SetLastSelectedRoom(objectHit);
                }
            }
        }

        public void SetMainPerspective(bool status)
        {
            mainPerspectiveOn = status;
        }

        public void DeselectLastSelectedRoom()
        {
            if (lastSelectedRoom != null)
            {
                var roomColliderHoverStatusLastSelectedObject = lastSelectedRoom.GetComponent<RoomColliderMaterialController>();

                if (!roomColliderHoverStatusLastSelectedObject.getSearchResultActive())
                {
                    roomColliderHoverStatusLastSelectedObject.setSelected(false);
                    roomColliderHoverStatusLastSelectedObject.setCurrentMaterial(unselectedMaterial);
                    lastSelectedRoom.GetComponent<Renderer>().material = unselectedMaterial;

                }
            }
        }

        public void SetLastSelectedRoom(Transform obj)
        {
            if (obj.tag.Equals("RoomCollider"))
            {
                DeselectLastSelectedRoom();

                lastSelectedRoom = obj.gameObject;
            }
        }

        public ClientRoom GetSelectedRoom()
        {
            if (lastSelectedRoom == null)
            {
                return null;
            }
            else
            {
                return lastSelectedRoom.GetComponent<ClientRoom>();

            }
        }

        public void SetUIMode(ApplicationMode uiMode)
        {
            mode = uiMode;
            ResetAllRoomColliderMaterials();
            switch (uiMode)
            {
                case ApplicationMode.INVENTORY:
                    break;
                case ApplicationMode.ROOM:
                    ResetAllRoomColliderMaterials();
                    break;
                case ApplicationMode.SIGN:
                    signageactivated = !signageactivated;
                    signage.SetActive(signageactivated);
                    break;
                case ApplicationMode.NAVIGATION:
                    break;
                case ApplicationMode.NONE:
                    ResetAllRoomColliderMaterials();
                    break;
            }
        }

        /// <summary>
        /// Refresh the active UI
        /// </summary>
        public static void RefreshUI()
        {
            switch (mode)
            {
                case ApplicationMode.INVENTORY:
                    break;
                case ApplicationMode.ROOM:
                    ResetAllRoomColliderMaterials();
                    break;
                case ApplicationMode.SIGN:
                    break;
                case ApplicationMode.NAVIGATION:
                    break;
                case ApplicationMode.NONE:
                    ResetAllRoomColliderMaterials();
                    break;
            }
        }

    }
}