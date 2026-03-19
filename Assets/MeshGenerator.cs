using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
	Mesh mesh;

	Vector3[] vertices;
	int[] triangles;
	Color[] colors;

	public int xSize = 20;
	public int zSize = 20;
	public float ySize = 2f;

	public float strength = 0.3f;
	
    public float offsetX = 0f;
    public float offsetZ = 0f;

	float minTerrainHeight;
	float maxTerrainHeight;

	public Gradient gradient;
	void Start()
	{
		mesh = new Mesh();
		mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
		GetComponent<MeshFilter>().mesh = mesh;
		//RandomOffset();
		CreateShape();
		UpdateMesh();

	}

	public void RandomOffset()
	{
        offsetX = Random.Range (0f, 9999f);
        offsetZ = Random.Range (0f, 9999f);
	}
	void Update()
	{
	}
	public void CreateShape()
	{
		vertices = new Vector3[(xSize + 1) * (zSize + 1)];
	

		for (int i = 0, z = 0; z <= zSize; z++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				float y = Mathf.PerlinNoise(x * strength + offsetX, z * strength + offsetZ) * ySize;
				vertices[i] = new Vector3(x, y, z);

				if (y > maxTerrainHeight) maxTerrainHeight = y;
				if (y < minTerrainHeight) minTerrainHeight = y;

				i++;
			}
		}

		triangles = new int[xSize * zSize * 6];

		int vert = 0;
		int tris = 0;

		for (int z = 0; z < zSize; z++)
		{
			for (int x = 0; x < xSize; x++)
			{
				triangles[tris + 0] = vert + 0;
				triangles[tris + 1] = vert + xSize + 1;
				triangles[tris + 2] = vert + 1;
				triangles[tris + 3] = vert + 1;
				triangles[tris + 4] = vert + xSize + 1;
				triangles[tris + 5] = vert + xSize + 2;

				vert++;
				tris += 6;
			}
			vert++;
		}

		colors = new Color[vertices.Length];
		for (int i = 0, z = 0; z <= zSize; z++)
		{
			for (int x = 0; x <= xSize; x++)
			{
				float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[i].y);
				colors[i] = gradient.Evaluate(height);
				i++;
			}
		}

	}

	public void UpdateMesh()
	{
		mesh.Clear();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.colors = colors;

		mesh.RecalculateNormals();
		//make it solid
		MeshCollider meshCollider = GetComponent<MeshCollider>();
    	if (meshCollider != null)
   	 	{
        	meshCollider.sharedMesh = mesh;
   		}
	}
}