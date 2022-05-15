using SmartHospital.ExplorerMode.Rooms.TagSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagInfoModel : MonoBehaviour
{
    /// <summary>
    /// Gets all DisplayedRoomInfo
    /// </summary>
    /// <returns></returns>
    public HashSet<Tag> getAllDisplayedRooms()
    {
        var displayedRoomsOutput = new HashSet<Tag>();

        return displayedRoomsOutput;
    }


    public void setDisplayedRoom(HashSet<Tag> rooms)
    {

    }

    /// <summary>
    /// Gets a Displayed room 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Tag getDisplayedRoomById(string id)
    {
        var displayedRoomOutput = new Tag();

        return displayedRoomOutput;
    }

    public void setDisplayedRoomInfo(Tag room)
    {

    }

    public void updateDisplayedRoomInfo(Tag room)
    {

    }
}
