using System;

namespace SmartHospital.Controller.ExplorerMode.Rooms.Details
{

    public interface IRoomDetailsView
    {
        event Action OnSaveButtonClick;
        event Action OnCancelButtonClick;
        event Action OnActivate;
        event Action OnDeactivate;

        string RoomId { get; set; }
        string Designation { get; set; }
        int Floor { get; set; }
        string BuildingSection { get; set; }
        int NumberOfWorkspaces { get; set; }
        string AccessControl { get; set; }
        bool AccessControlled { get; set; }


    }

}