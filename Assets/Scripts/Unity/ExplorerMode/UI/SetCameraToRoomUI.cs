using SmartHospital.Model;
using UnityEngine;

public class SetCameraToRoomUI : MonoBehaviour
{
    public Camera cam;
    public GameObject groundPosGO;

    public static GameObject TargetObject;

    public static bool camera_move_enabled = false;
    private static Vector3 camStartPosition;
    private static Vector3 Targetposition;

    private void Start()
    {
        cam = GetComponent<Camera>();
        camStartPosition = cam.transform.position;
    }

    void Update()
    {

        if (camera_move_enabled && TargetObject != null)
        {
            if (TargetObject != null)
                TargetObject.GetComponent<Renderer>().material.color = Color.blue;
            cam.transform.position = new Vector3(TargetObject.transform.position.x, TargetObject.transform.position.y + 5, TargetObject.transform.position.z);
        }
        else if (!camera_move_enabled)
        {
            setCameraToStartPosition();
        }
    }

    public void getCamera()
    {
        cam = GetComponent<Camera>();

    }

    //sets the camera position to a room position with the roomID:Room_id
    public static void setCameraToRoom(string roomID)
    {
        var roomIdGos = GameObject.FindGameObjectsWithTag("RoomCollider");

        foreach (GameObject roomIdGo in roomIdGos)
        {
            if (roomIdGo.GetComponent<ClientRoom>().RoomName != null && !roomIdGo.GetComponent<ClientRoom>().RoomName.Equals(""))
            {
                if (roomIdGo.GetComponent<ClientRoom>().RoomName.Equals(roomID))
                {
                    TargetObject = roomIdGo;
                    TargetObject.GetComponent<Renderer>().material.color = Color.red;
                }
            }

        }
        camera_move_enabled = true;
    }

    public static void disableCameraMove()
    {
        camera_move_enabled = false;
    }

    //Resets the cam position
    public static void setCameraToStartPosition()
    {
        //cam.transform.position = camStartPosition;
    }
}
