using System;
using System.Collections.Generic;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Database.Request;
using SmartHospital.Model;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static readonly InventoryRequest inventoryRequest = new InventoryRequest(HttpRoutes.INVENTORY, HttpRoutes.INVENTORYLIST);

    private InventoryCreateView CreateView;
    RoomDetailsView RoomView;

    private InventoryView View;

    private void Awake()
    {
        inventoryRequest.SetupHttpClient();
        CreateView = FindObjectOfType<InventoryCreateView>();
        View = FindObjectOfType<InventoryView>();
        RoomView = FindObjectOfType<RoomDetailsView>();

    }

    private void Start()
    {
        CreateView.OnCancelButtonClicked += () =>
        {
            CreateView.CreateInventarPanel.gameObject.SetActive(false);
        };

        CreateView.OnSaveButtonClicked += () =>
        {
            ClientRoom selectedClientRoom = SelectedRoomStatus.selectedObject.GetComponent<ClientRoom>();

            print(selectedClientRoom.MyRoom.NamePlate.BuildingSection);
            var inventoryitem = new InventoryItem
            {
                Name = CreateView.ItemName,
                Cost = CreateView.ItemCost,
                Designation = CreateView.ItemDesignation,
                Mass = CreateView.ItemMass,
                Size = CreateView.ItemSize,
                Room = selectedClientRoom.MyRoom,
                Item_Code = CreateView.ItemCode,
                Product_Group = CreateView.ItemProductGroup,
                Procurement_Staff = new Person()
                {
                    FirstName = "FN",
                    LastName = "LN",
                    Address = new Address()
                    {
                        Street = "",
                        City = "",
                        POB_Address = 0,
                        Country = "",
                        Region = "",
                        Street_Number = "0",
                        Zip_Code = 0
                    }
                    ,
                    FormOfAdress = "NONE",
                    Title = "NONE",
                    E_Mail = ""
                },
                Department = new Department()
                {
                    Name = "",
                    CostCentre = 9999,
                    FunctionalAreas = new List<FunctionalArea>() { new FunctionalArea() { Name = "", Type = "OTHER" }, new FunctionalArea { Name = "Empty", Type = "OTHER" } }
                },
                Model_Path = "",
                Producer = new Producer()
                {
                    Name = "",
                    Address = new Address()
                    {
                        Street = "",
                        City = "",
                        POB_Address = 0,
                        Country = "",
                        Region = "",
                        Street_Number = "0",
                        Zip_Code = 0
                    },
                    Description = "",
                    E_Mail = "",
                    Label_Path = "",
                    Remarks = "",
                    Website = "",
                    Responsible_Person = new Person()
                    {
                        FirstName = "FN",
                        LastName = "LN",
                        Address = new Address()
                        {
                            Street = "",
                            City = "",
                            POB_Address = 0,
                            Country = "",
                            Region = "",
                            Street_Number = "0",
                            Zip_Code = 0
                        }
                    ,
                        FormOfAdress = "NONE",
                        Title = "NONE",
                        E_Mail = ""
                    }
                },
                Product_Sheet_Path = ""

            };

            inventoryRequest.Put(inventoryitem);
            CreateView.CreateInventarPanel.gameObject.SetActive(false);

        };

        View.OnEditButtonClick += () =>
        {
            CreateView.CreateInventarPanel.gameObject.SetActive(true);

            CreateView.ItemName = View.ItemNameText.text;
            CreateView.ItemCost = Convert.ToDouble(View.ItemCostText.text);
            CreateView.ItemDesignation = View.ItemDesignationText.text;
            CreateView.ItemMass = Convert.ToDouble(View.ItemMassText.text);
            CreateView.ItemSize = Convert.ToDouble(View.ItemSizeText.text);
            CreateView.ItemCode = View.ItemCodeText.text;
            CreateView.ItemProductGroup = View.ItemProducerNameText.text;

        };

        View.OnCancelButtonClick += () =>
        {
            View.InventarPanel.gameObject.SetActive(false);
            RoomView.TransformParentPanel.gameObject.SetActive(true);

        };
    }
}