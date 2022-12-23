using System.IO;
using UnityEngine;

namespace DeTools.PivotTool.Export
{
    /// <summary>
    /// STL exporter exports an mesh intro an stl file
    /// </summary>
    public class StlExporter
	{
        /// <summary>
        /// writes an complete stl file from an mesh
        /// </summary>
        public static void MeshToStl(Mesh mesh, string filePath)
		{
			using (var writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
			{
				writer.Write(new byte[80]);

				int triangleCount = mesh.triangles.Length / 3;

				writer.Write(triangleCount);

				for (int i = 0; i < mesh.triangles.Length; i += 3)
				{
					Vector3 vertex1 = mesh.vertices[mesh.triangles[i]];
					Vector3 vertex2 = mesh.vertices[mesh.triangles[i + 1]];
					Vector3 vertex3 = mesh.vertices[mesh.triangles[i + 2]];

					Vector3 normal = Vector3.Cross(vertex2 - vertex1, vertex3 - vertex1).normalized;

					writer.Write(normal.x);
					writer.Write(normal.y);
					writer.Write(normal.z);
					writer.Write(vertex1.x);
					writer.Write(vertex1.y);
					writer.Write(vertex1.z);
					writer.Write(vertex2.x);
					writer.Write(vertex2.y);
					writer.Write(vertex2.z);
					writer.Write(vertex3.x);
					writer.Write(vertex3.y);
					writer.Write(vertex3.z);

					writer.Write((ushort)0);
				}
			}
		}
	}
}