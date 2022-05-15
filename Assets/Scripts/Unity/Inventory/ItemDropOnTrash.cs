using SmartHospital.Model;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDropOnTrash : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Image inventoryWarnImage;
    private Color inventoryWarnImageStartColor;
    public Color inventoryWarnImageColor;

    private void Awake()
    {
        inventoryWarnImageStartColor = inventoryWarnImage.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
            inventoryWarnImage.color = inventoryWarnImageColor;

    }

    public void OnDrop(PointerEventData eventData)
    {
        //Maybe show "Are you sure?" window (Tip)
        //Delete inventory item from room/ from list
        InventoryItemUIController inventoryScriptableObject = eventData.pointerDrag.GetComponent<InventoryItemUIController>();

        if ((inventoryScriptableObject.InventoryItem.Item_Code != null) && SelectedRoomStatus.getLockStatus())
        {
            ItemDragHandler dragHandler = eventData.pointerDrag.GetComponent<ItemDragHandler>();

            var clientRoom = SelectedRoomStatus.selectedObject.GetComponent<ClientRoom>();

            //Delete item to Room
            clientRoom.RemoveInventoryItem(inventoryScriptableObject.InventoryItem.Item_Code);




            //Refresh inventory list for room
            InventoryMainUIController uiController = FindObjectOfType<InventoryMainUIController>();

            if (dragHandler != null)
            {
                Destroy(dragHandler.placeHolder.gameObject);
            }

            eventData.pointerDrag.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Can't add item to full inventory List.");
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inventoryWarnImage.color = inventoryWarnImageStartColor;

    }
}
