using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlan : MonoBehaviour
{
    int currentLevel = 00;

    bool displayImage = false;
    public Image image;

    public Camera cam;

    public Sprite Plan_98;
    public Sprite Plan_99;
    public Sprite Plan_00;
    public Sprite Plan_01;
    public Sprite Plan_02;
    public Sprite Plan_03;


    Vector3 OldPos;
    Vector3 OldScale;

    // Use this for initialization
    void Start()
    {
        image.enabled = false;
        OldPos = transform.position;
        OldScale = transform.localScale;

        //SetDisplayImage();

    }

    // Update is called once per frame
    void Update()
    {

        if (displayImage)
        {
            if (Input.GetMouseButton(0))
            {
                transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);


                Vector3 screenPoint = Input.mousePosition;

                // transform.position = screenPoint * -1.0f;
                transform.position = screenPoint;

            }

            if (Input.GetMouseButtonUp(1))
            {
                //Vector3 screenPoint = Input.mousePosition;
                transform.position = OldPos;

                transform.localScale = OldScale;

            }
        }


    }



    public void ChangePicture()
    {

        if (currentLevel == 98)
        {
            image.sprite = Plan_98;
        }
        if (currentLevel == 99)
        {
            image.sprite = Plan_99;
        }
        if (currentLevel == 0)
        {
            image.sprite = Plan_00;
        }
        if (currentLevel == 1)
        {
            image.sprite = Plan_01;
        }
        if (currentLevel == 2)
        {
            image.sprite = Plan_02;
        }
        if (currentLevel == 3)
        {
            image.sprite = Plan_03;
        }


    }


    public void SetDisplayImage()
    {
        displayImage = !displayImage;

        image.enabled = displayImage;

    }

    public void SetDisplayImageToFalse()
    {

        image.enabled = false;

        displayImage = false;

    }


    public void SetNewLevel(int _level)
    {
        currentLevel = _level;
        ChangePicture();
    }


}
