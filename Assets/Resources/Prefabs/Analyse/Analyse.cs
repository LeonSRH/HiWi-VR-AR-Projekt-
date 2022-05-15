using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Model;
using System.Collections.Generic;
using UnityEngine;

public class Analyse : MonoBehaviour
{
    HashSet<GameObject> goRooms;

    HashSet<GameObject> allAllocatedRooms;

    //Complete Workspaces
    HashSet<GameObject> completeWorkspacesRooms;
    HashSet<GameObject> completeWorkspacesRoomsAll;

    HashSet<GameObject> emptyAllWorkspacesRooms;
    HashSet<GameObject> emptyOneOrAllWorkspacesRooms;

    [Header("Pie Graphs")]
    public GameObject[] pieGraphs;

    private LoadCSVRoomData roomDetails;

    private void Start()
    {
        roomDetails = FindObjectOfType<LoadCSVRoomData>();

        goRooms = RoomSearch.GetAllGORooms();

        searchForCompleteAndEmtyRooms();
    }

    public HashSet<GameObject> getEmptyOneOrAllWorkspacesRooms()
    {
        return emptyOneOrAllWorkspacesRooms;
    }

    public HashSet<GameObject> getEmptyAllWorkspacesRooms()
    {
        return emptyAllWorkspacesRooms;
    }

    public HashSet<GameObject> getAllAllocatedRooms()
    {
        return allAllocatedRooms;
    }

    public HashSet<GameObject> getCompleteWorkspacesRooms()
    {
        return completeWorkspacesRooms;
    }

    public void ConstructPieGraphs()
    {
        float[] valDepartments = new float[4];
        float[] valProfessionalGroups = new float[4];
        float[] valFunctionalAreas = new float[4];
        float[] valWorkers = new float[4];

        float[] valWorkspacesAnalyse = new float[4];
        valWorkspacesAnalyse[0] = emptyOneOrAllWorkspacesRooms.Count;

        valWorkspacesAnalyse[1] = emptyAllWorkspacesRooms.Count;

        valWorkspacesAnalyse[2] = completeWorkspacesRooms.Count;

        valWorkspacesAnalyse[3] = completeWorkspacesRoomsAll.Count;



        pieGraphs[0].GetComponent<PieGraph>().Name = "Arbeitsplätze";
        pieGraphs[0].GetComponent<PieGraph>().SetValues(valWorkspacesAnalyse);

        pieGraphs[1].GetComponent<PieGraph>().Name = "Abteilungen";
        pieGraphs[1].GetComponent<PieGraph>().SetValues(valDepartments);

        pieGraphs[2].GetComponent<PieGraph>().Name = "Fachabteilungen";
        pieGraphs[2].GetComponent<PieGraph>().SetValues(valFunctionalAreas);

        pieGraphs[3].GetComponent<PieGraph>().Name = "Berufsgruppen";
        pieGraphs[3].GetComponent<PieGraph>().SetValues(valProfessionalGroups);




    }

    public void searchForCompleteAndEmtyRooms()
    {
        emptyAllWorkspacesRooms = new HashSet<GameObject>();
        emptyOneOrAllWorkspacesRooms = new HashSet<GameObject>();

        allAllocatedRooms = new HashSet<GameObject>();

        completeWorkspacesRooms = new HashSet<GameObject>();

        foreach (GameObject go in goRooms)
        {
            var newRoomHandler = go.GetComponent<ClientRoom>();

            //add to all rooms
            allAllocatedRooms.Add(go);

            //workspaces in room
            var workspaces = newRoomHandler.MyRoom.NumberOfWorkspaces;

            //Dienstzimmer
            if (workspaces > 0)
            {
                var allEmpty = checkForAllWorkspacesEmpty(go, newRoomHandler.MyRoom.WorkersWithAccess);
                var oneEmpty = checkForOneOrAllWorkspacesEmpty(go, newRoomHandler.MyRoom.WorkersWithAccess);

                if (allEmpty)
                    emptyAllWorkspacesRooms.Add(go);

                if (oneEmpty)
                    emptyOneOrAllWorkspacesRooms.Add(go);
                else
                    completeWorkspacesRooms.Add(go);
            }

        }
    }

    private bool checkForAllWorkspacesEmpty(GameObject go, List<Worker> workspaces)
    {
        bool allWorkspacesEmpty = false;
        foreach (Worker worker in workspaces)
        {
            if (worker.LastName.ToLower().Equals("nn") || worker.LastName.ToLower().Equals(""))
            {
                allWorkspacesEmpty = true;
            }
            else if (!worker.LastName.ToLower().Equals("nn") || !worker.LastName.ToLower().Equals(""))
            {
                return false;
            }

        }


        return allWorkspacesEmpty;
    }

    private bool checkForOneOrAllWorkspacesEmpty(GameObject go, List<Worker> workspaces)
    {
        foreach (Worker worker in workspaces)
        {
            if (worker.LastName.ToLower().Equals("nn") || worker.LastName.ToLower().Equals(""))
            {
                return true;
            }
        }



        return false;
    }

}
