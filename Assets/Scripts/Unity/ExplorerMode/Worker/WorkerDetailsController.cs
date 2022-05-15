using System.Collections.Generic;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Database.Request;
using SmartHospital.Model;
using UnityEngine;

public class WorkerDetailsController : MonoBehaviour
{
    public static readonly WorkersRequest
        workerRequest = new WorkersRequest(HttpRoutes.WORKERS, HttpRoutes.WORKERSLIST);
    
    List<Worker> clientWorkers;
    public List<Worker> ClientWorkers
    {
        get => clientWorkers;
        set
        {
            clientWorkers = value;
        }
    }


    private void Awake()
    {
        workerRequest.SetupHttpClient();
        clientWorkers = new List<Worker>();
    }

    void OnGetWorkerList(List<Worker> workers)
    {
        ClientWorkers = workers;
    }
}