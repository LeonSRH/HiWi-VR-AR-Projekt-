using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class InventoryCreateView
{
    public Action OnSaveButtonClicked;
    public Action OnCancelButtonClicked;
    public TMP_InputField ItemNameInput { get; set; }

    public TMP_InputField ItemCostInput { get; set; }

    public TMP_InputField ItemDesignationInput { get; set; }

    public TMP_InputField ItemSizeInput { get; set; }

    public TMP_InputField ItemCodeInput { get; set; }

    public TMP_InputField ItemMassInput { get; set; }

    public TMP_InputField ItemResidualValue { get; set; }

    public TMP_Dropdown ItemProducerDropdown { get; set; }

    public TMP_Dropdown LinkedItemDropdown { get; set; }

    public TMP_Dropdown ProductGroupDropdown { get; set; }

    public Transform CreateInventarPanel { get; set; }
    public Button SaveButton { get; set; }
    public Button CancelButton { get; set; }

    private void Start()
    {
        SetupButtons();
        SetupDropdowns();
        CreateInventarPanel.gameObject.SetActive(false);
    }
    public void SetupButtons()
    {
        SaveButton.onClick.AddListener(() => OnSaveButtonClicked?.Invoke());
        CancelButton.onClick.AddListener(() => OnCancelButtonClicked?.Invoke());

    }

    public void SetupDropdowns()
    {
        ItemProducerDropdown.ClearOptions();
        ItemProducerDropdown.AddOptions(new List<string>() { "Hersteller 1", "Hersteller 2", "Hersteller 3" });

        LinkedItemDropdown.ClearOptions();
        LinkedItemDropdown.AddOptions(new List<string>() { "Objekt 1", "Objekt 2", "Objekt 3" });

        ProductGroupDropdown.ClearOptions();
        ProductGroupDropdown.AddOptions(new List<string>() {
            "Medizingerät statisch",
                "Statisch",
                "Medizingerät mobil",
                "Mobil",
                "Mikrologistik",
                "Sonstiges"});
    }
}