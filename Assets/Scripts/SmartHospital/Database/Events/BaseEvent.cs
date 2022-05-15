using System;
using Newtonsoft.Json;

namespace SmartHospital.ExplorerMode.Database.Events {
    public abstract class BaseEvent {
        [JsonProperty("user")] public User User { get; }
        [JsonProperty("time")] public DateTime TimeStamp { get; }
        [JsonProperty("event_type")] public string EventType { get; }

        protected BaseEvent(User user, DateTime timeStamp, string eventType) {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (eventType == null) throw new ArgumentNullException(nameof(eventType));
            User = user;
            TimeStamp = timeStamp;
            EventType = eventType;
        }
    }
}