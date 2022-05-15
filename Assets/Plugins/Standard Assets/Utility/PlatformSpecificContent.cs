using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityStandardAssets.Utility
{
#if UNITY_EDITOR

    [ExecuteInEditMode]
#endif
    public class PlatformSpecificContent : MonoBehaviour
#if UNITY_EDITOR
        , UnityEditor.Build.IActiveBuildTargetChanged
#endif
    {
        private enum BuildTargetGroup
        {
            Standalone,
            Mobile
        }

        [SerializeField]
#pragma warning disable CS0649 // Dem Feld "PlatformSpecificContent.m_BuildTargetGroup" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "".
        private BuildTargetGroup m_BuildTargetGroup;
#pragma warning restore CS0649 // Dem Feld "PlatformSpecificContent.m_BuildTargetGroup" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "".
        [SerializeField]
        private GameObject[] m_Content = new GameObject[0];
        [SerializeField]
        private MonoBehaviour[] m_MonoBehaviours = new MonoBehaviour[0];
        [SerializeField]
#pragma warning disable CS0649 // Dem Feld "PlatformSpecificContent.m_ChildrenOfThisObject" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "false".
        private bool m_ChildrenOfThisObject;
#pragma warning restore CS0649 // Dem Feld "PlatformSpecificContent.m_ChildrenOfThisObject" wird nie etwas zugewiesen, und es hat immer seinen Standardwert von "false".

#if !UNITY_EDITOR
	void OnEnable()
	{
		CheckEnableContent();
	}
#else
        public int callbackOrder
        {
            get
            {
                return 1;
            }
        }
#endif

#if UNITY_EDITOR

        private void OnEnable()
        {
            EditorApplication.update += Update;
        }


        private void OnDisable()
        {
            EditorApplication.update -= Update;
        }

        public void OnActiveBuildTargetChanged(BuildTarget previousTarget, BuildTarget newTarget)
        {
            CheckEnableContent();
        }

        private void Update()
        {
            CheckEnableContent();
        }
#endif


        private void CheckEnableContent()
        {
#if (UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_TIZEN)
		if (m_BuildTargetGroup == BuildTargetGroup.Mobile)
		{
			EnableContent(true);
		} else {
			EnableContent(false);
		}
#endif

#if !(UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8 || UNITY_TIZEN)
            if (m_BuildTargetGroup == BuildTargetGroup.Mobile)
            {
                EnableContent(false);
            }
            else
            {
                EnableContent(true);
            }
#endif
        }


        private void EnableContent(bool enabled)
        {
            if (m_Content.Length > 0)
            {
                foreach (var g in m_Content)
                {
                    if (g != null)
                    {
                        g.SetActive(enabled);
                    }
                }
            }
            if (m_ChildrenOfThisObject)
            {
                foreach (Transform t in transform)
                {
                    t.gameObject.SetActive(enabled);
                }
            }
            if (m_MonoBehaviours.Length > 0)
            {
                foreach (var monoBehaviour in m_MonoBehaviours)
                {
                    monoBehaviour.enabled = enabled;
                }
            }
        }
    }
}