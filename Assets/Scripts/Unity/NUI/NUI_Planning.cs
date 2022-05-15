using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NUI_Planning : MonoBehaviour
{
    FillPanel fillPanel;

    PlanningMainView roomMasterListView;
    MainView mainView;

    Transform ModeTransformPanel;
    Transform MasterListTransformPanel;

    private void Awake()
    {
        fillPanel = FindObjectOfType<FillPanel>();
        roomMasterListView = FindObjectOfType<PlanningMainView>();
        mainView = FindObjectOfType<MainView>();


        CreateMasterlist();
        CreateModePanel();

    }

    /// <summary>
    /// Right bound
    /// </summary>
    public void CreateMasterlist()
    {
        //Parent Panel

        Transform ParentObject = fillPanel.CreateParentObject(fillPanel.transform);
        ParentObject.localPosition = new Vector3(-200, -287, 0);
        ParentObject.name = "AARGH";
        mainView.PlanningButtonPanel = ParentObject;


        Transform ModePanelTransform = fillPanel.CreatePanel(100, 300, ParentObject,
            UI_Panel.enumForLayoutGroup.Vertical, true);


        Transform ParentTransform = fillPanel.CreatePanel(300, 900, UI_OrientationOnScreen.enumForOrientations.Left,
            UI_Panel.enumForLayoutGroup.Vertical, true); // Hier braucht es keine anordnung der KindObjekte

        ModePanelTransform.name = "Modi";
        ModeTransformPanel = ModePanelTransform;
        mainView.PlanningModePanel = ParentTransform;

        ParentTransform.name = "Master List";

        var scrollrect = ParentTransform.gameObject.AddComponent<ScrollRect>();
        Transform ScrollRectPanel = fillPanel.CreatePanel(100, 300, ParentTransform, UI_Panel.enumForLayoutGroup.Vertical, true);
        ScrollRectPanel.name = "Scroll Rect";
        MasterListTransformPanel = ScrollRectPanel;

        scrollrect.content = (RectTransform)ScrollRectPanel;
        scrollrect.scrollSensitivity = 10;

        var inventoryView = FindObjectOfType<InventoryView>();
        inventoryView.InventarPanel = ParentTransform;
        
    }

    /// <summary>
    /// Left bound
    /// </summary>
    public void CreateModePanel()
    {
        Transform ParentTransform = fillPanel.CreatePanel(300, 300, UI_OrientationOnScreen.enumForOrientations.Left,
            UI_Panel.enumForLayoutGroup.Vertical, true);
        ParentTransform.name = "Mode List";
        mainView.PlanningPanel = ParentTransform;

        roomMasterListView.InventoryModeButton =
            fillPanel.CreateButtonOnUIWithHandle("Inventar", ParentTransform, 0, 0, 0, 0);

        roomMasterListView.RoomModeButton =
            fillPanel.CreateButtonOnUIWithHandle("Rauminformation", ParentTransform, 0, 0, 0, 0);

        roomMasterListView.WorkersModeButton =
            fillPanel.CreateButtonOnUIWithHandle("Arbeitsplätze", ParentTransform, 0, 0, 0, 0);
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