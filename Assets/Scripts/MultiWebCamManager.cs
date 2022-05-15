using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Create WebCamPrefabs and assignes the webcamtexture to it
/// Created 04.03.2019
/// By Katharina Shits
/// </summary>
public class MultiWebCamManager : MonoBehaviour
{
    public GameObject webcamTexturePrefab;

    private string[] nameOfCams;

    private List<WebCamTexture> webcamTextures = new List<WebCamTexture>();

    public Texture m_MyTexture;

    private void Start()
    {
        int numOfCams = WebCamTexture.devices.Length;
        nameOfCams = new string[numOfCams];

        for (int i = 0; i < numOfCams; i++)
        {
            nameOfCams[i] = WebCamTexture.devices[i].name;
            GameObject go = Instantiate(webcamTexturePrefab,
                new Vector3((i * 6), 0, 0), Quaternion.identity) as GameObject;
            go.transform.parent = gameObject.transform;

            WebCamTexture webcamTexture = new WebCamTexture();

            webcamTexture.deviceName = nameOfCams[i];
            webcamTextures.Add(webcamTexture);

            go.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = webcamTextures[i];

            webcamTextures[i].Play();
        }

        
    }

    void Update()
    {
        //Press the space key to switch between Filter Modes
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Switch the Texture's Filter Mode
            m_MyTexture.filterMode = SwitchFilterModes();
            //Output the current Filter Mode to the console
            Debug.Log("Filter mode : " + m_MyTexture.filterMode);
        }
    }

    //Switch between Filter Modes when the user clicks the Button
    FilterMode SwitchFilterModes()
    {
        //Switch the Filter Mode of the Texture when user clicks the Button
        switch (m_MyTexture.filterMode)
        {
            //If the FilterMode is currently Bilinear, switch it to Point on the Button click
            case FilterMode.Bilinear:
                m_MyTexture.filterMode = FilterMode.Point;
                break;

            //If the FilterMode is currently Point, switch it to Trilinear on the Button click
            case FilterMode.Point:
                m_MyTexture.filterMode = FilterMode.Trilinear;
                break;

            //If the FilterMode is currently Trilinear, switch it to Bilinear on the Button click
            case FilterMode.Trilinear:
                m_MyTexture.filterMode = FilterMode.Bilinear;
                break;
        }
        //Return the new Texture FilterMode
        return m_MyTexture.filterMode;
    }
}
