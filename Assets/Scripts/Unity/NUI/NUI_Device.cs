using System.Collections.Generic;
using SmartHospital.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using SmartHospital.Controller.ExplorerMode.Rooms;
using TMPro;
using Vector3 = UnityEngine.Vector3;

public class NUI_Device : MonoBehaviour
{
    private readonly List<Medical_Device> _listOfDevices = new List<Medical_Device>();
    FillPanel fillPanel;

    TextMeshProUGUI _id, _name, _producer, _info;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();

        // fixme: Generate dummy medical devices
        _listOfDevices.Add(new Medical_Device
        {
            Name = "SOMATOM Perspective",
            Room = new ServerRoom
            {
                RoomName = "6420.01.031"
            },
            Producer = new Producer
            {
                Name = "Siemens Healthineers"
            },
            Designation = "CT Gerät"
        });
        _listOfDevices.Add(new Medical_Device
        {

            Name = "ACUSION Sequoia",
            Room = new ServerRoom
            {
                RoomName = "6420.03.618"
            },
            Producer = new Producer
            {
                Name = "Siemens Healthineers"
            },
            Designation = "Ultraschall Gerät"
        });
        _listOfDevices.Add(new Medical_Device
        {
            Name = "SOMATOM Perspective",
            Room = new ServerRoom
            {
                RoomName = "6420.00.610"
            },
            Producer = new Producer
            {
                Name = "Siemens Healthineers"
            },
            Designation = "Videolaryngoskopie"
        });


        fillPanel = FindObjectOfType<FillPanel>();

        _createHeader();
        _createList();
        _createDeviceInformation();
    }


    void _createHeader()
    {
        float yPos = 505;
        fillPanel.CreateHeaderOnUI("Devices", transform, 300, 65, -700, yPos);
        fillPanel.CreateButtonOnUI("Return", transform, (_value) => { SceneManager.LoadScene("MainScene_Neutral"); }, 100, 50, 700, -yPos);
    }

    void _createList()
    {
        Transform Parent = fillPanel.CreateParentObject(transform);
        Parent.transform.localPosition = new Vector3(0, 0, 0);

        Transform table =
            fillPanel.CreatePanel(1400, 150, Parent, UI_Panel.enumForLayoutGroup.Vertical,
                false);
        table.transform.localPosition = new Vector3(0, 0, 0);


        foreach (Medical_Device medical_Device in _listOfDevices)
        {
            Transform localElement =
                fillPanel.CreatePanel(300, 30, table, UI_Panel.enumForLayoutGroup.Horizontal, false);

            fillPanel.CreateTextElementOnUI($"Gerät Nr.: {medical_Device.Item_Code}", localElement);
            fillPanel.CreateButtonOnUI("Info", localElement, value =>
            {
                _id.text = medical_Device.Item_Code;
                _name.text = medical_Device.Name;
                _producer.text = medical_Device.Producer.Name;
                _info.text = medical_Device.Designation;
            });
            fillPanel.CreateButtonOnUI("Position", localElement, value =>
            {
                PlayerPrefs.SetString("device_position", medical_Device.Room.RoomName);
                PlayerPrefs.SetInt("flag", 1);
                SceneManager.LoadScene("MainScene_Neutral");
            });
        }
    }

    void _createDeviceInformation()
    {
        Transform parent = fillPanel.CreateParentObject(transform);
        parent.transform.localPosition = new Vector3(200, 100, 0);

        RectTransform infoTransform = fillPanel.CreatePanel(600, 300, parent, UI_Panel.enumForLayoutGroup.Vertical, false).GetComponent<RectTransform>();
        infoTransform.anchorMin = new Vector2(0.5f, 0.5f);
        infoTransform.anchorMax = new Vector2(0.5f, 0.5f);
        infoTransform.transform.localPosition = new Vector3(200, 200, 0);
        _id = _createKeyValuePair(infoTransform, "ID");
        _name = _createKeyValuePair(infoTransform, "Name");
        _producer = _createKeyValuePair(infoTransform, "Hersteller");
        _info = _createKeyValuePair(infoTransform, "Info");
    }

    TextMeshProUGUI _createKeyValuePair(Transform infoTransform, string key)
    {
        RectTransform rowTransform = fillPanel.CreatePanel(600, 300, infoTransform, UI_Panel.enumForLayoutGroup.Horizontal, false).GetComponent<RectTransform>();
        fillPanel.CreateTextElementOnUI(key, rowTransform);
        return fillPanel.CreateTextElementOnUIWithHandle("", rowTransform, 0, 0, 0, 0);
    }



}