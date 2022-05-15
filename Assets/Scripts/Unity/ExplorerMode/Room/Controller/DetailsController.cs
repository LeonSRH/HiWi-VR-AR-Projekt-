using SmartHospital.Common;
using SmartHospital.ExplorerMode.Database.Handler;
using SmartHospital.ExplorerMode.Rooms.Locator;
using SmartHospital.ExplorerMode.Rooms.TagSystem;
using SmartHospital.Model;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UnityEngine;

namespace SmartHospital.Controller.ExplorerMode.Rooms.Details
{

    public class DetailsController : BaseController
    {
        public RoomDetailsView View;

        public RESTHandler RestHandler;
        public RoomPainter RoomPainter;
        public RoomLocator2D RoomLocator;
        bool _active;

        ClientRoom _selectedRoom;

        void Awake()
        {
            View.OnDeactivate += () => _active = false;
            View.OnActivate += () => _active = true;
            View.OnSaveButtonClick += () =>
            {

                var details = new ServerRoom(View.RoomId, View.NumberOfWorkspaces, new List<Worker>(), View.Designation, "NONE", new NamePlate("0"),
            new List<Tag>(), new List<Workspace>(), new RoomParameters(), new Collider(), "0", new Department(), "");


                RestHandler.StartRequest(RestHandler.UpdateRoomDetailsRequest(_selectedRoom.MyRoom, details));
            };
        }

        void OnEnable()
        {
            RoomLocator.OnSelectionChange += OnSelectionChange;
            RoomLocator.OnDeselection += OnDeselection;
        }

        void OnDisable()
        {
            RoomLocator.OnSelectionChange -= OnSelectionChange;
            RoomLocator.OnDeselection -= OnDeselection;
        }

        void OnDeselection() { }

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

            RoomPainter.PaintRoom(_selectedRoom, _selectedRoom.Tags.Select(roomTag => roomTag.Color).ToArray());
            ChangeDetails();
        }

        /// <summary>
        /// Method changes the displayed details in the <see cref="IRoomDetailsView"/>
        /// </summary>
        void ChangeDetails()
        {
            RestHandler.StartRequest(RestHandler.GetRoomDetailsRequest(_selectedRoom.MyRoom, callback: roomDetails =>
            {
                if (roomDetails == null)
                {
                    roomDetails = new ServerRoom(View.RoomId, View.NumberOfWorkspaces, new List<Worker>(), View.Designation, "NONE", new NamePlate("0"),
            new List<Tag>(), new List<Workspace>(), new RoomParameters(), new Collider(), "0", new Department(), "");

                }

                View.RoomId = roomDetails.RoomName;
                View.NumberOfWorkspaces = (int)roomDetails.NumberOfWorkspaces;
                View.AccessControl = roomDetails.AccessStyle.ToString();
            }));
        }
    }

}