using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.ExplorerMode.Rooms.TagSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmartHospital.Model
{
    /// <inheritdoc cref="IRoom" />
    /// <summary>
    ///     Class represents a room that should be used for coloring and handling of tags.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public sealed class ClientRoom : MonoBehaviour, IRoom
    {
        [Header("Room Id")]
        [SerializeField]
        string id;

        ServerRoom myRoom;

        public string RoomName => id;

        public ServerRoom MyRoom
        {
            get => myRoom;
            set
            { myRoom = value; }
        }

        public Collider Collider => myRoom.Collider;

        public List<Tag> Tags => myRoom.Tags;

        public List<InventoryItem> InventoryItems;

        void Awake()
        {
            myRoom = new ServerRoom(id, GetComponent<Collider>());

            InventoryItems = new List<InventoryItem>() { new InventoryItem() { Item_Code = "1" ,Name = "Tisch", Cost = 100, Designation = "Holz braun",
                Size = 5.1, Mass = 20, Producer = new Producer() { Name = "Tisch Hersteller", Description = "Beschreibung" }, Product_Group = "" },
                new InventoryItem() { Item_Code = "2" ,Name = "Stuhl", Cost = 200, Designation = "Holz weiß",
                Size = 8, Mass = 10, Producer = new Producer() { Name = "Schrank Hersteller", Description = "Beschreibung" }, Product_Group = "" },
                new InventoryItem() { Item_Code = "3" ,Name = "Computer", Cost = 50, Designation = "Standard",
                Size = 1, Mass = 1, Producer = new Producer() { Name = "Steckdosen Hersteller", Description = "Beschreibung" }, Product_Group = "" },
                new InventoryItem() { Item_Code = "0" ,Name = "Patientenliege", Cost = 200, Designation = "Holz weiß",
                Size = 5.1, Mass = 20, Producer = new Producer() { Name = "Schrank Hersteller", Description = "Beschreibung" }, Product_Group = "" }
            };
            myRoom.InventoryItems = InventoryItems;
            //Check for id input
            if (id == null)
            {
                Debug.LogError($"Room (Unity-Name: {gameObject.name}) id is null");
            }

            if (id.Length < 1)
            {
                Debug.LogError($"Room (Unity-Name: {gameObject.name}) has no ID");
            }

            if (Collider == null)
            {
                Debug.LogError($"Room (Unity-Name: {gameObject.name}, ID: {id}) has no collider");
            }

        }

        #region Tags

        public void AddTag(Tag tagToAdd) => myRoom.Tags.Add(tagToAdd);

        public void RemoveTag(Tag tagToRemove) => myRoom.Tags.Remove(tagToRemove);

        public void ReplaceTags(IEnumerable<Tag> newTags) => myRoom.ReplaceTags(newTags);

        public bool ContainsTags(params Tag[] tags)
        {
            return tags.All(lambdaTag => myRoom.Tags.Contains(lambdaTag));
        }

        public bool ContainsTagsExclusive(params Tag[] tags) => tags.Length == myRoom.Tags.Count && ContainsTags(tags);

        #endregion

        #region Inventory

        public void AddInventoryItem(InventoryItem itemToAdd) => myRoom.InventoryItems.Add(itemToAdd);

        public void RemoveInventoryItem(string item_code)
        {

            InventoryItem inventoryItemToRemove = null;
            foreach (InventoryItem item in myRoom.InventoryItems)
            {
                if (item.Item_Code.Equals(item_code))
                {
                    inventoryItemToRemove = item;
                }
            }

            if (inventoryItemToRemove != null)
            {
                Debug.Log("Removed.");
                myRoom.InventoryItems.Remove(inventoryItemToRemove);
                InventoryItems.Remove(inventoryItemToRemove);
            }
            else
                Debug.LogError($"Inventory Item with Item-Code: ({item_code}) doesn't exist for the Room (Unity-Name: {gameObject.name})");


        }

        public void ReplaceInventoryItems(IEnumerable<InventoryItem> newInventoryItems) =>
            myRoom.ReplaceInventoryItems(newInventoryItems);

        #endregion

        /// <summary>
        /// Sets the details for the room information into the roomdetail contrainer
        /// </summary>
        /// <param name="roomID">Room Id of a room</param>
        /// <param name="designation">Room designation</param>
        /// <param name="workspaces_number">number of workspaces</param>
        /// <param name="workspaces">workspaces with people belonging to this workspace</param>
        /// <param name="workspaces_with_access">people which have access to this room</param>
        /// <param name="access">true if this room is access destricted</param>
        /// <param name="displayedRoomInfo">the nameplate belonging to this room</param>
        /// <param name="tags">room tags</param>
        public void setNewRoomDetails(string designation, int workspaces_number,
            List<Workspace> workspaces, List<Worker> workspaces_with_access, Access_Style access_Style,
            NamePlate displayedRoomInfo, string size, List<Tag> tags, Department department, string comments)
        {
            MyRoom.Designation = designation;
            MyRoom.NumberOfWorkspaces = workspaces_number;
            MyRoom.Workspaces = workspaces;
            MyRoom.WorkersWithAccess = workspaces_with_access;
            MyRoom.AccessStyle = access_Style.ToString();
            MyRoom.NamePlate = displayedRoomInfo;
            MyRoom.Tags = tags;
            MyRoom.Size = size;
            MyRoom.Department = department;
            MyRoom.Comments = comments;
        }


        /// <summary>
        /// Sets the info of the room nameplate
        /// </summary>
        /// <param name="designation1">First Designation Text</param>
        /// <param name="designation2">Second Designation Text</param>
        /// <param name="designation3">Third Designation Text</param>
        /// <param name="style">Style Type of the Nameplate</param>
        /// <param name="displayed_room_id">Room id</param>
        /// <param name="displayed_clinical_room_id">visible room id</param>
        /// <param name="pictoDoor">pictogram on the door</param>
        /// <param name="pictoSign">pictogram on the nameplate</param>
        public void setNewRoomDisplayedRoomInfos(string designation1, string designation2, string designation3,
            Style style, string displayed_room_id, string displayed_clinical_room_id, Pictogramm pictoDoor,
            Pictogramm pictoSign)
        {
            MyRoom.NamePlate.Designation.Add(designation1);
            MyRoom.NamePlate.Designation.Add(designation2);
            MyRoom.NamePlate.Designation.Add(designation3);
            MyRoom.NamePlate.Style = style.ToString();
            MyRoom.NamePlate.RoomName = displayed_room_id;
            MyRoom.NamePlate.VisibleRoomName = displayed_clinical_room_id;
            MyRoom.NamePlate.DoorPictogram = Pictogramm.NONE.ToString();
            MyRoom.NamePlate.SignPictogram = Pictogramm.NONE.ToString();
        }
    }
}