using UnityEngine;

namespace Assets.Plugins.Lomenu_UI.Scripts {
	public class Exit : MonoBehaviour
	{	
		public void QuitGame()
		{
			Debug.Log ("As you wish! :)");
			Application.Quit();
		}
	}
}
