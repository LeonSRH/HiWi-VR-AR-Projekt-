using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RoomSizeCalculation;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Model;
using Unity.ExplorerMode.Room.Details;
using UnityEngine;

public class DataAnalytics : MonoBehaviour
{
    private WorkerDetailsController workerController;

    private LoadCSVRoomData loadExcelData;
    private static HashSet<Worker> AllWorkers;
    private static HashSet<Worker> AllWorkersInRooms;
    private static HashSet<ServerRoom> AllRooms;

    private static event EventHandler _callWorkersEvent
    {
        add {
            //WorkerDetailsController.workerRequest.GetList();
        }
        remove { }
    }
    private void Start()
    {
        workerController = FindObjectOfType<WorkerDetailsController>();
        loadExcelData = FindObjectOfType<LoadCSVRoomData>();

        AllWorkers = loadExcelData.Workers;
        AllWorkersInRooms = loadExcelData.AllWorkers;
        AllRooms = loadExcelData.Rooms;


    }

    static void GetWOrkerInfo(object sender, EventArgs e)
    {
    }

    public static List<string> getNotASRConformData()
    {
        var rooms = AllRooms;
        List<string> output = new List<string>();


        foreach (var room in rooms)
        {
            var calculatedColor =
                RoomSizeCalculator.CalculateTrafficLightColor(room.Size, room.NumberOfWorkspaces);

            if (calculatedColor == TrafficLightColor.RED || calculatedColor == TrafficLightColor.YELLOW)
            {
                output.Add(room.RoomName);
            }
        }
        return output;
    }


    public static List<string> getRoomsWithNoDesignation()
    {
        var rooms = AllRooms;
        List<string> output = new List<string>();


        foreach (var room in rooms)
        {
            if (room.Designation.Equals("") || room.Department.CostCentre == 0 ||
                room.Size.Equals(""))
            {
                output.Add(room.RoomName);
            }
        }

        return output;
    }

    public void getWorkerInfoCache()
    {
        _callWorkersEvent += GetWOrkerInfo;

        Thread.Sleep(100);
        _callWorkersEvent -= null;

    }

    public static List<string> getWorkersWithMissingInformation()
    {

        HashSet<string> workerNames = new HashSet<string>();

        foreach (var worker in AllWorkers)
        {
            if (worker.LastName.Equals("NN") || worker.LastName.Equals("") || worker.FirstName.Equals("") ||
                worker.Staff_Id == 0 || worker.Employee_Id == 0)
            {
                workerNames.Add(worker.LastName);
            }
        }

        return workerNames.ToList();
    }


    public static List<string> getWorkersWithNoRoomInformation()
    {
        var rooms = AllRooms;
        var allWorkersInRooms = AllWorkersInRooms;
        var allWorkers = AllWorkers;
        HashSet<string> outputHash = new HashSet<string>();

        foreach (Worker worker in allWorkers)
        {
            bool inList = true;
            foreach (Worker wo in allWorkersInRooms)
            {
                if (wo.FirstName.Equals(worker.FirstName) && wo.LastName.Equals(worker.LastName))
                {
                    inList = false;
                }
            }

            if (!inList)
            {
                outputHash.Add(worker.LastName);
            }
        }

        return outputHash.ToList();
    }
}