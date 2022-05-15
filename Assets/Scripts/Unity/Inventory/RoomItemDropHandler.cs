using SmartHospital.Model;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomItemDropHandler : IItemDropHandler, IDropHandler

{
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItemUIController inventoryScriptableObject = eventData.pointerDrag.GetComponent<InventoryItemUIController>();

        if (!(inventoryScriptableObject.InventoryItemPlaceholder.inRoom) && SelectedRoomStatus.getLockStatus())
        {
            ItemDragHandler dragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            var clientRoom = SelectedRoomStatus.selectedObject.GetComponent<ClientRoom>();
            InventoryItemSO item = inventoryScriptableObject.InventoryItemPlaceholder;

            //Add item to Room
            clientRoom.AddInventoryItem(new InventoryItem() { Name = item.name, Item_Code = item.item_code });

            //Refresh inventory list for room
            InventoryMainUIController uiController = FindObjectOfType<InventoryMainUIController>();

            dragHandler.parentPlaceholder = dragHandler.placeHolder.transform.parent;
            Destroy(dragHandler.placeHolder.gameObject);
            uiController.CreateInventoryUIForRoom(SelectedRoomStatus.selectedObject);

        }
        else
        {
            Debug.LogError("Can't add item to full inventory List.");
        }
    }
}
