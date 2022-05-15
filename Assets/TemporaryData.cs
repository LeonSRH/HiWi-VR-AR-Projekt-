using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SmartHospital.Model;

public class TemporaryData : MonoBehaviour
{


    //RoomInfoModel room;


   public  ClientRoom currentclientRoom;
    

    // Start is called before the first frame update
    void Start()
    {

        // namePlateVIew.

        //SelectedRoomStatus.selectedObject

        //currentclientRoom.

        TemporaryData[] objs = FindObjectsOfType<TemporaryData>();
        

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

    }
}
