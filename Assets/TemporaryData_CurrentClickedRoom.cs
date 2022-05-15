using SmartHospital.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TemporaryData_CurrentClickedRoom : MonoBehaviour
{

    static GameObject roomSelected;
    public string roomName;

    public ClientRoom clientRoom;

    public void LoadNewSceneWithGameObject()
    {
        roomSelected = SelectedRoomStatus.selectedObject;
        Debug.Log(roomSelected.GetComponent<ClientRoom>().RoomName);

        roomName = roomSelected.GetComponent<ClientRoom>().RoomName;


        //clientRoom.MyRoom.NamePlate

        //clientRoom.MyRoom.NamePlate.DoorPictogram;



        // Wenn Visible Room Name Null or 0 dann ist es Dienstzimmer
        

        //clientRoom.MyRoom.NamePlate.Style


        DontDestroyOnLoad(this);
        SceneManager.LoadScene("DoorSign", LoadSceneMode.Single);
    }
    
}
