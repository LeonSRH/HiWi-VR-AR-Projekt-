using UnityEngine;
using UnityEngine.UI;

namespace Assets.Plugins.Lomenu_UI.Scripts {
	[RequireComponent(typeof(Button))]
	public class ButtonSound : MonoBehaviour {

		public AudioClip sound;
		private Button button { get { return GetComponent<Button>(); } }
		private AudioSource source { get { return GetComponent<AudioSource>(); } }

		void Start () {
			gameObject.AddComponent<AudioSource>();
			source.clip = sound;
			source.playOnAwake = false;
			button.onClick.AddListener(PlaySound);
		}
	
		void PlaySound () {
			source.PlayOneShot(sound);
		}
	}
}
