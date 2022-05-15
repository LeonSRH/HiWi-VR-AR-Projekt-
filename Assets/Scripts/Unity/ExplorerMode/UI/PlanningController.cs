using SmartHospital.Controller;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Model;
using System.Collections.Generic;
using UnityEngine;

public class PlanningController : MonoBehaviour
{
    public delegate void ChangeRoom(string roomName);

    public delegate void ChangeMode(int roomName);


    ChangeRoom _changeRoomAction, _changeWorkerRoomAction;
    private ChangeMode _changeModeRoomAction, _changeModeRoomWorkerAction, _changeModeInventoryRoomAction;

    private PlanningMainView _roomMasterListView;
    private ChangeWorkerView _changeWorkerView;
    private LoadCSVRoomData excelData;

    public Material planningMaterial;
    public Material unselectedMaterial;


    private NUI_Planning _uiModelCreator;

    private void Start()
    {
        _roomMasterListView = FindObjectOfType<PlanningMainView>();
        _changeWorkerView = FindObjectOfType<ChangeWorkerView>();
        _uiModelCreator = FindObjectOfType<NUI_Planning>();
        excelData = FindObjectOfType<LoadCSVRoomData>();

        _changeModeRoomAction += ChangeRoomModeInfo;
        _changeModeRoomWorkerAction += ChangeWorkerModeInfo;
        _changeModeInventoryRoomAction += ChangeInventoryModeInfo;

        _changeRoomAction += ChangeRoomMode;
        _changeWorkerRoomAction += ShowWorkeInfo;

        _roomMasterListView.OnInventoryModeClick += () => { ChangePlanningMode(PlanningMode.INVENTORY); };

        _roomMasterListView.OnRoomModeClick += () => { ChangePlanningMode(PlanningMode.ROOMS); };

        _roomMasterListView.OnWorkersModeClick += () => { ChangePlanningMode(PlanningMode.WORKERS); };
    }

    public void ShowWorkeInfo(string workerName)
    {
        _changeWorkerView.ChangeWorkerPanel.gameObject.SetActive(true);
        foreach (Worker w in excelData.AllWorkers)
        {
            if (w.FirstName.Contains(workerName) || w.LastName.Contains(workerName))
            {
                _changeWorkerView.Worker = w;
            }
            else
            {
                _changeWorkerView.Worker = new Worker()
                {
                    LastName = workerName
                };
            }
        }
    }


    public void ChangeRoomMode(string roomId)
    {
        var foundGo = SelectedRoomStatus.getRoomGOByRoomId(roomId);
        if (foundGo != null)
        {
            var roomCollider = foundGo.GetComponent<RoomColliderMaterialController>();
            if (foundGo.GetComponent<ClientRoom>() != null)
            {
                foundGo.GetComponent<Renderer>().material = planningMaterial;
                roomCollider.setCurrentMaterial(planningMaterial);
                roomCollider.setSearchResultActive(true);

            }

            SelectedRoomStatus.setLockedStatus(true);
            SelectedRoomStatus.hoverRoomInfo(foundGo);
        }

    }

    public void ChangeWorkerModeInfo(int mode)
    {
        switch (mode)
        {
            case 0:
                _uiModelCreator.CreateMasterButtonList(DataAnalytics.getWorkersWithMissingInformation(), _changeWorkerRoomAction);
                break;
            case 1:
                _uiModelCreator.CreateMasterButtonList(DataAnalytics.getWorkersWithNoRoomInformation(), _changeWorkerRoomAction);
                break;
            case 2:
                break;
            default:
                break;
        }

    }

    public void ChangeRoomModeInfo(int mode)
    {
        switch (mode)
        {
            case 0:
                markFoundObjects(DataAnalytics.getNotASRConformData());
                _uiModelCreator.CreateMasterButtonList(DataAnalytics.getNotASRConformData(), _changeRoomAction);
                break;
            case 1:
                markFoundObjects(DataAnalytics.getRoomsWithNoDesignation());
                _uiModelCreator.CreateMasterButtonList(DataAnalytics.getRoomsWithNoDesignation(), _changeRoomAction);
                break;
            case 2:
                break;
            default:
                break;
        }
    }


    public void ChangeInventoryModeInfo(int mode)
    {

        switch (mode)
        {
            case 0:
                List<string> inventoryItemNames = new List<string>() { "Tisch2", "Tisch1" };
                _uiModelCreator.CreateMasterButtonList(inventoryItemNames, _changeRoomAction);
                break;
            case 1:
                break;
            case 2:
                break;
            default:
                break;
        }
    }

    private List<string> getASRPlanningRoomIds()
    {
        throw new System.NotImplementedException();
    }

    public void ChangePlanningMode(PlanningMode mode)
    {
        switch (mode)
        {
            case PlanningMode.ROOMS:
                _uiModelCreator.CreateModeButtonList(_roomMasterListView.roomsModeList, _changeModeRoomAction);
                break;
            case PlanningMode.INVENTORY:
                _uiModelCreator.CreateModeButtonList(_roomMasterListView.inventoryModeList,
                    _changeModeInventoryRoomAction);
                break;
            case PlanningMode.WORKERS:
                _uiModelCreator.CreateModeButtonList(_roomMasterListView.workersModeList, _changeModeRoomWorkerAction);
                break;
        }
    }

    public enum PlanningMode
    {
        ROOMS,
        INVENTORY,
        WORKERS
    }

    /// <summary>
    /// Changes the Material of the Collider
    /// </summary>
    /// <param name="foundObjectIds">found Object ids</param>
    public void markFoundObjects(List<string> foundObjectIds)
    {
        var roomColl = GameObject.FindGameObjectsWithTag("RoomCollider");

        MainSceneUIController.ResetAllRoomColliderMaterials();

        if (foundObjectIds != null)
        {
            //marked state
            foreach (GameObject room in roomColl)
            {
                var roomCollider = room.GetComponent<RoomColliderMaterialController>();
                if (room.GetComponent<ClientRoom>() != null)
                {
                    if (foundObjectIds.Contains(room.GetComponent<ClientRoom>().RoomName))
                    {
                        room.GetComponent<Renderer>().material = planningMaterial;
                        roomCollider.setCurrentMaterial(planningMaterial);
                        roomCollider.setSearchResultActive(true);
                    }
                }

            }
        }
    }
}