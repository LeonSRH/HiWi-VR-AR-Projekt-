using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.ExplorerMode.Rooms.TagSystem;
using SmartHospital.Model;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using Vector3 = SmartHospital.Controller.ExplorerMode.Rooms.Vector3;

namespace SmartHospital.Database.Request
{
    /// <summary>
    /// Request Tests for REST Api - Inventory & Room
    /// 
    /// Created: 10/2019 by KS
    /// </summary>
    public class RequestTests : MonoBehaviour
    {
        #region Variables
        //Request types
        InventoryRequest inventoryRequest;
        RoomRequest roomRequest;
        WorkersRequest workerRequest;

        #region Test parameter

        private string testRoomId1;

        private string testRoomId2;

        private string testRoomId3;
        Address testAddress;
        Professional_Group testProfessionalGroup;
        List<FunctionalArea> testFunctionalAreas;
        FunctionalArea testFunctionalArea;
        Department testDepartment;
        List<Worker> testWorkersList;
        List<Workspace> testWorkspacesList;
        Worker testWorker;
        Worker testWorker2;
        Workspace testWorkspace;
        Workspace testWorkspace2;
        NamePlate testNamePlate;
        RoomParameters testRoomParameter;

        ServerRoom testRoom1;
        ServerRoom testRoom2;
        ServerRoom testRoom3;
        ServerRoom roomGetFromServer;
        List<ServerRoom> testRooms;


        Producer testProducer;
        Person testPerson;

        InventoryItem testInventoryItem1;
        InventoryItem testInventoryItem2;
        InventoryItem testInventoryItem3;
        List<InventoryItem> testInventoryItemsList;
        #endregion

        public static ServerRoom testGet = new ServerRoom("10", 2, new List<Worker>(),
            "2AP Dienstzimmer mechanisch mit inventar",
            Access_Style.MECHANICAL.ToString(), new NamePlate(), new List<Tag>(), new List<Workspace>(),
            new RoomParameters(), new Collider(), "10", new Department(), "");

        #endregion


        //Setup Requests
        private void Awake()
        {
            inventoryRequest = new InventoryRequest(HttpRoutes.INVENTORY, HttpRoutes.INVENTORYLIST);
            inventoryRequest.SetupHttpClient();
            roomRequest = new RoomRequest(HttpRoutes.ROOMS, HttpRoutes.ROOMLIST);
            roomRequest.SetupHttpClient();
            workerRequest = new WorkersRequest(HttpRoutes.WORKERS, HttpRoutes.EMPTY);
            workerRequest.SetupHttpClient();
        }

        //Start Test
        private void Start()
        {
            SetupTestParameter();
            StartRoomTests();
            StartInventoryTests();
        }

        private void SetupTestParameter()
        {
            testRoomId1 = "1000031";
            testRoomId2 = "1000021";
            testRoomId3 = "1000011";
            testAddress = new Address()
            {
                Street = "test street",
                Street_Number = "1",
                Zip_Code = 67022,
                City = "Heidelberg",
                Country = "Germany",
                POB_Address = 10,
                Region = "Baden-Württemberg"
            };
            testProfessionalGroup = new Professional_Group()
            {
                Name = "Professional group 1",
                Skills = new List<string>() { "Skill 1", "Skill 2", "Skill 3" }
            };
            testFunctionalArea = new FunctionalArea() { Name = "Functional Area 1", Type = "OTHER" };
            testFunctionalAreas = new List<FunctionalArea>()
                {testFunctionalArea, new FunctionalArea() {Name = "Test Functional Area 2", Type = "GENERAL"}};
            testDepartment = new Department()
            {
                Name = "Department Test name",
                CostCentre = 9231,
                FunctionalAreas = testFunctionalAreas
            };
            testWorker = new Worker()
            {
                FirstName = "WorkerFirstName",
                LastName = "WorkerLastName",
                FormOfAdress = "NONE",
                Title = "NONE",
                Address = testAddress,
                Employee_Id = 1,
                Staff_Id = 1,
                Function = "Worker function",
                Department = testDepartment,
                Professional_Group = testProfessionalGroup,
                E_Mail = ""
            };
            testWorker2 = new Worker()
            {
                FirstName = "Worker2FirstName",
                LastName = "Worker2LastName",
                FormOfAdress = "NONE",
                Title = "NONE",
                Address = testAddress,
                Employee_Id = 2,
                Staff_Id = 2,
                Function = "Worker2 function",
                Department = testDepartment,
                Professional_Group = testProfessionalGroup,
                E_Mail = "",

            };
            testWorkspace = new Workspace() { Worker = testWorker };
            testWorkspace2 = new Workspace() { Worker = testWorker2 };
            testWorkersList = new List<Worker> { testWorker, testWorker2 };
            testWorkspacesList = new List<Workspace> { testWorkspace, testWorkspace2 };
            testNamePlate = new NamePlate()
            {
                RoomName = "6420.01.031",
                Floor = 1,
                VisibleRoomName = "031",
                BuildingSection = Floor_Area.EMPTY.ToString(),
                Designation = new List<string> { "" },
                SignPictogram = Pictogramm.NONE.ToString(),
                DoorPictogram = Pictogramm.NONE.ToString(),
                Style = Style.NONE.ToString()
            };
            testPerson = new Person()
            {
                FirstName = "Test Person Firstname",
                LastName = "Test Person Lastname",
                FormOfAdress = "NONE",
                Title = "NONE",
                Address = testAddress,
                E_Mail = ""
            };
            testProducer = new Producer()
            {
                Name = "Test Producer name",
                Address = testAddress,
                Description = "Test Producer Description",
                E_Mail = "TetsProducer.LastName@hd.de",
                Label_Path = "image.jpg",
                Phone_Number = "0622156111",
                Remarks = "Remarks in production",
                Website = "www.google.de",
                Responsible_Person = testPerson
            };
            testRoomParameter = new RoomParameters()
            {
                Height = 2d,
                Length = 2d,
                Width = 2d,
                Position = new Vector3(0d, 0d, 0d),
                Rotation = new Vector3(0d, 0d, 0d)
            };

            testRoom1 = new ServerRoom(testRoomId1, 0, new List<Worker>(), "Lager", Access_Style.NONE.ToString(),
                testNamePlate, new List<Tag>(), new List<Workspace>(), testRoomParameter, new Collider(), "5",
                testDepartment, "");
            testRoom2 = new ServerRoom(testRoomId2, 2, testWorkersList, "2AP Dienstzimmer",
                Access_Style.ELECTRICAL.ToString(), testNamePlate, new List<Tag>(), testWorkspacesList,
                testRoomParameter, new Collider(), "20", testDepartment, "");
            testRoom3 = new ServerRoom(testRoomId3, 2, testWorkersList, "2AP Dienstzimmer mechanisch mit inventar",
                Access_Style.MECHANICAL.ToString(), testNamePlate, new List<Tag>(), testWorkspacesList,
                testRoomParameter, new Collider(), "10", testDepartment, "");
            testRooms = new List<ServerRoom> { testRoom3, testRoom2 };

            testInventoryItem1 = new InventoryItem
            {
                Name = "Test Tisch1",
                Cost = 10,
                Department = testDepartment,
                Designation = "brauner Tisch",
                Procurement_Staff = testPerson,
                Item_Code = "1",
                Mass = 5,
                Size = 2,
                Producer = testProducer,
                Product_Group = Product_Group.MOBILE.ToString()
            };
            testInventoryItem2 = new InventoryItem
            {
                Name = "Tisch2",
                Cost = 11,
                Department = testDepartment,
                Designation = "grüner Tisch",
                Procurement_Staff = testPerson,
                Item_Code = "2",
                Mass = 5,
                Size = 2,
                Producer = testProducer,
                Product_Group = Product_Group.MOBILE.ToString()
            };
            testInventoryItem3 = new InventoryItem
            {
                Name = "Fenster",
                Cost = 2,
                Department = testDepartment,
                Designation = "rundes Fenster",
                Procurement_Staff = testPerson,
                Item_Code = "3",
                Mass = 1,
                Size = 6,
                Producer = testProducer,
                Product_Group = Product_Group.NOTMOBILE.ToString()
            };
            testInventoryItemsList = new List<InventoryItem>
                {testInventoryItem3, testInventoryItem2, testInventoryItem1};
        }

        /// <summary>
        /// Room Tests
        /// </summary>
        #region room

        private async void StartRoomTests()
        {

            var putTest = await ServerRoomPutTest(testRoom1);
            var putListTest = await ServerRoomsPutTest(testRooms);

            var getTest = await ServerRoomGetTest(testRoom1.RoomName);
            var getListTest = await ServerRoomsGetTest();
            var deleteTest = await ServerRoomDeleteById(testRoom1.RoomName);

            if (putTest && putListTest && getTest && getListTest && deleteTest)
                Debug.Log("Room-Tests were successful");
            else
                Debug.LogError("Room-Tests were not successful");


        }

        private async Task<bool> ServerRoomPutTest(ServerRoom room)
        {
            try
            {
                var result = await roomRequest.Put(room);
                if (result)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {room}");
                return false;
            }
        }

        private async Task<bool> ServerRoomGetTest(string roomId)
        {
            try
            {
                ServerRoom room = new ServerRoom();
                room = await roomRequest.GetById(roomId);

                if (room.RoomName.Equals(roomId))
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {roomId}");
                return false;
            }
        }

        private async Task<bool> ServerRoomDeleteById(string roomId)
        {
            try
            {
                var result = await roomRequest.DeleteById(roomId);
                if (result)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {roomId}");

                return false;
            }
        }

        private async Task<bool> ServerRoomsPutTest(List<ServerRoom> rooms)
        {
            try
            {
                var result = await roomRequest.PutList(rooms);

                if (result)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {rooms}");

                return false;
            }
        }

        private async Task<bool> ServerRoomsGetTest()
        {
            try
            {
                var result = await roomRequest.GetList();
                if (result != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed.");

                return false;
            }
        }

        #endregion

        /// <summary>
        /// Inventory Tests
        /// </summary>
        #region inventory

        public async void StartInventoryTests()
        {

            var putTest = await InventoryItemPutTest(testInventoryItem1);
            var putListTest = await InventoryItemsPutTest(testInventoryItemsList);

            var getTest = await InventoryItemGetTest(testInventoryItem1.Item_Code);
            var getListTest = await InventoryItemsGetTest();
            var deleteTest = await InventoryItemDeletetest(testInventoryItem1.Item_Code);

            if (putTest && putListTest && getTest && getListTest && deleteTest)
                Debug.Log("Inventory-Tests were successful");
            else
                Debug.LogError("Inventory-Tests were not successful");

        }

        public async Task<bool> InventoryItemPutTest(InventoryItem inventoryItem)
        {
            try
            {
                var result = await inventoryRequest.Put(inventoryItem);
                if (result)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {inventoryItem}");
                return false;
            }
        }

        public async Task<bool> InventoryItemGetTest(string inventoryId)
        {
            try
            {
                var result = await inventoryRequest.GetById(inventoryId);
                if (result.Item_Code.Equals(inventoryId))
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {inventoryId}");
                return false;
            }
        }

        public async Task<bool> InventoryItemsPutTest(List<InventoryItem> items)
        {
            try
            {
                var result = await inventoryRequest.PutList(items);
                if (result)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {items}");
                return false;
            }
        }

        public async Task<bool> InventoryItemsGetTest()
        {

            try
            {
                var result = await inventoryRequest.GetList();
                if (result != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed.");
                return false;
            }
        }

        public async Task<bool> InventoryItemDeletetest(string inventoryId)
        {
            try
            {
                var result = await inventoryRequest.DeleteById(inventoryId);
                if (result)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed.");
                return false;
            }
        }

        #endregion

        /// <summary>
        /// Worker Tests
        /// </summary>
        #region worker

        public async void StartWorkerTests()
        {
            var putTest = await WorkerPutTest(testWorker);
            //var putListTest = await WorkersPutTest(testWorkersList);

            var getTest = await WorkerGetTest(testWorker.Staff_Id.ToString());
            //var getListTest = await WorkersGetTest();
            var deleteTest = await WorkerDeleteTest(testWorker.Staff_Id.ToString());

            if (putTest && getTest && deleteTest)
                Debug.Log("Worker-Tests were successful");
            else

                Debug.LogError("Worker-Tests were not successful");
        }

        public async Task<bool> WorkerPutTest(Worker worker)
        {
            try
            {
                var result = await workerRequest.Put(worker);
                if (result)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {worker}");
                return false;
            }
        }

        public async Task<bool> WorkerGetTest(string staff_Id)
        {
            try
            {
                Worker worker = new Worker();
                worker = await workerRequest.GetById(staff_Id);

                if (worker.Staff_Id.Equals(staff_Id))
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {staff_Id}");
                return false;
            }
        }

        public async Task<bool> WorkersPutTest(List<Worker> workers)
        {
            try
            {
                var result = await workerRequest.PutList(workers);

                if (result)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {workers}");

                return false;
            }
        }

        public async Task<bool> WorkersGetTest()
        {
            try
            {
                var result = await workerRequest.GetList();
                if (result != null)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed.");

                return false;
            }
        }

        public async Task<bool> WorkerDeleteTest(string staff_id)
        {
            try
            {
                var result = await workerRequest.DeleteById(staff_id);
                if (result)
                    return true;
                else
                    return false;
            }
            catch
            {
                Debug.LogError(MethodBase.GetCurrentMethod() + $"Test failed. - Parameter: {staff_id}");

                return false;
            }
        }

        #endregion
    }
}