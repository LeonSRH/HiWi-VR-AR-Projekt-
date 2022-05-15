using System;
using Newtonsoft.Json;

namespace SmartHospital.ExplorerMode.Database.Events {
    public static class WorkerEvents {
        static class Types {
            public const string CreateWorkerString = "create_worker_event";
            public const string DeleteWorkerString = "delete_worker_event";
            public const string UpdateFirstNameString = "update_first_name_event";
            public const string UpdateLastNameString = "update_last_name_event";
            public const string UpdatePositionString = "update_position_event";
            public const string UpdateProfessionalGroupString = "update_professional_group_event";
        }

        public class WorkerBaseEvent : BaseEvent {
            [JsonProperty("card_number")] public int CardNumber { get; }

            public WorkerBaseEvent(User user, DateTime timeStamp, string eventType, int cardNumber) : base(user,
                timeStamp, eventType) {
                if (cardNumber < 0)
                    throw new ArgumentOutOfRangeException(nameof(cardNumber), "Should be or greater than zero");
                CardNumber = cardNumber;
            }
        }

        [JsonObject]
        public class CreateWorker : WorkerBaseEvent {
            [JsonConstructor]
            public CreateWorker(User user, DateTime timeStamp, string eventType, int cardNumber) : base(user,
                timeStamp, Types.CreateWorkerString, cardNumber) {
            }
        }

        [JsonObject]
        public class DeleteWorker : WorkerBaseEvent {
            [JsonConstructor]
            public DeleteWorker(User user, DateTime timeStamp, string eventType, int cardNumber) : base(user,
                timeStamp, Types.DeleteWorkerString, cardNumber) {
            }
        }

        [JsonObject]
        public class UpdateFirstName : WorkerBaseEvent {
            [JsonProperty("first_name")] public string FirstName { get; }

            [JsonConstructor]
            public UpdateFirstName(User user, DateTime timeStamp, string eventType, int cardNumber,
                string firstName) : base(
                user, timeStamp, Types.UpdateFirstNameString, cardNumber) {
                FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            }
        }

        [JsonObject]
        public class UpdateLastName : WorkerBaseEvent {
            [JsonProperty("last_name")] public string LastName { get; }

            [JsonConstructor]
            public UpdateLastName(User user, DateTime timeStamp, string eventType, int cardNumber,
                string lastName) : base(
                user, timeStamp, Types.UpdateLastNameString, cardNumber) {
                LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            }
        }

        [JsonObject]
        public class UpdatePosition : WorkerBaseEvent {
            [JsonProperty("position")] public string Position { get; }

            [JsonConstructor]
            public UpdatePosition(User user, DateTime timeStamp, string eventType, int cardNumber,
                string position) : base(
                user, timeStamp, Types.UpdatePositionString, cardNumber) {
                Position = position ?? throw new ArgumentNullException(nameof(position));
            }
        }

        [JsonObject]
        public class UpdateProfessionalGroup : WorkerBaseEvent {
            [JsonProperty("professional_group")] public string ProfessionalGroup { get; }

            [JsonConstructor]
            public UpdateProfessionalGroup(User user, DateTime timeStamp, string eventType, int cardNumber,
                string professionalGroup) : base(
                user, timeStamp, Types.UpdateProfessionalGroupString, cardNumber) {
                ProfessionalGroup = professionalGroup ?? throw new ArgumentNullException(nameof(professionalGroup));
            }
        }
    }
}