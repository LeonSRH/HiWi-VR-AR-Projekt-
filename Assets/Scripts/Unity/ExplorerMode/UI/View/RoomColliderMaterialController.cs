using SmartHospital.Model;
using UnityEngine;

public class RoomColliderMaterialController : MonoBehaviour
{
    private Material currentMaterial;
    private Material selectMaterial;
    private Material startedStartMaterial;

    private bool selected = false;
    private bool searchResult = false;


    private void Awake()
    {
        selectMaterial = (Material)Resources.Load("SelectMaterial", typeof(Material));
        startedStartMaterial = (Material)Resources.Load("UnselectedMaterial", typeof(Material));
        currentMaterial = startedStartMaterial;
    }

    private void OnMouseExit()
    {
        unHoverObject();
    }


    private void OnMouseEnter()
    {
        hoverObject();

        if (tag == "RoomCollider" && GetComponent<ClientRoom>().RoomName != "" && !SelectedRoomStatus.getLockStatus())
        {
            SelectedRoomStatus.hoverRoomInfo(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.name.Equals("Furniture"))
        {
            Debug.Log("Furtinure entered");
        }
    }

    private void OnMouseDown()
    {
        selected = !selected;
        if (selected)
        {
            SelectedRoomStatus.selectedObject = gameObject;
            SelectedRoomStatus.showRoomInfo(gameObject);
            setCurrentMaterial(selectMaterial);

            SelectedRoomStatus.setLockedStatus(true);
        }
        else
        {
            SelectedRoomStatus.setLockedStatus(false);
            setCurrentMaterial(startedStartMaterial);

            SelectedRoomStatus.setLockedStatus(false);
        }
    }

    public void hoverObject()
    {
        GetComponent<Renderer>().material = selectMaterial;
    }

    public void unHoverObject()
    {
        GetComponent<Renderer>().material = currentMaterial;
    }

    public void setSelected(bool sel)
    {
        selected = sel;
    }

    public bool getSelected()
    {
        return selected;
    }

    public void setCurrentMaterial(Material mat)
    {
        if (!searchResult)
        {
            currentMaterial = mat;
        }
    }

    public Material getCurrentMaterial()
    {
        return currentMaterial;
    }

    public bool getSearchResultActive()
    {
        return searchResult;
    }

    public void setSearchResultActive(bool result)
    {
        searchResult = result;
    }
}