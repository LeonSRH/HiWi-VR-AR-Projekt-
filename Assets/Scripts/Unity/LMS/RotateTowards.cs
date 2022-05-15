using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowards : MonoBehaviour
{
    public Camera mainCamera;
    public int x = 0;
    public int y = 90;
    public int z = 0;
    void Update()
    {
        var newRotation = Quaternion.LookRotation(mainCamera.transform.position) * Quaternion.Euler(x, y, z);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, (float)(Time.deltaTime * 5.0));
    }
}
