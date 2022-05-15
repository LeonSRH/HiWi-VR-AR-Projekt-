using UnityEngine;
using TMPro;
using SmartHospital.Model;

public class MobileSearch : MonoBehaviour
{
    public TMP_InputField searchInputField;
    private GameObject[] roomCollider;
    private GameObject foundObject;

    private GameObject mainCamera;
    private GameObject overViewCamera;

    public GameObject[] floors = new GameObject[7];
    public Material selectedMaterial;
    public Material unselectedMaterial;

    void Start()
    {
        roomCollider = GameObject.FindGameObjectsWithTag("RoomCollider");

        mainCamera = GameObject.Find("Main Camera");
        overViewCamera = GameObject.Find("Overview Camera");
    }

    public void StartSearch()
    {
        foreach (GameObject room in roomCollider)
        {
            if (room.GetComponent<ClientRoom>().RoomName != null)
            {
                var txt = searchInputField.text;
                if (room.GetComponent<ClientRoom>().RoomName.Contains(txt))
                {
                    markFoundObject(room);
                    foundObject = room;
                    setCameraOverview();
                }
            }
        }
    }

    private void markFoundObject(GameObject obj)
    {
        if (obj != null)
        {
            obj.GetComponent<Renderer>().material = selectedMaterial;
        }
    }

    private void setCameraOverview()
    {
        overViewCamera.SetActive(true);
        mainCamera.SetActive(false);

        foreach (GameObject floor in floors)
        {
            floor.SetActive(false);
        }

        foundObject.transform.parent.transform.parent.transform.parent.gameObject.SetActive(true);


    }

    public void setMainView()
    {
        foreach (GameObject f in floors)
        {
            f.SetActive(true);
        }

    }

    public void setFloorOverview(int floor)
    {
        foreach (GameObject f in floors)
        {
            f.SetActive(false);
        }

        switch (floor)
        {
            case 0:
                floors[0].SetActive(true);
                break;
            case 1:
                floors[1].SetActive(true);
                break;
            case 2:
                floors[2].SetActive(true);
                break;
            case 3:
                floors[3].SetActive(true);
                break;
            case 4:
                floors[4].SetActive(true);
                break;
            case 5:
                floors[5].SetActive(true);
                break;
        }
    }
}
