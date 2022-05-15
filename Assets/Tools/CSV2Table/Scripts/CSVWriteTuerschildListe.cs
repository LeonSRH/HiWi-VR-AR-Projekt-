using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Model;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVWriteTuerschildListe : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();


    // Use this for initialization
    public void save()
    {

        var gameObjectsWithRoomInfo = GameObject.FindGameObjectsWithTag("RoomCollider");
        List<ServerRoom> rooms = new List<ServerRoom>();

        foreach (GameObject gameObjectWithRoomInfo in gameObjectsWithRoomInfo)
        {
            if (gameObjectWithRoomInfo.GetComponent<ClientRoom>() != null)
            {
                rooms.Add(gameObjectWithRoomInfo.GetComponent<ClientRoom>().MyRoom);
            }
        }
        Save(rooms);
    }

    void Save(List<ServerRoom> rooms)
    {

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[11];
        rowDataTemp[0] = "Raumnummer";
        rowDataTemp[1] = "Bauteil";
        rowDataTemp[2] = "Bezeichnung1";
        rowDataTemp[3] = "Bezeichnung2";
        rowDataTemp[4] = "Bezeichnung3";
        rowDataTemp[5] = "Ebene";
        rowDataTemp[6] = "Style";
        rowDataTemp[7] = "leitendeRaumnummer";
        rowDataTemp[8] = "klinischeRaumnummer";
        rowDataTemp[9] = "PictogrammSchild";
        rowDataTemp[10] = "PictogrammTuer";

        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        foreach (ServerRoom room in rooms)
        {
            rowDataTemp = new string[11];


            if (room.RoomName != null && !room.RoomName.Equals("") && room.NamePlate.RoomName != null)
            {
                rowDataTemp[0] = room.NamePlate.RoomName; ;

                rowDataTemp[1] = room.NamePlate.BuildingSection.ToString();
                room.NamePlate.Designation.Add(rowDataTemp[2]);
                room.NamePlate.Designation.Add(rowDataTemp[3]);
                room.NamePlate.Designation.Add(rowDataTemp[4]);
                rowDataTemp[5] = "" + room.NamePlate.Floor;
                rowDataTemp[6] = room.NamePlate.Style.ToString();
                rowDataTemp[7] = room.NamePlate.RoomName;
                rowDataTemp[8] = room.NamePlate.VisibleRoomName;
                rowDataTemp[9] = room.NamePlate.SignPictogram.ToString();
                rowDataTemp[10] = room.NamePlate.DoorPictogram.ToString();

                if (!rowData.Contains(rowDataTemp))
                    rowData.Add(rowDataTemp);
            }
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ";";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }


        string filePath = getPath();

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        FileStream fs = new FileStream(filePath, FileMode.Create);
        StreamWriter outStream = new StreamWriter(fs, Encoding.UTF8);
        if (sb.Length > 0)
            outStream.WriteLine(sb);
        outStream.Close();
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Tuerschilder_CSV.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }

}
