using UnityEngine;

namespace Assets.Plugins.Lomenu_UI.Scripts {
	public class LaunchURL : MonoBehaviour {

		public string URL; 

		public void urlLinkOrWeb() 
		{
			Application.OpenURL(URL);
		}
	}
}
