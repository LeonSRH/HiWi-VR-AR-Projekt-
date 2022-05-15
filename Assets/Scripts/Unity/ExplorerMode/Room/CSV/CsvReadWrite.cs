using SmartHospital.Controller.ExplorerMode.Rooms;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CsvReadWrite
{
    public List<string[]> rowData = new List<string[]>();

    public void Save(string[] doubleRooms)
    {
        // Creating First row of titles manually..
        string[] rowDataTemp = new string[1];
        rowDataTemp[0] = "Raumnummer";
        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        foreach (string room in doubleRooms)
        {
            rowDataTemp = new string[1];
            rowDataTemp[0] = "" + room; // Raumnummer
            rowData.Add(rowDataTemp);

        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public void SaveNotAsrConformRooms(ServerRoom[] rooms)
    {
        // Creating First row of titles manually..
        string[] rowDataTemp = new string[4];
        rowDataTemp[0] = "Raumnummer";
        rowDataTemp[1] = "Grösse";
        rowDataTemp[2] = "Arbeitsplätze";
        rowDataTemp[3] = "Konform";
        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        foreach (ServerRoom room in rooms)
        {
            rowDataTemp = new string[4];
            rowDataTemp[0] = room.RoomName; // Raumnummer
            rowDataTemp[1] = room.Size; // Grösse
            rowDataTemp[2] = "" + room.NumberOfWorkspaces; // Arbeitsplätze
            rowDataTemp[3] = "nein"; // Raumnummer

            rowData.Add(rowDataTemp);

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
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }
    // Following method is used to retrive the relative path as device platform
    public string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Resources/CSV/" + "Saved_data.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }

}
