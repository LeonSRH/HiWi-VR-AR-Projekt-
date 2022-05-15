using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class IItemDropHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        ItemDragHandler dragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        if (dragHandler != null)
            dragHandler.parentPlaceholder = this.transform;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        ItemDragHandler dragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        if (dragHandler != null && dragHandler.parentPlaceholder == this.transform)
            dragHandler.parentPlaceholder = this.transform;

    }
}
