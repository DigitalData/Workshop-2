// COMP30019 - Graphics and Interaction
// (c) University of Melbourne, 2022

using System.Linq;
using UnityEngine;

// Note: The attribute below specifies that this component must coexist with a
// MeshFilter component on the same game object. If it doesn't exist, the Unity
// engine will create one automatically.
[RequireComponent(typeof(MeshFilter))]
public class GeneratePyramid : MonoBehaviour
{
    private void Start()
    {
        // First we'll get the MeshFilter attached to this game object, in the
        // same way that we got the MeshRenderer component last week.
        var meshFilter = GetComponent<MeshFilter>();
        
        // Now we can create a cube mesh and assign it to the mesh filter.
        meshFilter.mesh = CreateMesh();
    }

    private Mesh CreateMesh()
    {
        // Step 0: Create the mesh object. This contains various data structures
        // that allow us to define complex 3D objects. Recommended reading:
        // - https://docs.unity3d.com/ScriptReference/Mesh.html
        var mesh = new Mesh
        {
            name = "Pyramid"
        };

        // Step 1: Define the vertices. These are "points" in 3D space that
        // allow us to construct 3D geometry. For everyone's sanity, we have
        // ordered them such that consecutive chunks of three vertices represent
        // individual triangles. This does not have to be the case though, and
        // depends on the way the mesh surface itself is defined (step 3).
        mesh.SetVertices(new[]
        {
            // Bottom face
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),
            new Vector3(-1.0f, -1.0f, 1.0f),
            
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),

            // Left face
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(-1.0f, -1.0f, 1.0f),
            new Vector3(0.0f, 1.0f, 0.0f),

            // Right face
            new Vector3(1.0f, -1.0f, -1.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(1.0f, -1.0f, 1.0f),

            // Front face
            new Vector3(-1.0f, -1.0f, 1.0f),
            new Vector3(1.0f, -1.0f, 1.0f),
            new Vector3(0.0f, 1.0f, 0.0f),

            // Back face
            new Vector3(-1.0f, -1.0f, -1.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(1.0f, -1.0f, -1.0f)
        });

        // Step 2: Define the vertex colours. There is a one-to-one index
        // mapping between the vertices defined above and these colours below,
        // so the length of both arrays must be the same.
        mesh.SetColors(new[]
        {
            // Bottom face
            Color.red,
            Color.red,
            Color.red,
            
            Color.red,
            Color.red,
            Color.red,

            // Left face            
            Color.yellow,
            Color.yellow,
            Color.yellow,

            // Right face            
            Color.green,
            Color.green,
            Color.green,

            // Front face
            Color.blue,
            Color.blue,
            Color.blue,

            // Back face
            Color.magenta,
            Color.magenta,
            Color.magenta,
        });

        // Step 3: Define the indices. The indices "connect" vertices together
        // in order to define the triangles that represent the mesh surface.
        // The indices are simply an integer array whereby consecutive chunks
        // of 3 integers represent individual triangles as index mappings. For
        // example: [2, 3, 9] defines a single triangle composed of the points
        // vertices[2], vertices[3], vertices[9] (and the respective colours).
        //
        // Luckily for us this is easy to automate because we already ordered
        // the above vertex and colour arrays just like this! We only need to
        // generate a range of integers from 0 to the # of vertices - 1:
        var indices = Enumerable.Range(0, mesh.vertices.Length).ToArray();
        mesh.SetIndices(indices, MeshTopology.Triangles, 0);
        
        // Note that the topology argument specifies that we are in fact
        // defining *triangles* in our indices array. It is also possible to
        // define the mesh surface using quads (MeshTopology.Quads).

        return mesh;
    }
}
