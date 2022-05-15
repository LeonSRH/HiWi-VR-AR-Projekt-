using UnityEngine;
/// <summary>
/// Defines a created UI Object of <see cref="FillPanel"/> struct.
/// </summary>
public class NUI_Object
{
    public string name { get; set; }

    public enum UIType
    {
        BUTTON, PANEL, IMAGE, TEXT, INPUTFIELD, NONE
    }

    public UIType Type { get; set; }

    //TODO: Change to RectTransform
    public Transform RectTransform { get; set; }

    public GameObject Parent { get; set; }

    public NUI_Object(string name, UIType type, Transform rectTransform, GameObject parent)
    {
        Parent = parent;
        this.name = name;
        Type = type;
        RectTransform = rectTransform;
    }
}
