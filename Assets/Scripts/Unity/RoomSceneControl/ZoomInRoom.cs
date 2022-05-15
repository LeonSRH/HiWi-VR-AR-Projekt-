using SmartHospital.Controller;
using SmartHospital.Model;
using UnityEngine;

public class ZoomInRoom : MonoBehaviour
{

    public Transform roomTarget;

    public Transform startPosition;

    private MainSceneMainController mainController;
    private MainSceneUIController mainUIController;

    [SerializeField] private LayerMask normalCMask;
    [SerializeField] private LayerMask roomColliderIgnoreMask;

    [SerializeField]
    private Camera roomCam, mainCam;

    private void Start()
    {
        startPosition = transform;

    }


    public void lookAtRoomTarget()
    {
        mainUIController = FindObjectOfType<MainSceneUIController>();
        mainController = GameObject.FindObjectOfType<MainSceneMainController>();



        var ebene = 0;

        var roomIdGos = GameObject.FindGameObjectsWithTag("RoomCollider");

        foreach (GameObject room in roomIdGos)
        {
            var clientRoom = room.GetComponent<ClientRoom>();
            if (clientRoom.RoomName != null && !clientRoom.RoomName.Equals(""))
            {

                if (clientRoom.RoomName.Equals(mainUIController.GetSelectedRoom().RoomName))
                {
                    roomTarget = room.transform;
                    ebene = clientRoom.MyRoom.NamePlate.Floor;
                }
            }

        }

        prepareCamera();
        mainController.EnableFloorOverviewMode(ebene + 2);

        if (roomTarget != null)
        {
            var camPosition = new Vector3(roomTarget.position.x, (roomTarget.position.y + 2), roomTarget.position.z);

            roomCam.transform.transform.position = camPosition;
        }
    }


    public void prepareCamera()
    {

        roomCam.gameObject.SetActive(true);
        mainCam.gameObject.SetActive(false);
        roomCam.cullingMask = roomColliderIgnoreMask;

    }

    public void Reset()
    {
        roomCam.cullingMask = normalCMask;
        roomCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
    }

}
