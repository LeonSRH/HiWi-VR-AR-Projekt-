using SmartHospital.Controller;
using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Controller.ExplorerMode.Rooms.Details;
using TMPro;
///=================================================================================================================================
///=================================================================================================================================
///=================================================================================================================================
///=================================================================================================================================

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

// NUI_FLOOR

///=================================================================================================================================
///=================================================================================================================================
/// 
/// Author: Sebastian de Andrade ( AKA Meister des Codes und Herr der Chiffren)
/// Date: 24.5.2019 ( Im Jahre des Herrn.. Amen)
/// 
/// The Class NUI_Floors creates UI Elements for the Main system. It creates the elements via "Fillpanel", a UI Element factory.
/// 
///     
///=================================================================================================================================
///=================================================================================================================================


public class NUI_Floors : MonoBehaviour
{

    //=========================================================================================================================================================================================================
    //=========================================================================================================================================================================================================
    //=========================================================================================================================================================================================================


    public DigitalSinageManager digitalSinageManager;
    public RoomDetailsView roomDetailsView;
    public LoadCSVRoomData roomDetails;
    public ChangeWorkerView changeWorkerView;


    RoomSearch expandSearchView;

    MainSceneUIController uIController;
    MainSceneMainController changeSceneViewInOpeningScene;

    [Header("Grafiken für User Interface")]
    public Sprite ProfileImage, UKHD_Logo, BackgroundColor, BackgroundColor_White, Ampel, ButtonIcon_PDF, ButtonIcon_Auseinander, ButtonIcon_Zusammen, Logo;

    FillPanel fillPanel;
    DelegateContainer.DelegateCallInt _function; // Test Function

    DelegateContainer.DelegateCallInt _LoadInfoScreen, _LoadSchildScreen, _LoadAnalyseScreen, _LoadPowerBIScreen, _LoadDetailSucheScreen; // All load functions

    DelegateContainer.DelegateCallInt _LoadScene_Raumbuch, _LoadScene_Planung, _LoadScene_Rundgang, _LoadScene_WMS, _LoadDeviceTracker; // All load functions for different scenes


    DelegateContainer.DelegateCallInt _ChangeToRoom; // ChangeToRoom
    DelegateContainer.DelegateCallInt _UnfoldAndCloseBuilding; // ChangeToRoom
    DelegateContainer.DelegateCallInt _CameraMode; // ChangeToRoom

    DelegateContainer.DelegateCallInt _StartPowerBI; // PowerBi
    DelegateContainer.DelegateCallInt _TriggerAnalyse; // PowerBi

    DelegateContainer.DelegateCallInt _ToggleWorkerScreen; // WorkerScreen
    DelegateContainer.DelegateCallInt _ToggleWorkerScreen_ChangeWorker; // WorkerScreen ChangeWorker

    [Header("WebBrowser")]
    public GameObject Canvas_WebPlayer;
    public GameObject Canvas;

    bool InformatioScreenSet = false;

    MainView mainView;

    //=========================================================================================================================================================================================================
    //=========================================================================================================================================================================================================
    //=========================================================================================================================================================================================================



    /// <summary>
    /// Start method sets up all the links ( like to fillpanel)
    /// And creates the User interface
    /// </summary>
    /// 
    void Start()
    {
        roomDetails = FindObjectOfType<LoadCSVRoomData>();
        roomDetailsView = FindObjectOfType<RoomDetailsView>();
        mainView = FindObjectOfType<MainView>();
        expandSearchView = FindObjectOfType<RoomSearch>();
        changeWorkerView = FindObjectOfType<ChangeWorkerView>();

        uIController = FindObjectOfType<MainSceneUIController>();
        changeSceneViewInOpeningScene = FindObjectOfType<MainSceneMainController>();

        InitDelegateFunctions(); // fill all delegate methods

        fillPanel = FindObjectOfType<FillPanel>();

        // Start Create User Interface

        CreateHeader();

        CreateNavigationBar();

        //CreateFooter();

        CreateFloorSelection();

        CreateInformationScreenMenu();

        CreateInformationScreen(1);

        CreateWorkerScreen();

        CreateWorkerScreenChanger();

    }

    /// <summary>
    /// All Delegate shall be filled with the corresponding function ( mostly the same name with the _ difference
    /// _Xfunction = "delegate"
    /// Xfunction = "function" 
    /// </summary>
    void InitDelegateFunctions()
    {
        _LoadInfoScreen += CreateInformationScreen;
        _LoadSchildScreen += CreateSchildScreen;
        _LoadAnalyseScreen += CreateAnalyseScreen;
        _LoadPowerBIScreen += CreatePowerBIScreen;
        _LoadDetailSucheScreen += CreateDetailSucheScreen;

        _LoadDeviceTracker += LoadDeviceTracker;
        _LoadScene_Rundgang += LoadRundgang;
        _LoadScene_WMS += LoadWMS;

        _ChangeToRoom += ChangeToRoom;
        _UnfoldAndCloseBuilding += UnfoldAndCloseBuilding;
        _CameraMode += ChangeCameraMode;

        _StartPowerBI += StartPowerBI;
        _TriggerAnalyse += Triggeranalyse;
        _function += Function;
    }

    #region MainView

    /// <summary>
    /// Creates the Header on Top of the screen
    /// </summary>
    void CreateHeader()
    {
        float yPos = 500;

        //fillPanel.CreateHeaderOnUI("CAIA", transform, 300, 65, -700, yPos);
        fillPanel.CreateImageOnUI(Logo, transform, 65, 65, -800, yPos);

        mainView.UserNameText = fillPanel.CreateTextElementOnUIWithHandle("Max Mustermann", transform, 200, 50, 550, yPos);
        fillPanel.CreateImageOnUI(ProfileImage, transform, 75, 75, 700, yPos);  // Profile Image
        //fillPanel.CreateImageOnUI(UKHD_Logo, transform, 55, 75, 800, yPos); // UKHD Logo
    }

    /// <summary>
    /// Navigation bar for different menu points
    /// </summary>
    void CreateNavigationBar()
    {
        float yPosOfOptions = 430;
        float gapOfButtonY = 200;

        mainView.RoombookButton = fillPanel.CreateButtonOnUIWithHandle("Raumbuch", transform, _function, 200, 50, -750, yPosOfOptions);
        mainView.InventoryModeButton = fillPanel.CreateButtonOnUIWithHandle("Inventar", transform, _function, 200, 50, -750 + (1 * gapOfButtonY), yPosOfOptions);
        //mainView.PlanningTabButton = fillPanel.CreateButtonOnUIWithHandle("Planung", transform, _function, 200, 50, -750 + (1 * gapOfButtonY), yPosOfOptions);
        fillPanel.CreateButtonOnUI("Rundgang", transform, _LoadScene_Rundgang, 200, 50, -750 + (2 * gapOfButtonY), yPosOfOptions);
        fillPanel.CreateButtonOnUI("OP-Schild", transform, _LoadScene_WMS, 200, 50, -750 + (3 * gapOfButtonY), yPosOfOptions);
        //mainView.LearningManagementSystemButton = fillPanel.CreateButtonOnUIWithHandle("Lernen", transform, _function, 200, 50, -750 + (4 * gapOfButtonY), yPosOfOptions);

    }

    /// <summary>
    ///  The Footer on Foot of the Screen
    /// </summary>
    void CreateFooter()
    {
        float yPos = 505; // Same as in Header

        fillPanel.CreateImageOnUI(BackgroundColor, transform, 2500, 30, 0, -yPos); // Background
        fillPanel.CreateTextElementOnUI("Version 0.51", transform, 200, 50, 750, -yPos); // Version Number
    }

    #endregion

    #region BuildingControlView

    /// <summary>
    /// Creat Floor Selection to click on specific floor
    /// </summary>
    void CreateFloorSelection()
    {
        float FloorSelect_Y_Pos = 150;
        float FloorSelect_X_Pos = -750;
        float gapOfButton = 50;

        Transform FloorSelection = fillPanel.CreateParentObject(transform);

        FloorSelection = fillPanel.CreatePanel(200, 400, UI_OrientationOnScreen.enumForOrientations.Left, UI_Panel.enumForLayoutGroup.Vertical, true);

        fillPanel.CreateImageOnUI(BackgroundColor_White, FloorSelection, 220, 370, FloorSelect_X_Pos, FloorSelect_Y_Pos); // Background

        fillPanel.CreateTextElementOnUI("Perspektive", FloorSelection, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos + 3 * gapOfButton);

        //== Create 2 horizontal buttons 
        Transform perspective = fillPanel.CreatePanel(200, 200, FloorSelection, UI_Panel.enumForLayoutGroup.Grid, false);
        fillPanel.CreateButtonOnUI("2D", perspective, _CameraMode, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos + 2 * gapOfButton, 0);
        fillPanel.CreateButtonOnUI("3D", perspective, _CameraMode, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos + 1 * gapOfButton, 1);


        fillPanel.CreateTextElementOnUI("Navigation", FloorSelection, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos + 3 * gapOfButton);

        Transform navi = fillPanel.CreatePanel(200, 200, FloorSelection, UI_Panel.enumForLayoutGroup.Grid, false);


        fillPanel.CreateButtonOnUI("03", navi, _ChangeToRoom, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos + 2 * gapOfButton, 5);   // Function Parameter (last element) is Change Room( int i)
        fillPanel.CreateButtonOnUI("02", navi, _ChangeToRoom, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos + 1 * gapOfButton, 4);
        fillPanel.CreateButtonOnUI("01", navi, _ChangeToRoom, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos + 0, 3);
        fillPanel.CreateButtonOnUI("00", navi, _ChangeToRoom, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos - 1 * gapOfButton, 2);
        fillPanel.CreateButtonOnUI("99", navi, _ChangeToRoom, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos - 2 * gapOfButton, 1);
        fillPanel.CreateButtonOnUI("98", navi, _ChangeToRoom, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos - 3 * gapOfButton, 0);

        fillPanel.CreateTextElementOnUI("Extra", FloorSelection, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos + 3 * gapOfButton);


        Transform buttons = fillPanel.CreatePanel(200, 200, FloorSelection, UI_Panel.enumForLayoutGroup.Grid, false);

        //fillPanel.CreateButtonOnUIWithHandle("", buttons, 200, 50, 0, 0, ButtonIcon_Auseinander);
        //fillPanel.CreateButtonOnUIWithHandle("", buttons, 200, 50, 0, 0, ButtonIcon_Zusammen);
        //fillPanel.CreateButtonOnUIWithHandle("", buttons, 200, 50, 0, 0, ButtonIcon_PDF);


        fillPanel.CreateButtonOnUI("Auseinander", FloorSelection, _UnfoldAndCloseBuilding, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos - 3 * gapOfButton, 1);
        fillPanel.CreateButtonOnUI("Zusammen", FloorSelection, _UnfoldAndCloseBuilding, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos - 3 * gapOfButton, 0);
        //fillPanel.CreateButtonOnUI("PDF Ansicht", FloorSelection, _function, 200, 50, FloorSelect_X_Pos, FloorSelect_Y_Pos - 3 * gapOfButton);

    }

    /// <summary>
    ///  Creates Information Screen Menu via fill Panel Class that is called in Awake function.
    /// </summary>
    public void CreateInformationScreenMenu()
    {
        Transform LeftButtonsParent = fillPanel.CreateParentObject(transform);
        mainView.informationScreenActionBar = LeftButtonsParent;

        float yPosOfInfoScreen = -195;
        float xPosOfInfoScreen = 550;

        float ySize = 50;
        float xSize = 100;

        fillPanel.CreateButtonOnUI("Tracking", LeftButtonsParent, _LoadDeviceTracker, xSize, ySize, xPosOfInfoScreen, yPosOfInfoScreen + (ySize * 2));
        fillPanel.CreateButtonOnUI("Schild", LeftButtonsParent, _LoadSchildScreen, xSize, ySize, xPosOfInfoScreen, yPosOfInfoScreen + (ySize * 1));
        fillPanel.CreateButtonOnUI("Raum Info", LeftButtonsParent, _LoadInfoScreen, xSize, ySize, xPosOfInfoScreen, yPosOfInfoScreen);
        fillPanel.CreateButtonOnUI("Analyse", LeftButtonsParent, _LoadAnalyseScreen, xSize, ySize, xPosOfInfoScreen, yPosOfInfoScreen - (ySize * 1));
        //mainView.PowerBiButton = fillPanel.CreateButtonOnUIWithHandle("PowerBI", LeftButtonsParent, _StartPowerBI, xSize, ySize, xPosOfInfoScreen, yPosOfInfoScreen - (ySize * 3));
        fillPanel.CreateButtonOnUI("Detail \n suche", LeftButtonsParent, _LoadDetailSucheScreen, xSize, ySize, xPosOfInfoScreen, yPosOfInfoScreen - (ySize * 2));
    }

    /// <summary>
    /// Worker Screen Changer via fill Panel class.
    /// </summary>
    public void CreateWorkerScreenChanger()
    {
        Transform Parent = fillPanel.CreateParentObject(transform);
        var scrollRect = Parent.gameObject.AddComponent<ScrollRect>();
        Parent.transform.localPosition = new Vector3(0, 0, 0);

        Parent.name = "WorkerScreen_Changer";

        Transform Workers = fillPanel.CreatePanel(1100, 600, Parent, UI_Panel.enumForLayoutGroup.Horizontal, true);
        Workers.transform.localPosition = new Vector3(-75, 0, 0); // set new position
        changeWorkerView.ChangeWorkerPanel = Workers;
        scrollRect.content = (RectTransform)Workers;

        Transform LocalElement = fillPanel.CreatePanel(200, 600, Workers, UI_Panel.enumForLayoutGroup.Vertical, true);

        Transform ImageElement = fillPanel.CreatePanel(600, 600, Workers, UI_Panel.enumForLayoutGroup.Vertical, true);
        fillPanel.CreateImageOnUI(BackgroundColor_White, ImageElement);

        fillPanel.CreateHeaderOnUI("Person", LocalElement);
        fillPanel.CreateTextElementOnUI("Titel", LocalElement, 0, 0, 0, 0);
        changeWorkerView.TitleDropdown = fillPanel.CreateDropDownOnUI("Title", LocalElement, new string[] { "NONE" }, _function);
        changeWorkerView.FirstNameInput = fillPanel.CreateInputFieldOnUIWithHandle("Vorname", LocalElement, 0, 0, 0, 0);
        changeWorkerView.LastNameInput = fillPanel.CreateInputFieldOnUIWithHandle("Nachname", LocalElement, 0, 0, 0, 0);
        changeWorkerView.EmployeeNumberInput = fillPanel.CreateInputFieldOnUIWithHandle("Mitarbeiter Nummer", LocalElement, 0, 0, 0, 0);
        changeWorkerView.CardNumberInput = fillPanel.CreateInputFieldOnUIWithHandle("Personal Nummer", LocalElement, 0, 0, 0, 0);
        changeWorkerView.FunctionInput = fillPanel.CreateInputFieldOnUIWithHandle("Function", LocalElement, 0, 0, 0, 0);
        fillPanel.CreateTextElementOnUI("Fachabteilung", LocalElement, 0, 0, 0, 0);
        changeWorkerView.DepartmentDropdown = fillPanel.CreateDropDownOnUI("Fach", LocalElement, new string[] { "NONE" }, _function);
        fillPanel.CreateTextElementOnUI("Arbeitsplatz", LocalElement, 0, 0, 0, 0);
        changeWorkerView.HasWorkspaceDropdown = fillPanel.CreateDropDownOnUI("Arbeitsplatz", LocalElement, new string[] { "NONE" }, _function);

        changeWorkerView.SaveButton = fillPanel.CreateButtonOnUIWithHandle("Speichern", LocalElement, 0, 0, 0, 0);
        changeWorkerView.CancelButton = fillPanel.CreateButtonOnUIWithHandle("Zurück", LocalElement, 0, 0, 0, 0);
    }

    /// <summary>
    /// Create Worker Screen
    /// </summary>
    public void CreateWorkerScreen()
    {
        Transform Parent = fillPanel.CreateParentObject(transform);
        mainView.WorkerPanel = Parent;
        Parent.transform.localPosition = new Vector3(0, 0, 0);
        Parent.name = "Workerscreen";


        Transform Workers = fillPanel.CreatePanel(1100, 600, Parent, UI_Panel.enumForLayoutGroup.Horizontal, true);
        Workers.transform.localPosition = new Vector3(-75, 0, 0);


        Transform LocalElement = fillPanel.CreatePanel(300, 600, Workers, UI_Panel.enumForLayoutGroup.Vertical, true);

        fillPanel.CreateHeaderOnUI("Pers.", LocalElement);

        fillPanel.CreateButtonOnUI("Bearbeiten", LocalElement, _function);
        fillPanel.CreateTextElementOnUI("Titel", LocalElement);
        fillPanel.CreateTextElementOnUI("Vorname", LocalElement);
        fillPanel.CreateTextElementOnUI("Nachname", LocalElement);
        fillPanel.CreateTextElementOnUI("Mitarb. NR", LocalElement);
        fillPanel.CreateTextElementOnUI("Pers. NR", LocalElement);
        fillPanel.CreateTextElementOnUI("Funktion", LocalElement);
        fillPanel.CreateTextElementOnUI("Fach", LocalElement);
        fillPanel.CreateTextElementOnUI("Arbeitsplatz", LocalElement);

        for (int z = 0; z < 6; z++)
        {
            Transform LocalElementMitarbeiter = fillPanel.CreatePanel(300, 600, Workers, UI_Panel.enumForLayoutGroup.Vertical, true);
            LocalElementMitarbeiter.name = "AP" + (z + 1);
            fillPanel.CreateHeaderOnUI("AP " + (z + 1).ToString(), LocalElementMitarbeiter);
            var workerView = LocalElementMitarbeiter.gameObject.AddComponent<WorkerView>();

            workerView.EditButton = fillPanel.CreateButtonOnUIWithHandle("+", LocalElementMitarbeiter, 0, 0, 0, 0);

            TextMeshProUGUI TitleText = fillPanel.CreateTextElementOnUIWithHandle("Title", LocalElementMitarbeiter, 0, 0, 0, 0);

            workerView.TitleText = TitleText;
            TextMeshProUGUI FirstNameText = fillPanel.CreateTextElementOnUIWithHandle("FirstName", LocalElementMitarbeiter, 0, 0, 0, 0);
            workerView.FirstNameText = FirstNameText;
            TextMeshProUGUI LastNameText = fillPanel.CreateTextElementOnUIWithHandle("LastName", LocalElementMitarbeiter, 0, 0, 0, 0);
            workerView.LastNameText = LastNameText;
            TextMeshProUGUI CardNumberText = fillPanel.CreateTextElementOnUIWithHandle("CardNumber", LocalElementMitarbeiter, 0, 0, 0, 0);
            workerView.CardNumberText = CardNumberText;
            TextMeshProUGUI EmployeeNumberText = fillPanel.CreateTextElementOnUIWithHandle("EmployeeNumber", LocalElementMitarbeiter, 0, 0, 0, 0);
            workerView.EmployeeNumberText = EmployeeNumberText;
            TextMeshProUGUI FunctionText = fillPanel.CreateTextElementOnUIWithHandle("Function", LocalElementMitarbeiter, 0, 0, 0, 0);
            workerView.FunctionText = FunctionText;
            TextMeshProUGUI DepartmentText = fillPanel.CreateTextElementOnUIWithHandle("Department", LocalElementMitarbeiter, 0, 0, 0, 0);
            workerView.DepartmentText = DepartmentText;
            TextMeshProUGUI HasWorkspaceText = fillPanel.CreateTextElementOnUIWithHandle("APBool", LocalElementMitarbeiter, 0, 0, 0, 0);
            workerView.HasWorkspaceText = HasWorkspaceText;
        }
        roomDetailsView.WorkerPanel = Workers;
    }

    /// <summary>
    /// Create Worker Screen
    /// </summary>
    public void CreateInformationScreen(int i)
    {

        float yPosOfInfoScreen = 0;
        float xPosOfInfoScreen = 300;

        float InfoScreenSize_Y = 850;

        fillPanel.ClearScreenGeography(UI_OrientationOnScreen.enumForOrientations.Right); // clear right hand side

        roomDetailsView.WorkplaceDirectiveImage = fillPanel.CreateImageOnUI(Ampel, transform, 100, 100, 535, 370); // AMPEL

        Transform Parent = fillPanel.CreateParentObject(transform);


        Parent = fillPanel.CreatePanel(290, 850, UI_OrientationOnScreen.enumForOrientations.Right, UI_Panel.enumForLayoutGroup.Vertical, true);
        Parent.name = "Information_Screen";
        roomDetailsView.TransformParentPanel = Parent;
        mainView.informationScreenMain = Parent;
        // Header
        fillPanel.CreateHeaderOnUI("Rauminfo", Parent, 300, 65, xPosOfInfoScreen, yPosOfInfoScreen + (InfoScreenSize_Y / 2) - 35);

        float heightOfFirstElement = 290; // the highest element. all added element shall add themselve below this first one.

        // KTG Nummer
        roomDetailsView.VisibleRoomNameInput = fillPanel.CreateInputFieldOnUIWithHandle("Klinische Raumnummer", Parent, 290, 85, xPosOfInfoScreen, yPosOfInfoScreen + heightOfFirstElement);


        // clinical room number
        roomDetailsView.RoomNameInput = fillPanel.CreateInputFieldOnUIWithHandle("Technische Raumnummer", Parent, 290, 85, xPosOfInfoScreen, yPosOfInfoScreen + heightOfFirstElement - (85 * 1));


        // room
        roomDetailsView.DesignationInput = fillPanel.CreateInputFieldOnUIWithHandle("Raumbez.", Parent, 290, 85, xPosOfInfoScreen, yPosOfInfoScreen + heightOfFirstElement - (85 * 2));

        // designation
        string[] fachAbteilungen = { "A", "B", "C" };
        roomDetailsView.DepartmentDropdown = fillPanel.CreateDropDownOnUI("Fachabteilung", Parent, fachAbteilungen, _function, 150, 30, xPosOfInfoScreen + 75, yPosOfInfoScreen + heightOfFirstElement - (85 * 3.7f));

        // DROP DOWN for building number
        string[] optionsElements = { "A", "B", "C", "E", "F", "X" };
        roomDetailsView.BuildingSectionDropdown = fillPanel.CreateDropDownOnUI("Gebäudeteil", Parent, optionsElements, _function, 100, 30, xPosOfInfoScreen + 100, yPosOfInfoScreen + heightOfFirstElement - (85 * 6.5f));

        // cost
        roomDetailsView.CostCentreAssignmentInput = fillPanel.CreateInputFieldOnUIWithHandle("Kostenstelle", Parent, 290, 85, xPosOfInfoScreen, yPosOfInfoScreen + heightOfFirstElement - (85 * 4.5f));

        // size of room
        roomDetailsView.RoomSizeInput = fillPanel.CreateInputFieldOnUIWithHandle("Größe des Raums ( m² ):", Parent, 290, 30, xPosOfInfoScreen, yPosOfInfoScreen + heightOfFirstElement - (85 * 5.2f));

        // work places
        roomDetailsView.NumberOfWorkspacesInput = fillPanel.CreateInputFieldOnUIWithHandle("Arbeitsplätze", Parent, 290, 85, xPosOfInfoScreen, yPosOfInfoScreen + heightOfFirstElement - (85 * 5.8f));
        roomDetailsView.AccessControlledToggle = fillPanel.CreateToggleOnUIWithHandle("Zugriffskontrolle", Parent, 0, 0, 0, 0);

        roomDetailsView.WorkerButton = fillPanel.CreateButtonOnUIWithHandle("Zeige Mitarbeiter", Parent, 0, 0, 0, 0);
        roomDetailsView.InventarButton = fillPanel.CreateButtonOnUIWithHandle("Zeige Inventar", Parent, 0, 0, 0, 0);

        // entrance control
        string[] optionsElementZugangskontrolle = { "Ja", "Nein" };

        // Tags
        roomDetailsView.CommentsInput = fillPanel.CreateInputFieldOnUIWithHandle("Kommentare", Parent, 290, 85, xPosOfInfoScreen, yPosOfInfoScreen + heightOfFirstElement - (85 * 7.2f));  // Tags wie bei Funktionsbereich

        // Button
        roomDetailsView.SaveButton = fillPanel.CreateButtonOnUIWithHandle("Speichern", Parent, 150, 30, xPosOfInfoScreen + 75, yPosOfInfoScreen + heightOfFirstElement - (85 * 8.0f));
        roomDetailsView.CancelButton = fillPanel.CreateButtonOnUIWithHandle("Abbrechen", Parent, 150, 30, xPosOfInfoScreen - 75, yPosOfInfoScreen + heightOfFirstElement - (85 * 8.0f));


        InformatioScreenSet = true;
    }



    /// <summary>
    /// Creates Shield Screne
    /// </summary>
    /// <param name="i"></param>
    public void CreateSchildScreen(int i)
    {
        digitalSinageManager.ToggleDigitalSinageUI();
    }

    /// <summary>
    ///  Screen for analysis of the Rooms like Working Places etc.
    /// </summary>
    /// <param name="i"></param>
    public void CreateAnalyseScreen(int i)
    {

        float yPosOfInfoScreen = 0;
        float xPosOfInfoScreen = 300;

        float InfoScreenSize_Y = 850;


        fillPanel.ClearScreenGeography(UI_OrientationOnScreen.enumForOrientations.Right);

        Transform Parent = fillPanel.CreatePanel(290, 850, UI_OrientationOnScreen.enumForOrientations.Right, UI_Panel.enumForLayoutGroup.Vertical, true);

        Parent.name = "AnalyseScreen";

        fillPanel.CreateHeaderOnUI("Analyse", Parent, 300, 65, xPosOfInfoScreen, yPosOfInfoScreen + (InfoScreenSize_Y / 2) - 35);

        fillPanel.CreateButtonOnUI("APs nicht verteilt (alle)", Parent, _TriggerAnalyse, 150, 150, xPosOfInfoScreen, yPosOfInfoScreen - (150 * 4), 0);

        fillPanel.CreateButtonOnUI("APs nicht verteilt (mehrere)", Parent, _TriggerAnalyse, 150, 150, xPosOfInfoScreen, yPosOfInfoScreen - (150 * 4), 1);
        fillPanel.CreateButtonOnUI("APs verteilt", Parent, _TriggerAnalyse, 150, 150, xPosOfInfoScreen, yPosOfInfoScreen - (150 * 4), 2);
        fillPanel.CreateButtonOnUI("Alle Räume", Parent, _TriggerAnalyse, 150, 150, xPosOfInfoScreen, yPosOfInfoScreen - (150 * 4), 3);
        fillPanel.CreateButtonOnUI("Unmark", Parent, _TriggerAnalyse, 150, 150, xPosOfInfoScreen, yPosOfInfoScreen - (150 * 4), 4);

        uIController.roomSizeInputAnalyse = fillPanel.CreateTextElementOnUIWithHandle("Quadrat", Parent, 150, 150, xPosOfInfoScreen, yPosOfInfoScreen - (150 * 4));
        uIController.roomsInputAnalyse = fillPanel.CreateTextElementOnUIWithHandle("Räume", Parent, 150, 150, xPosOfInfoScreen, yPosOfInfoScreen - (150 * 4));
        uIController.workspacesInputAnalyse = fillPanel.CreateTextElementOnUIWithHandle("Aps", Parent, 150, 150, xPosOfInfoScreen, yPosOfInfoScreen - (150 * 4));
    }


    /// <summary>
    /// Screen for POWER BI
    /// </summary>
    /// <param name="i"></param>
    public void CreatePowerBIScreen(int i)
    {

        float yPosOfInfoScreen = 0;
        float xPosOfInfoScreen = 300;

        float InfoScreenSize_Y = 850;

        fillPanel.ClearScreenGeography(UI_OrientationOnScreen.enumForOrientations.Right);

        Transform Parent = fillPanel.CreatePanel(290, 850, UI_OrientationOnScreen.enumForOrientations.Right, UI_Panel.enumForLayoutGroup.Vertical, true);
        Parent.name = "PowerBI_Screen";

        fillPanel.CreateHeaderOnUI("Power BI", Parent, 300, 65, xPosOfInfoScreen, yPosOfInfoScreen + (InfoScreenSize_Y / 2) - 35);

    }


    /// <summary>
    /// Detail Search for Rooms
    /// </summary>
    /// <param name="i"></param>
    public void CreateDetailSucheScreen(int i)
    {


        float yPosOfInfoScreen = 0;
        float xPosOfInfoScreen = 300;

        float InfoScreenSize_Y = 850;

        fillPanel.ClearScreenGeography(UI_OrientationOnScreen.enumForOrientations.Right);

        Transform Parent = fillPanel.CreatePanel(290, 850, UI_OrientationOnScreen.enumForOrientations.Right, UI_Panel.enumForLayoutGroup.Vertical, true);
        Parent.name = "Detailsuche";

        fillPanel.CreateHeaderOnUI("DetailSuche", Parent, 300, 65, xPosOfInfoScreen, yPosOfInfoScreen + (InfoScreenSize_Y / 2) - 35);

        // Level Selection
        string[] optionsElements = { "98", "99", "00", "01", "02", "03" };
        fillPanel.CreateDropDownOnUI("Ebene", Parent, optionsElements, _function, 150, 30, xPosOfInfoScreen + 75, yPosOfInfoScreen + 0 - (85 * 3.7f));


        //======================== 5 Elemente in a horiz. line
        Transform allTags = fillPanel.CreatePanel(300, 50, Parent, UI_Panel.enumForLayoutGroup.Horizontal, false);
        // ====== These are the Toggles for ABCDEF
        fillPanel.CreateToggleSmallOnUIWithHandle("A", allTags, 1, 1, 1, 1);
        fillPanel.CreateToggleSmallOnUIWithHandle("B", allTags, 1, 1, 1, 1);
        fillPanel.CreateToggleSmallOnUIWithHandle("C", allTags, 1, 1, 1, 1);
        fillPanel.CreateToggleSmallOnUIWithHandle("E", allTags, 1, 1, 1, 1);
        fillPanel.CreateToggleSmallOnUIWithHandle("F", allTags, 1, 1, 1, 1);


        // Room Number
        fillPanel.CreateInputFieldOnUIWithHandle("Raumnummer", Parent, 290, 30, xPosOfInfoScreen, yPosOfInfoScreen + 0 - (85 * 5.2f)); // Text

        // Designation
        string[] fachabteilungElements = { "FachA", "FachB", "FachC" };
        fillPanel.CreateDropDownOnUI("Fachabteilung", Parent, fachabteilungElements, _function, 150, 30, xPosOfInfoScreen + 75, yPosOfInfoScreen + 0 - (85 * 3.7f));


        // Workplaces
        string[] arbeitsplätzeElements = { "0", "1", "2" };
        fillPanel.CreateDropDownOnUI("Arbeitsplätze", Parent, arbeitsplätzeElements, _function, 150, 30, xPosOfInfoScreen + 75, yPosOfInfoScreen + 0 - (85 * 3.7f));

        // costs
        fillPanel.CreateInputFieldOnUIWithHandle("Kostenstelle", Parent, 290, 30, xPosOfInfoScreen, yPosOfInfoScreen + 0 - (85 * 5.2f)); // Text

        // Search Tags =======================================================
        Transform allTagsUnten = fillPanel.CreatePanel(300, 50, Parent, UI_Panel.enumForLayoutGroup.Grid, false);

        // == Elements as a Grid
        string lang = "Blubla";
        fillPanel.CreateToggleSmallOnUIWithHandle(lang, allTagsUnten, 1, 1, 1, 1);
        fillPanel.CreateToggleSmallOnUIWithHandle(lang, allTagsUnten, 1, 1, 1, 1);
        fillPanel.CreateToggleSmallOnUIWithHandle(lang, allTagsUnten, 1, 1, 1, 1);
        fillPanel.CreateToggleSmallOnUIWithHandle(lang, allTagsUnten, 1, 1, 1, 1);
        fillPanel.CreateToggleSmallOnUIWithHandle(lang, allTagsUnten, 1, 1, 1, 1);

        // Search Button
        fillPanel.CreateButtonOnUI("Suchen!!!", Parent, _function);
    }
    #endregion

    #region Actions and Delegates
    //=================================================================================================================================
    //=================================================================================================================================
    public void LoadRundgang(int i)
    {
        SceneManager.LoadScene("1_Floor_Level");

    }

    //=================================================================================================================================
    //=================================================================================================================================
    public void LoadWMS(int i)
    {
        SceneManager.LoadScene("OP_WMS_DoorSign");

    }
    //=================================================================================================================================
    //=================================================================================================================================
    public void ChangeToRoom(int i)
    {
        changeSceneViewInOpeningScene.EnableFloorOverviewMode(i);
    }
    //=================================================================================================================================
    //=================================================================================================================================

    private void Function(int _value)
    {
        // TODO: EMPTY FUNCTION - DELETE THIS

    }

    public void LoadDeviceTracker(int i)
    {
        SceneManager.LoadScene("DisplayListOfDevices");
    }

    public void UnfoldAndCloseBuilding(int i)
    {
        // int I = is the parameter either for building up the house or spliting it.
        // botH: Camera is set to "normal" = 3D view.

        if (i == 0)
        {
            changeSceneViewInOpeningScene.buildBuilding(); // Baue Haus
            changeSceneViewInOpeningScene.CameraModes(1); // kamera auf Normal
        }
        else if (i == 1)
        {
            changeSceneViewInOpeningScene.buildBuilding(); // Baue Haus


            changeSceneViewInOpeningScene.splitBuilding(); // teile Haus
            changeSceneViewInOpeningScene.CameraModes(1); // Setze Kamera auf normal
        }
    }

    public void ChangeCameraMode(int i)
    {
        changeSceneViewInOpeningScene.CameraModes(i);
    }

    public void StartPowerBI(int i)
    {
        Canvas.SetActive(false);
        Canvas_WebPlayer.SetActive(true);
    }

    public void Triggeranalyse(int i)
    {
        if (i == 0)
            uIController.markEmptyAllWorkspaces();
        else if (i == 1)
            uIController.markEmptyOneOrAllWorkspaces();
        else if (i == 2)
            uIController.markCompleteWorkspaces();
        else if (i == 3)
            uIController.markAllAllocatedRooms();
        else if (i == 4)
            uIController.Reset();
    }
    #endregion
}
