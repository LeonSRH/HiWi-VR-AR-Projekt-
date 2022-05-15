using System;
using Newtonsoft.Json;
using SmartHospital.Controller.ExplorerMode.Rooms;

namespace SmartHospital.Model
{
    public class InventoryItem
    {
        [JsonProperty(PropertyName = "itemCode")]
        public string Item_Code { get; set; }

        [JsonProperty(PropertyName = "name")] public string Name { get; set; }

        [JsonProperty(PropertyName = "modelPath")]
        public string Model_Path { get; set; }

        [JsonProperty(PropertyName = "designation")]
        public string Designation { get; set; }

        [JsonProperty(PropertyName = "procurementStaff")]
        public Person Procurement_Staff { get; set; }

        [JsonProperty(PropertyName = "department")]
        public Department Department { get; set; }

        [JsonProperty(PropertyName = "cost")] public double Cost { get; set; }

        [JsonProperty(PropertyName = "size")] public double Size { get; set; }

        [JsonProperty(PropertyName = "mass")] public double Mass { get; set; }

        [JsonProperty(PropertyName = "producer")]
        public Producer Producer { get; set; }

        [JsonProperty(PropertyName = "productSheetPath")]
        public string Product_Sheet_Path { get; set; }

        [JsonProperty(PropertyName = "group")] public string Product_Group { get; set; }

        [JsonProperty(PropertyName = "intendedPlace")]
        public ServerRoom Room { get; set; }

        public override bool Equals(object obj)
        {
            return obj is InventoryItem item &&
                   Item_Code == item.Item_Code;
        }
    }

    public enum Product_Group
    {
        NONE,
        MOBILE,
        NOTMOBILE,
        CONSUMABLE
    }

    public class Consumable : InventoryItem
    {
        [JsonProperty(PropertyName = "useByDate")]
        public DateTime Use_By_Date { get; set; }

        [JsonProperty(PropertyName = "documentation")]
        public Documentation Documentation { get; set; }
    }

    public class Medical_Device : InventoryItem
    {
        [JsonProperty(PropertyName = "firstLevelSupport")]
        public Person First_Level_Support { get; set; }

        [JsonProperty(PropertyName = "secondLevelSupport")]
        public Person Second_Level_Support { get; set; }

        [JsonProperty(PropertyName = "charge")]
        public float Charge { get; set; }

        [JsonProperty(PropertyName = "maintenanceFunction")]
        public DateTime Maintenance_Function { get; set; }

        [JsonProperty(PropertyName = "maintenanceAC")]
        public DateTime Maintenance_AC { get; set; }

        [JsonProperty(PropertyName = "technicalDocumentation")]
        public Technical_Documentation Technical_Documentation { get; set; }

        [JsonProperty(PropertyName = "trackingSystem")]
        public Tracking_System Tracking_System { get; set; }

        [JsonProperty(PropertyName = "macAddress")]
        public string Mac_Address { get; set; }
    }

    public class Tracking_System
    {
        [JsonProperty(PropertyName = "position")]
        public Vector3 Position { get; set; }

        [JsonProperty(PropertyName = "updateDate")]
        public DateTime Update_Date { get; set; }

        [JsonProperty(PropertyName = "active")]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "macAddress")]
        public string Mac_Address { get; set; }

        [JsonProperty(PropertyName = "charge")]
        public float Charge { get; set; }
    }
}