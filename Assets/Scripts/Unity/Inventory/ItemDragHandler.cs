using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Handler for the inventory item prefab, handles the drag event
/// 
/// @author KS
/// </summary>
public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [NonSerialized()]
    public Transform parentToReturnTo = null;
    public Transform parentPlaceholder = null;

    [NonSerialized()]
    public GameObject placeHolder = null;

    public Color highlitingColor;

    Color startColor;

    ItemDropHandler[] zones;

    private void Start()
    {
        zones = FindObjectsOfType<ItemDropHandler>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CreatePlaceholder();
        parentToReturnTo = transform.parent;
        parentPlaceholder = parentToReturnTo;

        //release the grid layout
        transform.SetParent(parentToReturnTo.parent);

        //Highlíght the drop zones
        foreach (ItemDropHandler zone in zones)
        {
            Transform zoneTransform = zone.transform;
            startColor = zoneTransform.gameObject.GetComponent<Image>().color;
            zoneTransform.gameObject.GetComponent<Image>().color = highlitingColor;
        }

        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        //Follow the mouse
        transform.position = eventData.position;

        if (placeHolder.transform.parent != parentPlaceholder)
            placeHolder.transform.SetParent(parentPlaceholder);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        foreach (ItemDropHandler zone in zones)
        {
            Transform zoneTransform = zone.transform;

            zoneTransform.gameObject.GetComponent<Image>().color = startColor;
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        Destroy(placeHolder);
    }

    /// <summary>
    /// Creates placeholder for inventory item on the actually inventory item slot
    /// </summary>
    private void CreatePlaceholder()
    {
        placeHolder = new GameObject();
        placeHolder.transform.SetParent(this.transform.parent);
        placeHolder.name = "Placeholder";
        LayoutElement element = placeHolder.AddComponent<LayoutElement>();
        element.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        element.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        element.flexibleHeight = 0;
        element.flexibleWidth = 0;
        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
        placeHolder.transform.SetParent(transform.parent);
    }
}
