using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LUI_RotateObject : MonoBehaviour {

	public float rotSpeed = 2;

    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X")*rotSpeed*Mathf.Deg2Rad;
		// Enable this if you want to move Y rotation
        //float rotY = Input.GetAxis("Mouse Y")*rotSpeed*Mathf.Deg2Rad;

#pragma warning disable CS0618 // "Transform.RotateAround(Vector3, float)" ist veraltet: "warning use Transform.Rotate instead."
        transform.RotateAround(Vector3.up, -rotX);
#pragma warning restore CS0618 // "Transform.RotateAround(Vector3, float)" ist veraltet: "warning use Transform.Rotate instead."
		// Enable this if you want to move Y rotation
        //transform.RotateAround(Vector3.right, rotY);
    }
}