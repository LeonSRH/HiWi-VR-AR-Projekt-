using System;
using System.Collections.Generic;
using System.Linq;
using SmartHospital.ExplorerMode.Rooms.TagSystem;
using SmartHospital.Model;
using UnityEngine;

namespace SmartHospital.Controller.ExplorerMode.Rooms.Details
{
    /// <summary>
    /// View class for <see cref="Model.ClientRoom"/> that implements <see cref="IRoomDetailsView"/>
    /// representation of the room details ui
    /// </summary>
    public partial class RoomDetailsView : MonoBehaviour
    {
        string _roomId;
        string _roomDesignation;
        int _numberOfWorkspaces;
        int _roomFloor;
        string _roomBuildingSection;
        string _roomAccessControl;
        bool _roomAccessControlled;
        string _roomSize;
        string _roomCostCentre;
        string _roomDepartment;

        string _roomComments;
        string _visibleRoomName;
        List<Worker> _workersWithAccess;

        List<Worker> WorkersWithAccess;
        List<Workspace> Workspaces;

        List<Tag> Tags;
        List<InventoryItem> Inventory;
        FunctionalArea FunctionalArea;

        public string RoomId
        {
            get
            {
                _roomId = RoomNameInput.text;
                return _roomId;
            }
            set
            {
                _roomId = value;
                if (RoomNameInput != null)
                    RoomNameInput.text = _roomId;
            }
        }

        public string Comments
        {
            get
            {
                _roomComments = CommentsInput.text;
                return _roomComments;
            }
            set
            {
                _roomComments = value;
                if (CommentsInput != null)
                    CommentsInput.text = _roomComments;
            }
        }

        public int NumberOfWorkspaces
        {
            get
            {
                //_numberOfWorkspaces = WorkerSlider.Value;
                _numberOfWorkspaces = Int32.Parse(NumberOfWorkspacesInput.text);
                return _numberOfWorkspaces;
            }
            set
            {
                _numberOfWorkspaces = value;
                if (NumberOfWorkspacesInput != null)
                    NumberOfWorkspacesInput.text = _numberOfWorkspaces.ToString();
                //WorkerSlider.Value = _numberOfWorkspaces;
            }
        }

        public string Designation
        {
            get
            {
                _roomDesignation = DesignationInput.text;
                return _roomDesignation;
            }
            set
            {
                _roomDesignation = value;
                if (DesignationInput != null)
                    DesignationInput.text = _roomDesignation;
            }
        }

        public string VisibleRoomName
        {
            get
            {
                _visibleRoomName = VisibleRoomNameInput.text;
                return _visibleRoomName;
            }
            set
            {
                _visibleRoomName = value;
                if (VisibleRoomNameInput != null)
                    VisibleRoomNameInput.text = _visibleRoomName;
            }
        }

        public int Floor
        {
            get
            {
                _roomFloor = Int32.Parse(FloorDropdown.options[0].text);
                return _roomFloor;
            }
            set
            {
                _roomFloor = value;
                var index = FloorDropdown.options.Where((data, i) => data.text[0].Equals(_roomFloor))
                    .Select((data, i) => i).GetEnumerator().Current;
                if (FloorDropdown != null)
                    FloorDropdown.value = index;
            }
        }

        //DROPDOWN
        public string AccessControl
        {
            get
            {
                //_roomAccessControl = AccessControlDropdown.options[0].text;
                if (AccessControlled)
                    return Access_Style.ELECTRICAL.ToString();
                else
                    return Access_Style.NONE.ToString();
            }
            set
            {
                _roomAccessControl = value;
                var index = AccessControlDropdown.options
                    .Where((data, i) => data.text[0].Equals(Enum.GetName(typeof(Access_Style), _roomAccessControl)))
                    .Select((data, i) => i).GetEnumerator().Current;
                AccessControlDropdown.value = index;
            }
        }

        public bool AccessControlled
        {
            get
            {
                _roomAccessControlled = AccessControlledToggle.isOn;
                return _roomAccessControlled;
            }
            set
            {
                _roomAccessControlled = value;
                if (AccessControlledToggle != null)
                {
                    AccessControlledToggle.isOn = _roomAccessControlled;
                }
            }
        }

        public string BuildingSection
        {
            get
            {
                _roomBuildingSection = BuildingSectionDropdown.options[0].text;
                return _roomBuildingSection;
            }
            set
            {
                _roomBuildingSection = value;
                var index = BuildingSectionDropdown.options.FindIndex((i) =>
                {
                    return i.text.Equals(_roomBuildingSection);
                });
                if (BuildingSectionDropdown != null)
                {
                    BuildingSectionDropdown.value = index;
                    BuildingSectionDropdown.Select();
                }
            }
        }

        public string RoomSize
        {
            get
            {
                _roomSize = RoomSizeInput.text;
                return _roomSize;
            }
            set
            {
                _roomSize = value;
                if (RoomSizeInput != null)
                    RoomSizeInput.text = _roomSize;
            }
        }

        public string Department
        {
            get
            {
                _roomDepartment = DepartmentDropdown.options[0].text;
                return _roomDepartment;
            }
            set
            {
                _roomDepartment = value;
                var index = DepartmentDropdown.options.FindIndex((i) => { return i.text.Equals(_roomDepartment); });
                if (DepartmentDropdown != null)
                {
                    DepartmentDropdown.value = index;
                    DepartmentDropdown.Select();
                }
            }
        }

        public string CostCentre
        {
            get
            {
                _roomCostCentre = CostCentreAssignmentInput.text;
                return _roomCostCentre;
            }
            set
            {
                _roomCostCentre = value;
                CostCentreAssignmentInput.text = _roomCostCentre;
            }
        }
    }
}