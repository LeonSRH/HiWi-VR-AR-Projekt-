using SmartHospital.Model;
using UnityEngine;

public class InventoryDragController : MonoBehaviour
{

    [SerializeField]
    private Material objectHighlightMaterial, objectNormalMaterial, objectNotInRangeMaterial;

    DragObject dragObject;

    public string RoomName { get; set; }

    bool inRoom = false;

    private void Start()
    {
        dragObject = this.GetComponent<DragObject>();
        Debug.Log(RoomName);
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("RoomCollider") && other.gameObject.GetComponent<ClientRoom>().RoomName.Equals(RoomName))
        {
            inRoom = true;
        }
        else
        {
            inRoom = false;
        }
    }


    private void OnMouseDrag()
    {

        if (inRoom)
        {
            dragObject.ChangeMaterial(objectHighlightMaterial);

        }
        else
        {
            dragObject.ChangeMaterial(objectNotInRangeMaterial);
        }
    }


    private void OnMouseUp()
    {
        dragObject.ChangeMaterial(objectNormalMaterial);
    }

}
