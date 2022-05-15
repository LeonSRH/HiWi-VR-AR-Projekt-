using SmartHospital.Controller.ExplorerMode.Rooms;
using SmartHospital.Database.Request;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSignController : MonoBehaviour
{

    TemporaryData_CurrentClickedRoom temporaryData;
    NUI_DoorSign _theDoorSignBuilder;

    // Start is called before the first frame update
    void Start()
    {

        

        temporaryData = FindObjectOfType<TemporaryData_CurrentClickedRoom>();
        _theDoorSignBuilder = GetComponent<NUI_DoorSign>();

        UpdateValuesFromTemporaryData();


    }

    public void UpdateValuesFromTemporaryData()
    {

        // Add here temporary Data //// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        string _abteil = "A";
        string _nameOben = "Oben";
        string _nameUnten = "Unten";
        string _beschreibung = "Beschreibung";
        string _klinischeNummer = "666";
        string _KTGNummer = "KTG Nummer";
        string _ebene = "Ebene 66";

        _theDoorSignBuilder.FillDoorSignValues(_abteil, _nameOben, _nameUnten, _beschreibung, _klinischeNummer, _KTGNummer, _ebene);


    }

    

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Q))
        {
        
            UpdateValuesFromTemporaryData();

        }
    }


}
