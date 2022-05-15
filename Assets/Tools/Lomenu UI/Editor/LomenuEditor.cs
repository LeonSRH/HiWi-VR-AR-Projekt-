using UnityEditor;
using UnityEngine;

namespace Assets.Editor {
	public class LomenuEditor : EditorWindow {

		private static LomenuEditor instance = null;

		[MenuItem("Plugins/Lomenu UI/Layouts/Create Aside")]

		static void CreateAside()
		{
			instance = Instantiate(Resources.Load<GameObject>("Aside Layout")).GetComponent<LomenuEditor>();
		}

		[MenuItem("Plugins/Lomenu UI/Layouts/Create Battlefield")]

		static void CreateBattlefield()
		{
			instance = Instantiate(Resources.Load<GameObject>("Battlefield Layout")).GetComponent<LomenuEditor>();
		}

		[MenuItem("Plugins/Lomenu UI/Layouts/Create Field Layout (3D)")]

		static void CreateField3D()
		{
			instance = Instantiate(Resources.Load<GameObject>("Field Layout (3D)")).GetComponent<LomenuEditor>();
		}

		[MenuItem("Plugins/Lomenu UI/Layouts/Create Field Layout")]

		static void CreateField()
		{
			instance = Instantiate(Resources.Load<GameObject>("Field Layout")).GetComponent<LomenuEditor>();
		}

		[MenuItem("Plugins/Lomenu UI/Layouts/Create Flample Layout")]

		static void CreateFlample()
		{
			instance = Instantiate(Resources.Load<GameObject>("Flample Layout")).GetComponent<LomenuEditor>();
		}

		public static void OnCustomWindow()
		{
			EditorWindow.GetWindow(typeof(LomenuEditor));
		}

		private void PreventWarning() {
			// Prevents Unity from displaying a warning in console
			instance.Close();
		}
	}
}
