using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;


/// <summary>
/// Author: SDA
/// DATE: 4.10.2019
/// Save and Load strings
/// 
/// 
/// 
/// History:
/// 
/// 1) Save and Load works (WORKS 4.10.2019)
/// 2) Seperate DATA from FUNCTION (TODO)
/// 
/// </summary>

public class SaveAndLoadFromFile : MonoBehaviour
{
    ///===================================================================================================================
    ///Save And Load
    ///Save a text file and read a string.
    ///====================================================================================================================
    

    [Header("Data Path - with @")] // @ sign lets save a string as a raw string. => /n /b etc.
    public string path = @"D:\MyTest.txt";

    [Header("The Objects that shall be  saved")]
    public GameObject[] gameObjectsToSave;

    /// <summary>
    /// Add text to a filestream byte by byte
    /// </summary>
    /// <param name="fs"></param>
    /// <param name="value"></param>
    private static void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        /*================================================================================================= DEBUG (DER GEILSTE DEBUG ÜBERHAUPT)
        string localBytes = "";
        for (int i = 0; i < info.Length; i++)
        {// localBytes += $"{info[i].ToString("x4")}|"; // HEX mit 4 Zahlen
            localBytes += $"{Convert.ToString(info[i], 2).PadLeft(8, '0')}|";} // Binär 
        Debug.Log("Bytes: " + localBytes);
        //==================================================================================================*/
        fs.Write(info, 0, info.Length);
    }

    /// <summary>
    /// Returns the local file to a string.
    /// </summary>
    /// <param name="_path"></param>
    public string LoadEntitiesFromTXT(string _path)
    {
        string localCopy = "";

        //Open the stream and read it back.
        using (FileStream fs = File.OpenRead(_path))
        {
            byte[] b = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding(true);

            while (fs.Read(b, 0, b.Length) > 0)
            {
                localCopy = temp.GetString(b);
                Debug.Log("Value: " + temp.GetString(b));
            }
          
        }
        return localCopy;
    }

    /// <summary>
    /// Deletes an existing file
    /// </summary>
    /// <param name="_path"></param>
    public void DeleteFileIfExist(string _path)
    {
        if (File.Exists(_path))
        {
            File.Delete(_path);
        }
    }


    /// <summary>
    /// Save GameObjects to the file
    /// </summary>
    /// <param name="allEntitiesToSave"></param>
    /// <param name="_path"></param>
    public void SaveEntitiesToTXT(GameObject[] allEntitiesToSave, string _path)
    {

        //================================= DELETE THE OLD FILE AND SAVE A NEW ONE
        DeleteFileIfExist(_path);



        using (FileStream fs = File.Create(_path))
        {
            //==HEADER=======================================================================================================
           /* AddText(fs, "Hospital\r\n--------------");
            AddText(fs, "\r\nCreated: " + System.DateTime.Today.ToString());
            AddText(fs, "\r\nAuthor: " + "Username");
            AddText(fs, "\r\n--------------");
            AddText(fs, "\r\nHierarchy:[Earth][Europe][Germany][BW][Heidelberg][Phantasieklinik]");
            AddText(fs, "\r\n--------------");*/
            //===============================================================================================================


            for (int i = 0; i < allEntitiesToSave.Length; i++)
            {
                //=====================================================
                //=====================================================
                // LOCAL COPY
                GameObject localCopy = allEntitiesToSave[i];            // copy the element
                //=====================================================
                //=====================================================
                // VECTOR 3
                float _XPos = localCopy.transform.position.x;
                float _YPos = localCopy.transform.position.y;
                float _ZPos = localCopy.transform.position.z;
                //=====================================================
                //=====================================================
                //=====================================================
                // MATERIAL
                string _material = "materialType";
                //=====================================================
                //=====================================================
                //=====================================================
                // DEFINE STRING SPLIT ELEMENT
                string split = @"%";
                string splitElement = @"?";
                //=====================================================
                //=====================================================
                //=====================================================
                //DATA ROW OF ELEMENT
                /*AddText(fs, "\r\n" + "[" + i.ToString() + "]" + split + allEntitiesToSave[i].name + split +
                            "Pos(" + _XPos.ToString() + "/" +
                                        _YPos.ToString() + "/" +
                                        _ZPos.ToString() + ")" + split +
                                        _material.ToString() + split + "ADD HERE MORE DATA"
                  
                 );*/

                AddText(fs, _XPos.ToString() + split + _YPos.ToString() + split + _ZPos.ToString()+ "1" + splitElement);
                //=====================================================
                //=====================================================
                //=====================================================

            }
        }
    }


    // Use this for initialization
    void Start()
    {
        SaveEntitiesToTXT(gameObjectsToSave, @"D:\MyTest.txt");     // Create a Save State
        LoadEntitiesFromTXT(@"D:\MyTest.txt");                      // Create a Load State and get it back as a string
    }
}
