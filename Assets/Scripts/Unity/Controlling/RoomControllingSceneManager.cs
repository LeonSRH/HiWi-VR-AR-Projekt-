using SmartHospital.Model;
using SmartHospital.TrainingRoom;
using System.Collections.Generic;
using UnityEngine;

public class RoomControllingSceneManager : MonoBehaviour
{
    public Material searchMaterial { get; set; }
    public Material selectMaterial { get; set; }
    public Material unselectedMaterial { get; set; }

    CameraOrbit cameraOrbit;

    GameObject _selectedRoom;
    private List<GameObject> _selectedRooms = new List<GameObject>();

    FollowMouseUI followMouse;

    private void Start()
    {
        selectMaterial = (Material)Resources.Load("SelectMaterial", typeof(Material));
        unselectedMaterial = (Material)Resources.Load("UnselectedMaterial", typeof(Material));
        searchMaterial = (Material)Resources.Load("SearchMaterial", typeof(Material));
        cameraOrbit = FindObjectOfType<CameraOrbit>();
        followMouse = FindObjectOfType<FollowMouseUI>();

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraOrbit.mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.transform.tag.Equals("RoomCollider"))
                {
                    if (_selectedRoom != hit.transform.gameObject && !_selectedRooms.Contains(hit.transform.gameObject))
                    {
                        _selectedRoom = hit.transform.gameObject;
                        selectObject(_selectedRoom);
                    }
                    else if (_selectedRooms.Contains(hit.transform.gameObject))
                    {
                        unselectObject(hit.transform.gameObject);
                    }

                }
            }
        }
    }

    private void selectObject(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = selectMaterial;

        var clientRoom = obj.GetComponent<ClientRoom>().MyRoom;
        followMouse.enableInfoPanel();

        _selectedRooms.Add(obj);

    }

    private void unselectObject(GameObject obj)
    {
        obj.GetComponent<Renderer>().material = unselectedMaterial;
        _selectedRooms.Remove(obj);
        _selectedRoom = null;
    }

}
