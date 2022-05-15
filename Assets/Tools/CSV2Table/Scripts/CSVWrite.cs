using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class CSVWrite : MonoBehaviour
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
            Save(rooms);
        }
    }

    void Save(List<ServerRoom> rooms)
    {

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[61];
        rowDataTemp[0] = "Raumnummer";

        rowDataTemp[1] = "Raumbezeichnung";
        rowDataTemp[2] = "Funktionsbereich";
        rowDataTemp[3] = "AP";
        rowDataTemp[4] = "Fachabteilung";

        int countRow = 0;
        for (int i = 0; i < 6; i++)
        {
            rowDataTemp[countRow + 5] = "KST" + i;
            rowDataTemp[countRow + 6] = "Berufsgruppe" + i;
            rowDataTemp[countRow + 7] = "Nachname" + i;
            rowDataTemp[countRow + 8] = "Vorname" + i;
            rowDataTemp[countRow + 9] = "Titel" + i;
            rowDataTemp[countRow + 10] = "Personalnummer" + i;
            rowDataTemp[countRow + 11] = "MiterarbeitsausweisNr" + i;
            rowDataTemp[countRow + 12] = "E_Mail_Adresse" + i;
            rowDataTemp[countRow + 13] = "FunktionMitarbeiter" + i;
            countRow += 9;
        }

        rowDataTemp[59] = "m2";
        rowDataTemp[60] = "Zutrittskontrolle";

        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        foreach (ServerRoom room in rooms)
        {
            rowDataTemp = new string[61];


            if (room.RoomName != null && !room.RoomName.Equals(""))
            {
                rowDataTemp[0] = room.RoomName;
                print("Saved.");

                rowDataTemp[1] = room.Designation;
                rowDataTemp[3] = "" + room.NumberOfWorkspaces;

                int rowCount = 0;
                var ap = room.NumberOfWorkspaces;

                for (int i = 0; i < ap; i++)
                {
                    rowDataTemp[rowCount + 5] = "" + room.Workspaces[i].Worker.Department.CostCentre;
                    //rowDataTemp[rowCount + 6] = room.Workspaces[i].Workspaces[i].Professional_Group.Name;
                    rowDataTemp[rowCount + 7] = room.Workspaces[i].Worker.LastName;
                    rowDataTemp[rowCount + 8] = room.Workspaces[i].Worker.FirstName;
                    rowDataTemp[rowCount + 9] = room.Workspaces[i].Worker.Title.ToString();
                    rowDataTemp[rowCount + 10] = "" + room.Workspaces[i].Worker.Staff_Id;
                    rowDataTemp[rowCount + 11] = "" + room.Workspaces[i].Worker.Employee_Id;
                    rowDataTemp[rowCount + 12] = room.Workspaces[i].Worker.E_Mail;
                    rowDataTemp[rowCount + 13] = room.Workspaces[i].Worker.Function;

                    rowCount += 9;
                }

                //rowDataTemp[59] = room.Roomparameters.Size.ToString();
                if (!Enum.Parse(typeof(Access_Style), room.AccessStyle).Equals(Access_Style.NONE))
                {
                    rowDataTemp[60] = "on";
                }
                else
                {
                    rowDataTemp[60] = "off";
                }

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
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Resources/CSV/" + "Raumbuch_Updated.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_data.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }

}