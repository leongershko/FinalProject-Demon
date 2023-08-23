using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class DoubleSideMesh : MonoBehaviour
{
    [SerializeField]
    private bool onlyReverse = true;
    
    private void Awake()
    {
        var meshFilter = GetComponent<MeshFilter>();
        var indices = meshFilter.mesh.GetIndices(0);
        var newIndices = new int[onlyReverse ? indices.Length : indices.Length * 2];
        if (!onlyReverse)
        {
            Array.Copy(indices, 0, newIndices, indices.Length, indices.Length);
        }
        Array.Reverse(indices);
        Array.Copy(indices, 0, newIndices, 0, indices.Length);
        meshFilter.mesh.SetIndices(newIndices, MeshTopology.Triangles, 0);

        var meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = meshFilter.mesh;
    }
}
