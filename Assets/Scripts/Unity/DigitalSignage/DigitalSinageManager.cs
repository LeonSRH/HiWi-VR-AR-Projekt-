using SmartHospital.Controller;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using SmartHospital.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;



/// <summary>
/// 
/// Digital Sinage Manager
/// 
/// Author: Sebastian de Andrade
/// Date 5.8.2019
/// 
/// 
/// Door Sign is added ( UI Unity Elements) to this class and shall be filled.
/// 
/// </summary>


public class DigitalSinageManager : MonoBehaviour
{
    public bool WithBuilding;

    RoomSearch expandSearch;
    ClientRoom bla;

    HashSet<ServerRoom> allRooms;
    HashSet<ServerRoom> allRooms_2;


    [Header("RoomLoaderFrom_CSV")]
    public LoadCSVRoomData loadRoomDetails;


    [Header("Colors")]
    public Color color_Bereich_A;
    public Color color_Bereich_B;
    public Color color_Bereich_C;
    public Color color_Bereich_D;
    public Color color_Bereich_E;
    public Color color_Bereich_F;


    [Header("Materials")]
    public Material HighlightMaterial;
    public Material NormalMaterial;

    [Header("Ebenen")]
    public MeshRenderer Ebene03;
    public MeshRenderer Ebene02;
    public MeshRenderer Ebene01;
    public MeshRenderer Ebene00;
    public MeshRenderer Ebene99;
    public MeshRenderer Ebene98;
    public TextMeshPro EbenenText;




    [Header("The Doorsign - Normal")]    // Normales Türschild
    [SerializeField]
#pragma warning disable CS0649 // Dem Feld "DigitalSinageManager.doorSign" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
    DoorSign doorSign;
#pragma warning restore CS0649 // Dem Feld "DigitalSinageManager.doorSign" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".

    [Space(5)]

    [SerializeField]
#pragma warning disable CS0649 // Dem Feld "DigitalSinageManager.doorSignUI" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
    DoorSignUI doorSignUI;
#pragma warning restore CS0649 // Dem Feld "DigitalSinageManager.doorSignUI" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".

    [SerializeField]
    DoorSignUI doorSignUI_Patient;

    [Header("The Doorsign - Lang")]    // Langes Türschild
    [SerializeField]
#pragma warning disable CS0649 // Dem Feld "DigitalSinageManager.doorSign" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
    DoorSign doorSignLang;
#pragma warning restore CS0649 // Dem Feld "DigitalSinageManager.doorSign" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".

    [Space(5)]

    [Header("The Doorsign - LargeNumber")]    // Normales Türschild // Türelement mit großer Türnummer

    [SerializeField]
#pragma warning disable CS0649 // Dem Feld "DigitalSinageManager.doorSignLargeNumber" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
    DoorSignLargeNumber doorSignLargeNumber;
#pragma warning restore CS0649 // Dem Feld "DigitalSinageManager.doorSignLargeNumber" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
    // Add UI Element


    [Header("The Doorsign - 1 Pictogramm ")]         // Türelement mit Pictogramm

    [SerializeField]
#pragma warning disable CS0649 // Dem Feld "DigitalSinageManager.doorSignPictogramm" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
    DoorSignPictogramm doorSignPictogramm;
#pragma warning restore CS0649 // Dem Feld "DigitalSinageManager.doorSignPictogramm" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
    // Add UI Element

    [Header("The Doorsign - 2 Pictogramms ")]         // Türelement mit 2 Pictogramms


    [SerializeField]
#pragma warning disable CS0649 // Dem Feld "DigitalSinageManager.doorSignTwoPictogramms" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
    DoorSignTwoPictogramms doorSignTwoPictogramms;
#pragma warning restore CS0649 // Dem Feld "DigitalSinageManager.doorSignTwoPictogramms" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "null".
    // Add UI Element



    [Header("Recorder")]
    public ScreenRecorder screenRecorder;


    public TextMeshProUGUI Log;
    bool PrintingStarted = false;


    public GameObject HospitalScree_Up;
    public GameObject HospitalScreen_Down;

    public TMP_InputField IDInputField;
    string _ID;


    MainSceneUIController uiController;
    public GameObject UI_DigitalSinage;
    public GameObject UI_DigitalSinage_Patient;

    public GameObject TheShield;

    // Start is called before the first frame update
    void Start()
    {

        uiController = GameObject.FindObjectOfType<MainSceneUIController>();


        //allRooms = loadRoomDetails.readRoomCSVFileAndReturnRooms("Raumbuch");                 // Load All Rooms
        var rooms = GameObject.FindGameObjectsWithTag("RoomCollider");

        allRooms = new HashSet<ServerRoom>();

        foreach (GameObject g in rooms)
        {

            if (g.GetComponent<ClientRoom>().MyRoom != null)
            {
                ServerRoom nwR = g.GetComponent<ClientRoom>().MyRoom;

                if (nwR.NamePlate.RoomName != "0")
                    allRooms.Add(nwR);
            }
        }

        int Zähler = 0;

        for (int i = 0; i < allRooms.Count; i++)
        {
            Zähler += 1;

        }

        Debug.Log("ZähleRäume: " + Zähler.ToString());


        WithBuilding = true;

        if (WithBuilding)
        {                         // Lade die Opening Scene um Photos vom Gebäude zu erstellen.
            HospitalScree_Up.SetActive(false);
        }
        else
        {

            HospitalScree_Up.SetActive(false);                                                          // Wenn diese nicht geladen wird, dann entferne Hospital Screens
            HospitalScreen_Down.SetActive(false);
        }



        ToggleDigitalSinageUI();


    }

    // Update is called once per frame
    void Update()
    {

        if (IDInputField != null)
            _ID = IDInputField.text;


        if (uiController != null)
        {
            if (uiController.GetSelectedRoom() != null)
            {
                UpdateDoorSignOnUI();
            }
        }
    }



    public void SaveAllDoorSigns()
    {
        print("Save All Door Signs");
        if (PrintingStarted == false)
            StartCoroutine(CreateAllDoorSigns());

    }

    /// <summary>
    /// Own LOG function
    /// </summary>
    /// <param name="_text"></param>
    void LogProcessOfCreation(string _text)
    {
        if (Log != null)
        {
            Log.text = _text;
        }
    }


    /// <summary>
    /// Marks the current building level based on a string that contain level number
    /// </summary>
    /// <param name="_level"></param>
    void DisplayLevel(string _level)
    {

        EbenenText.text = "Ebene " + _level.ToString();

        if (_level == "03")
        {
            Ebene03.material = HighlightMaterial;
            Ebene02.material = NormalMaterial;
            Ebene01.material = NormalMaterial;
            Ebene00.material = NormalMaterial;
            Ebene99.material = NormalMaterial;
            Ebene98.material = NormalMaterial;
        }


        if (_level == "02")
        {
            Ebene03.material = NormalMaterial;
            Ebene02.material = HighlightMaterial;
            Ebene01.material = NormalMaterial;
            Ebene00.material = NormalMaterial;
            Ebene99.material = NormalMaterial;
            Ebene98.material = NormalMaterial;
        }

        if (_level == "01")
        {
            Ebene03.material = NormalMaterial;
            Ebene02.material = NormalMaterial;
            Ebene01.material = HighlightMaterial;
            Ebene00.material = NormalMaterial;
            Ebene99.material = NormalMaterial;
            Ebene98.material = NormalMaterial;

        }


        if (_level == "00")
        {
            Ebene03.material = NormalMaterial;
            Ebene02.material = NormalMaterial;
            Ebene01.material = NormalMaterial;
            Ebene00.material = HighlightMaterial;
            Ebene99.material = NormalMaterial;
            Ebene98.material = NormalMaterial;
        }




        if (_level == "99")
        {
            Ebene03.material = NormalMaterial;
            Ebene02.material = NormalMaterial;
            Ebene01.material = NormalMaterial;
            Ebene00.material = NormalMaterial;
            Ebene99.material = HighlightMaterial;
            Ebene98.material = NormalMaterial;
        }



        if (_level == "98")
        {
            Ebene03.material = NormalMaterial;
            Ebene02.material = NormalMaterial;
            Ebene01.material = NormalMaterial;
            Ebene00.material = NormalMaterial;
            Ebene99.material = NormalMaterial;
            Ebene98.material = HighlightMaterial;
        }
    }


    /// <summary>
    /// Toggles the "The Shield" Gameobject ( active / not active)
    /// </summary>
    public void ToggleDigitalSinageUI()
    {

        bool _active = TheShield.activeInHierarchy;


        TheShield.SetActive(!_active);


    }

    //======================================================================================================= Folgender Code muss den aktiven, angeklickten Raum bekommen 
    public void SaveActiveDoorSign()
    {

        ClientRoom selectedRoom = uiController.GetSelectedRoom();


        if (selectedRoom != null)
        {
            if (selectedRoom.MyRoom.NamePlate != null)
            {
                CreateOneDoorSign(selectedRoom.MyRoom, true);
            }
        }

        foreach (ServerRoom room in allRooms)
        {
            if (room.RoomName == _ID)
            {

                LogProcessOfCreation("Room Found");

                return;
            }
        }

        LogProcessOfCreation("Room Not Found");
    } // !!!!!!!!!!!!!!!!!!!
    //======================================================================================================= Folgender Code muss den aktiven, angeklickten Raum bekommen 

    /// <summary>
    /// When Room is found it creates exactly the Door Sign.
    /// </summary>
    /// <param name="_IDB"></param>
    public void SaveOneDoorSign(string _IDB)
    {

        foreach (ServerRoom room in allRooms)
        {


            if (room.RoomName == _ID)
            {

                LogProcessOfCreation("Room Found");
                CreateOneDoorSign(room, true);
                return;
            }
        }


        LogProcessOfCreation("Room Not Found");

    }



    /// <summary>
    /// Updates the Door Sign via uiControler
    /// </summary>
    public void UpdateDoorSignOnUI()
    {
        ServerRoom room = uiController.GetSelectedRoom().MyRoom;
        DisplayDoorSignOnUI(room);
    }


    /// <summary>
    /// Takes a Room and fills the Door Sign ond The UI with information
    /// </summary>
    /// <param name="room"></param>
    public void DisplayDoorSignOnUI(ServerRoom room)
    {

        string Persons = "";

        Persons = AnredeUndNamen(room);

        NamePlate displayedRoom = room.NamePlate;   // Für genauere Informationen des Raumes.
        var BuildingSection = "";


        if (displayedRoom != null)
        {
            BuildingSection = room.NamePlate.BuildingSection;
        }
        else
        {
            BuildingSection = "X";
        }

        if (room.NamePlate.BuildingSection != null)
            BuildingSection = room.NamePlate.BuildingSection.ToString();
        else
            BuildingSection = "X";




        Color signColor = RetunSignColor(BuildingSection);    // Hole dir Farbe

        string RoomNumber = "0";


        /// Nur wenn keine Nummer, dann die letzten drei Zahlen der Room ID nehmen. =?========================================================<<<<<< NEUEABFRAGE
        if (displayedRoom != null)
        {
            if (displayedRoom.RoomName == null || displayedRoom.RoomName == "" || displayedRoom.RoomName == "0")
            {
                RoomNumber = string.Concat(room.RoomName[8], room.RoomName[9], room.RoomName[10]);
            }
            else
            {
                RoomNumber = displayedRoom.VisibleRoomName;

            }
        }
        else
        {
            RoomNumber = "0";
        }

        string ebene = "";
        if (displayedRoom != null)
        {
            ebene = "Ebene " + displayedRoom.Floor;
        }
        else
        {
            ebene = "Ebene empty";
        }

        var doorSignDesignation = "";
        foreach (string designtation in room.NamePlate.Designation)
        {
            doorSignDesignation += "\n" + designtation;
        }
        ///========================================================================================================================================


        if (BuildingSection == "A" || BuildingSection == "B" || BuildingSection == "C" || BuildingSection == "X")
        {

            UI_DigitalSinage.SetActive(true);
            UI_DigitalSinage_Patient.SetActive(false);

            FillDoorSignUI(
                doorSignUI,
               BuildingSection,                                                        // A, B , C, D , E ; X
               signColor,                                                              // Farbe
               ebene,              // Ebene, gelesen aus ID
               RoomNumber,      // Raumnummer, gelesen aus ID
               doorSignDesignation,/*room.Designation*/
               "",// Bezeichnung
               Persons,                                                                // Namen der Personen
               room.RoomName);                                                          // ID, sehr klein am unteren Ende des Schildes.



        }
        else
        {

            UI_DigitalSinage_Patient.SetActive(true);
            UI_DigitalSinage.SetActive(false);

            FillDoorSignUI(
                doorSignUI_Patient,
               BuildingSection,                                                        // A, B , C, D , E ; X
               signColor,                                                              // Farbe
               ebene,              // Ebene, gelesen aus ID
               RoomNumber,      // Raumnummer, gelesen aus ID
               doorSignDesignation,/*room.Designation*/
               "",// Bezeichnung
               Persons,                                                                // Namen der Personen
               room.RoomName);                                                          // ID, sehr klein am unteren Ende des Schildes.

        }



    }

    /// <summary>
    /// For Name Sake - it takes a Room and when it has workers it takes them and parses them with Titel and empty spaces.
    /// Otherwise we would have a mess with the names and they couldnt be displayed
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    string AnredeUndNamen(ServerRoom room)
    {
        //===================================================================================================================================
        //===================================================================================================================================
        //===================================================================================================================================
        // Hier werden die Namen im Raum behandelt "Person" ist gegebener Datentyp, Persons (mit s am Ende) ist eigener String der alle Namen beinhaltet

        List<Worker> allPersonsInARoom;

        // Nun lade alle Menschen aus dem Workspace in eigenes Array.
        if (room.NumberOfWorkspaces != 0)
        {


            allPersonsInARoom = new List<Worker>(); // erstelle array mit der Größe der Mitarbeiteranzahl

            foreach (Workspace workspace in room.Workspaces)
            {

                allPersonsInARoom.Add(workspace.Worker);

            } // Übernehme die Daten
            string Persons = "";

            for (int z = 0; z < allPersonsInARoom.Count; z++)              // Speicher nur Räume, wenn Name zugeteilt ist.
            {
                Title localTitle = (Title)Enum.Parse(typeof(Title), allPersonsInARoom[z].Title);
                Form_Of_Adress localAdress = (Form_Of_Adress)Enum.Parse(typeof(Form_Of_Adress), allPersonsInARoom[z].FormOfAdress);


                // 1) Definiere Anrede der Person

                switch (localAdress)
                {
                    //===========================================
                    //===========================================
                    //===========================================
                    case Form_Of_Adress.MRS:

                        Persons += "Frau ";
                        break;
                    //===========================================
                    //===========================================
                    //===========================================
                    case Form_Of_Adress.MR:
                        Persons += "Herr ";
                        break;
                    //===========================================
                    //===========================================
                    //===========================================
                    default:
                        Persons += "";
                        break;

                }

                switch (localTitle)
                {
                    //===========================================
                    //===========================================
                    //===========================================
                    case Title.NONE:

                        Persons += "";

                        break;
                    //===========================================
                    //===========================================
                    //===========================================
                    case Title.DOC:

                        Persons += "Dr. ";

                        break;
                    //===========================================
                    //===========================================
                    //===========================================
                    case Title.PD_DOC:

                        Persons += "PD Dr. ";

                        break;
                    //===========================================
                    //===========================================
                    //===========================================
                    case Title.PROF:

                        Persons += "Prof. ";

                        break;
                    //===========================================
                    //===========================================
                    //===========================================
                    case Title.PROF_DOC:

                        Persons += "Prof. Dr. ";

                        break;
                    case Title.PROF_DOC_DOC:

                        Persons += "Prof. Dr. Dr. ";

                        break;
                    case Title.PD_DOC_DOC:

                        Persons += "PD Dr. Dr. ";

                        break;
                    //===========================================
                    //===========================================
                    //===========================================
                    default:

                        Persons += "";

                        break;



                }

                // 2) Definiere Vorname der Person

                if (allPersonsInARoom[z].FirstName != null)
                {
                    Persons += FirstNameCutter(allPersonsInARoom[z].FirstName);
                    Persons += "";
                }

                // 3) Definiere Nachname der Person

                if (allPersonsInARoom[z].LastName != null) Persons += ClearAllGaps(allPersonsInARoom[z].LastName);


                // 4) Wenn Mehr als eine Person, dann beginne mit einer neuen Linie
                if (Persons != "")
                    Persons += " \n";

            }




            // Hier endet das Bearbeiten des "Persons String" der alle Namen im Raum beinhaltet
            //===================================================================================================================================
            //===================================================================================================================================
            //===================================================================================================================================
            return Persons;
        }

        return "";

    }






    /// <summary>
    /// Returns a Color based on the wing of the building. This is wrtitten hard coded.
    /// </summary>
    /// <param name="_abschnitt"></param>
    /// <returns></returns>
    Color RetunSignColor(string _abschnitt)
    {

        string BuildingSection = _abschnitt;
        Color signColor = Color.grey;

        // Einfärben der Akzentfarbe des Buttons. Farben orientieren sich um Design und wurden mit Pipette übernommen.
        if (BuildingSection == "A")
            signColor = color_Bereich_A;

        if (BuildingSection == "B")
            signColor = color_Bereich_B;

        if (BuildingSection == "C")
            signColor = color_Bereich_C;

        if (BuildingSection == "D")
            signColor = color_Bereich_D;

        if (BuildingSection == "E")
            signColor = color_Bereich_E;

        if (BuildingSection == "F")
            signColor = color_Bereich_F;


        return signColor;
    }




    /// <summary>
    /// One Door Sign ( on A4 Paper Size) shall be filles with a Room. At the end it takes a snapshot via a camera ans saves it as jpeg or png file
    /// </summary>
    /// <param name="room"></param>
    /// <param name="_takePhoto"></param>
    public void CreateOneDoorSign(ServerRoom room, bool _takePhoto)
    {
        NamePlate displayedRoom = room.NamePlate;
        /**
         * 
        if (WithBuilding)
            SetCameraToRoomUI.setCameraToRoom(room.RoomName);            // Wenn das Haus vorhanden ist, dann richte die Kamera an die Position des generierten Raumes
        **/

        Debug.Log("RoomID: " + "" + room.RoomName);

        // Hier wird das Stockwerk dargestellt. Aus der Raum ID wird die Ebene
        //als String übergeben. Funktion liest das ein und Färbt die Ebene in einem 3D Modell über das Material ein.

        string floor = "";

        if (room.NamePlate != null)
        {
            floor = "" + room.NamePlate.Floor;
        }

        string BuildingSection = room.NamePlate.BuildingSection;

        Color signColor = RetunSignColor(BuildingSection);

        if (room.RoomName != "")
            DisplayLevel(floor);              // Ebene, gelesen aus ID)


        string Persons = AnredeUndNamen(room);
        // Definiere anrede und Namen



        //=========================================================================================================================
        //=========================================================================================================================

        // Hier entscheidet sich, welcher Typus an Schild eingebaut wird. ( Das DoorSignObjekt könnte dann auch vor die Kamera gestztwerden, für das Photo)


        int rnd = (int)Enum.Parse(typeof(Style), displayedRoom.Style);

        //int rnd = 1;

        Debug.Log("Room Stil: " + "" + displayedRoom.Style + "Number: " + rnd.ToString());


        string style = rnd.ToString();

        var doorSignDesignation = "";
        foreach (string designtation in room.NamePlate.Designation)
        {
            doorSignDesignation += "\n" + designtation;
        }

        if (style == "1")        //=============================================================== Normales Büroschild
        {

            screenRecorder.transform.localPosition = new Vector3(0, 0, -10.0f);   // Setze neue Position, für photographie.



            FillDoorSign(

                    BuildingSection,                                                        // A, B , C, D , E ; X
                    signColor,                                                              // Farbe
                    string.Concat("Ebene ", room.RoomName[5], room.RoomName[6]),              // Ebene, gelesen aus ID
                    string.Concat(room.RoomName[8], room.RoomName[9], room.RoomName[10]),      // Raumnummer, gelesen aus ID
                    doorSignDesignation,                                   // Bezeichnung
                    "",                                  // Untertitel
                    Persons,                                                                // Namen der Personen
                    room.RoomName                                                            // ID, sehr klein am unteren Ende des Schildes.
            );
        }
        else if (style == "2")       //=============================================================== Große Türnummer
        {

            screenRecorder.transform.localPosition = new Vector3(-21.0f, 0, -10.0f);  // Setze neue Position, für photographie.

            FillDoorSign_LargeNumbers(

                    BuildingSection,                                                        // A, B , C, D , E ; X
                    signColor,                                                              // Farbe
                    string.Concat("Ebene ", room.RoomName[5], room.RoomName[6]),              // Ebene, gelesen aus ID
                    displayedRoom.VisibleRoomName,      // Raumnummer, gelesen aus ID
                    doorSignDesignation,                                   // Bezeichnung
                    "",                                  // Untertitel
                    room.RoomName                                                            // ID, sehr klein am unteren Ende des Schildes.
            );
        }
        else if (style == "3")       //=============================================================== 1 Pictogramm
        {


            screenRecorder.transform.localPosition = new Vector3(-41.0f, 0, -10.0f);   // Setze neue Position, für photographie.

            FillDoorSign_Pictogramm(

                    BuildingSection,                                                        // A, B , C, D , E ; X
                    signColor,                                                              // Farbe
                    string.Concat("Ebene ", room.RoomName[5], room.RoomName[6]),              // Ebene, gelesen aus ID
                    string.Concat(room.RoomName[8], room.RoomName[9], room.RoomName[10]),      // Raumnummer, gelesen aus ID
                    doorSignDesignation,                                   // Bezeichnung
                    "",                                  // Untertitel
                    room.RoomName                                                            // ID, sehr klein am unteren Ende des Schildes.
            );
        }
        else if (style == "4")       //=============================================================== 2 Pictogramm ( wahrscheinlich gibts nur ein o. 2 Schilder im Gebäude, wo das zutrifft)
        {


            screenRecorder.transform.localPosition = new Vector3(-61.0f, 0, -10.0f);   // Setze neue Position, für photographie.

            FillDoorSign_TwoPictogramms(

                    BuildingSection,                                                        // A, B , C, D , E ; X
                    signColor,                                                              // Farbe
                    string.Concat("Ebene ", room.RoomName[5], room.RoomName[6]),              // Ebene, gelesen aus ID
                    string.Concat(room.RoomName[8], room.RoomName[9], room.RoomName[10]),      // Raumnummer, gelesen aus ID
                    doorSignDesignation,                                   // Bezeichnung
                    "",                                  // Untertitel
                    room.RoomName                                                            // ID, sehr klein am unteren Ende des Schildes.

                );



        }
        else if (style == "5")       //=============================================================== 2 Pictogramm ( wahrscheinlich gibts nur ein o. 2 Schilder im Gebäude, wo das zutrifft)
        {


            screenRecorder.transform.localPosition = new Vector3(19.3f, 0, -10.0f);   // Setze neue Position, für photographie.
            Debug.Log("langes schild");
            FillDoorSign_Lang(

                   BuildingSection,                                                        // A, B , C, D , E ; X
                    signColor,                                                              // Farbe
                    string.Concat("Ebene ", room.RoomName[5], room.RoomName[6]),              // Ebene, gelesen aus ID
                    string.Concat(room.RoomName[8], room.RoomName[9], room.RoomName[10]),      // Raumnummer, gelesen aus ID
                    doorSignDesignation,                                   // Bezeichnung
                    "",                                  // Untertitel
                    Persons,                                                                // Namen der Personen
                    room.RoomName                                                            // ID, sehr klein am unteren Ende des Schildes.

                );



        }
        else        //=============================================================== Normales Büroschild
        {

            screenRecorder.transform.localPosition = new Vector3(0, 0, -10.0f);   // Setze neue Position, für photographie.

            FillDoorSign(

                    BuildingSection,                                                        // A, B , C, D , E ; X
                    signColor,                                                              // Farbe
                    string.Concat("Ebene ", room.RoomName[5], room.RoomName[6]),              // Ebene, gelesen aus ID
                    string.Concat(room.RoomName[8], room.RoomName[9], room.RoomName[10]),      // Raumnummer, gelesen aus ID
                    doorSignDesignation,                                   // Bezeichnung
                    "",                                  // Untertitel
                    Persons,                                                                // Namen der Personen
                    room.RoomName                                                            // ID, sehr klein am unteren Ende des Schildes.
            );
        }


        //=========================================================================================================================
        //=========================================================================================================================

        if (_takePhoto)
        {
            screenRecorder.prefix = room.RoomName;   // Das Prefix wird vor die Datei angesetzt,
            screenRecorder.CaptureScreenshot();     // Foto wird geschossen,
        }


    }



    /// <summary>
    /// Creates all Door Signs with a Co Routine. It taktes the photos one by one.
    /// </summary>
    /// <returns></returns>
    IEnumerator CreateAllDoorSigns()
    {
        //=============================================================================================
        //=============================================================================================

        PrintingStarted = true;

        Debug.Log(" CreateAllDoorSignsStarted");

        // Provisorisches setzen einer kleineren Pixel Größe.
        //screenRecorder.captureHeight = 620;
        //screenRecorder.captureWidth = 877;


        int i = 0;


        foreach (ServerRoom room in allRooms)
        {

            //LogProcessOfCreation("Create and Print: " + room.Room_id.ToString() + " " + i.ToString() + "/" + countAllRooms.ToString());
            if (room.RoomName != null)
            {
                //string value = "";
                Debug.Log(" CheckDoorSign");
                // DisplayedRoom dR = room.
                //if(room.Door_plate_number)
                // value = room.Room_id[5].ToString() + room.Room_id[6].ToString();
                //value = "01";
                //Debug.Log("Value is: " + value);
                //if (room.Workspace_number != 0) // Nur, wenn leute Arbeiten
                if (room.NamePlate != null)
                    if (room.NamePlate.Designation.First().ToString().Contains("IMC Station E") || room.NamePlate.Floor.ToString().Contains("6420.00")) // Nur, wenn leute Arbeiten
                    {

                        yield return new WaitForSeconds(0.1f);     // Hält die Funktion auf - es könnte sein, dass Take Screenshot zum Rendern etwas länger benötigt.
                        CreateOneDoorSign(room, true);               // Übergeb den Room , TakePhoto = true, erstelle screenshot.
                                                                     //yield return new WaitForSeconds(0.1f);
                                                                     //screenRecorder.prefix = room.Room_id;   // Das Prefix wird vor die Datei angesetzt,
                                                                     //screenRecorder.CaptureScreenshot();     // Foto wird geschossen,


                        i++;

                        if ((i >= 300) || Input.GetKeyDown(KeyCode.Escape))      // Selbsterstellte EndBedingung
                        {
                            LogProcessOfCreation("Stopped");
                            PrintingStarted = false;
                            break;
                        }
                    }
            }
        }


        LogProcessOfCreation("Success!!!! ");
        PrintingStarted = false;

    }


    /// <summary>
    /// Nimmt einen String und schreibt ihn in "Karl" => "K."
    /// Cuts away the name und reduces it to the first letter. "Karl" => "K."
    /// </summary>

    string FirstNameCutter(string _name)
    {


        char[] nameArray = new char[_name.Length];

        nameArray = StringToCharArray(_name);


        if (_name.Length > 0)
            return nameArray[0].ToString() + ". ";
        else
            return "";

    }

    /// <summary>
    /// Säubert einen String und wirft alles SPACES heraus. "  B L u a gdk234" => "BLuagdk234"
    /// Clears a string and throws away all SPACES " B L u a gdk234" => "BLuagdk234"
    /// </summary>
    string ClearAllGaps(string _text)
    {

        string cleanedText = "";
        char[] charText = new char[_text.Length];
        char[] cleanedCharText = new char[_text.Length];

        char CheckFor = ' ';


        charText = StringToCharArray(_text);



        foreach (char ch in charText)
        {

            if (ch.CompareTo(CheckFor) == 0)    // Wenn ein Space gefunden wurde, tue nichts. Wenn ein Buchstabe gefunden, dass füge ihn zu cleanedText hinzu.
            {
                cleanedText += ch.ToString();
            }
            else
            {

                cleanedText += ch.ToString();

            }
        }


        return cleanedText;

    }

    /// <summary>
    /// Nimmt einen String und zerhacktstückelt es in ein Chararray der Länge des Strings
    /// Takes a string and butchers it into a char array with the lenght of the String
    /// </summary>
    char[] StringToCharArray(string _text)
    {
        char[] charArr = _text.ToCharArray();
        return charArr;
    }

    /// <summary>
    /// Das Türschild wird mit Werten beschrieben.
    /// Writes into the doors sign values.
    /// </summary>
    /// 
    public void FillDoorSign(

        string _abteil,
        Color _farbe,
        string _ebene,
        string _raumNummer,
        string _bezeichnung_1,
        string _bezeichnung_2,
        string _personenImRaum,
        string _id_nummer

    )
    {

        doorSign.Abteil.text = _abteil;
        doorSign.FarbElement.color = _farbe;
        doorSign.Ebene.text = _ebene;
        doorSign.Raumnummer.text = _raumNummer;
        doorSign.Bezeichnung.text = _bezeichnung_1;
        doorSign.Bezeichnung_2.text = _bezeichnung_2;
        doorSign.PersonenImRaum.text = _personenImRaum;
        doorSign.ID_Nummer.text = _id_nummer;
        doorSign.AkzentFarbe.color = _farbe;

    }

    // <summary>
    /// Das Türschild wird mit Werten beschrieben.
    /// Writes into the doors sign values.
    /// </summary>
    /// 
    public void FillDoorSign_Lang(

        string _abteil,
        Color _farbe,
        string _ebene,
        string _raumNummer,
        string _bezeichnung_1,
        string _bezeichnung_2,
        string _personenImRaum,
        string _id_nummer

    )
    {

        doorSignLang.Abteil.text = _abteil;
        doorSignLang.FarbElement.color = _farbe;
        doorSignLang.Ebene.text = _ebene;
        doorSignLang.Raumnummer.text = _raumNummer;
        doorSignLang.Bezeichnung.text = _bezeichnung_1;
        doorSignLang.Bezeichnung_2.text = _bezeichnung_2;
        doorSignLang.PersonenImRaum.text = _personenImRaum;
        doorSignLang.ID_Nummer.text = _id_nummer;
        doorSignLang.AkzentFarbe.color = _farbe;

    }

    public void FillDoorSign_LargeNumbers(

        string _abteil,
        Color _farbe,
        string _ebene,
        string _raumNummer,
        string _bezeichnung_1,
        string _bezeichnung_2,
        string _id_nummer

    )
    {
        doorSignLargeNumber.Abteil.text = _abteil;
        doorSignLargeNumber.FarbElement.color = _farbe;
        doorSignLargeNumber.Ebene.text = _ebene;
        doorSignLargeNumber.Raumnummer.text = _raumNummer;
        doorSignLargeNumber.Bezeichnung.text = _bezeichnung_1;
        doorSignLargeNumber.Bezeichnung_2.text = _bezeichnung_2;
        //doorSignLargeNumber.PersonenImRaum.text = _personenImRaum;    Gibts ja nicht mehr
        doorSignLargeNumber.ID_Nummer.text = _id_nummer;
        // doorSignLargeNumber.AkzentFarbe.color = _farbe;
    }


    public void FillDoorSign_Pictogramm(

        string _abteil,
        Color _farbe,
        string _ebene,
        string _raumNummer,
        string _bezeichnung_1,
        string _bezeichnung_2,
        string _id_nummer

    )
    {
        doorSignPictogramm.Abteil.text = _abteil;
        doorSignPictogramm.FarbElement.color = _farbe;
        doorSignPictogramm.Ebene.text = _ebene;
        doorSignPictogramm.Raumnummer.text = _raumNummer;
        doorSignPictogramm.Bezeichnung.text = _bezeichnung_1;
        doorSignPictogramm.Bezeichnung_2.text = _bezeichnung_2;
        //doorSignLargeNumber.PersonenImRaum.text = _personenImRaum;    Gibts ja nicht mehr
        doorSignPictogramm.ID_Nummer.text = _id_nummer;
        //doorSignPictogramm.AkzentFarbe.color = _farbe;

        // Erweiterung der Möglichkeit den SpriteRenderer mit dem Pictogramm anzusprechen.

    }


    public void FillDoorSign_TwoPictogramms(

         string _abteil,
         Color _farbe,
         string _ebene,
         string _raumNummer,
         string _bezeichnung_1,
         string _bezeichnung_2,
         string _id_nummer

    )
    {

        doorSignTwoPictogramms.Abteil.text = _abteil;
        doorSignTwoPictogramms.FarbElement.color = _farbe;
        doorSignTwoPictogramms.Ebene.text = _ebene;
        doorSignTwoPictogramms.Raumnummer.text = _raumNummer;
        doorSignTwoPictogramms.Bezeichnung.text = _bezeichnung_1;
        doorSignTwoPictogramms.Bezeichnung_2.text = _bezeichnung_2;
        //doorSignLargeNumber.PersonenImRaum.text = _personenImRaum;    Gibts ja nicht mehr
        doorSignTwoPictogramms.ID_Nummer.text = _id_nummer;
        //doorSignPictogramm.AkzentFarbe.color = _farbe;

        // Erweiterung der Möglichkeit den SpriteRenderer mit dem Pictogramm anzusprechen.

    }



    public void FillDoorSignUI(

        DoorSignUI doorSignUI,
        string _abteil,
        Color _farbe,
        string _ebene,
        string _raumNummer,
        string _bezeichnung_1,
        string _bezeichnung_2,
        string _personenImRaum,
        string _id_nummer


        )
    {
        doorSignUI.Abteil.text = _abteil;
        doorSignUI.FarbElement.color = _farbe;
        doorSignUI.Ebene.text = _ebene;
        doorSignUI.Raumnummer.text = _raumNummer;
        doorSignUI.Bezeichnung.text = _bezeichnung_1;
        doorSignUI.Bezeichnung_2.text = _bezeichnung_2;
        doorSignUI.PersonenImRaum.text = _personenImRaum;
        doorSignUI.ID_Nummer.text = _id_nummer;
    }



}





//===============================================================
//===============================================================
//===============================================================

// Das klassische Türschild, einmal für die Darstellung als abphotographierbares Element
// und einmal als UI Element, dass am Haus selbst dargestellt wird. Bei beiden Elementen kann es (logischerweise) eine Diskrepanz in der Darstellung geben.
// => Schöner were eine Darstellung des Abfotographierbaren, ohne Spiegelung!!!!!

[System.Serializable]
public class DoorSign
{
    //[SerializeField]
    //public TMP_text Raumnummer;
    [SerializeField]
    public TextMeshPro Abteil;

    [SerializeField]
    public SpriteRenderer FarbElement;

    [SerializeField]
    public TextMeshPro Ebene;

    [SerializeField]
    public TextMeshPro Raumnummer;


    [SerializeField]
    public TextMeshPro Bezeichnung;

    [SerializeField]
    public TextMeshPro Bezeichnung_2;

    [SerializeField]
    public TextMeshPro PersonenImRaum;

    [SerializeField]
    public TextMeshPro ID_Nummer;

    public SpriteRenderer AkzentFarbe;

}

[System.Serializable]
public class DoorSignUI
{
    //[SerializeField]
    //public TMP_text Raumnummer;
    [SerializeField]
    public TextMeshProUGUI Abteil;

    [SerializeField]
    public Image FarbElement;

    [SerializeField]
    public TextMeshProUGUI Ebene;

    [SerializeField]
    public TextMeshProUGUI Raumnummer;


    [SerializeField]
    public TextMeshProUGUI Bezeichnung;

    [SerializeField]
    public TextMeshProUGUI Bezeichnung_2;

    [SerializeField]
    public TextMeshProUGUI PersonenImRaum;

    [SerializeField]
    public TextMeshProUGUI ID_Nummer;


}

//===============================================================
//===============================================================
//===============================================================

// Hier wird das Türschild mit nur einem Pictogramm dargestellt ( Daten werden gefüllt)

[System.Serializable]
public class DoorSignPictogramm
{
    //[SerializeField]
    //public TMP_text Raumnummer;
    [SerializeField]
    public TextMeshPro Abteil;

    [SerializeField]
    public SpriteRenderer FarbElement;

    [SerializeField]
    public TextMeshPro Ebene;

    [SerializeField]
    public TextMeshPro Raumnummer;

    [SerializeField]
    public TextMeshPro Bezeichnung;

    [SerializeField]
    public TextMeshPro Bezeichnung_2;

    [SerializeField]
    public MeshRenderer Pictogram;


    [SerializeField]
    public TextMeshPro ID_Nummer;

    public SpriteRenderer AkzentFarbe;

}

//===============================================================
//===============================================================
//===============================================================

// Sehr ähnlich, aber mit 2 Pictogrammen. Wird im System selten vorkommen.

[System.Serializable]
public class DoorSignTwoPictogramms
{
    //[SerializeField]
    //public TMP_text Raumnummer;
    [SerializeField]
    public TextMeshPro Abteil;

    [SerializeField]
    public SpriteRenderer FarbElement;

    [SerializeField]
    public TextMeshPro Ebene;

    [SerializeField]
    public TextMeshPro Raumnummer;

    [SerializeField]
    public TextMeshPro Bezeichnung;

    [SerializeField]
    public TextMeshPro Bezeichnung_2;

    [SerializeField]
    public MeshRenderer PictogramLeft;

    [SerializeField]
    public MeshRenderer PictogramRight;


    [SerializeField]
    public TextMeshPro ID_Nummer;

    public SpriteRenderer AkzentFarbe;

}

//===============================================================
//===============================================================
//===============================================================

// Geeignet für Patientenzimmer. Keine Namen werden dargestellt - nur die Raumnummer in groß und restlicher Balast.
[System.Serializable]
public class DoorSignLargeNumber
{
    //[SerializeField]
    //public TMP_text Raumnummer;
    [SerializeField]
    public TextMeshPro Abteil;

    [SerializeField]
    public SpriteRenderer FarbElement;

    [SerializeField]
    public TextMeshPro Ebene;

    [SerializeField]
    public TextMeshPro Raumnummer;

    [SerializeField]
    public TextMeshPro Bezeichnung;

    [SerializeField]
    public TextMeshPro Bezeichnung_2;

    [SerializeField]
    public TextMeshPro ID_Nummer;

    public SpriteRenderer AkzentFarbe;

}

//===============================================================



