using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.Export
{
	/// <summary>
	/// the supported files that can be saved
	/// </summary>
	public enum SaveFiles
	{
		empty = 0,
		asset = 1,
		obj = 2,
		fbx = 3,
		stl = 4,
	}
}
namespace DeTools.PivotTool.Export
{
	/// <summary>
	/// datasaver contains an sort function that wil be used to chose the right export methode
	/// </summary>
	public class DataSaver
	{
		const string saveText = "Save As";

		public static void SaveMesh(SaveFiles type, MeshFilter meshFilter)
		{
			if (type == SaveFiles.empty)
			{
				return;
			}

			string assetType = type.ToString();

			string savedMeshName = meshFilter.sharedMesh.name;
			string savePath = EditorUtility.SaveFilePanelInProject(saveText, savedMeshName, assetType, string.Empty);
			if (string.IsNullOrEmpty(savePath))
				return;


			Mesh mesh = meshFilter.sharedMesh;
			if (!string.IsNullOrEmpty(AssetDatabase.GetAssetPath(mesh)))
				mesh = EditorWindow.Instantiate(mesh);


			switch (type)
			{
				case SaveFiles.asset:
					AssetDatabase.CreateAsset(mesh, savePath);
					break;
				case SaveFiles.obj:
					ObjExporter.MeshToFile(mesh, savePath);
					break;
				case SaveFiles.fbx:
					FbxExporter.MeshToFbx(meshFilter, savePath);
					break;
				case SaveFiles.stl:
					StlExporter.MeshToStl(mesh, savePath);
					break;
			}

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}