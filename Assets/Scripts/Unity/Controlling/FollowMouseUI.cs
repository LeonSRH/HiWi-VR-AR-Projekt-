using System;
using TMPro;
using UnityEngine;

public class FollowMouseUI : MonoBehaviour
{
    public Canvas parentCanvas;

    // Main Camera for the conversion between mouse position and ui position
    public Camera mainCamera;

    [Header("TextFields for the data")]
    public TextMeshProUGUI RoomNameTextField;

    public TextMeshProUGUI DesignationTextField;

    public TextMeshProUGUI ApTextField;

    public TextMeshProUGUI SizeTextField;
    
    public Transform InfoPanel;


    public void Start()
    {
        Vector2 pos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, Input.mousePosition,
            parentCanvas.worldCamera,
            out pos);
    }

    public void enableInfoPanel()
    {

        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition, parentCanvas.worldCamera,
            out movePos);

        transform.position = parentCanvas.transform.TransformPoint(movePos);
    }
}
