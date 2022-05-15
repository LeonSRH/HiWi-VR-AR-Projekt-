using System;
using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class TimedObjectDestructor : MonoBehaviour
    {
        [SerializeField] private float m_TimeOut = 1.0f;
        [SerializeField] private bool m_DetachChildren = false;


        private void Awake()
        {
            Invoke("DestroyNow", m_TimeOut);
        }


        private void DestroyNow()
        {
            if (m_DetachChildren)
            {
                transform.DetachChildren();
            }
#pragma warning disable CS0618 // "Object.DestroyObject(Object)" ist veraltet: "use Object.Destroy instead."
            DestroyObject(gameObject);
#pragma warning restore CS0618 // "Object.DestroyObject(Object)" ist veraltet: "use Object.Destroy instead."
        }
    }
}
