using System;
using System.CodeDom;
using JetBrains.Annotations;
using Newtonsoft.Json;
using SmartHospital.ExplorerMode.Rooms.TagSystem;

namespace SmartHospital.ExplorerMode.Database.Events {
    public static class RoomEvents {
        static class Types {
            public const string CreateRoomString = "create_room_event";
            public const string DeleteRoomString = "delete_room_event";
            public const string AddTagString = "add_tag_event";
            public const string RemoveTagString = "remove_tag_event";
        }

        public class RoomBaseEvent : BaseEvent {
            [JsonProperty("room_name")] public string RoomName { get; }

            public RoomBaseEvent(User user, DateTime timeStamp, string eventType, string roomName) : base(user,
                timeStamp,
                eventType) {
                RoomName = roomName ?? throw new ArgumentNullException(nameof(roomName));
            }
        }

        [JsonObject]
        public class CreateRoom : RoomBaseEvent {
            [JsonConstructor]
            public CreateRoom(User user, DateTime timeStamp, string roomName) : base(user,
                timeStamp, Types.CreateRoomString, roomName) {
            }
        }

        [JsonObject]
        public class DeleteRoom : RoomBaseEvent {
            [JsonConstructor]
            public DeleteRoom(User user, DateTime timeStamp, string roomName) : base(user,
                timeStamp, Types.DeleteRoomString, roomName) {
            }
        }

        [JsonObject]
        public class AddTag : RoomBaseEvent {
            [JsonProperty("tag_id")] public string TagId { get; }

            [JsonConstructor]
            public AddTag(User user, DateTime timeStamp, string roomName, string tagId) : base(
                user,
                timeStamp, Types.AddTagString, roomName) {
                TagId = tagId ?? throw new ArgumentNullException(nameof(tagId));
            }
        }

        [JsonObject]
        public class RemoveTag : RoomBaseEvent {
            [JsonProperty("tag_id")] public string TagId { get; }

            [JsonConstructor]
            public RemoveTag(User user, DateTime timeStamp, string roomName, string tagId) :
                base(user,
                    timeStamp, Types.RemoveTagString, roomName) {
                TagId = tagId ?? throw new ArgumentNullException(nameof(tagId));
            }
        }
    }
}