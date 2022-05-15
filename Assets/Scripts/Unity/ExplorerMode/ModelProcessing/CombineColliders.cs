using System.Collections.Generic;
using UnityEngine;

namespace SmartHospital.ModelProcessing {
    /*
     * 
     * USAGE:
     * 
     * Put all Meshes to be merged in one empty object
     * 
     * Add components (no further configuration necassary):
     *  Mesh Renderer
     *  Mesh Filter
     *  Mesh Collider
     *  MarkupMaterial (Folder Material)
     *  CombineColliders.cs (This Script, Folder Scripts)
     * 
     *  Merging happens on Game Start
     */
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class CombineColliders : MonoBehaviour {
        void Awake() {
            CombineMeshes();
            GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
            GetComponent<Renderer>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Default"); // = 0
            gameObject.SetActive(true);
        }

        void CombineMeshes() {
            //Vector3 oldSca = transform.localScale;
            var oldRot = transform.rotation;
            var oldPos = transform.position;

            //transform.localScale = Vector3.one;
            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;

            var meshFilters = GetComponentsInChildren<MeshFilter>();

            //CombineInstance[] combiners = new CombineInstance[meshFilters.Length];
            var combiners = new List<CombineInstance>();

            foreach (var t in meshFilters) {
                if (t.gameObject.Equals(gameObject)) {
                    continue;
                }

                var current = new CombineInstance {
                    mesh = t.sharedMesh,
                    transform = t.transform.localToWorldMatrix
                };
                combiners.Add(current);
                Destroy(t.gameObject);
            }

            var finalMesh = new Mesh();

            finalMesh.CombineMeshes(combiners.ToArray(), true);

            GetComponent<MeshFilter>().sharedMesh = finalMesh;
            GetComponent<MeshCollider>().sharedMesh = finalMesh;

            transform.localScale = new Vector3(1, 1, 1);
            transform.rotation = oldRot;
            transform.position = oldPos;

            //Debug.Log(name + " is combining meshes! ");
        }
    }
}