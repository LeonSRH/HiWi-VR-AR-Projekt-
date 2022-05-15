using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using SmartHospital.ExplorerMode.Rooms.TagSystem;
using UnityEngine;

namespace SmartHospital.ExplorerMode.Database.Events {
    public static class TagEvents {
        static class Types {
            public const string CreateTagString = "create_tag_event";
            public const string DeleteTagString = "delete_tag_event";
            public const string UpdateTagString = "update_tag_name_event";
            public const string UpdateTagColorString = "update_tag_color_event";
        }

        public class TagBaseEvent : BaseEvent {
            [JsonProperty("tag_id")] public string TagId { get; }

            public TagBaseEvent(User user, DateTime timeStamp, string eventType, string tagId) : base(user,
                timeStamp, eventType) {
                TagId = tagId ?? throw new ArgumentNullException(nameof(tagId));
            }
        }

        [JsonObject]
        public class CreateTag : TagBaseEvent {
            [JsonConstructor]
            public CreateTag(User user, DateTime timeStamp, string eventType, string tagId) : base(user,
                timeStamp, Types.CreateTagString, tagId) {
            }
        }

        [JsonObject]
        public class DeleteTag : TagBaseEvent {
            [JsonConstructor]
            public DeleteTag(User user, DateTime timeStamp, string eventType, string tagId) : base(user,
                timeStamp, Types.DeleteTagString, tagId) {
            }
        }

        [JsonObject]
        public class UpdateTagName : TagBaseEvent {
            [JsonProperty("tag_name")] public string TagName { get; }

            [JsonConstructor]
            public UpdateTagName(User user, DateTime timeStamp, string eventType, string tagId,
                string tagName) : base(
                user,
                timeStamp, Types.UpdateTagString, tagId) {
                TagName = tagName ?? throw new ArgumentNullException(nameof(tagName));
            }
        }

        [JsonObject]
        public class UpdateTagColor : TagBaseEvent {
            [JsonProperty("tag_color")]
            [JsonConverter(typeof(ColorSerializer))]
            public Color TagColor { get; }

            [JsonConstructor]
            public UpdateTagColor(User user, DateTime timeStamp, string eventType, string tagId, Color tagColor) :
                base(user,
                    timeStamp, Types.UpdateTagColorString, tagId) {
                if (tagColor == null) throw new ArgumentNullException(nameof(tagColor));
                TagColor = tagColor;
            }
        }
    }
}