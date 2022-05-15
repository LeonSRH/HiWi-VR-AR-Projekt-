using UnityEngine.EventSystems;

public class ItemDropHandler : IItemDropHandler, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        ItemDragHandler dragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

        if (dragHandler != null)
            dragHandler.parentToReturnTo = this.transform;
    }

}
