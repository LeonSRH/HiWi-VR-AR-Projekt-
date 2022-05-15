using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NUI_LMS : MonoBehaviour
{

    FillPanel fillPanel;

    LMSMainView lmsMainView;

    Transform ModeTransformPanel;
    Transform MasterListTransformPanel;

    int zaehler = 0;
    TextMeshProUGUI zaehlerTransform;

    private void Awake()
    {
        fillPanel = FindObjectOfType<FillPanel>();
        lmsMainView = FindObjectOfType<LMSMainView>();

        //CreateMasterlist();
        //CreateOrientationPanel();
        //CreateModePanel();
        CreateUserPanel();

    }

    private void Start()
    {
    }
    /// <summary>
    /// Right bound
    /// </summary>
    public void CreateMasterlist()
    {
        //Parent Panel

        Transform ParentObject = fillPanel.CreatePanel(300, 300, UI_OrientationOnScreen.enumForOrientations.Right,
            UI_Panel.enumForLayoutGroup.Vertical, true);
        ParentObject.name = "Master Parent";

    }

    public void CreateUserPanel()
    {
        Transform ParentObject = fillPanel.CreatePanel(300, 600, UI_OrientationOnScreen.enumForOrientations.Right, UI_Panel.enumForLayoutGroup.Vertical, true);
        ParentObject.name = "User";
        var testPanel = fillPanel.CreatePanel(300, 300, ParentObject, UI_Panel.enumForLayoutGroup.Vertical, true);
        fillPanel.CreateTextElementOnUI("Lernsession", testPanel);


        fillPanel.CreateTextElementOnUI("Name:", ParentObject);

        fillPanel.CreateTextElementOnUI("Smart Hospital", ParentObject);

        fillPanel.CreateTextElementOnUI("Lessions completed:", ParentObject);

        zaehlerTransform = fillPanel.CreateTextElementOnUIWithHandle("0", ParentObject);


    }

    public void addZaehler()
    {
        zaehler++;
        zaehlerTransform.SetText("" + zaehler);
    }
    /// <summary>
    /// Left bound
    /// </summary>
    public void CreateModePanel()
    {
        float yPosOfOptions = 430;
        float gapOfButtonY = 200;

        lmsMainView.ModeBuilding = fillPanel.CreateButtonOnUIWithHandle("Gebäude", transform, null, 200, 50, -750, yPosOfOptions);
        lmsMainView.ModeDevices = fillPanel.CreateButtonOnUIWithHandle("Geräte", transform, null, 200, 50, -750 + (gapOfButtonY), yPosOfOptions);
        lmsMainView.ModeProcess = fillPanel.CreateButtonOnUIWithHandle("Prozesse", transform, null, 200, 50, -750 + (2 * gapOfButtonY), yPosOfOptions);

    }

    public void CreateOrientationPanel()
    {
        Transform ParentTransform = fillPanel.CreatePanel(80, 80, UI_OrientationOnScreen.enumForOrientations.Left, UI_Panel.enumForLayoutGroup.Vertical, true);

        lmsMainView.ControlMode2D = fillPanel.CreateButtonOnUIWithHandle("2D", ParentTransform, 0, 0, 0, 0);
        lmsMainView.ControlMode3D = fillPanel.CreateButtonOnUIWithHandle("3D", ParentTransform, 0, 0, 0, 0);


    }

    //Right bound in master list
    public void CreateMasterButtonList(List<string> items, PlanningController.ChangeRoom action)
    {
        var children = new List<GameObject>();
        foreach (Transform child in MasterListTransformPanel) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        foreach (string item in items)
        {
            var button = fillPanel.CreateButtonOnUIWithHandle(item, MasterListTransformPanel, 0, 0, 0, 0);
            button.name = item;
            button.onClick.AddListener(() => action(button.name));
        }
    }

    //Right bound in master list
    public void CreateModeButtonList(string[] items, PlanningController.ChangeMode action)
    {
        var children = new List<GameObject>();
        foreach (Transform child in ModeTransformPanel) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        for (var i = 0; i < items.Length; i++)
        {
            var button = fillPanel.CreateButtonOnUIWithHandle(items[i], ModeTransformPanel, 0, 0, 0, 0);
            button.name = items[i];
            var buttonMode = i;
            button.onClick.AddListener(() => action(buttonMode));
        }
    }
}
