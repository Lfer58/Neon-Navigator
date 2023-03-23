using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineScript : MonoBehaviour
{

    public LineRenderer line;

    public EdgeCollider2D edgeCollider;

    public List<Vector2> edges;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetEdgeCollider(line);
    }

    public void SetEdgeCollider (LineRenderer line) 
    {
        edges = new List<Vector2>();

        for (int i = 0; i < line.positionCount; i++)
        {
            Vector3 linePoint = line.GetPosition(i);
            edges.Add(new Vector2(linePoint.x, linePoint.y));
        }

        edgeCollider.SetPoints(edges);
    }

    // public void GenerateMeshCollider()
    // {
    //     MeshCollider collider = GetComponent<MeshCollider>();

    //     if (collider == null) 
    //     {
    //         collider = gameObject.AddComponent<MeshCollider>();
    //     }

    //     Mesh mesh = new Mesh();
    //     line.BakeMesh(mesh);
    //     collider.sharedMesh = mesh;
    // }
}
