using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using SmartHospital.ExplorerMode.Rooms.TagSystem;
                
namespace SmartHospital.ExplorerMode.Services.JSON {

    public sealed class TagJsonService : JsonConverter {
        
        public static string SerializeTag(Tag tag) {
            return JsonConvert.SerializeObject(tag);
        }

        public static Tag DeserializeTag(string json) {
            return JsonConvert.DeserializeObject<Tag>(json);
        }

        public static List<Tag> DeserializeTags(string json) {
            return JsonConvert.DeserializeObject<IEnumerable<Tag>>(json).ToList();
        }
    }
}