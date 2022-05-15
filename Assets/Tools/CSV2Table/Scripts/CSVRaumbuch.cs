using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CSVRaumbuch : MonoBehaviour
{
    public TextAsset file;

    private void Start()
    {
        Load(file);
    }
    

    public class Row
    {
        public string Room_Id;
        public string Room_Designation;
        public string Room_Functional_Area;
        public string Room_Building_Section;
        public string Raumfunktion;
        public string Room_Workspaces_Number;
        public string Room_Specialist_Department;

        public string Room_Cost_Centre_1;
        public string Room_Professional_Group_1;
        public string Room_Lastname_1;
        public string Room_Firstname_1;
        public string Room_Title_1;
        public string Room_Employee_Id_1;
        public string Room_Staff_Id_1;
        public string Room_E_Mail_Adress_1;
        public string Room_Employee_Function_1;

        public string Room_Cost_Centre_2;
        public string Room_Professional_Group_2;
        public string Room_Lastname_2;
        public string Room_Firstname_2;
        public string Room_Title_2;
        public string Room_Employee_Id_2;
        public string Room_Staff_Id_2;
        public string Room_E_Mail_Adress_2;
        public string Room_Employee_Function_2;

        public string Room_Cost_Centre_3;
        public string Room_Professional_Group_3;
        public string Room_Lastname_3;
        public string Room_Firstname_3;
        public string Room_Title_3;
        public string Room_Employee_Id_3;
        public string Room_Staff_Id_3;
        public string Room_E_Mail_Adress_3;
        public string Room_Employee_Function_3;

        public string Room_Cost_Centre_4;
        public string Room_Professional_Group_4;
        public string Room_Lastname_4;
        public string Room_Firstname_4;
        public string Room_Title_4;
        public string Room_Employee_Id_4;
        public string Room_Staff_Id_4;
        public string Room_E_Mail_Adress_4;
        public string Room_Employee_Function_4;

        public string Room_Cost_Centre_5;
        public string Room_Professional_Group_5;
        public string Room_Lastname_5;
        public string Room_Firstname_5;
        public string Room_Title_5;
        public string Room_Employee_Id_5;
        public string Room_Staff_Id_5;
        public string Room_E_Mail_Adress_5;
        public string Room_Employee_Function_5;

        public string Room_Cost_Centre_6;
        public string Room_Professional_Group_6;
        public string Room_Lastname_6;
        public string Room_Firstname_6;
        public string Room_Title_6;
        public string Room_Employee_Id_6;
        public string Room_Staff_Id_6;
        public string Room_E_Mail_Adress_6;
        public string Room_Employee_Function_6;

        public string Room_m2;
        public string Room_Access_Control;

    }

    List<Row> rowList = new List<Row>();
    List<ServerRoom> roomList = new List<ServerRoom>();
    bool isLoaded = false;

    private List<ServerRoom> rowListToNewLoadedRoomList(List<Row> rows)
    {
        List<ServerRoom> roomsList = new List<ServerRoom>();
        foreach (Row r in rows)
        {
            ServerRoom room = new ServerRoom();
            room.RoomName = r.Room_Id;
            room.Designation = r.Room_Designation;
            room.Department.Name = r.Room_Functional_Area;
            float.TryParse(r.Room_m2, out float roomsize);
            //room.Roomparameters.Size = roomsize;
            Int32.TryParse(r.Room_Workspaces_Number, out int workspace_number);
            room.NumberOfWorkspaces = workspace_number;
            Worker[] Workspaces = new Worker[workspace_number];
            Int32.TryParse(r.Room_Cost_Centre_1, out int cost_centre_1);
            Int32.TryParse(r.Room_Cost_Centre_2, out int cost_centre_2);
            Int32.TryParse(r.Room_Cost_Centre_3, out int cost_centre_3);
            Int32.TryParse(r.Room_Cost_Centre_4, out int cost_centre_4);
            Int32.TryParse(r.Room_Cost_Centre_5, out int cost_centre_5);
            Int32.TryParse(r.Room_Cost_Centre_6, out int cost_centre_6);

            for (int i = 0; i < workspace_number; i++)
            {
                Worker workspace = new Worker();
                workspace.Department.CostCentre = cost_centre_1;
            }

            room.Department.Name = r.Room_Specialist_Department;



        }

        return roomList;

    }

    public bool IsLoaded()
    {
        return isLoaded;
    }

    public List<Row> GetRowList()
    {
        return rowList;
    }

    public void Load(TextAsset csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv.text);
        for (int i = 1; i < grid.Length; i++)
        {
            Row row = new Row();
            row.Room_Id = grid[i][0];
            row.Room_Designation = grid[i][1];
            row.Room_Functional_Area = grid[i][2];
            row.Room_Building_Section = grid[i][3];
            row.Raumfunktion = grid[i][4];
            row.Room_Workspaces_Number = grid[i][5];
            row.Room_Specialist_Department = grid[i][6];

            row.Room_Cost_Centre_1 = grid[i][7];
            row.Room_Professional_Group_1 = grid[i][8];
            row.Room_Lastname_1 = grid[i][9];
            row.Room_Firstname_1 = grid[i][10];
            row.Room_Title_1 = grid[i][11];
            row.Room_Employee_Id_1 = grid[i][12];
            row.Room_Staff_Id_1 = grid[i][13];
            row.Room_E_Mail_Adress_1 = grid[i][14];
            row.Room_Employee_Function_1 = grid[i][15];


            row.Room_Cost_Centre_2 = grid[i][16];
            row.Room_Professional_Group_2 = grid[i][17];
            row.Room_Lastname_2 = grid[i][18];
            row.Room_Firstname_2 = grid[i][19];
            row.Room_Title_2 = grid[i][20];
            row.Room_Employee_Id_2 = grid[i][21];
            row.Room_Staff_Id_2 = grid[i][22];
            row.Room_E_Mail_Adress_2 = grid[i][23];
            row.Room_Employee_Function_2 = grid[i][24];

            row.Room_Cost_Centre_3 = grid[i][25];
            row.Room_Professional_Group_3 = grid[i][26];
            row.Room_Lastname_3 = grid[i][27];
            row.Room_Firstname_3 = grid[i][28];
            row.Room_Title_3 = grid[i][29];
            row.Room_Employee_Id_3 = grid[i][30];
            row.Room_Staff_Id_3 = grid[i][31];
            row.Room_E_Mail_Adress_3 = grid[i][32];
            row.Room_Employee_Function_3 = grid[i][33];

            row.Room_Cost_Centre_4 = grid[i][34];
            row.Room_Professional_Group_4 = grid[i][35];
            row.Room_Lastname_4 = grid[i][36];
            row.Room_Firstname_4 = grid[i][37];
            row.Room_Title_4 = grid[i][38];
            row.Room_Employee_Id_4 = grid[i][39];
            row.Room_Staff_Id_4 = grid[i][40];
            row.Room_E_Mail_Adress_4 = grid[i][41];
            row.Room_Employee_Function_4 = grid[i][42];

            row.Room_Cost_Centre_5 = grid[i][43];
            row.Room_Professional_Group_5 = grid[i][44];
            row.Room_Lastname_5 = grid[i][45];
            row.Room_Firstname_5 = grid[i][46];
            row.Room_Title_5 = grid[i][47];
            row.Room_Employee_Id_5 = grid[i][48];
            row.Room_Staff_Id_5 = grid[i][49];
            row.Room_E_Mail_Adress_5 = grid[i][50];
            row.Room_Employee_Function_5 = grid[i][51];

            row.Room_Cost_Centre_6 = grid[i][52];
            row.Room_Professional_Group_6 = grid[i][53];
            row.Room_Lastname_6 = grid[i][54];
            row.Room_Firstname_6 = grid[i][55];
            row.Room_Title_6 = grid[i][56];
            row.Room_Employee_Id_6 = grid[i][57];
            row.Room_Staff_Id_6 = grid[i][58];
            row.Room_E_Mail_Adress_6 = grid[i][59];
            row.Room_Employee_Function_6 = grid[i][60];

            row.Room_m2 = grid[i][61];
            row.Room_Access_Control = grid[i][62];

            rowList.Add(row);
        }
        isLoaded = true;
    }

    public int NumRows()
    {
        return rowList.Count;
    }

    public Row GetAt(int i)
    {
        if (rowList.Count <= i)
            return null;
        return rowList[i];
    }

    public Row Find_Raumnummer(string find)
    {
        return rowList.Find(x => x.Room_Id == find);
    }

    public Row Find_Raumbezeichnung(string find)
    {
        return rowList.Find(x => x.Room_Designation == find);
    }

    public Row Find_Funktionsbereich(string find)
    {
        return rowList.Find(x => x.Room_Functional_Area == find);
    }

    public Row Find_AP(string find)
    {
        return rowList.Find(x => x.Room_Workspaces_Number == find);
    }

    public Row Find_Fachabteilung(string find)
    {
        return rowList.Find(x => x.Room_Specialist_Department == find);
    }

    //Person1
    public Row Find_Room_Cost_Centre_1(string find)
    {
        return rowList.Find(x => x.Room_Cost_Centre_1 == find);
    }

    public Row Find_Room_Professional_Group_1(string find)
    {
        return rowList.Find(x => x.Room_Professional_Group_1 == find);
    }


    public Row Find_Room_Lastname_1(string find)
    {
        return rowList.Find(x => x.Room_Lastname_1 == find);
    }

    public Row Find_Room_Firstname_1(string find)
    {
        return rowList.Find(x => x.Room_Firstname_1 == find);
    }

    public Row Find_Room_Title_1(string find)
    {
        return rowList.Find(x => x.Room_Title_1 == find);
    }

    public Row Find_Room_Employee_Id_1(string find)
    {
        return rowList.Find(x => x.Room_Employee_Id_1 == find);
    }

    public Row Find_Room_Staff_Id_1(string find)
    {
        return rowList.Find(x => x.Room_Staff_Id_1 == find);
    }

    public Row Find_Room_E_Mail_Adress_1(string find)
    {
        return rowList.Find(x => x.Room_E_Mail_Adress_1 == find);
    }

    public Row Find_Room_Employee_Function_1(string find)
    {
        return rowList.Find(x => x.Room_Employee_Function_1 == find);
    }

    //Person2

    public Row Find_Room_Cost_Centre_2(string find)
    {
        return rowList.Find(x => x.Room_Cost_Centre_2 == find);
    }

    public Row Find_Room_Professional_Group_2(string find)
    {
        return rowList.Find(x => x.Room_Professional_Group_2 == find);
    }

    public Row Find_Room_Lastname_2(string find)
    {
        return rowList.Find(x => x.Room_Lastname_2 == find);
    }

    public Row Find_Room_Firstname_2(string find)
    {
        return rowList.Find(x => x.Room_Firstname_2 == find);
    }

    public Row Find_Room_Title_2(string find)
    {
        return rowList.Find(x => x.Room_Title_2 == find);
    }

    public Row Find_Room_Employee_Id_2(string find)
    {
        return rowList.Find(x => x.Room_Employee_Id_2 == find);
    }

    public Row Find_Room_Staff_Id_2(string find)
    {
        return rowList.Find(x => x.Room_Staff_Id_2 == find);
    }

    public Row Find_Room_E_Mail_Adress_2(string find)
    {
        return rowList.Find(x => x.Room_E_Mail_Adress_2 == find);
    }

    public Row Find_Room_Employee_Function_2(string find)
    {
        return rowList.Find(x => x.Room_Employee_Function_2 == find);
    }

    public Row Find_E_Mail_Adresse6(string find)
    {
        return rowList.Find(x => x.Room_E_Mail_Adress_1 == find);
    }

    public Row Find_FunktionMitarbeiter6(string find)
    {
        return rowList.Find(x => x.Room_Employee_Function_1 == find);
    }


    public Row Find_m2(string find)
    {
        return rowList.Find(x => x.Room_m2 == find);
    }

    public Row Find_Zutrittskontrolle(string find)
    {
        return rowList.Find(x => x.Room_Access_Control == find);
    }

    public List<Row> FindAll_Raumnummer(string find)
    {
        return rowList.FindAll(x => x.Room_Id == find);
    }


    public List<Row> FindAll_Raumbezeichnung(string find)
    {
        return rowList.FindAll(x => x.Room_Designation == find);
    }

    public List<Row> FindAll_Funktionsbereich(string find)
    {
        return rowList.FindAll(x => x.Room_Functional_Area == find);
    }

    public List<Row> FindAll_AP(string find)
    {
        return rowList.FindAll(x => x.Room_Workspaces_Number == find);
    }

    public List<Row> FindAll_Fachabteilung(string find)
    {
        return rowList.FindAll(x => x.Room_Specialist_Department == find);
    }

    public List<Row> FindAll_KST1(string find)
    {
        return rowList.FindAll(x => x.Room_Cost_Centre_1 == find);
    }

    public List<Row> FindAll_Berufsgruppe(string find)
    {
        return rowList.FindAll(x => x.Room_Professional_Group_1 == find);
    }

    //Person1
    public List<Row> FindAll_Nachname1(string find)
    {
        return rowList.FindAll(x => x.Room_Lastname_1 == find);
    }

    public List<Row> FindAll_Vorname1(string find)
    {
        return rowList.FindAll(x => x.Room_Firstname_1 == find);
    }

    public List<Row> FindAll_Titel1(string find)
    {
        return rowList.FindAll(x => x.Room_Title_1 == find);
    }

    public List<Row> FindAll_Personalnummer1(string find)
    {
        return rowList.FindAll(x => x.Room_Employee_Id_1 == find);
    }

    public List<Row> FindAll_MiterarbeitsausweisNr1(string find)
    {
        return rowList.FindAll(x => x.Room_Staff_Id_1 == find);
    }

    public List<Row> FindAll_E_Mail_Adresse1(string find)
    {
        return rowList.FindAll(x => x.Room_E_Mail_Adress_1 == find);
    }

    public List<Row> FindAll_FunktionMitarbeiter1(string find)
    {
        return rowList.FindAll(x => x.Room_Employee_Function_1 == find);
    }


    public List<Row> FindAll_E_Mail_Adresse6(string find)
    {
        return rowList.FindAll(x => x.Room_E_Mail_Adress_1 == find);
    }

    public List<Row> FindAll_FunktionMitarbeiter6(string find)
    {
        return rowList.FindAll(x => x.Room_Employee_Function_1 == find);
    }


    public List<Row> FindAll_m2(string find)
    {
        return rowList.FindAll(x => x.Room_m2 == find);
    }

    public List<Row> FindAll_Zutrittskontrolle(string find)
    {
        return rowList.FindAll(x => x.Room_Access_Control == find);
    }

}
