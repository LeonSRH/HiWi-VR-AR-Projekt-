using System;
using System.Collections.Generic;
using SmartHospital.Model;
using UnityEngine;

public partial class InventoryView : MonoBehaviour
{
    InventoryItem _inventoryItem;

    public event Action OnItemDropdownValueChanges;
    public event Action OnEditButtonClick;
    public event Action OnCancelButtonClick;

    public void Setup()
    {
        if (SelectedRoomStatus.selectedObject != null)
        {
            List<InventoryItem> inventoryItems =
                SelectedRoomStatus.selectedObject.GetComponent<ClientRoom>().InventoryItems;
            List<string> options = new List<string>();
            if (inventoryItems != null)
            {
                foreach (InventoryItem item in inventoryItems)
                {
                    options.Add(item.Name);
                }
            }

            SetupDropdown(options);
            ShowInventoryItemInfo();
        }
    }

    private void Start()
    {
        SetupButtons();
    }

    public void SetupDropdown(List<string> options)
    {
        ItemDropdown.ClearOptions();
        ItemDropdown.AddOptions(options);

        OnItemDropdownValueChanges += () => ShowInventoryItemInfo();
    }

    public void SetupButtons()
    {
        CancelButton.onClick.AddListener(() => OnCancelButtonClick?.Invoke());
        EditButton.onClick.AddListener(() => OnEditButtonClick?.Invoke());
        ItemDropdown.onValueChanged.AddListener((i) => OnItemDropdownValueChanges?.Invoke());
    }

    public void ShowInventoryItemInfo()
    {
        var selectedRoom = SelectedRoomStatus.selectedObject;
        InventoryItem foundInventoryItem = selectedRoom.GetComponent<ClientRoom>().InventoryItems
            .Find((o) => o.Name.Equals(ItemDropdown.options[ItemDropdown.value].text));

        if (foundInventoryItem != null)
        {
            InventoryItem = foundInventoryItem;
        }
    }

    public InventoryItem InventoryItem
    {
        get
        {
            _inventoryItem = new InventoryItem()
            {
                Name = ItemNameText.text,
                Cost = (float)Convert.ToDouble(ItemCostText.text),
                Designation = ItemDesignationText.text,
                Size = (float)Convert.ToDouble(ItemSizeText.text),
                Item_Code = ItemCodeText.text,
                Mass = (float)Convert.ToDouble(ItemMassText.text),
                Producer = new Producer() { Name = ItemProducerNameText.text },
                Product_Group = Product_Group.NONE.ToString()
            };

            return _inventoryItem;
        }
        set
        {
            _inventoryItem = value;
            ItemNameText.SetText(_inventoryItem.Name);
            ItemCostText.SetText("" + _inventoryItem.Cost);
            ItemDesignationText.SetText(_inventoryItem.Designation);
            ItemSizeText.SetText(_inventoryItem.Size + "");
            ItemCodeText.SetText(_inventoryItem.Item_Code);
            ItemMassText.SetText(_inventoryItem.Mass + "");
            ItemProducerNameText.SetText(_inventoryItem.Producer.Name);
            ItemProductGroupText.SetText(_inventoryItem.Product_Group.ToString());
        }
    }
}