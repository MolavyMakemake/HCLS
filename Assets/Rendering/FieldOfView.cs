using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class FieldOfView : MonoBehaviour
{
    public Player player;

    public float fov = 107;
    public float near = 60;

    [Range(2, 100)] public int rays = 10;

    public float viewDistance = 10;

    Mesh mesh;

    private void FixedUpdate()
    {
        //int rays = 4;


        Vector3[] vertices = new Vector3[rays + 2];

        float theta = 0;
        float dtheta = fov / (rays - 1);
        for (int i = 0; i < rays; i++)
        {
            float a = Mathf.Deg2Rad * theta;
            Vector3 normal = new Vector3(Mathf.Cos(a), 0, Mathf.Sin(a));
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.rotation * normal, out hit, viewDistance))
            {
                vertices[i + 1] = Quaternion.Inverse(transform.rotation) * (hit.point - transform.position);
            }
            else
                vertices[i + 1] = normal * viewDistance;

            theta -= dtheta;
        }

        if (mesh.vertices.Length != vertices.Length)
            ClearMesh(vertices);

        else
            mesh.vertices = vertices;
    }

    void ClearMesh(Vector3[] vertices)
    {
        int[] triangles = new int[3 * (rays - 1)];

        for (int i = 0; i < rays - 1; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = i + 1;
            triangles[3 * i + 2] = i + 2;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = new Vector2[vertices.Length];
        mesh.triangles = triangles;
    } 

    private void OnEnable()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }
}
