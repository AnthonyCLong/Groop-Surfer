using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MakeRamp : MonoBehaviour {
    public List<GameObject> ramps;

    public PhysicMaterial mat;

    public void Start() {
        //MakeSegment();
        foreach(GameObject ramp in ramps) {
            UpdateInternals(ramp);
        }
    }

    public void UpdateInternals(GameObject ramp) {
        ramp.tag = "Ramp";
        foreach(Transform child in ramp.transform) {
            BoxCollider coll = child.gameObject.AddComponent<BoxCollider>();
            coll.material = mat;
            child.tag = "Ramp";
        }
    }

    public Vector2 PixelToUV(int x, int y) {
        Vector2 ret = new Vector2(x/27f, y/27f);
        return ret;
    }

    public void MakeSegment() {
        List<Vector3> verts = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        List<int> tris = new List<int>();

        //Front Face
        int vertLength = verts.Count;
        verts.Add(new Vector3(-37.5f, -37.5f, 0));
        verts.Add(new Vector3(37.5f, -37.5f, 0));
        verts.Add(new Vector3(37.5f, 37.5f, 0));
        verts.Add(new Vector3(-37.5f, 37.5f, 0));

        uvs.Add(PixelToUV(0, 10));
        uvs.Add(PixelToUV(17, 10));
        uvs.Add(PixelToUV(17, 27));
        uvs.Add(PixelToUV(0, 27));

        tris.Add(vertLength + 0);
        tris.Add(vertLength + 2);
        tris.Add(vertLength + 1);
        tris.Add(vertLength + 0);
        tris.Add(vertLength + 3);
        tris.Add(vertLength + 2);

        //Left Face
        vertLength = verts.Count;
        verts.Add(new Vector3(-37.5f, -37.5f, 25));
        verts.Add(new Vector3(-37.5f, -37.5f, 0));
        verts.Add(new Vector3(-37.5f, 37.5f, 0));
        verts.Add(new Vector3(-37.5f, 37.5f, 25));

        uvs.Add(PixelToUV(22, 10));
        uvs.Add(PixelToUV(17, 10));
        uvs.Add(PixelToUV(17, 27));
        uvs.Add(PixelToUV(22, 27));

        tris.Add(vertLength + 0);
        tris.Add(vertLength + 2);
        tris.Add(vertLength + 1);
        tris.Add(vertLength + 0);
        tris.Add(vertLength + 3);
        tris.Add(vertLength + 2);

        //Right Face
        vertLength = verts.Count;
        verts.Add(new Vector3(37.5f, -37.5f, 0));
        verts.Add(new Vector3(37.5f, -37.5f, 25));
        verts.Add(new Vector3(37.5f, 37.5f, 25));
        verts.Add(new Vector3(37.5f, 37.5f, 0));

        uvs.Add(PixelToUV(22, 10));
        uvs.Add(PixelToUV(27, 10));
        uvs.Add(PixelToUV(27, 27));
        uvs.Add(PixelToUV(22, 27));

        tris.Add(vertLength + 0);
        tris.Add(vertLength + 2);
        tris.Add(vertLength + 1);
        tris.Add(vertLength + 0);
        tris.Add(vertLength + 3);
        tris.Add(vertLength + 2);

        //Top Face
        vertLength = verts.Count;
        verts.Add(new Vector3(-37.5f, 37.5f, 0));
        verts.Add(new Vector3(37.5f, 37.5f, 0));
        verts.Add(new Vector3(37.5f, 37.5f, 25));
        verts.Add(new Vector3(-37.5f, 37.5f, 25));

        uvs.Add(PixelToUV(0, 5));
        uvs.Add(PixelToUV(17, 5));
        uvs.Add(PixelToUV(17, 10));
        uvs.Add(PixelToUV(0, 10));

        tris.Add(vertLength + 0);
        tris.Add(vertLength + 2);
        tris.Add(vertLength + 1);
        tris.Add(vertLength + 0);
        tris.Add(vertLength + 3);
        tris.Add(vertLength + 2);

        //Bottom Face
        vertLength = verts.Count;
        verts.Add(new Vector3(37.5f, -37.5f, 25));
        verts.Add(new Vector3(-37.5f, -37.5f, 25));
        verts.Add(new Vector3(-37.5f, -37.5f, 0));
        verts.Add(new Vector3(37.5f, -37.5f, 0));

        uvs.Add(PixelToUV(0, 5));
        uvs.Add(PixelToUV(17, 5));
        uvs.Add(PixelToUV(17, 0));
        uvs.Add(PixelToUV(0, 0));

        tris.Add(vertLength + 0);
        tris.Add(vertLength + 1);
        tris.Add(vertLength + 2);
        tris.Add(vertLength + 0);
        tris.Add(vertLength + 2);
        tris.Add(vertLength + 3);

        //Back Face
        vertLength = verts.Count;
        verts.Add(new Vector3(37.5f, -37.5f, 25));
        verts.Add(new Vector3(-37.5f, -37.5f, 25));
        verts.Add(new Vector3(-37.5f, 37.5f, 25));
        verts.Add(new Vector3(37.5f, 37.5f, 25));

        uvs.Add(PixelToUV(0, 10));
        uvs.Add(PixelToUV(17, 10));
        uvs.Add(PixelToUV(17, 27));
        uvs.Add(PixelToUV(0, 27));

        tris.Add(vertLength + 0);
        tris.Add(vertLength + 2);
        tris.Add(vertLength + 1);
        tris.Add(vertLength + 0);
        tris.Add(vertLength + 3);
        tris.Add(vertLength + 2);

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = verts.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();

        // AssetDatabase.CreateAsset(mesh, "Assets/Ramps/Segment.asset");
        // AssetDatabase.SaveAssets();
    }


    public void Bend(float angle) {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        int step = 15;
        int vertCount = 0;

        //Front Face Vertices
        vertices.Add(new Vector3(-25, -25, 0));
        vertices.Add(new Vector3(-25, 25, 0));
        vertices.Add(new Vector3(25, -25, 0));
        vertices.Add(new Vector3(25, 25, 0));

        //Front Face Triangles
        triangles.Add(1);
        triangles.Add(3);
        triangles.Add(2);
        triangles.Add(1);
        triangles.Add(2);
        triangles.Add(0);

        for (float theta = 270; theta < (270 + angle); theta += step) {
            //Inner Bend Vertices
            Vector3 inner1 = new Vector3(-25, 50 * Mathf.Sin(Mathf.Deg2Rad * theta) + 75, 50 * Mathf.Cos(Mathf.Deg2Rad * theta));
            Vector3 inner2 = new Vector3(-25, 50 * Mathf.Sin(Mathf.Deg2Rad * (theta + step)) + 75, 50 * Mathf.Cos(Mathf.Deg2Rad * (theta + step)));
            vertices.Add(inner1);
            vertices.Add(new Vector3(25, inner1.y, inner1.z));
            vertices.Add(inner2);
            vertices.Add(new Vector3(25, inner2.y, inner2.z));

            //Outer Bend Vertices
            Vector3 outer1 = new Vector3(-25, 100 * Mathf.Sin(Mathf.Deg2Rad * theta) + 75, 100 * Mathf.Cos(Mathf.Deg2Rad * theta));
            Vector3 outer2 = new Vector3(-25, 100 * Mathf.Sin(Mathf.Deg2Rad * (theta + step)) + 75, 100 * Mathf.Cos(Mathf.Deg2Rad * (theta + step)));
            vertices.Add(outer1);
            vertices.Add(new Vector3(25, outer1.y, outer1.z));
            vertices.Add(outer2);
            vertices.Add(new Vector3(25, outer2.y, outer2.z));

            vertCount = vertices.Count;

            //Positive X Vertices
            vertices.Add(vertices[vertCount - 3]);
            vertices.Add(vertices[vertCount - 7]);
            vertices.Add(vertices[vertCount - 1]);
            vertices.Add(vertices[vertCount - 5]);

            //Negative X Vertices
            vertices.Add(vertices[vertCount - 4]);
            vertices.Add(vertices[vertCount - 8]);
            vertices.Add(vertices[vertCount - 2]);
            vertices.Add(vertices[vertCount - 6]);

            vertCount = vertices.Count;

            //Inner Bend Triangles
            triangles.Add(vertCount - 15);
            triangles.Add(vertCount - 16);
            triangles.Add(vertCount - 14);
            triangles.Add(vertCount - 15);
            triangles.Add(vertCount - 14);
            triangles.Add(vertCount - 13);

            //Outer Bend Triangles
            triangles.Add(vertCount - 11);
            triangles.Add(vertCount - 9);
            triangles.Add(vertCount - 10);
            triangles.Add(vertCount - 11);
            triangles.Add(vertCount - 10);
            triangles.Add(vertCount - 12);

            //Positive X Triangles
            triangles.Add(vertCount - 8);
            triangles.Add(vertCount - 7);
            triangles.Add(vertCount - 5);
            triangles.Add(vertCount - 8);
            triangles.Add(vertCount - 5);
            triangles.Add(vertCount - 6);

            //Negative X Triangles
            triangles.Add(vertCount - 3);
            triangles.Add(vertCount - 4);
            triangles.Add(vertCount - 2);
            triangles.Add(vertCount - 3);
            triangles.Add(vertCount - 2);
            triangles.Add(vertCount - 1);
        }

        //Top Face Vertices
        vertices.Add(vertices[vertCount - 14]);
        vertices.Add(vertices[vertCount - 13]);
        vertices.Add(vertices[vertCount - 10]);
        vertices.Add(vertices[vertCount - 9]);

        vertCount = vertices.Count;

        //Top Face Triangles
        triangles.Add(vertCount - 3);
        triangles.Add(vertCount - 4);
        triangles.Add(vertCount - 2);
        triangles.Add(vertCount - 3);
        triangles.Add(vertCount - 2);
        triangles.Add(vertCount - 1);

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();

        // AssetDatabase.CreateAsset(mesh, "Assets/Ramps/" + angle + ".asset");
        // AssetDatabase.SaveAssets();
    }
}
