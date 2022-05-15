using CI.HttpClient;
using Newtonsoft.Json;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Model;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerBIStreaming : MonoBehaviour
{

    public string pushURL = "https://api.powerbi.com/beta/314967a4-5dbe-41e5-b950-a897ea8c5ec0/datasets/655f0e5e-c402-4791-8bb1-bf56248bbae0/rows?key=h5G7O6hjOKh6M4ztub7JFj8%2F8aPP6ZgrYrCilzM6W3nT7FyoNdCt76WAmskBxOqAkdc0EnpLab9w4VoJMb1fGQ%3D%3D";
    HttpClient client;
    public TMP_InputField searchField;
    LoadCSVRoomData excelData;
    public Image StatusImage;
    List<ServerRoom> rooms;
    LuceneSearcher luceneSearcher = new LuceneSearcher();

    private void Awake()
    {
        client = new HttpClient();
        excelData = FindObjectOfType<LoadCSVRoomData>();
        rooms = new List<ServerRoom>(excelData.Rooms);
        luceneSearcher.createIndex(rooms);


    }

    public void PowerBIReportSearchTest()
    {
        string searchText = searchField.text;

        StatusImage.color = Color.red;

        var outputRooms = luceneSearcher.useRoomIndex(searchText + "*");
        var outputProfessionalGroup = luceneSearcher.useIndex((searchText + "*"), new[] { "Person_ProfessionalGroup" });
        var outputDepartments = luceneSearcher.useIndex((searchText + "*"), new[] { "Person_Specialist_Department" });
        var outputWorker = luceneSearcher.useIndex((searchText), new[] { "Person_Firstname", "Person_Lastname", "Person_StaffId", "Person_EmployeeId" });

        //Count workspaces for powerBiOutput
        int outputWorkspaces = 0;
        List<string> outPutServerRooms = new List<string>(outputRooms);
        foreach (ServerRoom room in rooms)
        {
            if (outPutServerRooms.Contains(room.RoomName))
            {
                outputWorkspaces += room.NumberOfWorkspaces;
            }
        }

        PowerBIReport report = new PowerBIReport()
        {
            Date = DateTime.Now,
            UserName = Environment.UserName,
            FoundRooms = (decimal)outputRooms.Count,
            FoundWorker = (decimal)outputWorker.Count,
            FoundProfessionalGroups = (decimal)outputProfessionalGroup.Count,
            FoundDepartments = (decimal)outputDepartments.Count,
            FoundWorkspaces = outputWorkspaces



        };


        Post(report);
    }

    public void Post(PowerBIReport report)
    {
        var reportSerialized = JsonConvert.SerializeObject(report);
        print(reportSerialized);

        StringContent content = new StringContent("[" + reportSerialized + "]");

        client.Post(new Uri(pushURL), content, HttpCompletionOption.AllResponseContent, (r) =>
                {
#pragma warning disable 0219
                    string responseData = r.ReadAsString();
                    UnityEngine.Debug.Log("Posted value " + responseData);
                    StatusImage.color = Color.green;
#pragma warning restore 0219
                });
        //wait
        System.Threading.Thread.Sleep(1000);


    }

}

