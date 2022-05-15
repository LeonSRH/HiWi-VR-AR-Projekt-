using Assets.Scripts;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.ExplorerMode.Rooms.TagSystem;
using SmartHospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmartHospital.Controller.ExplorerMode.Rooms
{
    public partial class LoadCSVRoomData : MonoBehaviour
    {

        RoomInfoCache roomScriptManager;

        #region Container

        HashSet<ServerRoom> rooms { get; set; }
        HashSet<FunctionalArea> functionalAreas { get; set; }
        HashSet<Professional_Group> professional_Groups { get; set; }
        HashSet<Department> departments { get; set; }
        HashSet<NamePlate> roomNameplates { get; set; }
        HashSet<Worker> workers { get; set; }
        HashSet<Worker> allWorkers { get; set; }
        HashSet<InventoryItem> inventoryItems { get; set; }
        HashSet<string> buildingSections;


        // Container Get-Set
        public HashSet<Professional_Group> Professional_Groups
        {
            get => professional_Groups;
            set => professional_Groups = value;
        }

        public HashSet<NamePlate> RoomNameplates
        {
            get => roomNameplates;
            set => roomNameplates = value;
        }

        public HashSet<Department> Departments
        {
            get => departments;
            set => departments = value;
        }

        public HashSet<string> BuildingSections
        {
            get => buildingSections;
            set => buildingSections = value;
        }

        public HashSet<FunctionalArea> FunctionalAreas
        {
            get => functionalAreas;
            set => functionalAreas = value;
        }

        public HashSet<ServerRoom> Rooms
        {
            get => rooms;
            set => rooms = value;
        }

        public HashSet<Worker> Workers
        {
            get => workers;
            set => workers = value;
        }

        public HashSet<Worker> AllWorkers
        {
            get => allWorkers;
            set => allWorkers = value;
        }

        public HashSet<InventoryItem> InventoryItems
        {
            get => inventoryItems;
            set => inventoryItems = value;
        }

        #endregion

        private void Awake()
        {
            RoomNameplates = new HashSet<NamePlate>();
            Rooms = new HashSet<ServerRoom>();
            Workers = new HashSet<Worker>();
            FunctionalAreas = new HashSet<FunctionalArea>();
            Professional_Groups = new HashSet<Professional_Group>();
            Departments = new HashSet<Department>();
            buildingSections = new HashSet<string>();

            roomScriptManager = FindObjectOfType<RoomInfoCache>();

            var gameObjectRooms = GameObject.FindGameObjectsWithTag("RoomCollider");

            foreach (GameObject room in gameObjectRooms)
            {
                if (!room.GetComponent<RoomColliderMaterialController>())
                {
                    room.AddComponent<RoomColliderMaterialController>();
                }

                if (!room.GetComponent<ClientRoom>())
                {
                    room.AddComponent<ClientRoom>();
                    NamePlate dr = new NamePlate()
                    {
                        RoomName = "0",
                        Floor = 0,
                        VisibleRoomName = "0",
                        BuildingSection = Floor_Area.EMPTY.ToString(),
                        Designation = new List<string> { "" },
                        SignPictogram = Pictogramm.NONE.ToString(),
                        DoorPictogram = Pictogramm.NONE.ToString(),
                        Style = Style.NONE.ToString()
                    };

                    List<Worker> person = new List<Worker>();
                    List<Workspace> workspaces = new List<Workspace>();
                    List<Tag> tags = new List<Tag>();
                    Department department = new Department()
                    {
                        Name = "Leer",
                        CostCentre = 9999,
                        FunctionalAreas = new List<FunctionalArea>()
                            {new FunctionalArea() {Name = "empty", Type = "EMPTY"}}
                    };

                    room.GetComponent<ClientRoom>()
                        .setNewRoomDetails("", 0, workspaces, person, Access_Style.NONE, dr, "0", tags, department, "");
                }
            }


            ReadCSVFileAndSaveNamePlateData("Tuerschildliste");
            ReadCSVFileAndSaveRoomData("Raumbuch");
            ReadCSVFileAndSaveWorkerRoomData("Mitarbeiter");
            //TODO: Add Inventar
            //ReadCSVFileAndSaveInventoryData("Inventarliste");

        }

        private void Start()
        {
            AssignCSVRoomInfoToCollider("RoomCollider", Rooms);
            AssignContainer(Rooms);
            var roomsToPut = Rooms.ToList();

            //RoomDetailsController.roomRequest.PutList(roomsToPut);
        }

        #region CSV Reader

        /// <summary>
        /// reads the csv file and splits into each column and row + adds the rooms to the newRoom list
        /// </summary>
        /// <param name="csvName">the csv name which should be read</param>
        public void ReadCSVFileAndSaveRoomData(string csvName)
        {
            HashSet<ServerRoom> newRooms = new HashSet<ServerRoom>();
            TextAsset roomData = Resources.Load<TextAsset>("CSV/" + csvName);
            string[] data = roomData.text.Split(new char[] { '\n' });
            for (int i = 1; i < data.Length - 1; i++)
            {
                string[] row = data[i].Split(new char[] { ';' });

                if (row[0] != "")
                {
                    if (row.Length > 60)
                    {
                        var room = ParseRowToRoomObject(row);
                        newRooms.Add(room);
                    }
                }
            }

            Rooms = newRooms;
        }

        public void ReadCSVFileAndSaveWorkerRoomData(string csvName)
        {
            HashSet<Worker> workers = new HashSet<Worker>();
            TextAsset roomData = Resources.Load<TextAsset>("CSV/" + csvName);
            string[] data = roomData.text.Split(new char[] { '\n' });
            for (int i = 1; i < data.Length - 1; i++)
            {
                string[] row = data[i].Split(new char[] { ';' });

                if (row[0] != "")
                {
                    if (row.Length > 10)
                    {
                        var worker = ParseRowToWorkerObject(row);
                        workers.Add(worker);
                    }
                }
            }

            AllWorkers = workers;
        }

        /// <summary>
        /// reads the csv file and assigns the displayed rooms to "displayedRooms"
        /// </summary>
        /// <param name="csvName"></param>
        public void ReadCSVFileAndSaveNamePlateData(string csvName)
        {
            TextAsset roomData = Resources.Load<TextAsset>("CSV/" + csvName);

            string[] data = roomData.text.Split(new char[] { '\n' });
            for (int i = 1; i < data.Length - 1; i++)
            {
                string[] row = data[i].Split(new char[] { ';' });
                if (row[0] != "")
                {
                    var room = ParseRowToDisplayedRoomObject(row);

                    List<NamePlate> dispalyedRoomsToDeleteDouble = new List<NamePlate>();
                    foreach (NamePlate dr in RoomNameplates)
                    {
                        if (room.RoomName.Equals(dr.RoomName))
                        {
                            if (dr.Designation.Equals(""))
                            {
                                dispalyedRoomsToDeleteDouble.Add(dr);
                            }
                        }
                    }

                    foreach (NamePlate dr in dispalyedRoomsToDeleteDouble)
                    {
                        RoomNameplates.Remove(dr);
                    }

                    RoomNameplates.Add(room);
                }
            }
        }

        #endregion

        #region Parsing

        /// <summary>
        /// Parse the excel info row of a displayed room info into DisplayedRoom Obj
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private NamePlate ParseRowToDisplayedRoomObject(string[] row)
        {
            NamePlate displayedRoom = new NamePlate
            {
                RoomName = "4321" + row[0].Substring(4),
                BuildingSection = "EMPTY"
            };

            if (!displayedRoom.RoomName.Equals(""))
            {
                if (!row[7].Equals("") && !(row[7] == null))
                {
                    displayedRoom.VisibleRoomName = row[7];
                }
                else if (!(row[8] == null) && !(row[8].Equals("")))
                {
                    displayedRoom.VisibleRoomName = row[8];
                }
                else
                {
                    displayedRoom.VisibleRoomName = "0";
                }

                Int32.TryParse(row[5], out int floor);
                displayedRoom.Floor = floor;
                displayedRoom.BuildingSection = returnFloorLabelByRoomId(displayedRoom.RoomName).ToString();
                displayedRoom.Designation = new List<string>();

                if (row[3] != null)
                {
                    displayedRoom.Designation.Add(row[2]);
                }


                if (row[4] != null)
                {
                    displayedRoom.Designation.Add(row[3]);
                }

                if (row[9] != null)
                {
                    displayedRoom.SignPictogram = "NONE";
                }
                else
                {
                    displayedRoom.SignPictogram = "NONE";
                }

                if (row[10] != null)
                {
                    displayedRoom.DoorPictogram = "NONE";
                }
                else
                {
                    displayedRoom.DoorPictogram = "NONE";
                }


                switch (row[6])
                {
                    case "Style 1":
                        displayedRoom.Style = Style.STYLE1.ToString();
                        break;
                    case "Style 2":
                        displayedRoom.Style = Style.STYLE2.ToString();
                        break;
                    case "Style 3":
                        displayedRoom.Style = Style.STYLE3.ToString();
                        break;
                    case "Style 4":
                        displayedRoom.Style = Style.STYLE4.ToString();
                        break;
                    case "Style 5":
                        displayedRoom.Style = Style.STYLE5.ToString();
                        break;
                    case "STYLE1":
                        displayedRoom.Style = Style.STYLE1.ToString();
                        break;
                    case "STYLE2":
                        displayedRoom.Style = Style.STYLE2.ToString();
                        break;
                    case "STYLE3":
                        displayedRoom.Style = Style.STYLE3.ToString();
                        break;
                    case "STYLE4":
                        displayedRoom.Style = Style.STYLE4.ToString();
                        break;
                    case "STYLE5":
                        displayedRoom.Style = Style.STYLE5.ToString();
                        break;
                    default:
                        displayedRoom.Style = Style.NONE.ToString();
                        break;
                }
            }
            else
            {
                displayedRoom.RoomName = "0";
                displayedRoom.Floor = 0;
                displayedRoom.VisibleRoomName = "0";
                displayedRoom.BuildingSection = Floor_Area.EMPTY.ToString();
                displayedRoom.Designation.Add("");
                displayedRoom.SignPictogram = Pictogramm.NONE.ToString();
                displayedRoom.DoorPictogram = Pictogramm.NONE.ToString();
                displayedRoom.Style = Style.NONE.ToString();
            }

            return displayedRoom;
        }

        /// <summary>
        /// Parse csv row info of a room into a <see cref="ServerRoom"/> Object
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private ServerRoom ParseRowToRoomObject(string[] row)
        {
            List<Worker> listOfWorker = new List<Worker>();
            List<Workspace> listOfWorkspaces = new List<Workspace>();
            List<Tag> tags = new List<Tag>();
            Workspace workspace = new Workspace();
            NamePlate namePlate = new NamePlate();
            int costCentreRoom;

            if (!row[6].Equals(""))
            {
                costCentreRoom = Convert.ToInt32(row[6]);
            }
            else
            {
                costCentreRoom = 9999;
            }

            ;
            Department department = new Department()
            {
                Name = "",
                CostCentre = costCentreRoom,
                FunctionalAreas = new List<FunctionalArea>()
                {
                    new FunctionalArea()
                    {
                        Name = row[2],
                        Type = "EMPTY"
                    }
                }
            };

            if (!row[5].Equals("") && row[5] != null)
            {
                department.Name = row[5];
            }
            else
            {
                department.Name = "Leer";
            }

            Access_Style access_Style = Access_Style.ELECTRICAL;
            Int32.TryParse(row[4], out int numberOfWorkspaces);
            if (row[4] == null)
                numberOfWorkspaces = 0;

            if (row[0] != null)
                namePlate = SearchForNamePlateByRoomId(row[0]);

            switch (row[61])
            {
                case "on":
                    access_Style = Access_Style.ELECTRICAL;
                    break;
                case "off":
                    access_Style = Access_Style.NONE;
                    break;
                default:
                    access_Style = Access_Style.MECHANICAL;
                    break;
            }

            char[] sizeChars = row[60].ToCharArray();

            // change comma of the size to a dot
            string size = "";
            for (int i = 0; i < sizeChars.Length; i++)
            {
                {
                    if (sizeChars[i].Equals(","))
                    {
                        sizeChars[i] = '.';
                    }
                }
                size += sizeChars[i];
            }

            ServerRoom roomDetails = new ServerRoom(row[0], numberOfWorkspaces, listOfWorker, row[1],
                access_Style.ToString(),
                namePlate, tags, listOfWorkspaces, new RoomParameters(), new Collider(), size, department, "");


            HashSet<int> costCentreList = new HashSet<int>();
            var rowCount = 0;
            for (int i = 0; i < numberOfWorkspaces; i++)
            {
                var person = new Worker()
                {
                    RoomName = "4321" + row[0].Substring(4),
                    HasWorkspace = true,
                    Department = new Department()
                    {
                        Name = "Leer",
                        CostCentre = 9999,
                        FunctionalAreas = new List<FunctionalArea>()
                        {
                            new FunctionalArea()
                            {
                                Name = "empty",
                                Type = "EMPTY"
                            }
                        }
                    },
                    Professional_Group = new Professional_Group()
                    { Name = "Berufsbezeichnung", Skills = new List<string>() { "0", "1" } },
                    LastName = "",
                    FirstName = "",
                    Title = Title.NONE.ToString(),
                    Employee_Id = 0,
                    Staff_Id = 0,
                    E_Mail = "",
                    FormOfAdress = Form_Of_Adress.NONE.ToString(),
                    Address = new Address
                    {
                        Street = "",
                        Street_Number = "1",
                        Zip_Code = 0,
                        City = "",
                        Country = "Germany",
                        POB_Address = 0,
                        Region = ""
                    },
                    Function = ""
                };

                if (!row[rowCount + 8].Equals(""))
                {
                    Int32.TryParse(row[rowCount + 6], out int costCentreNumber);

                    if (!row[5].Equals("") && row[5] != null)
                        person.Department.Name = row[5];
                    else
                        person.Department.Name = "Leer";
                    person.Department.CostCentre = costCentreNumber;

                    costCentreList.Add(costCentreNumber);
                    if (row[rowCount + 8] != null)
                        person.LastName = row[rowCount + 8];
                    else
                        person.LastName = "";

                    if (row[rowCount + 9] != null)
                        person.FirstName = row[rowCount + 9];
                    else
                    {
                        person.Function = "";
                    }

                    switch (row[rowCount + 10])
                    {
                        case "Dr.":
                            person.Title = Title.DOC.ToString();
                            break;
                        case "PD Dr.":
                            person.Title = Title.PD_DOC.ToString();
                            break;
                        case "Prof.":
                            person.Title = Title.PROF.ToString();
                            break;
                        case "Prof. Dr.":
                            person.Title = Title.PROF_DOC.ToString();
                            break;
                        case "Prof. Dr. Dr.":
                            person.Title = Title.PROF_DOC_DOC.ToString();
                            break;
                        case "PD Dr. Dr.":
                            person.Title = Title.PD_DOC_DOC.ToString();
                            break;
                        default:
                            person.Title = Title.NONE.ToString();
                            break;
                    }

                    Int32.TryParse(row[rowCount + 11], out int employeeID);
                    person.Employee_Id = employeeID;

                    Int32.TryParse(row[rowCount + 12], out int staffID);
                    person.Staff_Id = staffID;
                    person.E_Mail = "" + person.FirstName + "." + person.LastName + "@med.uni-heidelberg.de";
                    person.Function = row[rowCount + 14];

                    person.Professional_Group = new Professional_Group
                    {
                        Name = row[rowCount + 16],
                        Skills = new List<string> { row[rowCount + 14] }
                    };

                    person.FormOfAdress = Form_Of_Adress.NONE.ToString();

                    rowCount = rowCount + 9;
                }
                else
                {
                    person.Department = new Department()
                    {
                        Name = "Leer",
                        CostCentre = 9999,
                        FunctionalAreas = new List<FunctionalArea>() { new FunctionalArea() { Name = "", Type = "OTHER" } }
                    };
                    person.Professional_Group = new Professional_Group()
                    { Name = "", Skills = new List<string>() { "0", "1" } };
                    person.LastName = "";
                    person.FirstName = "";
                    person.Title = Title.NONE.ToString();
                    person.Employee_Id = 0;
                    person.Staff_Id = 0;
                    person.E_Mail = "";
                    person.FormOfAdress = Form_Of_Adress.NONE.ToString();

                    person.Function = "";
                }

                listOfWorker.Add(person);
                Workspace ws = new Workspace();
                ws.Worker = person;
                listOfWorkspaces.Add(ws);
            }

            roomDetails.Workspaces = listOfWorkspaces;
            roomDetails.WorkersWithAccess = listOfWorker;

            return roomDetails;
        }

        /// <summary>
        /// Parse excel string array with excel rows into a worker object
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Worker ParseRowToWorkerObject(string[] row)
        {
            Worker worker = new Worker()
            {
                RoomName = "Empty",
                HasWorkspace = true,
                Department = new Department()
                {
                    Name = "Leer",
                    CostCentre = 9999,
                    FunctionalAreas = new List<FunctionalArea>()
                    {
                        new FunctionalArea()
                        {
                            Name = "empty",
                            Type = "EMPTY"
                        }
                    }
                },
                Professional_Group = new Professional_Group()
                { Name = "Berufsbezeichnung", Skills = new List<string>() { } },
                LastName = "",
                FirstName = "",
                Title = Title.NONE.ToString(),
                Employee_Id = 0,
                Staff_Id = 0,
                E_Mail = "",
                FormOfAdress = Form_Of_Adress.NONE.ToString(),
                Address = new Address
                {
                    Street = "",
                    Street_Number = "1",
                    Zip_Code = 0,
                    City = "",
                    Country = "Germany",
                    POB_Address = 0,
                    Region = ""
                },
                Function = ""
            };

            if (!row[0].Equals(""))
            {
                if (row[1] != null)
                    worker.LastName = row[1];
                else
                    worker.LastName = "";

                if (row[2] != null)
                    worker.FirstName = row[2];
                else
                {
                    worker.Function = "";
                }

                switch (row[3])
                {
                    case "Dr. ":
                        worker.Title = Title.DOC.ToString();
                        break;
                    case "PD Dr. ":
                        worker.Title = Title.PD_DOC.ToString();
                        break;
                    case "Prof. ":
                        worker.Title = Title.PROF.ToString();
                        break;
                    case "Prof. Dr. ":
                        worker.Title = Title.PROF_DOC.ToString();
                        break;
                    default:
                        worker.Title = Title.NONE.ToString();
                        break;
                }

                Int32.TryParse(row[10], out int employeeID);
                worker.Employee_Id = employeeID;

                Int32.TryParse(row[0], out int staffID);
                worker.Staff_Id = staffID;
                worker.E_Mail = "" + worker.FirstName + "." + worker.LastName + "@med.uni-heidelberg.de";
                worker.Function = row[4];

                worker.Professional_Group = new Professional_Group
                {
                    Name = row[4],
                    Skills = new List<string> { }
                };

                worker.FormOfAdress = Form_Of_Adress.NONE.ToString();
            }

            ;


            foreach (Worker w in Workers)
            {
                if (w.Employee_Id.Equals(worker.Employee_Id))
                {
                    worker.Department.CostCentre = w.Department.CostCentre;
                    worker.Department.Name = w.Department.Name;
                    worker.Professional_Group.Skills.Add(w.Professional_Group.Name);

                    w.FirstName = worker.FirstName;
                    w.LastName = worker.LastName;
                    w.Staff_Id = worker.Staff_Id;
                    w.Title = worker.Title;
                }
            }

            return worker;
        }

        /// <summary>
        /// Returns Floor Area Label by given Room Id
        /// </summary>
        /// <param name="Room_Id"></param>
        /// <returns></returns>
        private Floor_Area returnFloorLabelByRoomId(string Room_Id)
        {
            Floor_Area floor_Label = Floor_Area.EMPTY;

            int letzteRaumNummer;
            if (Room_Id != null)
            {
                string[] teil = Room_Id.Split(new char[] { '.' });
                int.TryParse(teil[2], out letzteRaumNummer);

                if (letzteRaumNummer < 100)
                {
                    floor_Label = Floor_Area.A;
                }
                else if (letzteRaumNummer < 200 && letzteRaumNummer >= 100)
                {
                    floor_Label = Floor_Area.B;
                }
                else if (letzteRaumNummer < 300 && letzteRaumNummer >= 200)
                {
                    floor_Label = Floor_Area.C;
                }
                else if (letzteRaumNummer < 500 && letzteRaumNummer >= 300)
                {
                    floor_Label = Floor_Area.A;
                }
                else if (letzteRaumNummer < 600 && letzteRaumNummer >= 500)
                {
                    floor_Label = Floor_Area.E;
                }
                else if (letzteRaumNummer >= 500)
                {
                    floor_Label = Floor_Area.F;
                }

                return floor_Label;
            }
            else
            {
                return Floor_Area.EMPTY;
            }
        }

        #endregion

        #region Assignment

        /// <summary>
        /// assigns the read csv list to the given collider tag
        /// </summary>
        /// <param name="colliderTag">the collider tag for object the information should be assigned to</param>
        public void AssignCSVRoomInfoToCollider(string colliderTag, HashSet<ServerRoom> rooms)
        {
            GameObject[] roomColliderToAssign = GameObject.FindGameObjectsWithTag(colliderTag);
            foreach (GameObject room in roomColliderToAssign)
            {
                if (room.GetComponent<ClientRoom>() != null)
                {
                    var clientRoom = room.GetComponent<ClientRoom>().MyRoom;

                    foreach (ServerRoom excelRoomDetails in rooms)
                    {
                        if (clientRoom.RoomName.Length <= 5)
                        {
                            Debug.LogError("Zu Kurz!");
                            Debug.LogError(room.name);
                        }
                        if (clientRoom.RoomName.Substring(4).Equals(excelRoomDetails.RoomName.Substring(4)))
                        {
                            clientRoom.Designation = excelRoomDetails.Designation;
                            clientRoom.NumberOfWorkspaces = excelRoomDetails.NumberOfWorkspaces;
                            clientRoom.Workspaces = excelRoomDetails.Workspaces;
                            clientRoom.WorkersWithAccess = excelRoomDetails.WorkersWithAccess;
                            clientRoom.AccessStyle = excelRoomDetails.AccessStyle;
                            clientRoom.NamePlate = excelRoomDetails.NamePlate;
                            clientRoom.Size = excelRoomDetails.Size;
                            clientRoom.Tags = excelRoomDetails.Tags;
                            clientRoom.Department = excelRoomDetails.Department;

                        }

                    }
                }
            }
        }

        static void GetCache(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Adds Roominfo data to the Room Container (Workers, Departments, Functional Areas, Professional Groups)
        /// </summary>
        /// <param name="rooms">CSV data rooms</param>
        private void AssignContainer(HashSet<ServerRoom> rooms)
        {
            foreach (ServerRoom room in rooms)
            {
                foreach (Worker worker in room.WorkersWithAccess)
                {
                    //Add Workers to HashSet Container
                    if (worker != null)
                    {
                        Workers.Add(worker);

                        //Add Professional Group to HashSet Container
                        if (worker.Professional_Group != null)
                            if (worker.Professional_Group.Name != null && !worker.Professional_Group.Name.Equals(""))
                                Professional_Groups.Add(worker.Professional_Group);

                        //Add Departments to HashSet Container
                        if (worker.Department != null)
                            if (worker.Department.Name != null && !worker.Department.Name.Equals(""))
                                Departments.Add(worker.Department);
                    }
                }

                if (room.NamePlate.BuildingSection != null)
                {
                    BuildingSections.Add(room.NamePlate.BuildingSection);
                }
                else if (room.NamePlate == null)
                {
                    Debug.Log(room.RoomName + " - no Nameplate");
                }
            }



            //Add Functional Areas to HashSet Container
            foreach (Department deparment in Departments)
            {
                foreach (FunctionalArea functionalArea in deparment.FunctionalAreas)
                {
                    if (functionalArea != null && !functionalArea.Name.Equals(""))
                        FunctionalAreas.Add(functionalArea);
                }
            }

            roomScriptManager.ExcelDepartments = Departments;
            roomScriptManager.ExcelFunctionalAreas = FunctionalAreas;
            roomScriptManager.ExcelProfessionalGroups = Professional_Groups;
            roomScriptManager.ExcelBuildingSections = BuildingSections;

        }

        #endregion

        /// <summary>
        /// Return a NamePlate of the Room by RoomId
        /// </summary>
        /// <param name="roomId">Room Id of the room</param>
        /// <returns>NamePlate of the Room; if no NamePlate is found returns Placeholder dummy Nameplate</returns>
        private NamePlate SearchForNamePlateByRoomId(string roomId)
        {
            //Standard NamePlate Object
            NamePlate displayedRoom = new NamePlate
            {
                RoomName = roomId,
                Floor = 0,
                VisibleRoomName = "0",
                BuildingSection = Floor_Area.EMPTY.ToString(),
                Designation = new List<string> { "" },
                SignPictogram = Pictogramm.NONE.ToString(),
                DoorPictogram = Pictogramm.NONE.ToString(),
                Style = Style.NONE.ToString()
            };

            foreach (NamePlate display in RoomNameplates)
            {
                if (roomId.Substring(4) == display.RoomName.Substring(4))
                {
                    displayedRoom = display;
                }
            }

            return displayedRoom;
        }


    }
}