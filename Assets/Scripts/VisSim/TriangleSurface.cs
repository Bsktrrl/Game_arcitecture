using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using UnityEngine;
using static TriangleSurface;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class TriangleSurface : MonoBehaviour
{
    //Information found by hitting a TriangleSurface
    public struct Hit
    {
        public Vector3 position;
        public Vector3 normal;

        public bool isHit;
    }

    public Mesh task_mesh;

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
        gameObject.GetComponent<MeshFilter>().mesh = task_mesh;

        //Assign the vertices and triangles to the mesh
        task_mesh.vertices = verticies;
        task_mesh.triangles = indecies;
    }

    Vector3 Barycentric(Vector2 a, Vector2 b, Vector2 c, Vector2 p)
    {
        Vector2 ab = b - a;
        Vector2 ac = c - a;

        Vector3 abc = Vector3.Cross(ab, ac);
        float normal = abc.magnitude;

        Vector2 pa = a - p;
        Vector2 pb = b - p;
        Vector2 pc = c - p;

        float x = Vector3.Cross(pa, pb).z / normal;
        float y = Vector3.Cross(pb, pc).z / normal;
        float z = Vector3.Cross(pc, pa).z / normal;

        x *= -1f;
        y *= -1f;
        z *= -1f;

        #region Old
        //float u, v, w;

        //Vector2 v0 = a - p;
        //Vector2 v1 = b - p;
        //Vector2 v2 = c - p;

        //float d00 = Vector2.Dot(v0, v0);
        //float d01 = Vector2.Dot(v0, v1);
        //float d11 = Vector2.Dot(v1, v1);
        //float d20 = Vector2.Dot(v2, v0);
        //float d21 = Vector2.Dot(v2, v1);

        //float denom = d00 * d11 - d01 * d01;

        //v = (d11 * d20 - d01 * d21) / denom;
        //w = (d00 * d21 - d01 * d20) / denom;
        //u = 1.0f - v - w;
        #endregion

        return new Vector3(x, y, z);
    }
    Vector3 CrossTime(Vector2 A, Vector2 B)
    {
        Vector3 cross = new Vector3();

        cross.z = (A.x * B.y) - (A.y * B.x);

        return cross;
    }

    public Hit GetCollision(Vector2 position)
    {
        var hit = new Hit();
        hit.position.x = position.x;
        hit.position.z = position.y;

        for (int i = 0; i < indecies.Length; i += 3)
        {
            int i1 = indecies[i];
            int i2 = indecies[i + 1];
            int i3 = indecies[i + 2];

            Vector3 v1 = verticies[i1];
            Vector3 v2 = verticies[i2];
            Vector3 v3 = verticies[i3];

            Vector2 a1 = new Vector2(verticies[i1].x, verticies[i1].z);
            Vector2 a2 = new Vector2(verticies[i2].x, verticies[i2].z);
            Vector2 a3 = new Vector2(verticies[i3].x, verticies[i3].z);

            Vector3 temp = Barycentric(a1, a2, a3, position);

            //print("Barycentric: " + temp.x + " | " + temp.y + " | " + temp.z);

            if (temp.x >= 0f && temp.x <= 1f && temp.y >= 0f && temp.y <= 1f && temp.z >= 0f && temp.z <= 1f)
            {
                var y = verticies[i1].y * temp.x + verticies[i2].y * temp.y + verticies[i3].y * temp.z;
                //print($"{verticies[i1].y} * {temp.x} + {verticies[i2].y} * {temp.y} + {verticies[i3].y} * {temp.z} = {y}");
                hit.position.y = y;
                hit.normal = Vector3.Cross(v2 - v1, v3 - v2).normalized;
                hit.isHit = true;

                return hit;
            }
        }

        return hit;
    }

    //public float GetHeight(Vector2 point)
    //{
    //    for (int i = 0; i < indecies.Length; i += 3)
    //    {
    //        int i1 = indecies[i];
    //        int i2 = indecies[i + 1];
    //        int i3 = indecies[i + 2];

    //        Vector2 v1 = verticies[i1];
    //        Vector2 v2 = verticies[i2];
    //        Vector2 v3 = verticies[i3];

    //        float u, v, w;
    //        Barycentric(v1, v2, v3, point, out u, out v, out w);

    //        if (u >= 0f && u <= 1f && v >= 0f && v <= 1f && w >= 0f && w <= 1f)
    //        {
    //            float h = verticies[i1].y * u + verticies[i2].y * v + verticies[i3].y * w;
    //        }
    //    }

    //    return -1;
    //}
}
