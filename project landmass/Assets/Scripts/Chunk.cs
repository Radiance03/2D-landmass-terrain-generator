using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{

	public MeshRenderer meshRenderer;
	public MeshFilter meshFilter;
	int vertexIndex = 0;
	List<Vector3> vertices = new List<Vector3>();
	List<int> triangles = new List<int>();
	List<Vector2> uvs = new List<Vector2>();
	void Start()
	{
		//createChunk(new Vector2(0,0));
		//RenderMesh();
	}

	public void DrawBlock(Vector3 pos,int textureID)
    {
		vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[0, 0]]);
		vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[0, 1]]);
		vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[0, 2]]);
		vertices.Add(pos + VoxelData.voxelVerts[VoxelData.voxelTris[0, 3]]);

		AddTexture(textureID);

		//makes the right triangles out of the vertices
		triangles.Add(vertexIndex);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 2);
		triangles.Add(vertexIndex + 2);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 3);
		vertexIndex += 4;


	}
	public void RenderMesh()
    {
		Mesh mesh = new Mesh();
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.uv = uvs.ToArray();

		mesh.RecalculateNormals();

		meshFilter.mesh = mesh;
	}
	public void createChunk(Vector2 pos)
    {
		for (int x = 0; x < 30; x++)
		{
			for (int y = 0; y < 30; y++)
			{
				DrawBlock(new Vector3(x + pos.x , y + pos.y , 0),0);


			}
		}
	}

	void AddTexture(int textureID)
	{

		float y = textureID / VoxelData.TextureAtlasSizeInBlocks;
		float x = textureID - (y * VoxelData.TextureAtlasSizeInBlocks);

		x *= VoxelData.NormalizedBlockTextureSize;
		y *= VoxelData.NormalizedBlockTextureSize;

		y = 1f - y - VoxelData.NormalizedBlockTextureSize;


		uvs.Add(new Vector2(x, y));
		uvs.Add(new Vector2(x, y + VoxelData.NormalizedBlockTextureSize));
		uvs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y));
		uvs.Add(new Vector2(x + VoxelData.NormalizedBlockTextureSize, y + VoxelData.NormalizedBlockTextureSize));





	}

}