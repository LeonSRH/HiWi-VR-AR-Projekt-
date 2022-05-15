using System;
using System.Collections.Generic;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Database.Request;
using SmartHospital.ExplorerMode.Rooms.Locator;
using SmartHospital.Model;
using UnityEngine;

namespace Unity.ExplorerMode.Room.Details
{
    public class RoomDetailsController : MonoBehaviour
    {
        public static readonly RoomRequest roomRequest = new RoomRequest(HttpRoutes.ROOMS, HttpRoutes.ROOMLIST);

        public LoadCSVRoomData excelData;
        bool _active;

        private ClientRoom _selectedRoom;
        public RoomInfoCache roomInfoCache;

        public RoomDetailsView View;
        public MainView MainView;

        void Awake()
        {
            View = FindObjectOfType<RoomDetailsView>();
            MainView = FindObjectOfType<MainView>();
            excelData = FindObjectOfType<LoadCSVRoomData>();
            roomInfoCache = FindObjectOfType<RoomInfoCache>();


            roomRequest.SetupHttpClient();

            View.OnSaveButtonClick += () =>
            {
                _selectedRoom = SelectedRoomStatus.selectedObject.GetComponent<ClientRoom>();
                ServerRoom room =
                    _selectedRoom.MyRoom;

                room.RoomName = View.RoomId;
                room.Designation = View.Designation;
                room.NumberOfWorkspaces = View.NumberOfWorkspaces;
                room.AccessStyle = View.AccessControl;
                room.NamePlate.VisibleRoomName = View.VisibleRoomName;
                room.Department = roomInfoCache.GetDepartmentByName(View.Department, "client");
                room.NamePlate.BuildingSection = View.BuildingSection;
                room.Size = View.RoomSize;
                room.Comments = View.Comments;

                roomRequest.Put(room);
            };

            View.OnCancelButtonClick += () =>
            {
                Debug.Log(roomInfoCache.ServerDepartments.Count);

            };

            View.OnWorkerButtonClick += () =>
            {
                if (SelectedRoomStatus.selectedObject != null)
                    _selectedRoom = SelectedRoomStatus.selectedObject.GetComponent<ClientRoom>();
                else
                    SelectedRoomStatus.setLockedStatus(false);

                // Toggle visibility of worker panel
                MainView.WorkerPanel.gameObject.SetActive(!MainView.WorkerPanel.gameObject.activeSelf);

                if (_selectedRoom != null && View.WorkerPanel.gameObject.activeSelf)
                {
                    SelectedRoomStatus.setLockedStatus(true);
                    View.SetupWorkerView(_selectedRoom.MyRoom.WorkersWithAccess);
                }
                else
                {
                    SelectedRoomStatus.setLockedStatus(false);
                }
            };

            View.OnDoorSignButtonClick += () =>
            {
                var obj = Instantiate(new GameObject());
                var script = obj.AddComponent<TemporaryData_CurrentClickedRoom>();
                script.LoadNewSceneWithGameObject();
            };

            
            //roomRequest.GetDepartments();
        }

        void OnLoadDepartmentCache(List<Department> departments)
        {
            var output = new HashSet<Department>();

            foreach (Department d in departments)
            {
                output.Add(d);
            }
            roomInfoCache.ServerDepartments = output;
            Debug.Log(output.Count + " Departments");


        }

        void OnLoadCache(List<ServerRoom> serverRooms)
        {
            var functionalAreas = new HashSet<string>();
            var professionalGroups = new HashSet<string>();

            foreach (ServerRoom room in serverRooms)
            {
                foreach (FunctionalArea functionalArea in room.Department.FunctionalAreas)
                {
                    functionalAreas.Add(functionalArea.Name);
                }
                foreach (Worker worker in room.WorkersWithAccess)
                {
                    professionalGroups.Add(worker.Professional_Group.Name);
                }
            }

            roomInfoCache.ServerFunctionalAreas = functionalAreas;
            Debug.Log(functionalAreas.Count + " Functional Areas");
            roomInfoCache.ServerProfessionalGroups = professionalGroups;
            Debug.Log(professionalGroups.Count + " Professional Groups");
        }

        void OnRoomSelection(ServerRoom serverRoom)
        {
            _selectedRoom = SelectedRoomStatus.selectedObject.GetComponent<ClientRoom>();
            View.RoomId = serverRoom.RoomName;
            View.Designation = serverRoom.Designation;
            View.NumberOfWorkspaces = serverRoom.NumberOfWorkspaces;
            if (serverRoom.NamePlate != null)
                View.VisibleRoomName = serverRoom.NamePlate.VisibleRoomName;
            View.Department = serverRoom.Department.Name;
            View.CostCentre = serverRoom.Department.CostCentre.ToString();
            View.Comments = serverRoom.Comments;
            if (serverRoom.NamePlate != null)
                View.BuildingSection = serverRoom.NamePlate.BuildingSection;
            else
                Debug.Log("Room id " + serverRoom.RoomName);

            _selectedRoom.MyRoom = serverRoom;
        }


        void OnSelectionChange(Collider newRoom, RoomSelectionMode selectionMode)
        {
            if (!_active)
            {
                return;
            }

            _selectedRoom = null;
            _selectedRoom = newRoom.GetComponent<ClientRoom>();

            if (_selectedRoom == null)
            {
                Debug.LogError("Invalid Room");
                return;
            }

            if (string.IsNullOrWhiteSpace(_selectedRoom.RoomName))
            {
                Debug.LogError("Room has no ID");
            }
        }
    }
}