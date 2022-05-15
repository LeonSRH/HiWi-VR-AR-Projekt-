using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NUI_DoorSign : MonoBehaviour
{


    public Sprite DoorSign_BG;
    public Sprite DigitalSinage_HeaderMask;


    public Sprite WC_Symbol;
    public Sprite RedCross_Symbol;
    public Sprite Wheelchair_Symbol;
    public Sprite WrappingTable_Symbol;


    public Color Color_A;
    public Color Color_B;
    public Color Color_C;
    public Color Color_E;
    public Color Color_F;



    FillPanel fillPanel;
    DigitalRoomSignView digitalRoomSignView;


    GameObject Type0;
    GameObject Type1;
    GameObject Type2;


    // Start is called before the first frame update
    void Start()
    {


        fillPanel =  GameObject.FindObjectOfType<FillPanel>();
        digitalRoomSignView = GameObject.FindObjectOfType<DigitalRoomSignView>();




        SetDoorSignActive(1); // Set Door sign active creates the one door and destroy the old ones.

        //System.Diagnostics.Process.Start("Heidelberger_Schloss_von_Gerrit_Berckheyde_167_BW");



    }

    // Update is called once per frame
    void Update()
    {
        
    }



    void DoorNumber(string _number, float _pos_X , Transform _root, float _size_y, float _pos_y, float _textSize)
    {
        TextMeshProUGUI TuerNummer = fillPanel.CreateTextElementOnUIWithHandle("000", _root, 200, _size_y, _pos_X, _pos_y);
        TuerNummer.fontSize = _textSize;
        digitalRoomSignView.Room_Clinical_Number = TuerNummer;
    }

    void RoomName(float _sizeX, float _sizeY, Transform _root, float _fontSize)
    {

        float posX = 50;

        digitalRoomSignView.Room_Name_up = fillPanel.CreateTextElementOnUIWithHandle("Bez_1", _root, 300, _sizeY, posX, 100);
        digitalRoomSignView.Room_Name_down = fillPanel.CreateTextElementOnUIWithHandle("Bez_2", _root, 300, _sizeY, posX, 40);

        float fontSize = _fontSize;

        digitalRoomSignView.Room_Name_up.alignment = TextAlignmentOptions.Left;

       digitalRoomSignView.Room_Name_up.fontSize = fontSize;
        digitalRoomSignView.Room_Name_down.fontSize = fontSize;

    }

    public enum DoorSignSymbol_Enum { WC, Wheelchair, WrappingTable, RedCross };

    public void CreateDoorSign_Image(Vector3 _pos, float _scale, DoorSignSymbol_Enum doorSignSymbol_Enum)
    {

        Transform Root = fillPanel.CreateParentObject(transform);
        Root.name = "DoorSign_Image_Parent";

        float HeaderPos_y = 180;

        float Size_y = 0;

        Image Background = fillPanel.CreateImageOnUI(DoorSign_BG, Root, 520, 520, 0, 0);
        // Background.color = Color.white;


        digitalRoomSignView.Room_Floor = fillPanel.CreateTextElementOnUIWithHandle("Ebene 99", Root, 250, Size_y, 0, HeaderPos_y);
        digitalRoomSignView.Room_Floor.fontSize = 40;



       // DoorNumber("000", 60, Root, Size_y, HeaderPos_y);

        RoomName(300, 50, Root,50);

        Transform Names = fillPanel.CreatePanel(300, 200, Root, UI_Panel.enumForLayoutGroup.Vertical, false);
        Names.localPosition = new Vector3(50, -110, 0);
        // Names.localPosition = _pos;


        float X_Size = 250;
        float Y_Size = 250;

        float localPos_X = 30;
        float localPos_Y = -70;

        switch (doorSignSymbol_Enum)
        {

            case DoorSignSymbol_Enum.RedCross:

                fillPanel.CreateImageOnUI(RedCross_Symbol, Root, X_Size, Y_Size, localPos_X, localPos_Y);

                break;

            case DoorSignSymbol_Enum.Wheelchair:

                fillPanel.CreateImageOnUI(Wheelchair_Symbol, Root, X_Size, Y_Size, localPos_X, localPos_Y);

                break;

            case DoorSignSymbol_Enum.WC:

                fillPanel.CreateImageOnUI(WC_Symbol, Root, X_Size, Y_Size, localPos_X, localPos_Y);

                break;

            case DoorSignSymbol_Enum.WrappingTable:

                fillPanel.CreateImageOnUI(WrappingTable_Symbol, Root, X_Size, Y_Size, localPos_X, localPos_Y);

                break;
         
            default:

                fillPanel.CreateImageOnUI(WC_Symbol, Root, X_Size, Y_Size, localPos_X, localPos_Y);

                break;
        }





        digitalRoomSignView.Room_Description = fillPanel.CreateTextElementOnUIWithHandle("Name1 Von und Dippel", Names, 100, Size_y, 0, 150);
        digitalRoomSignView.Room_Description.fontSize = 25;


        //  fillPanel.CreateTextElementOnUIWithHandle("Name2 sdsdsdsdsdsdsd", Names, 100, Size_y, 0, 150);
        // fillPanel.CreateTextElementOnUIWithHandle("Name3sdsdsdsdsdsdsdsdsdsdds", Names, 100, Size_y, 0, 150);

        Image colorElementUpLeft = fillPanel.CreateImageOnUI(DigitalSinage_HeaderMask, Root, 300, 150, -100, HeaderPos_y);
        //colorElementUpLeft.color = Color.green;

        digitalRoomSignView.Room_Building_Section_Image = colorElementUpLeft;



        TextMeshProUGUI Abteil = fillPanel.CreateTextElementOnUIWithHandle("A", Root, 50, Size_y, -220, HeaderPos_y);
        Abteil.fontSize = 70;

        digitalRoomSignView.Room_Building_Section = Abteil;

        float scale = _scale;
        Root.localScale = new Vector3(scale, scale, scale);
        Root.localPosition = _pos;



        digitalRoomSignView.Room_KTG_Number = fillPanel.CreateTextElementOnUIWithHandle("KTGNUMMA", Root, 250, Size_y, 0, -240);


        //TestAccess();

       // FillDoorSignValues("C", "Besprechungsraum", "Privat", "Sebastian de Andrade", "305", "626555", "Ebene 66");

        Type0 = Root.gameObject;


    }

    public void CreateDoorSign_BigNumber(Vector3 _pos, float _scale)
    {
        Transform Root = fillPanel.CreateParentObject(transform);
        Root.name = "DoorSign_BigNumber_Parent";

        float HeaderPos_y = 180;

        float Size_y = 0;

        Image Background = fillPanel.CreateImageOnUI(DoorSign_BG, Root, 520, 520, 0, 0);
        // Background.color = Color.white;


        digitalRoomSignView.Room_Floor = fillPanel.CreateTextElementOnUIWithHandle("Ebene 99", Root, 250, Size_y, 0, HeaderPos_y);
        digitalRoomSignView.Room_Floor.fontSize = 40;



        DoorNumber("000", 0, Root, Size_y, -90,100);

        RoomName(300, 50, Root, 30);

       /* Transform Names = fillPanel.CreatePanel(300, 200, Root, UI_Panel.enumForLayoutGroup.Vertical, false);
        Names.localPosition = new Vector3(50, -110, 0);



        digitalRoomSignView.Room_Description = fillPanel.CreateTextElementOnUIWithHandle("Name1 Von und Dippel", Names, 100, Size_y, 0, 150);
        digitalRoomSignView.Room_Description.fontSize = 25;*/


        //  fillPanel.CreateTextElementOnUIWithHandle("Name2 sdsdsdsdsdsdsd", Names, 100, Size_y, 0, 150);
        // fillPanel.CreateTextElementOnUIWithHandle("Name3sdsdsdsdsdsdsdsdsdsdds", Names, 100, Size_y, 0, 150);

        Image colorElementUpLeft = fillPanel.CreateImageOnUI(DigitalSinage_HeaderMask, Root, 300, 150, -100, HeaderPos_y);
        //colorElementUpLeft.color = Color.green;

        digitalRoomSignView.Room_Building_Section_Image = colorElementUpLeft;



        TextMeshProUGUI Abteil = fillPanel.CreateTextElementOnUIWithHandle("A", Root, 50, Size_y, -220, HeaderPos_y);
        Abteil.fontSize = 70;

        digitalRoomSignView.Room_Building_Section = Abteil;

        float scale = _scale;
        Root.localScale = new Vector3(scale, scale, scale);
        Root.localPosition = _pos;


        digitalRoomSignView.Room_KTG_Number = fillPanel.CreateTextElementOnUIWithHandle("KTGNUMMA", Root, 250, Size_y, 0, -240);


        //TestAccess();

        //FillDoorSignValues("C", "Besprechungsraum", "Privat", "Sebastian de Andrade", "305", "626555", "Ebene 66");



        Type1 = Root.gameObject;
    }

    public void CreateDoorSign(Vector3 _pos, float _scale)
    {
        Transform Root = fillPanel.CreateParentObject(transform);
        Root.name = "DoorSignParent";

        float HeaderPos_y = 180;

        float Size_y = 0;

        Image Background = fillPanel.CreateImageOnUI(DoorSign_BG, Root, 520, 520, 0, 0);
       // Background.color = Color.white;

        
        digitalRoomSignView.Room_Floor =  fillPanel.CreateTextElementOnUIWithHandle("Ebene 99", Root, 250, Size_y, 0, HeaderPos_y);
        digitalRoomSignView.Room_Floor.fontSize = 40;



        DoorNumber("000", 180,Root,Size_y,HeaderPos_y,60);

        RoomName(300, 50, Root,30);

        Transform Names = fillPanel.CreatePanel(300, 200, Root, UI_Panel.enumForLayoutGroup.Vertical, false);
        Names.localPosition = new Vector3(50, -110, 0);

        

        digitalRoomSignView.Room_Description =  fillPanel.CreateTextElementOnUIWithHandle("Name1 Von und Dippel", Names, 100, Size_y, 0, 150);
        digitalRoomSignView.Room_Description.fontSize = 25;


        //  fillPanel.CreateTextElementOnUIWithHandle("Name2 sdsdsdsdsdsdsd", Names, 100, Size_y, 0, 150);
        // fillPanel.CreateTextElementOnUIWithHandle("Name3sdsdsdsdsdsdsdsdsdsdds", Names, 100, Size_y, 0, 150);

        Image colorElementUpLeft = fillPanel.CreateImageOnUI(DigitalSinage_HeaderMask, Root, 300, 150, -100, HeaderPos_y);
        //colorElementUpLeft.color = Color.green;

        digitalRoomSignView.Room_Building_Section_Image = colorElementUpLeft;



        TextMeshProUGUI Abteil  = fillPanel.CreateTextElementOnUIWithHandle("A", Root, 50, Size_y, -220, HeaderPos_y);
        Abteil.fontSize = 70;

        digitalRoomSignView.Room_Building_Section = Abteil;

        float scale = _scale;
        Root.localScale = new Vector3(scale, scale, scale);
        Root.localPosition = _pos;


        digitalRoomSignView.Room_KTG_Number = fillPanel.CreateTextElementOnUIWithHandle("KTGNUMMA", Root, 250, Size_y, 0, -240);


        //TestAccess();

      //  FillDoorSignValues("C", "Besprechungsraum", "Privat", "Sebastian de Andrade", "305", "626555", "Ebene 66");


        Type2 = Root.gameObject;

    }



    void DestroyOtherDoorSign(GameObject _gameObj)
    {

        if (_gameObj != null)
        {
            Destroy(_gameObj.gameObject);
        }
    }



    public void SetDoorSignActive(int _type)
    {

        if (_type == 0)
        {

            CreateDoorSign_Image(new Vector3(0, 0, 0), 1.0f, DoorSignSymbol_Enum.Wheelchair);

            DestroyOtherDoorSign(Type1);
            DestroyOtherDoorSign(Type2);

        }
        else if (_type == 1)
        {

            CreateDoorSign_BigNumber(new Vector3(0, 0, 0), 1.0f);

            DestroyOtherDoorSign(Type0);
            DestroyOtherDoorSign(Type2);
            
        }
        else if (_type == 2)
        {

            CreateDoorSign(new Vector3(0, 0, 0), 1.0f);

            DestroyOtherDoorSign(Type0);
            DestroyOtherDoorSign(Type1);

        }


    }


    public void SetSection(string _section)
    {

        digitalRoomSignView.Room_Building_Section.text = _section;

        if (_section == "A")
        {

            digitalRoomSignView.Room_Building_Section_Image.color = Color_A;
        }
        if (_section == "B")
        {
            digitalRoomSignView.Room_Building_Section_Image.color = Color_B;

        }
        if (_section == "C")
        {
            digitalRoomSignView.Room_Building_Section_Image.color = Color_C;

        }
        if (_section == "E")
        {
            digitalRoomSignView.Room_Building_Section_Image.color = Color_E;

        }
        if (_section == "F")
        {

            digitalRoomSignView.Room_Building_Section_Image.color = Color_F;
        }

    }

    public void TestAccess()
    {
        digitalRoomSignView.Room_Name_up.text = "Diens";
        digitalRoomSignView.Room_Name_down.text = "Notfall Annahme";
        digitalRoomSignView.Room_Building_Section.text = "Z";
        // digitalRoomSignView.Room_Building_Section_Image.color = Color.clear;
        SetSection("B");
        digitalRoomSignView.Room_Description.text = "123456789123456789123456789\nHerr Abeli Fram Lokali 2\nWorker 2\nWorker 2\nWorker 2\nWorker 2";
    }



    public void FillDoorSignValues_Image(

        string _section,
        string _roomName_up,
        string _roomName_down,
        string _description,
        string _clinical_number,
        string _KTG_number,
        string _floor
        )
    {

        SetSection(_section);
        digitalRoomSignView.Room_Name_up.text = _roomName_up;
        digitalRoomSignView.Room_Name_down.text = _roomName_down;
        digitalRoomSignView.Room_Description.text = _description;
        digitalRoomSignView.Room_Clinical_Number.text = _clinical_number;
        digitalRoomSignView.Room_KTG_Number.text = _KTG_number;
        digitalRoomSignView.Room_Floor.text = _floor;


    }





    public void FillDoorSignValues(
        
        string _section,
        string _roomName_up,
        string _roomName_down,
        string _description,
        string _clinical_number,
        string _KTG_number,
        string _floor
        )
    {

        SetSection(_section);
        digitalRoomSignView.Room_Name_up.text = _roomName_up;
        digitalRoomSignView.Room_Name_down.text = _roomName_down;
        digitalRoomSignView.Room_Description.text = _description;
        digitalRoomSignView.Room_Clinical_Number.text = _clinical_number;
        digitalRoomSignView.Room_KTG_Number.text = _KTG_number;
        digitalRoomSignView.Room_Floor.text = _floor;


    }





}
