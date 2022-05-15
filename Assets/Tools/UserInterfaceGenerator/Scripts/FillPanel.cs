///=================================================================================================================================
///=================================================================================================================================
///=================================================================================================================================
///=================================================================================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



// FILLPANEL

///=================================================================================================================================
///=================================================================================================================================
/// 
/// Author: Sebastian de Andrade ( AKA Meister des Codes und Herr der Chiffren)
/// Date: 19.3.2019 ( Im Jahre des Herrn.. Amen)
/// 
/// This Class is for displaying the user interface on a Canvas. Each loaded scene displays its own UI und when reloading a new scene, the old UI should be destroyed.
/// 
/// HISTORY =================================
/// 22.3.19:    Added Comments and UI Display of Image + Writing of TODO List
/// 27.5.19:    Refactoring with #Regions
/// 24.6.19:    Set Styles in FillPanel for changing UI Colors and Style ( not finished)
/// 
/// TODO    =================================
/// Digital Sinage
/// Role Models
/// Array For Each Scene that logs all UI Elements ( For Destroying UI by unloading Component)
/// 
///=================================================================================================================================
///=================================================================================================================================


public class FillPanel : MonoBehaviour
{
    [Header("Orientations On Screen")]
    public GameObject PanelUp, PanelRight, PanelLeft, PanelBottom, PanelTopSmall;   // Change Position for different Canvases

    GameObject[] AllPanelAnchors;
    public List<GameObject> UI_AllTemporaryElements;    // For writing Object in History => Change This to List linked to Scene ( maybe Scriptable Object)

    [Header("The Panel")]
    public GameObject Panel;                        // Prefab for Panel


    [Header("All Creatable UI Elements")]
    public GameObject DropDown;                     // Prefab for DropDown UI
    public GameObject TextElement;                  // Prefab for TextElement UI
    public GameObject HeaderElement;                // Prefab for Header UI
    public GameObject ButtonElement;                // Prefab for Button UI
    public GameObject TextAndInput;                 // Prefab for Text And Input UI
    public GameObject Image;                        // Prefab for Image on UI
    public GameObject Toggle;                       // Prefab for Toggle on UI
    public GameObject Toggle_small;                 // Prefab for Toggle on UI

    UnityEvent debugCall = new UnityEvent();
    UnityAction action;


    DelegateContainer.DelegateCallInt _function;

    // Start is called before the first frame update
    void Start()
    {

        UI_AllTemporaryElements = new List<GameObject>();


        AllPanelAnchors = new GameObject[5];
        AllPanelAnchors[0] = PanelUp;
        AllPanelAnchors[1] = PanelRight;
        AllPanelAnchors[2] = PanelBottom;
        AllPanelAnchors[3] = PanelLeft;
        AllPanelAnchors[4] = PanelTopSmall;

        _function += OnValueChangeFunktion;

    }


    #region Style

    // An External Script that holds and manages all the colors for the ui Triggers these following functions in an Awake method. 

    public void SetStyle_Toggle_small()
    {

    }


    public void SetFont(NUI_Color_Style colorStyle)
    {

        TMP_FontAsset _normalFont = colorStyle._fontType_normal;
        TMP_FontAsset _HeaderFont = colorStyle._fontType_header;

        // HEADER
        TextMeshProUGUI textHeader = HeaderElement.GetComponentInChildren<TextMeshProUGUI>();
        textHeader.font = _HeaderFont;

        // NORMAL TEXT
        TextMeshProUGUI textNormal = TextElement.GetComponentInChildren<TextMeshProUGUI>();
        textNormal.font = _normalFont;


        // ADD ALL OTHER NOTMAL FONTS

    }

    public void SetStyle_Toggle(NUI_Color_Style colorStyle)
    {


        Image image = Toggle.GetComponent<Image>();
        TextMeshProUGUI text = Toggle.GetComponentInChildren<TextMeshProUGUI>();
        Toggle toggle = Toggle.GetComponentInChildren<Toggle>();

        image.color = colorStyle._Toggle_Main;
        text.color = colorStyle._Toggle_Text;




        ColorBlock colorBlock = ColorBlock.defaultColorBlock;

        colorBlock.normalColor = colorStyle._Toggle_Main;
        colorBlock.highlightedColor = colorStyle._Toggle_Highlight;
        colorBlock.pressedColor = colorStyle._Toggle_Pressed;

        toggle.colors = colorBlock;


    }

    public void SetStyle_InputField(NUI_Color_Style colorStyle)
    {
        
        Image image = TextAndInput.GetComponent<Image>();
        TextMeshProUGUI text = TextAndInput.GetComponentInChildren<TextMeshProUGUI>();
        TMP_InputField input = TextAndInput.GetComponentInChildren<TMP_InputField>();


        image.color = colorStyle._InputField_Main;
        text.color = colorStyle._InputField_Text;




    }

    public void SetStyle_DropDown(NUI_Color_Style colorStyle)
    {
        Image image = DropDown.GetComponent<Image>();
        TextMeshProUGUI text = DropDown.GetComponentInChildren<TextMeshProUGUI>();
        TMP_Dropdown dropDown = DropDown.GetComponent<TMP_Dropdown>();


        text.color = colorStyle._DropDown_Text;
        image.color = colorStyle._DropDown_Main;

        ColorBlock colorBlock = ColorBlock.defaultColorBlock;

        colorBlock.normalColor = colorStyle._DropDown_Main;
        colorBlock.highlightedColor = colorStyle._Button_Highlight;
        colorBlock.pressedColor = colorStyle._Button_Pressed;

        dropDown.colors = colorBlock;

    }


    public void SetStyle_TextElement(NUI_Color_Style colorStyle)
    {

        Image image = TextElement.GetComponent<Image>();
        TextMeshProUGUI text = TextElement.GetComponentInChildren<TextMeshProUGUI>();

        image.color = colorStyle._TextElement_Main;
        text.color = colorStyle._TextElement_Text;

    }



    public void SetStyle_Header(NUI_Color_Style colorStyle)
    {
        Image image = HeaderElement.GetComponent<Image>();
        TextMeshProUGUI text = HeaderElement.GetComponentInChildren<TextMeshProUGUI>();

        image.color = colorStyle._Header_Main;
        text.color = colorStyle._Header_Text;
    }

    public void SetStyle_Button(NUI_Color_Style colorStyle)
    {

        Button buttonElement = ButtonElement.GetComponentInChildren<Button>();
        TextMeshProUGUI text = ButtonElement.GetComponentInChildren<TextMeshProUGUI>();
        Image image = ButtonElement.GetComponent<Image>();

        image.color = colorStyle._Button_Main;


        ColorBlock colorBlock = ColorBlock.defaultColorBlock;

        colorBlock.normalColor = colorStyle._Button_Main;
        colorBlock.highlightedColor = colorStyle._Button_Highlight;
        colorBlock.pressedColor = colorStyle._Button_Pressed;

        buttonElement.colors = colorBlock;

        text.color = colorStyle._Button_Text;
    }

    #endregion




    void SetSceneLoading()
    {


    }
    public void SetSettingsForLayoutGrid(Transform _panel, float GridSize_X, float GridSize_Y)
    {

        // 1. Create a Panel ( function retunrns Transform of new panel)
        // 2. Call: SetSettingsForLayoutGrid (PanelTransform, Size_x, Size_Y)

        GridLayoutGroup grid = _panel.GetComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(GridSize_X, GridSize_Y);
    }

    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // TESTING MENU
    IEnumerator CreateMenu()
    {

        float _timer = 1.0f;



        Transform _newpanel = transform;//CreatePanel("left");

        yield return new WaitForSeconds(_timer);

        CreateHeaderOnUI("KrankenHaus", _newpanel);

        yield return new WaitForSeconds(_timer);

        CreateTextElementOnUI("Blablabla = 100", _newpanel);

        yield return new WaitForSeconds(_timer);

        CreateTextElementOnUI("Es geht noch mehr", _newpanel);

        yield return new WaitForSeconds(_timer);

        //CreateButtonOnUI("BUTTONS :D", _newpanel, action);

        yield return new WaitForSeconds(_timer);

        CreateInputFieldOnUI("Und Eingabemasken", _newpanel);

        yield return new WaitForSeconds(_timer);

        CreateTextElementOnUI("MEHR PANELS", _newpanel);

        yield return new WaitForSeconds(_timer);

        // _newpanel = CreatePanel("left");

        yield return new WaitForSeconds(_timer);

        CreateTextElementOnUI("Hier ist es", _newpanel);

    }
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // RANDOM VALUE
    string GetRandomValue()
    {

        // Returns a Random Value as a String

        string localString;


        float RandomValue = Random.Range(0.0f, 150.0f);

        localString = RandomValue.ToString();

        return localString;
    }
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // CREATE PANEL
    #region CreatePanel








    public Transform CreatePanel(float _X, float _Y, UI_OrientationOnScreen.enumForOrientations _OrientationOnScreen, UI_Panel.enumForLayoutGroup _PanelLayoutGroup, bool writeInTemp)
    {

        Transform parent;         // local copy;
        GameObject obj;            // local copy;

        // Switch case calls subfunction and adds an already defined Panel as Parent object
        switch (_OrientationOnScreen)
        {
            case UI_OrientationOnScreen.enumForOrientations.Left:

                parent = PanelLeft.transform;
                obj = PanelCreation_SubFunktion(_X, _Y, parent, _PanelLayoutGroup);

                break;

            case UI_OrientationOnScreen.enumForOrientations.Right:

                parent = PanelRight.transform;
                obj = PanelCreation_SubFunktion(_X, _Y, parent, _PanelLayoutGroup);

                break;

            case UI_OrientationOnScreen.enumForOrientations.Top:

                parent = PanelUp.transform;
                obj = PanelCreation_SubFunktion(_X, _Y, parent, _PanelLayoutGroup);

                break;

            case UI_OrientationOnScreen.enumForOrientations.Bottom:

                parent = PanelBottom.transform;
                obj = PanelCreation_SubFunktion(_X, _Y, parent, _PanelLayoutGroup);

                break;

            case UI_OrientationOnScreen.enumForOrientations.TopSmall:

                parent = PanelTopSmall.transform;
                obj = PanelCreation_SubFunktion(_X, _Y, parent, _PanelLayoutGroup);

                break;

            default:

                parent = PanelLeft.transform;
                obj = PanelCreation_SubFunktion(_X, _Y, parent, _PanelLayoutGroup);

                break;

        }

        obj.transform.SetParent(parent, false);  // Include it finally to the Hierarchy / Finale Hinzufügung zur Hierarchie damit es zum Kind-Objekt wird.


        // For saving it in a List to destroy the object later on. Maybe one Menu element shouldnt be destroyed, so we can igonre it.
        if (writeInTemp)
            UI_AllTemporaryElements.Add(obj); // Add obj to list, that later on every element can be destroyed.

        return obj.transform;
    }

    //=== Create Panel Without Switch Case - Set other Parent than the defined parents ( freestyle mode)
    public Transform CreatePanel(float _X, float _Y, Transform _Parent, UI_Panel.enumForLayoutGroup _PanelLayoutGroup, bool writeInTemp)
    {
        GameObject obj;            // local copy;

        obj = PanelCreation_SubFunktion(_X, _Y, _Parent, _PanelLayoutGroup);
        obj.transform.SetParent(_Parent, false);  // Include it finally to the Hierarchy / Finale Hinzufügung zur Hierarchie damit es zum Kind-Objekt wird.


        // For saving it in a List to destroy the object later on. Maybe one Menu element shouldnt be destroyed, so we can igonre it.
        if (writeInTemp)
            UI_AllTemporaryElements.Add(obj); // Add obj to list, that later on every element can be destroyed.

        return obj.transform;
    }







    #endregion
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // CREATE BUTTONS
    #region CreateButton




    // Create Button with FUNCTION
    public void CreateButtonOnUI(string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action)
    {

        // creates a Button on UI with a name and and a On click event for a delegate function
        // Add: changeable Button Parameters => CreateButtonOnUI ( string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action, >>FUNCTIONPARAMTER<<)

        GameObject Obj = Instantiate(ButtonElement, _parent.transform.position, _parent.transform.rotation);    // create Element
        Obj.transform.SetParent(_parent, false);                                                                //  Set Parent ( Panel)
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _buttonName;                                       //  Name
        SetTag_UI(Obj);

        Button button = Obj.GetComponentInChildren<Button>();                                                   // Button
        button.onClick.AddListener(

            delegate
            {
                _action(0); // >>FUNCTIONPARAMTER<< / like in CreateLoadButtonUI
            }

        );

    }


    // Create Button Overload with SIZE and POS
    public void CreateButtonOnUI(string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action, float _X, float _Y, float PosX, float PosY)
    {

        // creates a Button on UI with a name and and a On click event for a delegate function
        // Add: changeable Button Parameters => CreateButtonOnUI ( string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action, >>FUNCTIONPARAMTER<<)

        GameObject Obj = Instantiate(ButtonElement, _parent.transform.position, _parent.transform.rotation);    // create Element
        Obj.transform.SetParent(_parent, false);                                                                //  Set Parent ( Panel)
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _buttonName;                                       //  Name
        SetTag_UI(Obj);

        SetRectSize_SubFunction(_X, _Y, Obj, PosX, PosY);


        Button button = Obj.GetComponentInChildren<Button>();                                                   // Button
        button.onClick.AddListener(

            delegate
            {
                _action(0); // >>FUNCTIONPARAMTER<< / like in CreateLoadButtonUI
            }

        );

    }


    // Create Button Overload with PARAMETERS
    public void CreateButtonOnUI(string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action, float _X, float _Y, float PosX, float PosY, int _FunctionParameter)
    {

        // creates a Button on UI with a name and and a On click event for a delegate function
        // Add: changeable Button Parameters => CreateButtonOnUI ( string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action, >>FUNCTIONPARAMTER<<)

        GameObject Obj = Instantiate(ButtonElement, _parent.transform.position, _parent.transform.rotation);    // create Element
        Obj.transform.SetParent(_parent, false);                                                                //  Set Parent ( Panel)
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _buttonName;                                       //  Name
        SetTag_UI(Obj);

        SetRectSize_SubFunction(_X, _Y, Obj, PosX, PosY);


        Button button = Obj.GetComponentInChildren<Button>();                                                   // Button
        button.onClick.AddListener(

            delegate
            {
                _action(_FunctionParameter); // >>FUNCTIONPARAMTER<< / like in CreateLoadButtonUI
            }

        );
        // BUTTON.onClick.AddListener(() => YourFunction(YourParam));
    }


    // Create Button Overload WITHOUT ANY FUNCTION!

    public Button CreateButtonOnUIWithHandle(string _buttonName, Transform _parent, float _X, float _Y, float PosX, float PosY)
    {

        // creates a Button on UI with a name and and a On click event for a delegate function
        // Add: changeable Button Parameters => CreateButtonOnUI ( string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action, >>FUNCTIONPARAMTER<<)

        GameObject Obj = Instantiate(ButtonElement, _parent.transform.position, _parent.transform.rotation);    // create Element
        Obj.transform.SetParent(_parent, false);                                                                //  Set Parent ( Panel)
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _buttonName;                                       //  Name
        SetTag_UI(Obj);

        SetRectSize_SubFunction(_X, _Y, Obj, PosX, PosY);

        return Obj.GetComponentInChildren<Button>();

    }


    public Button CreateButtonOnUIWithHandle(string _buttonName, Transform _parent)
    {

        // creates a Button on UI with a name and and a On click event for a delegate function
        // Add: changeable Button Parameters => CreateButtonOnUI ( string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action, >>FUNCTIONPARAMTER<<)

        GameObject Obj = Instantiate(ButtonElement, _parent.transform.position, _parent.transform.rotation);    // create Element
        Obj.transform.SetParent(_parent, false);                                                                //  Set Parent ( Panel)
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _buttonName;                                       //  Name
        SetTag_UI(Obj);


        return Obj.GetComponentInChildren<Button>();

    }



    // Create Button Overload for changing Image!
    public Button CreateButtonOnUIWithHandle(string _buttonName, Transform _parent, float _X, float _Y, float PosX, float PosY, Sprite _sprite)
    {

        // creates a Button on UI with a name and and a On click event for a delegate function
        // Add: changeable Button Parameters => CreateButtonOnUI ( string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action, >>FUNCTIONPARAMTER<<)

        GameObject Obj = Instantiate(ButtonElement, _parent.transform.position, _parent.transform.rotation);    // create Element
        Obj.transform.SetParent(_parent, false);                                                                //  Set Parent ( Panel)
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _buttonName;                                       //  Name
        SetTag_UI(Obj);

        SetRectSize_SubFunction(_X, _Y, Obj, PosX, PosY);   // Call Subfunction and change Rect Size

        Button localButton = Obj.GetComponentInChildren<Button>(); // Get The Button
        localButton.GetComponent<Image>().sprite = _sprite;         // Change Sprite/Image of Button

        return localButton;
    }




    public Button CreateButtonOnUIWithHandle(string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action, float _X, float _Y, float PosX, float PosY)
    {

        // creates a Button on UI with a name and and a On click event for a delegate function
        // Add: changeable Button Parameters => CreateButtonOnUI ( string _buttonName, Transform _parent, DelegateContainer.DelegateCallInt _action, >>FUNCTIONPARAMTER<<)

        GameObject Obj = Instantiate(ButtonElement, _parent.transform.position, _parent.transform.rotation);    // create Element
        Obj.transform.SetParent(_parent, false);                                                                //  Set Parent ( Panel)
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _buttonName;                                       //  Name
        SetTag_UI(Obj);

        SetRectSize_SubFunction(_X, _Y, Obj, PosX, PosY);


        Button button = Obj.GetComponentInChildren<Button>();                                                   // Button
        button.onClick.AddListener(

            delegate
            {
                _action(0); // >>FUNCTIONPARAMTER<< / like in CreateLoadButtonUI
            }

        );

        return button;
        // BUTTON.onClick.AddListener(() => YourFunction(YourParam));
    }




    // CREATE LOAD BUTTON
    public void CreateLoadButtonOnUI(string _buttonName, Transform _parent, DelegateContainer.DelegateCallString _action, string _sceneToLoad)
    {

        // The Button Loads and Unloads new Scenes
        // ADD: Progressbar for Loading with red an green light if the scene is already loaded.

        GameObject Obj = Instantiate(ButtonElement, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _buttonName;
        SetTag_UI(Obj);

        Button button = Obj.GetComponentInChildren<Button>();
        button.onClick.AddListener(

            delegate
            {
                _action(_sceneToLoad); // String Parameter
            }

        );



    }

    #endregion
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================

    void TextClick()
    {
        Debug.Log("YEEEHAAAA");

    }

    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // CREATE HEADER
    #region CreateHeader
    public void CreateHeaderOnUI(string _headerName, Transform _parent)
    {
        // Create A Header

        GameObject Obj = Instantiate(HeaderElement, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        SetTag_UI(Obj);

        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _headerName;

    }

    // CREATE HEADER WITH SIZE

    public void CreateHeaderOnUI(string _headerName, Transform _parent, float _X, float _Y)
    {
        // Create A Header, overloaded with X and Y size

        GameObject Obj = Instantiate(HeaderElement, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _headerName;
        SetTag_UI(Obj);


        SetRectSize_SubFunction(_X, _Y, Obj);


    }

    // CREATE HEADER WITH SIZE And Pos


    public void CreateHeaderOnUI(string _headerName, Transform _parent, float _X, float _Y, float PosX, float PosY)
    {
        // Create A Header, overloaded with X and Y size

        GameObject Obj = Instantiate(HeaderElement, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _headerName;
        SetTag_UI(Obj);

        SetRectSize_SubFunction(_X, _Y, Obj, PosX, PosY);
        //rect.localPosition = new Vector2(PosX, PosY);


    }
    #endregion
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // CREATE DROPDOWN
    #region CreateDropDownMenus

    public TMP_Dropdown CreateDropDownOnUI(string _description, Transform _parent, string[] _optionsElements, DelegateContainer.DelegateCallInt _delegateCall)
    {


        // DropDown for UI with onValueChange Function

        //==================================================================================================
        // IMPORTANT: Delegate Function must be mirrored with OptionsElement Array
        // Your funciton loads with an in value 3 scene: 0 = Scene 1; 1 = Scene2; 2 = Scene3;
        //  Your String [] should have following elements = { Scene_1, Scene_2, Scene_3}
        // By clicking on the dropdown - OnValueChanged is called and calls the function with the int value
        //==================================================================================================

        //==================================================================================================
        // DE: Das Array für das DropDown Menü muss den Funktionumfang der delegierten Funktion wiederspiegeln.
        // Hierbei muss die Funktion basierend auf dem übergeben Parameter ( int des Dropdown) über ein Switch Case
        // Eine funktion durchführen - die bennenung der delegierten Funktion erfolgt durch das String Array.
        //==================================================================================================

        GameObject Obj = Instantiate(DropDown, _parent.transform.position, _parent.transform.rotation);             // Erschaffe Objekt;
        Obj.transform.SetParent(_parent, false);                                                                    // Setze Eltern Objekt;
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _description;                                          //Füge Description;

        SetTag_UI(Obj);

        // Hier wird das DropDown Feld befüllt.============================================================
        TMP_Dropdown dropDown = Obj.GetComponentInChildren<TMP_Dropdown>();

        // dropDown.ClearOptions();    //Diese Codezeile bereinigt es.



        // Befülle es mit mit String Array
        for (int i = 0; i < _optionsElements.Length; i++)
        {
            AddOptionToDropDown(dropDown, _optionsElements[i]);
        }



        dropDown.onValueChanged.AddListener(
        delegate
        {
            _delegateCall(dropDown.value);
            // _onValueChangeFunktion(dropDown.value);
            // OnValueChangeFunktion( dropDown.value);
        }
     );



        return dropDown;

    }



    public TMP_Dropdown CreateDropDownOnUI(string _description, Transform _parent, string[] _optionsElements)
    {


        // DropDown for UI with onValueChange Function

        //==================================================================================================
        // IMPORTANT: Delegate Function must be mirrored with OptionsElement Array
        // Your funciton loads with an in value 3 scene: 0 = Scene 1; 1 = Scene2; 2 = Scene3;
        //  Your String [] should have following elements = { Scene_1, Scene_2, Scene_3}
        // By clicking on the dropdown - OnValueChanged is called and calls the function with the int value
        //==================================================================================================

        //==================================================================================================
        // DE: Das Array für das DropDown Menü muss den Funktionumfang der delegierten Funktion wiederspiegeln.
        // Hierbei muss die Funktion basierend auf dem übergeben Parameter ( int des Dropdown) über ein Switch Case
        // Eine funktion durchführen - die bennenung der delegierten Funktion erfolgt durch das String Array.
        //==================================================================================================

        GameObject Obj = Instantiate(DropDown, _parent.transform.position, _parent.transform.rotation);             // Erschaffe Objekt;
        Obj.transform.SetParent(_parent, false);                                                                    // Setze Eltern Objekt;
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _description;                                          //Füge Description;
        SetTag_UI(Obj);


        // Hier wird das DropDown Feld befüllt.============================================================
        TMP_Dropdown dropDown = Obj.GetComponentInChildren<TMP_Dropdown>();

        // dropDown.ClearOptions();    //Diese Codezeile bereinigt es.



        // Befülle es mit mit String Array
        for (int i = 0; i < _optionsElements.Length; i++)
        {
            AddOptionToDropDown(dropDown, _optionsElements[i]);
        }


        return dropDown;

    }


    public TMP_Dropdown CreateDropDownOnUI(string _description, Transform _parent, string[] _optionsElements, DelegateContainer.DelegateCallInt _delegateCall, float X, float Y, float PosX, float PosY)
    {


        // DropDown for UI with onValueChange Function

        //==================================================================================================
        // IMPORTANT: Delegate Function must be mirrored with OptionsElement Array
        // Your funciton loads with an in value 3 scene: 0 = Scene 1; 1 = Scene2; 2 = Scene3;
        //  Your String [] should have following elements = { Scene_1, Scene_2, Scene_3}
        // By clicking on the dropdown - OnValueChanged is called and calls the function with the int value
        //==================================================================================================

        //==================================================================================================
        // DE: Das Array für das DropDown Menü muss den Funktionumfang der delegierten Funktion wiederspiegeln.
        // Hierbei muss die Funktion basierend auf dem übergeben Parameter ( int des Dropdown) über ein Switch Case
        // Eine funktion durchführen - die bennenung der delegierten Funktion erfolgt durch das String Array.
        //==================================================================================================

        GameObject Obj = Instantiate(DropDown, _parent.transform.position, _parent.transform.rotation);             // Erschaffe Objekt;

        TMP_Dropdown dropDown = Obj.GetComponentInChildren<TMP_Dropdown>();
        dropDown.ClearOptions();    //Diese Codezeile bereinigt es.


        Obj.transform.SetParent(_parent, false);                                                                    // Setze Eltern Objekt;
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _description;                                          //Füge Description;
        SetRectSize_SubFunction(X, Y, Obj, PosX, PosY);
        SetTag_UI(Obj);


        // Hier wird das DropDown Feld befüllt.============================================================



        // Befülle es mit mit String Array
        for (int i = 0; i < _optionsElements.Length; i++)
        {
            AddOptionToDropDown(dropDown, _optionsElements[i]);
        }



        dropDown.onValueChanged.AddListener(
        delegate
        {
            _delegateCall(dropDown.value);
            // _onValueChangeFunktion(dropDown.value);
            // OnValueChangeFunktion( dropDown.value);
        }
     );



        return dropDown;

    }

    #endregion
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // STRING ARRAY TO DROP DOWN
    void AddOptionToDropDown(TMP_Dropdown _tMP_Dropdown, string _optionName)
    {
        // Adds a string array to a drop down menu.
        // These element should mirror the function that is called in drop down menu

        /// <summary>
        /// Add an option to the dropdown menu as a string.
        /// </summary>
        /// <param name="_tMP_Dropdown"></param>
        /// <param name="_optionName"></param>

        TMP_Dropdown.OptionData list = new TMP_Dropdown.OptionData(_optionName);
        _tMP_Dropdown.options.Add(list);
    }
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // TEST FUNCTION

    void OnValueChangeFunktion(int _nr)
    {
        Debug.Log("Lese Array Aus_ " + "" + _nr);
    }

    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // CREATE IMAGE ON CANVAS
    #region Create Image



    public void CreateImageOnUI(Sprite _sprite, Transform _parent)
    {
        GameObject Obj = Instantiate(Image, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponent<Image>().sprite = _sprite;
        SetTag_UI(Obj);

    }

    public Image CreateImageOnUI(Sprite _sprite, Transform _parent, float X, float Y, float PosX, float PosY)
    {
        /// <summary>
        /// Create a Image on Usr Interface
        /// </summary>

        GameObject Obj = Instantiate(Image, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponent<Image>().sprite = _sprite;
        SetRectSize_SubFunction(X, Y, Obj, PosX, PosY);
        SetTag_UI(Obj);


        return Obj.GetComponentInChildren<Image>();

    }


    #endregion
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // CREATES INPUT FIELD
    #region CreateInputField

    public void CreateInputFieldOnUI(string _description, Transform _parent)
    {
        GameObject Obj = Instantiate(TextAndInput, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        SetTag_UI(Obj);


        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _description;



    }


    public void CreateInputFieldOnUI(string _description, Transform _parent, float _X, float _Y, float PosX, float PosY)
    {
        GameObject Obj = Instantiate(TextAndInput, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _description;

        SetTag_UI(Obj);


        SetRectSize_SubFunction(_X, _Y, Obj, PosX, PosY);


    }

    // Wichtig: Hier wird ein Handle entnommen, damit später auf die Funktion zugegriffen werden kann und auf dem Input Field Daten dargestellt werden können.
    public TMP_InputField CreateInputFieldOnUIWithHandle(string _description, Transform _parent, float _X, float _Y, float PosX, float PosY)
    {

        GameObject Obj = Instantiate(TextAndInput, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _description;


        SetTag_UI(Obj);


        TMP_InputField inputfield = Obj.GetComponentInChildren<TMP_InputField>();

        SetRectSize_SubFunction(_X, _Y, Obj, PosX, PosY);


        return inputfield;
    }

    public TMP_InputField CreateInputFieldOnUIWithHandle(string _description, Transform _parent)
    {

        GameObject Obj = Instantiate(TextAndInput, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _description;

        SetTag_UI(Obj);


        TMP_InputField inputfield = Obj.GetComponentInChildren<TMP_InputField>();


        return inputfield;
    }

    #endregion
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // CREATES TOGGLE ON UI    
    #region CreateToggle

    public Toggle CreateToggleOnUIWithHandle(string _description, Transform _parent, float _X, float _Y, float PosX, float PosY)
    {

        GameObject Obj = Instantiate(Toggle, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _description;

    
        SetTag_UI(Obj);

        Toggle toggle = Obj.GetComponentInChildren<Toggle>();

        SetRectSize_SubFunction(_X, _Y, Obj, PosX, PosY);


        return toggle;
    }


    public Toggle CreateToggleSmallOnUIWithHandle(string _description, Transform _parent, float _X, float _Y, float PosX, float PosY)
    {

        GameObject Obj = Instantiate(Toggle_small, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _description;

        SetTag_UI(Obj);


        Toggle toggle = Obj.GetComponentInChildren<Toggle>();

        SetRectSize_SubFunction(_X, _Y, Obj, PosX, PosY);


        return toggle;
    }

    #endregion
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // CREATES EMPTY PARENT OBJECT    
    #region CreateParentObject

    public Transform CreateParentObject(Transform _parent)
    {
        GameObject Obj = new GameObject("Parent", typeof(RectTransform));
        Obj.transform.SetParent(_parent, false);

        return Obj.transform;
    }

    #endregion
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // CREATES TEXT ELEMENT ON UI    
    #region CreateTextElement

    public void CreateTextElementOnUI(string _text, Transform _parent)
    {
        /// <summary>
        /// Nur ein TextElement auf dem Bildschirme.
        /// </summary>
        /// 
        GameObject Obj = Instantiate(TextElement, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _text;
        SetTag_UI(Obj);

    }

    // CREATES TEXT ELEMENT ON UI WITH SIZE  
    public void CreateTextElementOnUI(string _text, Transform _parent, float X, float Y)
    {
        /// <summary>
        /// Nur ein TextElement auf dem Bildschirme.
        /// </summary>

        GameObject Obj = Instantiate(TextElement, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _text;
        SetTag_UI(Obj);

        SetRectSize_SubFunction(X, Y, Obj);

    }

    // CREATES TEXT ELEMENT ON UI WITH SIZE AND POSITION  
    public void CreateTextElementOnUI(string _text, Transform _parent, float X, float Y, float PosX, float PosY)
    {
        /// <summary>
        /// Nur ein TextElement auf dem Bildschirme.
        /// </summary>

        GameObject Obj = Instantiate(TextElement, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        Obj.GetComponentInChildren<TextMeshProUGUI>().text = _text;
        SetTag_UI(Obj);


        SetRectSize_SubFunction(X, Y, Obj, PosX, PosY);

    }


    // CREATES TEXT ELEMENT ON UI WITH SIZE AND POSITION  
    public TextMeshProUGUI CreateTextElementOnUIWithHandle(string _text, Transform _parent, float X, float Y, float PosX, float PosY)
    {
        /// <summary>
        /// Nur ein TextElement auf dem Bildschirme.
        /// </summary>

        GameObject Obj = Instantiate(TextElement, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        SetTag_UI(Obj);


        TextMeshProUGUI localCopy = Obj.GetComponentInChildren<TextMeshProUGUI>();

        localCopy.text = _text;

        SetRectSize_SubFunction(X, Y, Obj, PosX, PosY);

        return localCopy;


    }

    // CREATES TEXT ELEMENT ON UI WITH SIZE AND POSITION  
    public TextMeshProUGUI CreateTextElementOnUIWithHandle(string _text, Transform _parent)
    {
        /// <summary>
        /// Nur ein TextElement auf dem Bildschirme.
        /// </summary>

        GameObject Obj = Instantiate(TextElement, _parent.transform.position, _parent.transform.rotation);
        Obj.transform.SetParent(_parent, false);
        SetTag_UI(Obj);


        TextMeshProUGUI localCopy = Obj.GetComponentInChildren<TextMeshProUGUI>();

        localCopy.text = _text;


        return localCopy;


    }

    #endregion
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // CLEAR USER INTERFACE
    #region ClearUserInterface
    public void ClearUserInterface()
    {

        //==================================================================================
        // destroys every element listed in UI_AllTemporaryElements
        // This should be called, when a external scene is unloaded, to destroy the buttons
        // and UI Elements that have been created by the external scene
        //==================================================================================

        //==================================================================================
        // NEXT STEP: Every external Scene needs a list of its own created buttons. The destruction of the elements should
        //==================================================================================

        foreach (GameObject obj in UI_AllTemporaryElements)
        {
            Destroy(obj);

        }

        Debug.Log("ClearTheHellOut");

        UI_AllTemporaryElements.Clear();

    }
    Transform[] GetChildsOfObject(Transform _Parent)
    {
        // GET all transforms of the child objects

        int Childs = _Parent.transform.childCount;

        Transform[] allChilds = new Transform[Childs];


        for (int i = 0; i < Childs; i++)
        {
            allChilds[i] = _Parent.transform.GetChild(i);
        }

        return allChilds;
    }
    public void ClearScreenGeography(UI_OrientationOnScreen.enumForOrientations _OrientationOnScreen)
    {
        Transform[] allChilds = new Transform[1];



        switch (_OrientationOnScreen)
        {
            case UI_OrientationOnScreen.enumForOrientations.Left:

                allChilds = GetChildsOfObject(PanelLeft.transform);

                break;

            case UI_OrientationOnScreen.enumForOrientations.Right:

                //allChilds = PanelRight.GetComponentsInChildren<GameObject>();
                allChilds = GetChildsOfObject(PanelRight.transform);

                break;

            case UI_OrientationOnScreen.enumForOrientations.Top:


                allChilds = GetChildsOfObject(PanelUp.transform);

                //allChilds = PanelUp.GetComponentsInChildren<GameObject>();



                break;

            case UI_OrientationOnScreen.enumForOrientations.Bottom:



                allChilds = GetChildsOfObject(PanelBottom.transform);

                //allChilds = PanelBottom.GetComponentsInChildren<GameObject>();

                break;

            case UI_OrientationOnScreen.enumForOrientations.TopSmall:


                allChilds = GetChildsOfObject(PanelTopSmall.transform);

                //allChilds = PanelTopSmall.GetComponentsInChildren<GameObject>();

                break;

            default:

                allChilds = GetChildsOfObject(PanelLeft.transform);

                //allChilds = PanelTopSmall.GetComponentsInChildren<GameObject>();

                break;

        }



        for (int i = 0; i < allChilds.Length; i++)
        {

            allChilds[i].gameObject.SetActive(false);

        }


    }
    #endregion
    //=======================================================================================================================================================================
    //=======================================================================================================================================================================
    // SUBFUNCTIONS ( They are called by main functions linke CreateButton() )
    #region Subfunctions
    /// [eng]   Subfunktions are lower than normal functions. They are do very simple single tasks
    /// [ger]   Subfunktionen sind niedriger gestellt als standard funktionen. Sie führen einzig siimple instruktionen aus
    ///         und werden somit meist in Rahmen einen größeren Funktion gerufen.

    // SET THE SIZE OF A RECT ON UI
    public void SetRectSize_SubFunction(float _X, float _Y, GameObject _Obj)
    {
        RectTransform rect = _Obj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(_X, _Y);

    }

    public void SetRectSize_SubFunction(float _X, float _Y, GameObject _Obj, float PosX, float PosY)
    {
        RectTransform rect = _Obj.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(_X, _Y);
        rect.localPosition = new Vector2(PosX, PosY);
    }



    // THE CREATION OF THE PANEL
    GameObject PanelCreation_SubFunktion(float _X, float _Y, Transform _Parent, UI_Panel.enumForLayoutGroup _PanelLayoutGroup)
    {

        GameObject obj = Instantiate(Panel, _Parent.position, _Parent.rotation);        // Create Obj
        SetRectSize_SubFunction(_X, _Y, obj);                                           // Set Size of Rectangle

        obj = AddLayoutGroup_SubFunktion(_PanelLayoutGroup, obj);                       // Add The LayoutGroup


        return obj;

    }

    // CLEAN OBJ FROM LAYOUTGROUPS
    GameObject DestroyEveryLayoutgroup(GameObject _Obj)
    {

        /// Destroy Immediate is important - otherwise the destruction of component will not be registered in time and
        /// following code lines cant create another component.


        if (_Obj.HasComponent<HorizontalLayoutGroup>() == true)
        {
            DestroyImmediate(_Obj.GetComponent<HorizontalLayoutGroup>());

            Debug.Log("Horz Destroyed");

        }

        if (_Obj.HasComponent<VerticalLayoutGroup>() == true)
        {
            DestroyImmediate(_Obj.GetComponent<VerticalLayoutGroup>());

            Debug.Log("Vertical Destroyed");
        }

        if (_Obj.HasComponent<GridLayoutGroup>() == true)
        {
            DestroyImmediate(_Obj.GetComponent<GridLayoutGroup>());
            Debug.Log("Grid Destroyed");
        }


        return _Obj;


    }

    // ADD LAYOUTGROUP TO OBJECT
    GameObject AddLayoutGroup_SubFunktion(UI_Panel.enumForLayoutGroup _PanelLayoutGroup, GameObject _Obj)
    {

        switch (_PanelLayoutGroup)
        {
            case UI_Panel.enumForLayoutGroup.Horizontal:
                _Obj.AddComponent<HorizontalLayoutGroup>();
                break;

            case UI_Panel.enumForLayoutGroup.Vertical:
                _Obj.AddComponent<VerticalLayoutGroup>();
                break;

            case UI_Panel.enumForLayoutGroup.Grid:
                _Obj.AddComponent<GridLayoutGroup>();
                break;

            default:

                _Obj.AddComponent<VerticalLayoutGroup>();
                break;
        }


        return _Obj;

    }

    #endregion



    public void  SetTag_UI(GameObject _obj)
    {
        _obj.transform.tag = "UI";

    }
}

#region ExtraClasses

// DELEGATE CONTAINER FOR ALL POSSIBLE DELEGATE METHODS
public class DelegateContainer
{

    /// <summary>
    /// [eng]   Delegate Container class contains every possible Deligate datatype. They are used for the generic creation process of the user interface.
    ///         As a parameter must be declarated, we want to order everything about delegate centralized in this class.
    ///         Otherwise everything would grow - scattered by the wind - with zero control - chaos - death.
    /// [ger]   Die Delegate Container Klasse wird alle Deligate Datentypen inne haben, die im generischen Erstellen des User Interfaces benötigt werden.
    ///         Im Delegate muss ebenfalls der mögliche Parameter angegeben werden, daher wird das über eine zentrale Klasse definiert, damit unsere Delegate Typen 
    ///         in verschiedenen Ausführungen nicht wie Unkraut wachsen.
    /// </summary>

    public delegate void DelegateCallInt(int _value); // This defines what type of method you're going to call.
    public DelegateCallInt delegateCall;
    //===========================================================
    public delegate void DelegateCallString(string _name);
    public DelegateCallString delegateCallString;
}

// ROLE MODELS FOR USERS ( TODO)
public class UI_RollenModell
{
    /// <summary>
    /// [eng]   The User RoleModell should be given as a parameter to Ui elements for security reasons.
    /// [ger]    Das RollenModell sollte an das UI Element übergeben werden, damit geprüft werden kann, ob der betrachter es sehen kann.
    /// </summary>
}

// ADD ELEMENT ON ORIENTATION ( RIGHT, LEFT, UP, DOWN)s
public class UI_OrientationOnScreen
{



    /// <summary>
    /// [eng]   Orientation on screen contains just a central organized enum for all possible anchor points on the screen.
    /// [ger]   Die Klasse zentralisiert enums für die Andockstationen des Betrachter Maske - diese können in beliebiger und feingranularer Weise erweitert werden.
    /// </summary>

    public enum enumForOrientations { Left, Right, Top, Bottom, TopSmall };
    public enumForOrientations orientationsOnScreen;
}

// DEFINE PANEL TYPE => GRID RECOMMENDED
public class UI_Panel
{
    /// <summary>
    /// [eng] UI Panel defines Layoutgroup for panel
    /// [ger] Definier die Darstellungsordnung ( Horizontal, Vertikal oder im Gitter)
    /// </summary>
    /// 
    public enum enumForLayoutGroup { Horizontal, Vertical, Grid };
    public enumForLayoutGroup layoutGroup;
}

// CHECK IF THERE IS A COMPONENT ATTACHED
public static class hasComponent
{
    public static bool HasComponent<T>(this GameObject flag) where T : Component
    {
        return flag.GetComponent<T>() != null;
    }
}

#endregion
