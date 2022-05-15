using UnityEngine;
using System.Collections;

namespace Assets.Plugins.Scripts.TrainingRoomScripts
{
    public class CreateSettings : MonoBehaviour
    {
        public static void createSettingModule(int floor, string department)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.AddComponent<Script>();
            //cube.gameObject.GetComponent<Script>().setFloor();
        }
    }
}

