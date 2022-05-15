using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;



public class SaveImageOnHDD : MonoBehaviour
{

    public RectTransform rectTransform;



    void SaveTextureToFile(/*Texture2D _texture, string _filenName*/)
    {


        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        //tex.ReadPixels(new rect(0,0,1,1),0, 0);
        tex.Apply();



        byte [] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

        Debug.Log(Application.dataPath + "/../SavedScreen.png");

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
            SaveTextureToFile();


    }
}
