using UnityEngine;

public class RoomMaterialController : MonoBehaviour
{
    bool _selected;
    bool _searchResult;
    public bool selected
    {
        get => selected;
        set
        {
            _selected = value;
            selected = _selected;
        }
    }

    public bool searchResult
    {
        get => searchResult;
        set
        {
            _searchResult = value;
            searchResult = _searchResult;
        }
    }

    private void OnMouseExit()
    {
        unHoverObject();
    }

    private void OnMouseEnter()
    {
        hoverObject();
    }

    private void OnMouseDown()
    {
        selected = !selected;
        if (selected)
        {
        }
        else
        {
        }
    }

    /// <summary>
    /// sets the material to selectMaterial and "hovers" the collider
    /// </summary>
    public void hoverObject() { }


    /// <summary>
    /// "un hovers" the object - sets the material to the current material
    /// </summary>
    public void unHoverObject()
    {
    }

    /// <summary>
    /// sets the current material of the collider - status collider
    /// </summary>
    /// <param name="mat"></param>
    public void setCurrentMaterial(Material mat)
    {
        if (!searchResult)
        {
            // currentMaterial = mat;
        }
    }

}
