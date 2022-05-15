using SmartHospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SmartHospital.Controller.ExplorerMode.Rooms.Details
{
    public partial class RoomDetailsView
    {
        public event Action OnSaveButtonClick;
        public event Action OnCancelButtonClick;
        public event Action OnWorkerButtonClick;
        public event Action OnInventarButtonClick;
        public event Action OnDoorSignButtonClick;

        public event Action OnActivate;
        public event Action OnDeactivate;

        //Base room information -------------------------------------------------------
        public Transform TransformParentPanel { get; set; }

        public TMP_InputField RoomNameInput { get; set; }
        public TMP_InputField DesignationInput { get; set; }
        public TMP_InputField NumberOfWorkspacesInput { get; set; }
        public TMP_InputField VisibleRoomNameInput { get; set; }
        public TMP_Dropdown BuildingSectionDropdown { get; set; }
        public TMP_Dropdown FloorDropdown { get; set; }
        public TMP_Dropdown AccessControlDropdown { get; set; }
        public Toggle AccessControlledToggle { get; set; }
        public TMP_InputField RoomSizeInput { get; set; }
        public TMP_Dropdown DepartmentDropdown { get; set; }
        public TMP_InputField CostCentreAssignmentInput { get; set; }
        public TMP_InputField RoomFunctionInput { get; set; }
        public TMP_InputField CommentsInput { get; set; }


        public Image WorkplaceDirectiveImage { get; set; }

        public Transform WorkerPanel { get; set; }

        public Button SaveButton { get; set; }
        public Button CancelButton { get; set; }
        public Button WorkerButton { get; set; }
        public Button InventarButton { get; set; }
        public Button DoorSignButton { get; set; }


        bool _activated;

        RoomInfoCache roomInfoCache;
        InventoryView inventoryView;
        MainView mainView;
        MainSceneUIController mainSceneUIController;
        private ZoomInRoom cameraToRoom;

        void Start()
        {
            roomInfoCache = FindObjectOfType<RoomInfoCache>();
            inventoryView = FindObjectOfType<InventoryView>();
            mainView = FindObjectOfType<MainView>();
            cameraToRoom = GameObject.FindObjectOfType<ZoomInRoom>();
            mainSceneUIController = FindObjectOfType<MainSceneUIController>();


            OnInventarButtonClick += () => ToggleInventar();

            SetupButtons();
            SetupDropDowns();
        }

        /// <summary>
        /// Method sets up the worker view with all the right values.
        /// </summary>
        public void SetupWorkerView(List<Worker> workspaces)
        {
            WorkerPanel.gameObject.SetActive(true);
            for (int i = 1; i < WorkerPanel.childCount; i++)
            {
                var child = WorkerPanel.GetChild(i).gameObject;
                child.SetActive(false);
            }


            for (int z = 1; z < workspaces.Count + 1; z++)
            {
                var child = WorkerPanel.GetChild(z).gameObject;
                child.SetActive(true);
                if (workspaces[z - 1] != null && z > 0)
                    child.GetComponent<WorkerView>().Worker = workspaces[z - 1];
            }
        }

        private void ToggleInventar()
        {


            var roomIdGos = GameObject.FindGameObjectsWithTag("RoomCollider");

            foreach (GameObject go in roomIdGos)
            {
                if (go.GetComponent<ClientRoom>() != null)
                {
                    if (go.GetComponent<ClientRoom>().RoomName.Equals(mainSceneUIController.GetSelectedRoom().RoomName))
                    {
                        createInventoryItemsForRoom(go);
                    }
                }
            }


            cameraToRoom.lookAtRoomTarget();
            //TransformParentPanel.gameObject.SetActive(!TransformParentPanel.gameObject.activeSelf);
            mainView.activateInventoryView();
        }

        private void createInventoryItemsForRoom(GameObject selectedRoom)
        {
            var clientInventoryItems = selectedRoom.GetComponent<ClientRoom>().InventoryItems;

            foreach (InventoryItem inventoryItem in clientInventoryItems)
            {
                var prefab = Resources.Load("Prefabs/Inventory/" + inventoryItem.Name) as GameObject;
                var objCreated = Instantiate(prefab, new UnityEngine.Vector3(selectedRoom.transform.position.x, selectedRoom.transform.position.y, selectedRoom.transform.position.z), Quaternion.identity);


                objCreated.GetComponent<InventoryDragController>().RoomName = selectedRoom.GetComponent<ClientRoom>().RoomName;


            }
        }

        void SetupDropDowns()
        {
            HashSet<string> departmentNames = new HashSet<string>();

            departmentNames.Add("Leer");
            foreach (Department deparment in roomInfoCache.ExcelDepartments)
            {
                if (deparment.Name != null)
                    departmentNames.Add(deparment.Name);
            }

            if (DepartmentDropdown != null)
            {
                DepartmentDropdown.ClearOptions();
                DepartmentDropdown.AddOptions(departmentNames.ToList());
            }

        }

        void SetupButtons()
        {
            SaveButton.onClick.AddListener(() => OnSaveButtonClick?.Invoke());
            CancelButton.onClick.AddListener((() => OnCancelButtonClick?.Invoke()));
            WorkerButton.onClick.AddListener((() => OnWorkerButtonClick?.Invoke()));
            InventarButton.onClick.AddListener(() => OnInventarButtonClick?.Invoke());
            //DoorSignButton.onClick.RemoveAllListeners();
            //DoorSignButton.onClick.AddListener(() => OnDoorSignButtonClick?.Invoke());
        }
    }
}