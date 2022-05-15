using System;
using UnityEngine;
using UnityEngine.UI;

public class LMSMainView : MonoBehaviour
{
    public Button ControlMode3D, ControlMode2D;

    public Button ModeDevices, ModeBuilding, ModeProcess;

    public event Action On2DModeClicked, On3DModeClicked, OnDeviceModeClicked, OnBuildingModeClicked, OnProcessModeClicked;
    public LMS_MODE lms_Mode { get; set; }

    private void Start()
    {
        SetupButtons();
        lms_Mode = LMS_MODE.MODE_2D;
    }

    private void SetupButtons()
    {
        //ControlMode2D.onClick.AddListener(() => On2DModeClicked?.Invoke());
        //ControlMode3D.onClick.AddListener(() => On3DModeClicked?.Invoke());

        ModeDevices.onClick.AddListener(() => OnDeviceModeClicked?.Invoke());
        ModeBuilding.onClick.AddListener(() => OnBuildingModeClicked?.Invoke());
        ModeProcess.onClick.AddListener(() => OnProcessModeClicked?.Invoke());
    }

    public enum LMS_MODE
    {
        MODE_2D, MODE_3D
    }
}
