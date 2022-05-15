using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using SmartHospital.ExplorerMode.Rooms.TagSystem;
using SmartHospital.Model;
using UnityEngine;

namespace SmartHospital.Controller.ExplorerMode.Rooms {
    ///
    /// Data Model Room class
    ///
    public class ServerRoom : IRoom {
        public ServerRoom () { }

        public ServerRoom (string roomName, Collider collider) {
            RoomName = roomName;
            Collider = collider;

            if (collider != null) {
                Vector3 position = new Vector3 (collider.transform.position.x, collider.transform.position.y,
                    collider.transform.position.z);
                Vector3 rotation = new Vector3 (collider.transform.rotation.x, collider.transform.rotation.y,
                    collider.transform.rotation.z);

                RoomParameters = new RoomParameters {
                    Width = collider.bounds.size.x,
                    Length = collider.bounds.size.y,
                    Height = collider.bounds.size.z,
                    Position = position,
                    Rotation = rotation
                };
            } else {
                Debug.LogError ("Room Collider is null: " + roomName);
                RoomParameters = new RoomParameters {
                    Width = 0d,
                    Length = 0d,
                    Height = 0d,
                    Position = new Vector3 (0d, 0d, 0d),
                    Rotation = new Vector3 (0d, 0d, 0d)
                };
            }

            NumberOfWorkspaces = 0;
            WorkersWithAccess = new List<Worker> ();
            Designation = "";
            AccessStyle = "NONE";
            NamePlate = new NamePlate () { RoomName = RoomName, VisibleRoomName = "", Floor = 0, Designation = new List<string> { "" }, BuildingSection = "EMPTY", Style = "NONE", DoorPictogram = "NONE", SignPictogram = "NONE" };
            Tags = new List<Tag> ();
            Workspaces = new List<Workspace> { new Workspace () { } };
            Collider = collider;
            Size = "0";
            Comments = "";
            Department = new Department () { CostCentre = 9230, Name = "Allg.", FunctionalAreas = new List<FunctionalArea> () { } };
        }

        [JsonConstructor]
        public ServerRoom (string roomName, int numberOfWorkspaces, List<Worker> workersWithAccess, string designation,
            string accessStyle,
            NamePlate namePlate, List<Tag> tags, List<Workspace> workspaces, RoomParameters roomParameters,
            Collider collider, string size, Department department, string comments) {
            if (collider != null) {
                Vector3 position = new Vector3 (collider.transform.position.x, collider.transform.position.y,
                    collider.transform.position.z);
                Vector3 rotation = new Vector3 (collider.transform.rotation.x, collider.transform.rotation.y,
                    collider.transform.rotation.z);

                RoomParameters = new RoomParameters {
                    Width = collider.bounds.size.x,
                    Length = collider.bounds.size.y,
                    Height = collider.bounds.size.z,
                    Position = position,
                    Rotation = rotation
                };
            } else {
                RoomParameters = new RoomParameters {
                    Width = 0d,
                    Length = 0d,
                    Height = 0d,
                    Position = new Vector3 (0d, 0d, 0d),
                    Rotation = new Vector3 (0d, 0d, 0d)
                };
            }

            RoomName = roomName;
            NumberOfWorkspaces = numberOfWorkspaces;
            WorkersWithAccess = workersWithAccess;
            Designation = designation;
            AccessStyle = accessStyle;
            NamePlate = namePlate;
            Tags = tags;
            Workspaces = workspaces;
            Collider = collider;
            Size = size;
            Comments = comments;

            if (department != null)
                Department = department;
            else
                Department = new Department { Name = "Leer", CostCentre = 0000, FunctionalAreas = new List<FunctionalArea> () { } };
        }

        [JsonProperty ("roomName")] public string RoomName { get; set; }

        [JsonProperty ("numberOfWorkspaces")] public int NumberOfWorkspaces { get; set; }

        [JsonProperty ("workersWithAccess")] public List<Worker> WorkersWithAccess { get; set; }

        [JsonProperty ("designation")] public string Designation { get; set; }

        [JsonProperty ("accessStyle")] public string AccessStyle { get; set; }

        [JsonProperty ("namePlate")] public NamePlate NamePlate { get; set; }

        [JsonProperty ("roomParameters")] public RoomParameters RoomParameters { get; set; }

        [JsonProperty ("workspaces")] public List<Workspace> Workspaces { get; set; }

        [JsonProperty ("department")] public Department Department { get; set; }

        [JsonProperty ("comments")] public string Comments { get; set; }

        [JsonIgnore] public List<Tag> Tags { get; set; }

        [JsonIgnore] public Collider Collider { get; }

        [JsonIgnore] public string Size { get; set; }

        [JsonIgnore] public List<InventoryItem> InventoryItems { get; set; }

        public static implicit operator bool (ServerRoom room) {
            return room != null;
        }

        public void AddTag (Tag tagToAdd) => Tags.Add (tagToAdd);

        public void RemoveTag (Tag tagToRemove) => Tags.Remove (tagToRemove);

        public void ReplaceTags (IEnumerable<Tag> newTags) {
            Tags = new List<Tag> (newTags);
        }

        public void ReplaceInventoryItems (IEnumerable<InventoryItem> newInventoryItems) {
            InventoryItems = new List<InventoryItem> (newInventoryItems);
        }

        public bool ContainsTags (params Tag[] tags) {
            return tags.All (t => Tags.Contains (t));
        }

        public bool ContainsTagsExclusive (params Tag[] tags) => tags.Length == Tags.Count && ContainsTags (tags);
    }

    public class NamePlate {
        public NamePlate () { }

        public NamePlate (string roomName) {
            RoomName = roomName;
        }

        [JsonConstructor]
        public NamePlate (string roomName, string visibleRoomName, string buildingSection, int floor,
            List<string> designation, string style, string doorPictogram, string signPictogram) : this (roomName) {
            RoomName = roomName;
            VisibleRoomName = visibleRoomName;
            BuildingSection = buildingSection;
            Floor = floor;
            Designation = designation;
            Style = style;
            DoorPictogram = doorPictogram;
            SignPictogram = signPictogram;
        }

        [JsonProperty ("roomName")] public string RoomName { get; set; }

        [JsonProperty ("visibleRoomName")] public string VisibleRoomName { get; set; }

        [JsonProperty ("buildingSection")] public string BuildingSection { get; set; }

        [JsonProperty ("floor")] public int Floor { get; set; }

        [JsonProperty ("designation")] public List<string> Designation { get; set; }

        [JsonProperty ("style")] public string Style { get; set; }

        [JsonProperty ("doorPictogram")] public string DoorPictogram { get; set; }

        [JsonProperty ("signPictogram")] public string SignPictogram { get; set; }
    }

    public class RoomParameters {
        [JsonProperty ("width")] public double Width { get; set; }

        [JsonProperty ("length")] public double Length { get; set; }

        [JsonProperty ("height")] public double Height { get; set; }

        [JsonProperty ("position")] public Vector3 Position { get; set; }

        [JsonProperty ("rotation")] public Vector3 Rotation { get; set; }

        [JsonIgnore] public string Size { get; set; }
    }

    public class Vector3 {
        [JsonConstructor]
        public Vector3 (double xAchsis, double yAchsis, double zAchsis) {
            X = xAchsis;
            Y = yAchsis;
            Z = zAchsis;
        }

        [JsonProperty ("x")] public double X { get; set; }

        [JsonProperty ("y")] public double Y { get; set; }

        [JsonProperty ("z")] public double Z { get; set; }
    }

    public class Workspace {
        [JsonProperty ("worker")] public Worker Worker { get; set; }
    }
}