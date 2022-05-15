using UnityEngine;
using UnityEngine.UI;

namespace Assets.Plugins.Lomenu_UI.Scripts {
	public class Quality : MonoBehaviour {

		public Dropdown resxDropdown;
		public Dropdown qualityDropdown;

		Resolution[] resolutions;

		void Start()
		{
			resolutions = Screen.resolutions;
 
			for (int i = 0; i < resolutions.Length; i++)
			{
				resxDropdown.options.Add (new Dropdown.OptionData (ResToString (resolutions [i])));
 
				resxDropdown.value = i;
 
				resxDropdown.onValueChanged.AddListener(delegate { Screen.SetResolution(resolutions[resxDropdown.value].width, resolutions[resxDropdown.value].height, true);});
         
			}
			qualityDropdown.options.Clear();
			foreach (string t in QualitySettings.names) {
				qualityDropdown.options.Add (new Dropdown.OptionData (t));
			}
			qualityDropdown.onValueChanged.AddListener(
				delegate
				{
					SetQualityLevel();
				}
			);
		}
		public void SetQualityLevel()
		{
			QualitySettings.SetQualityLevel(qualityDropdown.value);
		}
	
		string ResToString(Resolution res)
		{
			return res.width + " x " + res.height;
		}
	}
}

