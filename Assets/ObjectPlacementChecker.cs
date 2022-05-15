using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Auhtor: SDA August 2019
/// </summary>

public class ObjectPlacementChecker : MonoBehaviour
{

    /// <summary>
    /// This one checks if there is a Collission with an ohter Building.
    /// </summary>


    [Header("Material")]
    public Material redMaterial;
    public Material greenMaterial;
    Material realMaterial;
    [Header("Link Mesh Obj")]
    public MeshRenderer meshRenderer;

    [Header("Public Bool")]
    public bool CollisionWithOtherBuilding = false;

    bool ghostPhase = true;

    void Start()
    {
        realMaterial = meshRenderer.material;
        meshRenderer.material = greenMaterial;
    }

    /// <summary>
    /// After being relased from ghost phase - the object shall retrun to the real material.
    /// </summary>
    public void ResetToOldMaterial()
    {
        ghostPhase = false;
        meshRenderer.material = realMaterial;
    }

    /// <summary>
    /// Check if collide with other building.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Building") && ghostPhase)
        {

            CollisionWithOtherBuilding = true;

            meshRenderer.material = redMaterial;

        }


    }

    /// <summary>
    /// Check if obj exits a collission.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Building") && ghostPhase)
        {

            CollisionWithOtherBuilding = false;

            meshRenderer.material = greenMaterial;

        }


    }


}
