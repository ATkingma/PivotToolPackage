using DeTools.PivotTool.Export;
using DeTools.PivotTool.Handlers;
using DeTools.PivotTool.Service;
using DeTools.PivotTool.Window;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.UIVieuwer
{
	/// <summary>
	/// PivotToolMain contains the main tab of the pivot tool here the exporting of the mesh is also dislayed and called from
	/// </summary>
	public class PivotToolMain
	{
		/// <summary>
		/// contains an string "Export"
		/// </summary>
		const string ExportTabName = "Export";

		/// <summary>
		/// contains an string "Save Mesh as Asset"
		/// </summary>
		const string saveAssetText = "Save Mesh as Asset";
		/// <summary>
		/// contains an string "Save Mesh as OBJ"
		/// </summary>
		const string saveOBJText = "Save Mesh as OBJ";
		/// <summary>
		/// contains an string "Save Mesh as fbx"
		/// </summary>
		const string saveFBXext = "Save Mesh as fbx";
		/// <summary>
		/// contains an string "Save Mesh as STL"
		/// </summary>
		const string saveSTLText = "Save Mesh as STL";

		/// <summary>
		/// contains an string "Gizmo"
		/// </summary>
		const string ButtonText = "Gizmo";
		/// <summary>
		/// contains an string "Set New Pivot"
		/// </summary>
		const string SetPivotText = "Set New Pivot";

		/// <summary>
		/// contains an string "Enabled"
		/// </summary>
		const string isOn = "Enabled ";
		/// <summary>
		/// contains an string "Disabled"
		/// </summary>
		const string isOff = "Disabled ";

		/// <summary>
		/// foldout boolean
		/// </summary>
		private static bool ExportTab = true;

		/// <summary>
		/// draws the ui of pivtoolmain class
		/// </summary>
		public static void DrawUI()
		{
			EnableButton();
			if (CheckIfSelected())
			{
				SetPivotButton(20);

				PivotToolEditor.AddHorizontalLine(Color.black);
				ExportTab = EditorGUILayout.Foldout(ExportTab, ExportTabName);
				if (ExportTab)
				{
					SaveMeshButton();
					SaveOBJButton();
					SaveFBXButton();
					SaveSTLButton();
				}
			}
		}

		/// <summary>
		/// enable button handles if the tool shows an pivto the user can move
		/// </summary>
		private static void EnableButton()
		{
			string buttonState = PositionHandle.showPositionTool ? isOff : isOn;

			if (GUILayout.Button(buttonState + ButtonText))
			{
				if (Selection.activeTransform != null)
				{
					PositionHandle.showPositionTool = !PositionHandle.showPositionTool;
					PositionHandle.ResetValues();
					PositionHandle.targetPosition = Selection.activeTransform.position;
				}
			}
		}

		/// <summary>
		/// calls the pivot on the position the handler is on when button pressed
		/// </summary>
		/// <param name="height"></param>
		public static void SetPivotButton(float height)
		{
			if (GUILayout.Button(SetPivotText,GUILayout.Height(height)))
			{
				PivotService.SetPivot(PositionHandle.targetPosition, PivotSettings.centerCollider, PivotSettings.centerNavmesh);
			}
		}

		/// <summary>
		/// calls the save as asset function
		/// </summary>
		private static void SaveMeshButton()
		{
			MeshFilter meshFilter = Selection.activeTransform.gameObject.transform.GetComponent<MeshFilter>();
			if (GUILayout.Button(saveAssetText))
			{
				DataSaver.SaveMesh(SaveFiles.asset, meshFilter);
			}
		}

		/// <summary>
		/// calls the save as obj function
		/// </summary>
		private static void SaveOBJButton()
		{
			MeshFilter meshFilter = Selection.activeTransform.gameObject.transform.GetComponent<MeshFilter>();
			if (GUILayout.Button(saveOBJText))
			{
				DataSaver.SaveMesh(SaveFiles.obj, meshFilter);
			}
		}

		/// <summary>
		/// calls the save as fbx function
		/// </summary>
		private static void SaveFBXButton()
		{
			MeshFilter meshFilter = Selection.activeTransform.gameObject.transform.GetComponent<MeshFilter>();
			if (GUILayout.Button(saveFBXext))
			{
				DataSaver.SaveMesh(SaveFiles.fbx, meshFilter);
			}
		}

		/// <summary>
		/// calls the save as stl function
		/// </summary>
		private static void SaveSTLButton()
		{
			MeshFilter meshFilter = Selection.activeTransform.gameObject.transform.GetComponent<MeshFilter>();
			if (GUILayout.Button(saveSTLText))
			{
				DataSaver.SaveMesh(SaveFiles.stl, meshFilter);
			}
		}

		/// <summary>
		/// checks if the currently sellected is not null and has an meshfilter
		/// </summary>
		private static bool CheckIfSelected()
		{
			if (Selection.activeTransform == null
				|| Selection.activeTransform.gameObject == null ||
				Selection.activeTransform.gameObject.transform == null ||
				Selection.activeTransform.gameObject.transform.GetComponent<MeshFilter>() == null)
			{
				return false;
			}
			return true;
		}
	}
}