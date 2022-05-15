using SmartHospital.Model;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public partial class InventoryMainUIController : MonoBehaviour
{
    public Transform InventoryRoomMainPanel;

    public Transform InventoryListMainPanel;

    public TextMeshProUGUI RoomNumberText;

    private InventorySearch InventorySearch;

    public TextMeshProUGUI input;

    //Prefab for the UI inventory item
    public GameObject inventoryPrefab;

    //Inventory item list of all rooms, which should be added to the inventory slots
    public InventoryItemSO[] inventoryItems { get; set; }

    //All inventory items, which should be added to the panel of all inventory slots
    public InventoryItemSO[] inventoryListItems { get; set; }

    public List<InventoryItem> inventoryItemsInRooms;
    private int initItemList = 0;
    private async void Start()
    {
        InventorySearch = FindObjectOfType<InventorySearch>();
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("RoomCollider");

        inventoryItemsInRooms = new List<InventoryItem>();
        inventoryListItems = new InventoryItemSO[]{ new InventoryItemSO("Tisch","1", Resources.Load("table", typeof(Sprite)) as Sprite, false),
        new InventoryItemSO("Stuhl","2", Resources.Load("chair", typeof(Sprite)) as Sprite, false),
        new InventoryItemSO("Steckdose","3", Resources.Load("plug", typeof(Sprite)) as Sprite, false),
        new InventoryItemSO("Sonstiges","0", Resources.Load("default", typeof(Sprite)) as Sprite, false)};
        initItemList = inventoryListItems.Length;
        ///TODO: Replace with server items
        //var inventoryCacheList = await InventoryController.inventoryRequest.GetList();

        //Find all inventory items in rooms
        foreach (GameObject room in rooms)
        {
            if (room.GetComponent<ClientRoom>() != null && room.GetComponent<ClientRoom>().InventoryItems != null)
            {
                foreach (InventoryItem item in room.GetComponent<ClientRoom>().InventoryItems)
                {
                    inventoryItemsInRooms.Add(item);

                }
            }
        }

        inventoryItems = new InventoryItemSO[inventoryItemsInRooms.Count];

        //Add Scriptable objects to array of all inventory items
        var inRooms = inventoryItemsInRooms.ToArray();
        for (int j = 0; j < inventoryItemsInRooms.Count; j++)
        {
            inventoryItems[j] = ScriptableObject.CreateInstance<InventoryItemSO>();
            inventoryItems[j].name = inRooms[j].Name;
            inventoryItems[j].item_code = inRooms[j].Item_Code;
            inventoryItems[j].inRoom = true;

            switch (inRooms[j].Name)
            {
                case "Tisch":
                    inventoryItems[j].image = Resources.Load("table", typeof(Sprite)) as Sprite;
                    break;
                case "Stuhl":
                    inventoryItems[j].image = Resources.Load("chair", typeof(Sprite)) as Sprite;
                    break;
                case "Steckdose":
                    inventoryItems[j].image = Resources.Load("plug", typeof(Sprite)) as Sprite;
                    break;
                case "Sonstiges":
                    inventoryItems[j].image = Resources.Load("default", typeof(Sprite)) as Sprite;
                    break;
                default:
                    inventoryItems[j].image = Resources.Load("default", typeof(Sprite)) as Sprite;
                    break;

            }
        }

        //Create first page of inventory items
        CreateInventoryUI(InventoryRoomMainPanel, inventoryItems);

        //All inventory items in cache
        CreateInventoryUI(InventoryListMainPanel, inventoryListItems);

        InventoryListMainPanel.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// Takes the inventory panel with inventoryItems prefabs and displays the given items
    /// </summary>
    /// <param name="panel">On which panel the items should be shown</param>
    /// <param name="items">Inventory Items as Scriptable object, which should be displayed on the panel</param>
    public void CreateInventoryUI(Transform panel, InventoryItemSO[] items)
    {
        RoomNumberText.enabled = false;
        foreach (Transform childInPanel in panel)
        {
            childInPanel.gameObject.SetActive(false);
        }

        for (int i = 0; i < items.Length; i++)
        {
            if (i < panel.childCount)
            {
                var itemCreated = panel.GetChild(i);

                itemCreated.GetComponent<InventoryItemUIController>().InventoryItemPlaceholder = items[i];
                itemCreated.GetComponent<InventoryItemUIController>().Display();
                itemCreated.gameObject.SetActive(true);
                itemCreated.transform.SetParent(panel);
            }
        }
    }

    /// <summary>
    /// Room specific UI elements in room inventory panel
    /// </summary>
    /// <param name="room">which inventory items should be shown in the room inventory panel</param>
    public void CreateInventoryUIForRoom(GameObject room)
    {
        RoomNumberText.enabled = true;
        RoomNumberText.SetText("Raum: " + room.GetComponent<ClientRoom>().RoomName);
        for (int c = 0; c < InventoryRoomMainPanel.childCount; c++)
        {
            InventoryRoomMainPanel.GetChild(c).gameObject.SetActive(false);
        }

        var InventoryObjects = room.GetComponent<ClientRoom>().InventoryItems.ToArray();

        for (int i = 0; i < InventoryObjects.Length; i++)
        {
            var itemCreated = InventoryRoomMainPanel.GetChild(i);

            if (!itemCreated.name.Equals("Placeholder"))
            {
                itemCreated.gameObject.SetActive(true);
                itemCreated.transform.SetParent(InventoryRoomMainPanel);

                var itemUiController = itemCreated.GetComponent<InventoryItemUIController>();
                itemUiController.InventoryItem = InventoryObjects[i];

                InventoryItemSO item = ScriptableObject.CreateInstance<InventoryItemSO>();
                item.name = InventoryObjects[i].Name;
                item.inRoom = true;
                item.item_code = InventoryObjects[i].Item_Code;
                item.image = Resources.Load("default") as Sprite;
                itemUiController.InventoryItemPlaceholder = item;

                itemCreated.GetComponent<InventoryItemUIController>().Display();
            }



        }

    }


    public void CreateInventoryUIForSearchResult(HashSet<string> inventoryItemIds)
    {
        List<InventoryItemSO> output = new List<InventoryItemSO>();

        foreach (InventoryItemSO item in inventoryListItems)
        {
            foreach (string inventoryId in inventoryItemIds)
            {
                if (item.item_code.Equals(inventoryId))
                {
                    output.Add(item);
                }
            }
        }

        CreateInventoryUI(InventoryRoomMainPanel, output.ToArray());
    }

    public void SearchInventoryItems()
    {
        var result = InventorySearch.useLuceneIndexSearch(input.text);

        CreateInventoryUIForSearchResult(result);
    }
}
