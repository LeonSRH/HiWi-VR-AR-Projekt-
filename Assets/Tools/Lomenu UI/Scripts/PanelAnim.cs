using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Plugins.Lomenu_UI.Scripts {
	public class PanelAnim : MonoBehaviour {

		public List<GameObject> pages = new List<GameObject>();
		public int currentPanelIndex = 0;
		public GameObject currentPanel;
		public CanvasGroup canvasGroup;

		public bool fadeOut = false;
		public bool fadeIn = false;
		public float fadeFactor = 8f;

		void Update ()
		{
			if (fadeOut)
				canvasGroup.alpha -= fadeFactor * Time.deltaTime;
			if (fadeIn) 
			{
				canvasGroup.alpha += fadeFactor * Time.deltaTime;
			}
		}

		public void newPanel(int newPage)
		{
			if (newPage != currentPanelIndex)
				StartCoroutine ("ChangePage", newPage);
		}

		public IEnumerator ChangePage (int newPage)
		{
			canvasGroup = currentPanel.GetComponent<CanvasGroup>();
			canvasGroup.alpha = 1f;
			fadeIn = false;
			fadeOut = true;

			while(canvasGroup.alpha > 0)
			{
				yield return 0;
			}
			currentPanel.SetActive(false);

			fadeIn = true;
			fadeOut = false;
			currentPanelIndex = newPage;
			currentPanel = pages [currentPanelIndex];
			currentPanel.SetActive (true);
			canvasGroup = currentPanel.GetComponent<CanvasGroup>();
			canvasGroup.alpha = 0f;

			while (canvasGroup.alpha <1f)
			{
				yield return 0;
			}

			canvasGroup.alpha = 1f;
			fadeIn = false;

			yield return 0;
		}
	}
}
