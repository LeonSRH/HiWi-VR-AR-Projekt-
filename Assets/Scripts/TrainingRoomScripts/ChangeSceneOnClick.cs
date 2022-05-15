using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnClick : MonoBehaviour
{

    public Texture2D exitTextureMouse;
    public CursorMode curModeMouse = CursorMode.Auto;
    public Vector2 hotSpotMouse = Vector2.zero;

    public void changeScene(string sceneName)
    {
        try
        {
            SceneManager.LoadScene(sceneName);
        }
        catch
        {
            Debug.Log("Scene crashed");
            throw new System.Exception("Scene crashed");
        }

    }

    private void OnMouseDown()
    {
        changeScene("1_Floor_Level");
    }

    private void Awake()
    {
        Cursor.SetCursor(null, Vector2.zero, curModeMouse);
    }

    public void OnMouseEnter()
    {
        if (gameObject.tag == "Waypoint")
        {
            Cursor.SetCursor(exitTextureMouse, hotSpotMouse, curModeMouse);
        }
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, curModeMouse);
    }
}
