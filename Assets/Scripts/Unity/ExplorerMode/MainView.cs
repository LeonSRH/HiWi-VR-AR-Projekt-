using System;
using System.Threading.Tasks;
using SmartHospital.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 01.05.2019
/// representation of the main UI 
/// </summary>
public class MainView : MonoBehaviour
{
    private string _userName;

    public event Action LoadPowerBi;
    public event Action LoadPlanning, LoadLMS, LoadInventoryMode, LoadRoombookMode;

    public TextMeshProUGUI UserNameText { get; set; }

    public Transform WorkerPanel { get; set; }

    public Transform PlanningPanel, PlanningModePanel, PlanningButtonPanel;

    public Button PowerBiButton { get; set; }
    public Button PlanningTabButton { get; set; }
    public Button LearningManagementSystemButton { get; set; }

    public Button RoombookButton { get; set; }

    public Button InventoryModeButton { get; set; }

    public Transform PowerBiPanel;

    [SerializeField]
    private Button Settings;

    public Transform informationScreenMain { get; set; }
    public Transform informationScreenActionBar { get; set; }

    public Transform inventoryScreenPanel;

    public Transform inventoryAllScreenPanel;

    private ZoomInRoom cameraToRoom;

    private void Start()
    {
        cameraToRoom = GameObject.FindObjectOfType<ZoomInRoom>();

        //PowerBiButton.onClick.RemoveAllListeners();
        //PowerBiButton.onClick.AddListener(() => LoadPowerBi?.Invoke());

        InventoryModeButton.onClick.RemoveAllListeners();
        InventoryModeButton.onClick.AddListener(() => LoadInventoryMode?.Invoke());

        RoombookButton.onClick.RemoveAllListeners();
        RoombookButton.onClick.AddListener(() => LoadRoombookMode?.Invoke());

        LoadInventoryMode +=
        () => cameraToRoom.lookAtRoomTarget();

        LoadRoombookMode += () => ReloadScene();
        WorkerPanel.gameObject.SetActive(false);
        inventoryScreenPanel.gameObject.SetActive(false);
        inventoryAllScreenPanel.gameObject.SetActive(false);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void activateInventoryView()
    {
        //Deactivate/Activate the Room info action bars
        informationScreenMain.gameObject.SetActive(false);
        informationScreenActionBar.gameObject.SetActive(false);

        //Deactivate/ Activate the Inventory info action bars

        //inventoryScreenPanel.gameObject.SetActive(true);
        //inventoryAllScreenPanel.gameObject.SetActive(true);
    }

    public string UserName
    {
        set
        {
            _userName = value;
            if (UserNameText != null)
                UserNameText.text = _userName;
        }
    }


    public void LoadSceneInMainViewAdditive(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    public void UnLoadSceneInMainView(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }
}