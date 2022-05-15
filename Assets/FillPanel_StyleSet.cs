using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Author SDA August 2019
/// </summary>

public class FillPanel_StyleSet : MonoBehaviour
{

    /// <summary>
    /// Color Schemes are edited in unity editor in the public ColorSchemes Array.
    /// </summary>

    public int currentColorScheme_Index;
    public NUI_Color_Style[] colorSchemes;


    // 3 differnet skyboxes to simulate day and night.
    //==========================================
    public Material skyBox_day;
    public Material skyBox_evening;
    public Material skyBox_night;
    //============================================

    FillPanel fillPanel; // Reference for setting THE STYLE
   


    // Start is called before the first frame update
    void Awake()
    {


        fillPanel = FindObjectOfType<FillPanel>();


        NUI_Color_Style currentColorScheme = colorSchemes[currentColorScheme_Index];




        // Set Font
        fillPanel.SetFont(currentColorScheme);

        fillPanel.SetStyle_Button(currentColorScheme);
        fillPanel.SetStyle_Header(currentColorScheme);
        fillPanel.SetStyle_TextElement(currentColorScheme);
        fillPanel.SetStyle_Toggle(currentColorScheme);
        fillPanel.SetStyle_InputField(currentColorScheme);

        fillPanel.SetStyle_DropDown(currentColorScheme);


        /// RenderSettings.skybox = (Material)Resources.Load("Skybox_Mat");


        //        fillPanel.SetStyle_Button()

    }

    /// <summary>
    /// A fast Input for swapping the skyboc ( a bit dirty)
    /// </summary>
    void SwapSkybox()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            RenderSettings.skybox = skyBox_day;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            RenderSettings.skybox = skyBox_evening;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            RenderSettings.skybox = skyBox_night;
        }

    }



    void Update()
    {
        SwapSkybox();
    }
}



/// <summary>
/// NUI Color Style is a container for all Colors that are used in the UI. They are created via an Array - so we have many differnet styles that can be created.
/// As we want full control for the colors - we have a color for each element. For Designer: Just define redundant colors.
/// </summary>

[System.Serializable]
public class NUI_Color_Style
{

    public string nameOfStyle;

    [Header("Font")]
    public TMP_FontAsset _fontType_normal;
    public TMP_FontAsset _fontType_header;


    [Header("Text Element")]
    public Color _TextElement_Main;
    public Color _TextElement_Text;

    [Header("Button")]
    public Color _Button_Main;
    public Color _Button_Highlight;
    public Color _Button_Pressed;
    public Color _Button_Text;

    [Header("Header")]
    public Color _Header_Main;
    public Color _Header_Text;

    [Header("Toggle")]
    public Color _Toggle_Main;
    public Color _Toggle_Highlight;
    public Color _Toggle_Pressed;
    public Color _Toggle_Text;

    [Header("InputField")]
    public Color _InputField_Main;
    public Color _InputField_Text;

    [Header("DropDown")]
    public Color _DropDown_Main;
    public Color _DropDown_Text;


}
