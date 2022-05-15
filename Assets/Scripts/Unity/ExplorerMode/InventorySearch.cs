using SmartHospital.Model;
using System.Collections.Generic;
using UnityEngine;

public class InventorySearch : MonoBehaviour
{
    LuceneSearcher searcher = new LuceneSearcher();

    private void Start()
    {
        var rooms = RoomSearch.GetAllGORooms();
        List<InventoryItem> inventoryitems = new List<InventoryItem>();
        foreach (GameObject room in rooms)
        {
            foreach (InventoryItem item in room.GetComponent<ClientRoom>().InventoryItems)
            {
                inventoryitems.Add(item);
            }
        }
        searcher.createIndex(inventoryitems);
    }

    public HashSet<string> useLuceneIndexSearch(string input)
    {
        if (!input.Equals("") && !input.Equals(" "))
        {
            HashSet<string> invetoryItemIds = searcher.useInventoryIndex(input);

            if (invetoryItemIds.Count < 1)
            {
                invetoryItemIds = searcher.useInventoryIndex(input + "*");
            }
            return invetoryItemIds;
        }

        return null;
    }

}
