using Lucene.Net.Analysis.Tokenattributes;
using UnityEngine;

public class CreatePlaceableInventoryitem : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToCreate;

    private GameObject objectToPlace;

    private int mouseClick = 0;

    private float floorZ;

    private Vector3 mOffset;

    public GameObject Room;

    private Camera cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("OverViewCamera").GetComponent<Camera>();
    }
    public void createObject()
    {

        mouseClick++;
    }

    private void Update()
    {
    }

    private void OnMouseDrag()
    {

        Debug.Log(objectToPlace);
    }

    private void OnMouseDown()
    {
        objectToPlace = Instantiate(objectToCreate, new Vector3(Input.mousePosition.x, 1, Input.mousePosition.z), Quaternion.identity);
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = floorZ;

        return cam.ScreenToWorldPoint(mousePoint);
    }
}
