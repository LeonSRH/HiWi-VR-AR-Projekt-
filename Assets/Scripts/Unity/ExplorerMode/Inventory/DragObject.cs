using SmartHospital.Model;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 mOffset;

    private float mZCoord;

    private bool inRoom { get; set; }

    [SerializeField]
    private Camera cam;


    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        inRoom = true;
    }


    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;

    }

    private void OnMouseDown()
    {
        mZCoord = cam.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return cam.ScreenToWorldPoint(mousePoint);
    }

    /// <summary>
    /// Changes the materials of all children objects of the gameObject
    /// </summary>
    /// <param name="newMat">Material which should be displayed in all children objects</param>
    public void ChangeMaterial(Material newMat)
    {
        var children = gameObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in children)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = newMat;
            }
            rend.materials = mats;
        }
    }

}
