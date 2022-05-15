using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SmartHospital.ExplorerMode.Database.Events {
    public static class RoomDetailsEvents {
        static class Types {
            public const string CreateRoomDetailsString = "create_room_details_event";
            public const string DeleteRoomDetailsString = "delete_room_details_event";
            public const string UpdateNumberOfWorkspacesString = "update_number_of_workspaces_event";
            public const string UpdateDepartmentString = "update_department_event";
            public const string AddWorkerString = "add_worker_event";
            public const string RemoveWorkerString = "remove_worker_event";
            public const string UpdateCostCentreAssignmentString = "update_cost_centre_assignment_event";
            public const string UpdateFunctionalAreaString = "update_functional_area_event";
            public const string UpdateRoomDesignationString = "update_room_designation_event";
            public const string UpdateBuildingSectionString = "update_building_section_event";
            public const string UpdateAccessControlledString = "update_access_controlled_event";
            public const string AddWorkerWithAccessString = "add_worker_with_access_event";
            public const string RemoveWorkerWithAccessString = "remove_worker_with_access_event";
            public const string UpdateRoomLengthString = "update_room_length_event";
            public const string UpdateRoomWidthString = "update_room_width_event";
            public const string UpdateRoomHeightString = "update_room_height_event";
            public const string UpdateRoomNameplateString = "update_room_nameplate_event";
            public const string UpdateAnnotationString = "update_annotation_event";
        }

        public abstract class RoomDetailsBase : BaseEvent {
            [JsonProperty("room_name")] public string RoomName { get; }

            protected RoomDetailsBase(User user, DateTime timeStamp, string eventType, string roomName) : base(user,
                timeStamp, eventType) {
                RoomName = roomName ?? throw new ArgumentNullException(nameof(roomName));
            }
        }

        [JsonObject]
        public class CreateRoomDetails : RoomDetailsBase {
            [JsonConstructor]
            public CreateRoomDetails(User user, DateTime timeStamp, string eventType, string roomName) : base(user,
                timeStamp, Types.CreateRoomDetailsString, roomName) {
            }
        }

        [JsonObject]
        public class DeleteRoomDetails : RoomDetailsBase {
            [JsonConstructor]
            public DeleteRoomDetails(User user, DateTime timeStamp, string eventType, string roomName) : base(user,
                timeStamp, Types.DeleteRoomDetailsString, roomName) {
            }
        }

        [JsonObject]
        public class UpdateNumberOfWorkspaces : RoomDetailsBase {
            [JsonProperty("number_of_workspaces")] public int NumberOfWorkspaces { get; }

            [JsonConstructor]
            public UpdateNumberOfWorkspaces(User user, DateTime timeStamp, string eventType, string roomName,
                int numberOfWorkspaces) : base(user, timeStamp, Types.UpdateNumberOfWorkspacesString, roomName) {
                if (numberOfWorkspaces < 0)
                    throw new ArgumentOutOfRangeException(nameof(numberOfWorkspaces), "Should be or greater than zero");
                NumberOfWorkspaces = numberOfWorkspaces;
            }
        }

        [JsonObject]
        public class UpdateDepartment : RoomDetailsBase {
            [JsonProperty("department")] public string Department { get; }

            [JsonConstructor]
            public UpdateDepartment(User user, DateTime timeStamp, string eventType, string roomName, string department)
                : base(user, timeStamp, Types.UpdateDepartmentString, roomName) {
                Department = department ?? throw new ArgumentNullException(nameof(department));
            }
        }

        [JsonObject]
        public class AddWorker : RoomDetailsBase {
            [JsonProperty("card_number")] public int CardNumber { get; }

            [JsonConstructor]
            public AddWorker(User user, DateTime timeStamp, string eventType, string roomName,
                int cardNumber) : base(user, timeStamp, Types.AddWorkerString, roomName) {
                if (cardNumber < 0)
                    throw new ArgumentOutOfRangeException(nameof(cardNumber), "Should be or greater than zero");
                CardNumber = cardNumber;
            }
        }

        [JsonObject]
        public class RemoveWorker : RoomDetailsBase {
            [JsonProperty("card_number")] public int CardNumber { get; }

            [JsonConstructor]
            public RemoveWorker(User user, DateTime timeStamp, string eventType, string roomName,
                int cardNumber) : base(user, timeStamp, Types.RemoveWorkerString, roomName) {
                if (cardNumber < 0)
                    throw new ArgumentOutOfRangeException(nameof(cardNumber), "Should be or greater than zero");
                CardNumber = cardNumber;
            }
        }

        [JsonObject]
        public class UpdateCostCentreAssignment : RoomDetailsBase {
            [JsonProperty("cost_centre_assignment")]
            public string CostCentreAssignment { get; }

            [JsonConstructor]
            public UpdateCostCentreAssignment(User user, DateTime timeStamp, string eventType, string roomName,
                string costCentreAssignment)
                : base(user, timeStamp, Types.UpdateCostCentreAssignmentString, roomName) {
                CostCentreAssignment =
                    costCentreAssignment ?? throw new ArgumentNullException(nameof(costCentreAssignment));
            }
        }

        [JsonObject]
        public class UpdateFunctionalArea : RoomDetailsBase {
            [JsonProperty("functional_area")] public string FunctionalArea { get; }

            [JsonConstructor]
            public UpdateFunctionalArea(User user, DateTime timeStamp, string eventType, string roomName,
                string functionalArea)
                : base(user, timeStamp, Types.UpdateFunctionalAreaString, roomName) {
                FunctionalArea = functionalArea ?? throw new ArgumentNullException(nameof(functionalArea));
            }
        }

        [JsonObject]
        public class UpdateRoomDesignation : RoomDetailsBase {
            [JsonProperty("room_designation")] public string RoomDesignation { get; }

            [JsonConstructor]
            public UpdateRoomDesignation(User user, DateTime timeStamp, string eventType, string roomName,
                string roomDesignation)
                : base(user, timeStamp, Types.UpdateRoomDesignationString, roomName) {
                RoomDesignation = roomDesignation ?? throw new ArgumentNullException(nameof(roomDesignation));
            }
        }

        [JsonObject]
        public class UpdateBuildingSection : RoomDetailsBase {
            [JsonProperty("building_section")] public string BuildingSection { get; }

            [JsonConstructor]
            public UpdateBuildingSection(User user, DateTime timeStamp, string eventType, string roomName,
                string buildingSection)
                : base(user, timeStamp, Types.UpdateBuildingSectionString, roomName) {
                BuildingSection = buildingSection ?? throw new ArgumentNullException(nameof(buildingSection));
            }
        }

        [JsonObject]
        public class UpdateAccessControlled : RoomDetailsBase {
            [JsonProperty("access_controlled")] public bool AccessControlled { get; }

            [JsonConstructor]
            public UpdateAccessControlled(User user, DateTime timeStamp, string eventType, string roomName,
                bool accessControlled) : base(user, timeStamp, Types.UpdateAccessControlledString, roomName) {
                AccessControlled = accessControlled;
            }
        }

        [JsonObject]
        public class AddWorkerWithAccess : RoomDetailsBase {
            [JsonProperty("card_number")] public int CardNumber { get; }

            [JsonConstructor]
            public AddWorkerWithAccess(User user, DateTime timeStamp, string eventType, string roomName,
                int cardNumber) : base(user, timeStamp, Types.AddWorkerWithAccessString, roomName) {
                if (cardNumber < 0)
                    throw new ArgumentOutOfRangeException(nameof(cardNumber), "Should be or greater than zero");
                CardNumber = cardNumber;
            }
        }

        [JsonObject]
        public class RemoveWorkerWithAccess : RoomDetailsBase {
            [JsonProperty("card_number")] public int CardNumber { get; }

            [JsonConstructor]
            public RemoveWorkerWithAccess(User user, DateTime timeStamp, string eventType, string roomName,
                int cardNumber) : base(user, timeStamp, Types.RemoveWorkerWithAccessString, roomName) {
                if (cardNumber < 0)
                    throw new ArgumentOutOfRangeException(nameof(cardNumber), "Should be or greater than zero");
                CardNumber = cardNumber;
            }
        }

        [JsonObject]
        public class UpdateRoomLength : RoomDetailsBase {
            [JsonProperty("room_length")] public float RoomLength { get; }

            [JsonConstructor]
            public UpdateRoomLength(User user, DateTime timeStamp, string eventType, string roomName, float roomLength)
                : base(user, timeStamp, Types.UpdateRoomLengthString, roomName) {
                if (roomLength <= 0)
                    throw new ArgumentOutOfRangeException(nameof(roomLength), "Should greater than zero");
                RoomLength = roomLength;
            }
        }

        [JsonObject]
        public class UpdateRoomWidth : RoomDetailsBase {
            [JsonProperty("room_width")] public float RoomWidth { get; }

            [JsonConstructor]
            public UpdateRoomWidth(User user, DateTime timeStamp, string eventType, string roomName, float roomWidth) :
                base(user, timeStamp, Types.UpdateRoomWidthString, roomName) {
                if (roomWidth <= 0)
                    throw new ArgumentOutOfRangeException(nameof(roomWidth), "Should greater than zero");
                RoomWidth = roomWidth;
            }
        }

        [JsonObject]
        public class UpdateRoomHeight : RoomDetailsBase {
            [JsonProperty("room_height")] public float RoomHeight { get; }

            [JsonConstructor]
            public UpdateRoomHeight(User user, DateTime timeStamp, string eventType, string roomName, float roomHeight)
                : base(user, timeStamp, Types.UpdateRoomHeightString, roomName) {
                if (roomHeight <= 0)
                    throw new ArgumentOutOfRangeException(nameof(roomHeight), "Should greater than zero");
                RoomHeight = roomHeight;
            }
        }

        [JsonObject]
        public class UpdateRoomNamePlate : RoomDetailsBase {
            [JsonProperty("room_nameplate")] public string RoomNameplate { get; }

            [JsonConstructor]
            public UpdateRoomNamePlate(User user, DateTime timeStamp, string eventType, string roomName,
                string roomNameplate)
                : base(user, timeStamp, Types.UpdateRoomNameplateString, roomName) {
                RoomNameplate = roomNameplate ?? throw new ArgumentNullException(nameof(roomNameplate));
            }
        }

        [JsonObject]
        public class UpdateAnnotation : RoomDetailsBase {
            [JsonProperty("annotation")] public string Annotation { get; }

            [JsonConstructor]
            public UpdateAnnotation(User user, DateTime timeStamp, string eventType, string roomName,
                string annotation) : base(user, timeStamp, Types.UpdateAnnotationString, roomName) {
                Annotation = annotation ?? throw new ArgumentNullException(nameof(annotation));
            }
        }
    }
}