using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public partial class DigitalRoomSignView
{
    string _roomName;
    string _visibleRoomName;
    string _buildingSection;
    int _floor;
    string _style;
    string _doorPictogramm;
    string _signPictogramm;


    public List<string> Designation { get; set; }

    public string RoomName
    {
        get
        {
           // _roomName = Room_Name.text;
            return _roomName;
        }

        set
        {
            _roomName = value;
           // Room_Name.text = _roomName;
        }
    }

    public string VisibleRoomName
    {
        get
        {
            _visibleRoomName = "";
            return _visibleRoomName;
        }
        set
        {
            _visibleRoomName = value;
           //""   = _visibleRoomName;
        }
    }

    public string BuildingSection
    {
        get
        {
            _buildingSection = "";
            return _buildingSection;
        }
        set
        {
            _buildingSection = value;

        }
    }

    public int Floor
    {
        get
        {
          //  _floor = Convert.ToInt32(FloorText.text);
            return _floor;

        }
        set
        {
            _floor = value;
           // FloorText.text = _floor.ToString();
        }
    }

    public string Style
    {
        get
        {
            //_style = StyleDropdown.options[0].text;
            return _style;
        }

        set
        {

            _style = value;
            //StyleDropdown.options.Where((data, i) => data.text[0].Equals(_style)).Select((data, i) => i).GetEnumerator().Current;
        }
    }

    public string DoorPictogram
    {
        get
        {
            //_doorPictogramm = DoorPictogramDropdown.options[0].text;
            return _doorPictogramm;
        }

        set
        {
            _doorPictogramm = value;
            //DoorPictogramDropdown.options.Where((data, i) => data.text[0].Equals(_doorPictogramm)).Select((data, i) => i).GetEnumerator().Current;
        }
    }

    public string SignPictogram
    {
        get
        {
            //_signPictogramm = SignPictogramDropdown.options[0].text;
            return _signPictogramm;
        }

        set
        {
            _signPictogramm = value;
            //SignPictogramDropdown.options.Where((data, i) => data.text[0].Equals(_signPictogramm)).Select((data, i) => i).GetEnumerator().Current;
        }
    }
}
