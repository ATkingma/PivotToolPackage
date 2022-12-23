using System.IO;
using UnityEngine;

namespace DeTools.PivotTool.Export
{
	/// <summary>
	/// obj exporter exports an obj file from an mesh
	/// </summary>
	public static class ObjExporter
	{
		/// <summary>
		/// creates an obj file from mesh
		/// </summary>
		public static void MeshToFile(Mesh mesh, string filePath)
		{
			using (var stream = new StreamWriter(filePath))
			{
				stream.WriteLine("# Created by Unity OBJ Exporter");
				stream.WriteLine("# www.github.com/smitpatel/UnityOBJExporter");
				stream.WriteLine("");

				foreach (var vertex in mesh.vertices)
				{
					stream.WriteLine("v " + -vertex.x + " " + vertex.y + " " + vertex.z);
				}

				stream.WriteLine("");

				foreach (var normal in mesh.normals)
				{
					stream.WriteLine("vn " + normal.x + " " + normal.y + " " + normal.z);
				}

				stream.WriteLine("");

				foreach (var uv in mesh.uv)
				{
					stream.WriteLine("vt " + uv.x + " " + uv.y);
				}

				stream.WriteLine("");

				for (var i = 0; i < mesh.subMeshCount; i++)
				{
					var triangles = mesh.GetTriangles(i);
					for (var j = 0; j < triangles.Length; j += 3)
					{
						stream.WriteLine("f " + (triangles[j] + 1) + "/" + (triangles[j] + 1) + "/" + (triangles[j] + 1) + " " +
										 (triangles[j + 2] + 1) + "/" + (triangles[j + 2] + 1) + "/" + (triangles[j + 2] + 1) + " " +
										 (triangles[j + 1] + 1) + "/" + (triangles[j + 1] + 1) + "/" + (triangles[j + 1] + 1));
					}
				}
			}
		}
	}
}