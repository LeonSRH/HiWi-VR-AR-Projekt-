using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace SmartHospital.ExplorerMode.Database.Events {
    static class Types {
        public const string CreateRoomDetailsStockString = "create_room_details_stock_event";
        public const string DeleteRoomDetailsStockString = "delete_room_details_stock_event";
        public const string UpdateNumberOfWorkspacesStockString = "update_number_of_workspaces_stock_event";
        public const string UpdateDepartmentStockString = "update_department_stock_event";
        public const string AddWorkerStockString = "add_worker_stock_event";
        public const string RemoveWorkerStockString = "remove_worker_stock_event";
        public const string UpdateCostCentreAssignmentString = "update_cost_centre_assignment_stock_event";
        public const string UpdateFunctionalAreaStockString = "update_functional_area_stock_event";
        public const string UpdateBuildingNumberStockString = "update_building_number_stock_event";
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
            timeStamp, Types.CreateRoomDetailsStockString, roomName) {
        }
    }

    [JsonObject]
    public class DeleteRoomDetails : RoomDetailsBase {
        [JsonConstructor]
        public DeleteRoomDetails(User user, DateTime timeStamp, string eventType, string roomName) : base(user,
            timeStamp, Types.DeleteRoomDetailsStockString, roomName) {
        }
    }

    [JsonObject]
    public class UpdateNumberOfWorkspaces : RoomDetailsBase {
        [JsonProperty("number_of_workspaces")] public int NumberOfWorkspaces { get; }

        [JsonConstructor]
        public UpdateNumberOfWorkspaces(User user, DateTime timeStamp, string eventType, string roomName,
            int numberOfWorkspaces) : base(user,
            timeStamp, Types.UpdateNumberOfWorkspacesStockString, roomName) {
            if (numberOfWorkspaces < 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfWorkspaces), "Should be or greater than zero");
            NumberOfWorkspaces = numberOfWorkspaces;
        }
    }

    [JsonObject]
    public class UpdateDepartment : RoomDetailsBase {
        [JsonProperty("department")] public string Department { get; }

        [JsonConstructor]
        public UpdateDepartment(User user, DateTime timeStamp, string eventType, string roomName,
            string department) : base(user, timeStamp, Types.UpdateDepartmentStockString, roomName) {
            Department = department ?? throw new ArgumentNullException(nameof(department));
        }
    }

    [JsonObject]
    public class AddWorker : RoomDetailsBase {
        [JsonProperty("card_number")] public int CardNumber { get; }

        [JsonConstructor]
        public AddWorker(User user, DateTime timeStamp, string eventType, string roomName,
            int cardNumber) : base(user,
            timeStamp, Types.AddWorkerStockString, roomName) {
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
            int cardNumber) : base(user,
            timeStamp, Types.RemoveWorkerStockString, roomName) {
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
            string costCentreAssignment) : base(user, timeStamp, Types.UpdateCostCentreAssignmentString, roomName) {
            CostCentreAssignment =
                costCentreAssignment ?? throw new ArgumentNullException(nameof(costCentreAssignment));
        }
    }

    [JsonObject]
    public class UpdateFunctionalArea : RoomDetailsBase {
        [JsonProperty("functional_area")] public string FunctionalArea { get; }

        [JsonConstructor]
        public UpdateFunctionalArea(User user, DateTime timeStamp, string eventType, string roomName,
            string functionalArea) : base(user, timeStamp, Types.UpdateFunctionalAreaStockString, roomName) {
            FunctionalArea = functionalArea ?? throw new ArgumentNullException(nameof(functionalArea));
        }
    }

    [JsonObject]
    public class UpdateBuildingNumber : RoomDetailsBase {
        [JsonProperty("building_number")] public int BuildingNumber { get; }

        [JsonConstructor]
        public UpdateBuildingNumber(User user, DateTime timeStamp, string eventType, string roomName,
            int buildingNumber) : base(user,
            timeStamp, Types.UpdateBuildingNumberStockString, roomName) {
            if (buildingNumber < 0)
                throw new ArgumentOutOfRangeException(nameof(buildingNumber), "Should be or greater than zero");
            BuildingNumber = buildingNumber;
        }
    }
}