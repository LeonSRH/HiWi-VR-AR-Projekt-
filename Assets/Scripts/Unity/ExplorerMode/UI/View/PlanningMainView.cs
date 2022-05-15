using System;
using UnityEngine;
using UnityEngine.UI;

public partial class PlanningMainView : MonoBehaviour
{
    public event Action OnRoomModeClick;
    public event Action OnWorkersModeClick;
    public event Action OnInventoryModeClick;

    public Button RoomModeButton { get; set; }
    public Button WorkersModeButton { get; set; }
    public Button InventoryModeButton { get; set; }

    private void Start()
    {
        SetupButtons();
    }

    private void SetupButtons()
    {
        RoomModeButton.onClick.AddListener(() => OnRoomModeClick?.Invoke());
        WorkersModeButton.onClick.AddListener(() => OnWorkersModeClick?.Invoke());
        InventoryModeButton.onClick.AddListener(() => OnInventoryModeClick?.Invoke());
    }
}
