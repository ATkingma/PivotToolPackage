using System.Linq;
using UnityEngine;
using UnityFBXExporter;

namespace DeTools.PivotTool.Export
{
	/// <summary>
	/// FBX exporter exports fbx files from an meshfilter
	/// </summary>
	public class FbxExporter
	{
        /// <summary>
        /// the type of shader that the material will be made from
        /// </summary>
        const string shaderType = "Standard";
        /// <summary>
        /// the name of the temporary gameobject that will be made
        /// </summary>
        const string tempObjectName = "tempGameObject";

        /// <summary>
        /// creates an fbx file from an mesh filter and uses FBXExporter.ExportgGameOBJtoFBx function from an outside source
        /// </summary>
        public static void MeshToFbx(MeshFilter _meshFilter, string filePath)
		{
			GameObject tempobject = new GameObject();
			tempobject.name = tempObjectName;
			var meshfilter = tempobject.AddComponent<MeshFilter>();
			meshfilter.sharedMesh = _meshFilter.mesh;
			var shader = Shader.Find(shaderType);

			FBXExporter.ExportGameObjToFBX(tempobject, filePath, false, false);

			var objects = GameObject.FindObjectsOfType(typeof(GameObject))
			.Where(obj => obj == tempobject);

			foreach (var obj in objects)
			{
				Object.DestroyImmediate(obj);
			}
		}
	}
}