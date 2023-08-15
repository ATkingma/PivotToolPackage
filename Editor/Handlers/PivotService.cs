using DeTools.PivotTool.Export;
using DeTools.PivotTool.Handlers;
using DeTools.PivotTool.UIVieuwer;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.Service
{
	/// <summary>
	/// pivot service changes the pivot of the mesh
	/// </summary>
	public class PivotService
	{
		/// <summary>
		/// 
		/// </summary>
		const string UNDO_ADJUST_PIVOT = "Move Pivot";
		/// <summary>
		/// string that contains (Clone) this is what needs to be replaced in the mesh name with copyString
		/// </summary>
		const string cloneString = "(Clone)";
		/// <summary>
		/// string that contains (Copy) that what replaces the mesh cloneString
		/// </summary>
		const string copyString = "(Copy)";

		/// <summary>
		/// this is the general call for setting an new pivot and also calls SetMeshInfo and SetChildInfo
		/// </summary>
		/// <param name="pivotPosition"></param>
		public static void SetPivot(Vector3 pivotPosition,Quaternion pivotRotation)
		{
			Transform SelectedObject = Selection.activeTransform;
			Vector3 oldpos = SelectedObject.position;
			pivotPosition -= oldpos;

			if (DataChecker.IsPrefab(SelectedObject))
			{
				Debug.LogWarning("Modifying prefabs directly is not allowed, Drag the mesh you want to change in the scene instead!");
				return;
			}

			if (pivotPosition == SelectedObject.position)
			{
				Debug.LogWarning("Pivot hasn't changed!");
				return;
			}

			SelectedObject.position -= oldpos;

			MeshFilter meshFilter = SelectedObject.GetComponent<MeshFilter>();

			if (!DataChecker.IsNull(meshFilter) && !DataChecker.IsNull(meshFilter.sharedMesh))
			{
				Undo.RecordObject(meshFilter, UNDO_ADJUST_PIVOT);

				SetMeshInfo(meshFilter, pivotPosition);
				SetMeshRotation(meshFilter, pivotRotation);
			}

			SetChildInfo(SelectedObject);
			UndoRedoPivot.AddOldPivot(meshFilter.sharedMesh);
			SelectedObject.position = pivotPosition + oldpos;


			if (PivotSettings.centerCollider)
			{
				ColliderHandle.SetColliderCenter(SelectedObject, meshFilter);
			}

			if (PivotSettings.centerNavmesh)
			{
				NavMeshHandle.SetNavmesh(SelectedObject, pivotPosition);
			}
		}

		/// <summary>
		/// sets the bounds of the mesh
		/// </summary>
		/// <param name="meshFilter"></param>
		/// <param name="pivotPos"></param>
		private static void SetMeshInfo(MeshFilter meshFilter, Vector3 pivotPos)
		{
			Mesh mesh = EditorWindow.Instantiate(meshFilter.sharedMesh);
			meshFilter.sharedMesh = mesh;

			Vector3[] vertices = mesh.vertices;
			Vector3[] normals = mesh.normals;
			Vector4[] tangents = mesh.tangents;

			if (pivotPos != Vector3.zero)
			{
				Vector3 deltaPosition = -pivotPos;
				for (int i = 0; i < vertices.Length; i++)
				{
					vertices[i] += deltaPosition;
				}
			}

			mesh.vertices = vertices;
			mesh.normals = normals;
			mesh.tangents = tangents;

			mesh.RecalculateBounds();

			string meshName = mesh.name;

			meshName = meshName.Replace(cloneString, string.Empty);
			meshName = meshName.Replace(copyString, string.Empty);
			meshName += copyString;

			mesh.name = meshName;
		}

		/// <summary>
		/// set the position for the children
		/// </summary>
		/// <param name="SelectedObject"></param>
		/// <returns></returns>
		private static Transform[] SetChildInfo(Transform SelectedObject)
		{
			Transform[] children = new Transform[SelectedObject.childCount];
			Vector3[] childrenPositions = new Vector3[children.Length];
			Quaternion[] childrenRotations = new Quaternion[children.Length];
			for (int i = children.Length - 1; i >= 0; i--)
			{
				children[i] = SelectedObject.GetChild(i);
				childrenPositions[i] = children[i].position;
				childrenRotations[i] = children[i].rotation;

				Undo.RecordObject(children[i], UNDO_ADJUST_PIVOT);
			}

			Undo.RecordObject(SelectedObject, UNDO_ADJUST_PIVOT);

			for (int i = 0; i < children.Length; i++)
			{
				children[i].position = childrenPositions[i];
				children[i].rotation = childrenRotations[i];
			}
			return children;
		}

		public static void SetnewMesh(Mesh mesh)
		{
			Selection.activeTransform.GetComponent<MeshFilter>().sharedMesh = mesh;
		}

		public static void SetMeshRotation(MeshFilter meshFilter, Quaternion pivotRotation)
		{
			meshFilter.transform.rotation = pivotRotation;
			Vector3[] vertices = meshFilter.sharedMesh.vertices;
			for (int i = 0; i < vertices.Length; i++)
			{
				vertices[i] = pivotRotation * vertices[i];
			}
			meshFilter.sharedMesh.vertices = vertices;

			meshFilter.sharedMesh.RecalculateNormals();
			meshFilter.sharedMesh.RecalculateBounds();
			meshFilter.sharedMesh.Optimize();
		}
	}
}