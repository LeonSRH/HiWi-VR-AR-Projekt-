using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverViewZoom : MonoBehaviour
{

    public int cameraCurrentZoom = 8;
    public int cameraZoomMax = 500;
    public int cameraZoomMin = 500;

    public float zoomSpeed = 1;
    public float targetOrtho;
    public float smoothSpeed = 2.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;

    void Start()
    {
        var camera = this.GetComponent<Camera>();
        targetOrtho = camera.orthographicSize;
    }

    void Update()
    {
        /**
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            if (cameraCurrentZoom < cameraZoomMax)
            {
                cameraCurrentZoom += 1;
                camera.orthographicSize = Mathf.Max(camera.orthographicSize + 1);
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            if (cameraCurrentZoom > cameraZoomMin)
            {
                cameraCurrentZoom -= 1;
                camera.orthographicSize = Mathf.Min(camera.orthographicSize - 1);
            }
        }**/

        var camera = this.GetComponent<Camera>();
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
        }

        camera.orthographicSize = Mathf.MoveTowards(camera.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
    }

}

