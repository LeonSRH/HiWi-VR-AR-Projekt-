using UnityEngine;

namespace SmartHospital.ModelProcessing {
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    [ExecuteInEditMode]
    public class CombineCollidersExecEditor : MonoBehaviour {
        void Start() {
            CombineMeshes();
            GetComponent<MeshCollider>().sharedMesh = GetComponent<MeshFilter>().sharedMesh;
            GetComponent<Renderer>().enabled = true;
            gameObject.SetActive(true);
        }


        /*
---------------------------------- use only in editor! -------------------------------------------------

*/

        public void CombineMeshes() {
            //Vector3 oldSca = transform.localScale;
            var oldRot = transform.rotation;
            var oldPos = transform.position;

            //transform.localScale = Vector3.one;
            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;

            var meshFilters = GetComponentsInChildren<MeshFilter>();

            var combiners = new CombineInstance[meshFilters.Length];

            for (var i = 0; i < meshFilters.Length; i++) {
                combiners[i].mesh = meshFilters[i].sharedMesh;
                combiners[i].transform = meshFilters[i].transform.localToWorldMatrix;
                meshFilters[i].gameObject.SetActive(false);
            }

            var finalMesh = new Mesh();

            finalMesh.CombineMeshes(combiners);

            GetComponent<MeshFilter>().sharedMesh = finalMesh;

            //transform.localScale = new Vector3((float)0.6298221, (float)0.351526, (float)0.4184591);
            transform.localScale = new Vector3(1, 1, 1);
            transform.rotation = oldRot;
            transform.position = oldPos;

            Debug.Log(name + " is combining meshes! ");
        }
    }
}