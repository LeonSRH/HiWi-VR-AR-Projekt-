using SmartHospital.Controller.ExplorerMode.Rooms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class RoomparameterView : MonoBehaviour
{

    double Length { get; set; }
    double Height { get; set; }
    double Size { get; set; }

    public double Width
    {
        get
        {
            //_width = RoomNameInput.text;
            //return _width;
            return 0d;
        }
        set
        {
            //_width = value;
            //WidthInput.text = value;
        }
    }

}


