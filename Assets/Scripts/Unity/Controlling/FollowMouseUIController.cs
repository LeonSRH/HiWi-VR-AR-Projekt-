using SmartHospital.Controller.ExplorerMode.Rooms;
using System;
using UnityEngine;

public class FollowMouseUIController : MonoBehaviour
{

    private FollowMouseUI followMouseUI;


    string _roomName;
    string _designation;
    int _numberOfWorkspaces;
    string _roomSize;


    private void Start()
    {
        followMouseUI = FindObjectOfType<FollowMouseUI>();
    }

    public void SetQuickInfo(ServerRoom clientRoomInfo)
    {
        followMouseUI.InfoPanel.gameObject.SetActive(true);
        followMouseUI.enableInfoPanel();
        RoomName = clientRoomInfo.RoomName;
        Designation = clientRoomInfo.Designation;
        RoomSize = clientRoomInfo.Size;
        NumberOfWorkspaces = clientRoomInfo.NumberOfWorkspaces;
    }

    public void DisableQuickInfo()
    {
        followMouseUI.InfoPanel.gameObject.SetActive(false);
    }

    public string RoomName
    {
        get
        {
            _roomName = followMouseUI.RoomNameTextField.text;
            return _roomName;
        }
        set
        {
            _roomName = value;
            followMouseUI.RoomNameTextField.text = _roomName;
        }
    }

    public string Designation
    {
        get
        {
            _designation = followMouseUI.DesignationTextField.text;
            return _designation;
        }
        set
        {
            _designation = value;
            followMouseUI.DesignationTextField.text = _designation;
        }
    }

    public int NumberOfWorkspaces
    {
        get
        {
            _numberOfWorkspaces = Convert.ToInt32(followMouseUI.ApTextField.text);
            return _numberOfWorkspaces;
        }
        set
        {
            _numberOfWorkspaces = value;
            followMouseUI.ApTextField.text = _numberOfWorkspaces.ToString();
        }
    }

    public String RoomSize
    {
        get
        {
            _roomSize = followMouseUI.SizeTextField.text;
            return _roomSize;
        }
        set
        {
            _roomSize = value;
            followMouseUI.SizeTextField.text = _roomSize;
        }
    }
}
