using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MeshSpawn : MonoBehaviour
{
    public Mesh task_mesh;
    public GameObject mesh_gameObject;

    Vector3[] verticies;
    int[] indecies;


    //--------------------


    private void Start()
    {
        SetupMesh();
    }


    //--------------------


    void SetupMesh()
    {
        //Verticies
        verticies = new Vector3[6];

        verticies[0] = new Vector3(0, 0, 0);
        verticies[1] = new Vector3(56, 0, 56);
        verticies[2] = new Vector3(0, 21.6f, 56);
        verticies[3] = new Vector3(56, 11, 0);
        verticies[4] = new Vector3(112, 0, 0);
        verticies[5] = new Vector3(112, 13, 56);

        //Triangles
        indecies = new int[12];

        indecies[0] = 0;
        indecies[1] = 2;
        indecies[2] = 1;

        indecies[3] = 0;
        indecies[4] = 1;
        indecies[5] = 3;

        indecies[6] = 3;
        indecies[7] = 1;
        indecies[8] = 4;

        indecies[9] = 4;
        indecies[10] = 1;
        indecies[11] = 5;

        //Instantiate the mesh and attach it to the mesh filter
        task_mesh = new Mesh();
        mesh_gameObject.GetComponent<MeshFilter>().mesh = task_mesh;

        //Assign the vertices and triangles to the mesh
        task_mesh.vertices = verticies;
        task_mesh.triangles = indecies;
    }
}
