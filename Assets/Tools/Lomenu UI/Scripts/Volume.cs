using UnityEngine;

namespace Assets.Plugins.Lomenu_UI.Scripts {
	public class Volume : MonoBehaviour {

		public void VolumeControl(float volumeControl) {
			AudioListener.volume = volumeControl; 
		}
	}
}
