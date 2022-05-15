using Assets.Plugins.Scripts.TrainingRoomScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LMSMainController : MonoBehaviour
{
    LMSMainView view;
    LoadScene sceneLoader;

    private void Awake()
    {
        sceneLoader = FindObjectOfType<LoadScene>();
        view = FindObjectOfType<LMSMainView>();
    }

    private void Start()
    {
        view.On2DModeClicked += () =>
        {
            view.lms_Mode = LMSMainView.LMS_MODE.MODE_2D;
            SceneManager.UnloadSceneAsync("RoomControllingScene");
        };
        view.On3DModeClicked += () =>
        {
            view.lms_Mode = LMSMainView.LMS_MODE.MODE_3D;
            sceneLoader.LoadSceneAs("RoomControllingScene");
        };

        view.OnBuildingModeClicked += () =>
        {
            //Setup Button List in MainView
            //Check active mode and setup 3D objects if needed
            switch (view.lms_Mode)
            {
                case LMSMainView.LMS_MODE.MODE_3D:
                    break;
                case LMSMainView.LMS_MODE.MODE_2D:
                    break;
                default:
                    break;
            }
        };
        view.OnDeviceModeClicked += () =>
        {
            switch (view.lms_Mode)
            {
                case LMSMainView.LMS_MODE.MODE_3D:
                    break;
                case LMSMainView.LMS_MODE.MODE_2D:
                    break;
                default:
                    break;
            }
        };
        view.OnProcessModeClicked += () =>
        {
            switch (view.lms_Mode)
            {
                case LMSMainView.LMS_MODE.MODE_3D:
                    break;
                case LMSMainView.LMS_MODE.MODE_2D:
                    break;
                default:
                    break;
            }
        };
    }
}
