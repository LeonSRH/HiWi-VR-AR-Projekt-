using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// created 28.05.2019 by KS
/// Manager for the Power BI Scene
/// </summary>
public class PowerBISceneManager : MonoBehaviour
{
    /// <summary>
    /// Unloads the power bi scene
    /// </summary>
    public void DestroySelf()
    {
        SceneManager.UnloadSceneAsync("PowerBIScene");
    }
}
