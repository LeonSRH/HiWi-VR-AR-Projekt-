using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class NamePlateVIew : MonoBehaviour
{
    string _roomName;
    string _visibleRoomName;
    int _floor;
    string _doorPictogramm;
    string _signPictogramm;
    string _style;
    string _buildingSection;

    List<string> Designation { get; set; }

    string RoomName
    {
        get
        {
            _roomName = Displayed_Room_Id_Input.text;
            return _roomName;
        }
        set
        {
            _roomName = value;
            Displayed_Room_Id_Input.text = _roomName;
        }
    }

    string VisibleRoomName
    {
        get
        {
            _visibleRoomName = Displayed_Clinical_Room_Id_Input.text;
            return _visibleRoomName;
        }
        set
        {
            _visibleRoomName = value;
            Displayed_Clinical_Room_Id_Input.text = _visibleRoomName;
        }
    }

    string Style
    {
        get
        {
            _style = StyleDropdown.options[0].text;
            return _style;
        }
        set
        {

            _style = value;
            var index = StyleDropdown
                            .options.Where((data, i) => data.text[0].Equals(Enum.GetName(typeof(Style), _style))).Select((data, i) => i)
                            .GetEnumerator().Current;
            StyleDropdown.value = index;

        }
    }

    string DoorPictogram
    {
        get
        {
            _doorPictogramm = Picto_Door_Input.text;
            return _doorPictogramm;
        }
        set
        {
            _doorPictogramm = value;
            Picto_Door_Input.text = _doorPictogramm;
        }
    }
    string SignPictogram
    {
        get
        {
            _signPictogramm = Picto_Doorsign_Input.text;
            return _signPictogramm;

        }
        set
        {

            _signPictogramm = value;
            Picto_Doorsign_Input.text = _signPictogramm;
        }
    }
}
