using SmartHospital.Model;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Created 28.02.2019
/// Autor: Katharina Shits
/// </summary>
public class MobileUiController : MonoBehaviour
{

    [Header("Cams")]
    public GameObject mainCam;
    public GameObject overViewCam;

    [Header("Active Game Objects")]
    public GameObject[] floors;
    private GameObject activeFloor;

    [Header("UI Elements")]
    public TMP_InputField input;

    private List<GameObject> rooms;
    private RoomSearch ExpandSearch;

    [Header("Materials")]
    public Material searchRendMaterial;
    public Material unselectMaterial;

    private void Start()
    {
        var roomsArray = GameObject.FindGameObjectsWithTag("RoomCollider");
        rooms = roomsArray.OfType<GameObject>().ToList();

        ExpandSearch = FindObjectOfType<RoomSearch>();
        setMainView();
    }

    /// <summary>
    /// Uses the lucene search for the mobile input field
    /// </summary>
    public void UseLuceneSearch()
    {
        var inputText = input.text;
        HashSet<string> foundings = new HashSet<string>();

        if (inputText.Length > 0)
        {
            var inputWords = ExpandSearch.splitInputSearchWordsIntoList(inputText);

            if (inputWords.Count == 1)
            {
                foundings = ExpandSearch.useLuceneIndexSearch(inputText + "*");
            }
            else
            {
                foundings = ExpandSearch.MoreWordsLuceneSearch(inputWords);
            }

            resetGO();
            if (foundings.Count == 1)
            {
                setOverViewView(getFloorFromRoomId(foundings.First()));
                ExpandSearch.markFoundObjects(foundings);
            }
            else
            {
                setMainView();
                ExpandSearch.markFoundObjects(foundings);
            }

        }
    }

    private void resetGO()
    {

        foreach (GameObject room in rooms)
        {
            var roomCollider = room.GetComponent<RoomColliderMaterialController>();
            if (room.GetComponent<ClientRoom>() != null)
            {
                room.GetComponent<Renderer>().material = unselectMaterial;
                roomCollider.setSelected(false);
                roomCollider.setSearchResultActive(false);
                roomCollider.setCurrentMaterial(unselectMaterial);

            }
        }
    }

    public void setMainView()
    {
        foreach (GameObject go in floors)
        {
            go.SetActive(true);
        }
        mainCam.SetActive(true);
        overViewCam.SetActive(false);
        overViewCam.GetComponent<Camera>().enabled = false;
        mainCam.GetComponentInChildren<Camera>().enabled = true;
    }

    public void setOverViewView(int floor)
    {
        foreach (GameObject go in floors)
        {
            go.SetActive(false);
        }
        floors[floor].SetActive(true);
        overViewCam.SetActive(true);
        overViewCam.GetComponent<Camera>().enabled = true;
        mainCam.SetActive(false);
        mainCam.GetComponentInChildren<Camera>().enabled = false;
    }


    private int getFloorFromRoomId(string roomId)
    {
        int output = 0;

        string[] textInputStrings = roomId.Split('.');

        switch (textInputStrings[1])
        {
            case "98":
                output = 0;
                break;
            case "99":
                output = 1;
                break;
            case "00":
                output = 2;
                break;
            case "01":
                output = 3;
                break;
            case "02":
                output = 4;
                break;
            case "03":
                output = 5;
                break;
            case "04":
                output = 6;
                break;
            default:
                break;
        }

        return output;
    }
}
