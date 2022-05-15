using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class InventoryView
{
    public TMP_Dropdown ItemDropdown { get; set; }

    public TextMeshProUGUI ItemNameText { get; set; }

    public TextMeshProUGUI ItemCostText { get; set; }

    public TextMeshProUGUI ItemDesignationText { get; set; }

    public TextMeshProUGUI ItemSizeText { get; set; }

    public TextMeshProUGUI ItemCodeText { get; set; }

    public TextMeshProUGUI ItemMassText { get; set; }

    public TextMeshProUGUI ItemProducerNameText { get; set; }

    public TextMeshProUGUI ItemLinkedItemText { get; set; }

    public TextMeshProUGUI ItemProductGroupText { get; set; }

    public Transform InventarPanel { get; set; }

    public Button EditButton { get; set; }

    public Button CancelButton { get; set; }


    public void ToggleInventarPanel()
    {
        InventarPanel.gameObject.SetActive(!InventarPanel.gameObject.activeSelf);
    }
}