using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Plugins.Scripts.TrainingRoomScripts
{
    public class LoadScene : MonoBehaviour
    {
        public void ChangeToScene(string sceneName)
        {
            LoadingScreen.LoadScene(sceneName);
        }

        public void LoadSceneAs(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }

        public void saveSettings(int floor, string department)
        {
            CreateSettings.createSettingModule(floor, department);
        }
    }
}

