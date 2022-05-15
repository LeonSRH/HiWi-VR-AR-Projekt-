using System;
using Newtonsoft.Json;

namespace SmartHospital.ExplorerMode.Database.Events {
    [JsonObject]
    public class User {
        [JsonProperty("user_name")] public string UserName { get; set; }

        [JsonConstructor]
        public User(string userName) {
            if (userName == null) throw new ArgumentNullException(nameof(userName));
            UserName = userName;
        }
    }
}