using DeTools.PivotTool.Service;
using DeTools.PivotTool.UIVieuwer;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.Handlers
{
	/// <summary>
	/// Position handle is an custom editor from the gameobject type and uses the OnSceneGUIManager function for most of its work withing this tool will be the handling of the new pivot position
	/// </summary>
	public class PositionHandle:Editor
	{
        /// <summary>
        ///  this is an string that contains NewPosition + the position of the old pivot position
        /// </summary>
        const string oldPositionText = "OldPosition ";
		/// <summary>
		///  this is an string that contains NewPosition + the position of the new pivot position
		/// </summary>
        const string newPositionText = "NewPosition ";
		/// <summary>
		/// this is an string that contains Distance + the distance from the old position and the new position
		/// </summary>
		const string distanceText = "Distance ";

        /// <summary>
        /// this is an string that contains Old Pivot Position and will be used to show the user witch position is the old and new position
        /// </summary>
        const string oldPos = "Old Pivot Position";
        /// <summary>
        /// this is an string that contains New Pivot Position and will be used to show the user witch position is the old and new position
        /// </summary>
        const string newPos = "New Pivot Position";
		/// <summary>
		/// this is an string that contains an string key that will be check for input for vertex snapping
		/// </summary>
		const string vertexSnapInput = "c";
        /// <summary>
        /// this is an string that contains an string key that will be check for input for unDoing
        /// </summary>
        const string undoInput = "-";

        /// <summary>
        /// this is an string that contains an string key that will be check for input for redDoing
        /// </summary>
        const string redoInput = "+";

        /// <summary>
        /// this is an bool that will be set when the position needs to be shown and may move
        /// </summary>
        public static bool ShowPositionTool; 
		
		/// <summary>
        /// this is an bool that will be set when the Rotation needs to be shown and may move
        /// </summary>
        public static bool ShowRotationTool;
		/// <summary>
		/// this is an bool that will be set when input is received an vertex snapping can be used
		/// </summary>
		public static bool VertSnapping;

		/// <summary>
		/// the position of the new pivot
		/// </summary>
		public static Vector3 TargetPosition { get { return m_TargetPosition; } set { m_TargetPosition = value; } }

		/// <summary>
		/// the private value of the new pivot
		/// </summary>
		private static Vector3 m_TargetPosition = new Vector3(0f, 0f, 0f);

		/// <summary>
		/// latest saved position
		/// </summary>
		private static Vector3 latestSaved;
		/// <summary>
		/// the old Gameobject that was or is selected
		/// </summary>
		private static GameObject oldObject = null;

		/// <summary>
		/// the position of the new pivot
		/// </summary>
		public static Quaternion TargetRotation { get { return m_TargetRotation; } set { m_TargetRotation = value; } }

		/// <summary>
		/// the private value of the new pivot
		/// </summary>
		private static Quaternion m_TargetRotation = Quaternion.identity;

		/// <summary>
		/// within this function some the position will be reset because of user experience
		/// </summary>
		public static void ResetPosition()
		{
			m_TargetPosition = Vector3.zero;
			if (Selection.activeTransform == null)
			{
				return;
			}
			GameObject targetObject = Selection.activeTransform.gameObject;
			if (targetObject == null)
			{
				return;
			}
			m_TargetPosition = targetObject.transform.position;
			if (oldObject != targetObject)
			{
				TargetPosition = targetObject.gameObject.transform.position;
				m_TargetRotation = targetObject.gameObject.transform.rotation;
			}
		}

		/// <summary>
		/// within this function some the rotation will be reset because of user experience
		/// </summary>
		public static void ResetRotation()
		{
			if (Selection.activeTransform == null)
			{
				return;
			}
			GameObject targetObject = Selection.activeTransform.gameObject;
			if (targetObject == null)
			{
				return;
			}
			if (oldObject != targetObject)
			{
				m_TargetRotation = targetObject.gameObject.transform.rotation;
			}
		}

		/// <summary>
		/// drawscene function draws the scene gui and wil be called from sceneguimanager
		/// </summary>
		public static void DrawScene()
		{
			if (ShowPositionTool)
			{

				if (Selection.activeTransform == null)
				{
					return;
				}

				GameObject targetObject = Selection.activeTransform.gameObject;


				Tools.hidden = true;

				DrawGuids(targetObject);

				CheckInput(targetObject);

				if (CheckOldObject(targetObject)||Selection.activeTransform==null|| (Selection.activeTransform.GetComponent<MeshFilter>()==null))
				{
					return;
				}

				UndoRedoPivot.AddNewPivot(Selection.activeTransform.GetComponent<MeshFilter>().sharedMesh);
			}
			else
			{
				if (!MultiplePositionHandle.EnableMultiPivot)
				{
					Tools.hidden = false;
				}
			}
		}

		/// <summary>
		/// Draws some debug guids like old/new pivot position also distance
		/// </summary>
		/// <param name="targetObject"></param>
		private static void DrawGuids(GameObject targetObject)
		{
			GUI.color = Color.red;
			EditorGUI.BeginChangeCheck();

			Handles.PositionHandle(targetObject.transform.position, Quaternion.identity);

			Handles.Label(targetObject.transform.position, oldPos);

			GUI.color = Color.green;

            if (ShowRotationTool)
            {
				Quaternion newTargetRotation = Handles.RotationHandle(m_TargetRotation, TargetPosition);
				m_TargetRotation = newTargetRotation;
            }

            Vector3 newTargetPosition = Handles.PositionHandle(TargetPosition, m_TargetRotation);

			if (EditorGUI.EndChangeCheck())
			{
				if(Vector3.Distance(latestSaved, newTargetPosition) > UndoRedoPosition.undoRedoDistance)
				{
					latestSaved = newTargetPosition;
					UndoRedoPosition.SavePosition(latestSaved);
				}
                TargetPosition = newTargetPosition;
			}
			Handles.Label(newTargetPosition, newPos);

			GUI.color = Color.blue;
			Vector3 offset = Vector3.zero;
			if (PivotSettings.oldPositionInfo)
			{
				offset += new Vector3(0, 0.2f, 0);
				Handles.Label(newTargetPosition += offset,oldPositionText + targetObject.transform.position.ToString());
			}

			if (PivotSettings.newPositionInfo)
			{
				offset += new Vector3(0, 0.2f, 0);
				Handles.Label(newTargetPosition + offset, newPositionText + newTargetPosition.ToString());
			}

			if (PivotSettings.useDistanceInfo)
			{
				offset += new Vector3(0, 0.2f, 0);
				Handles.Label(newTargetPosition + offset, distanceText + Vector3.Distance(targetObject.transform.position, newTargetPosition).ToString());
			}

            if (PivotSettings.newRotationInfo)
            {
				offset += new Vector3(0, 0.2f, 0);
				Handles.Label(newTargetPosition + offset, "Rotation" + m_TargetRotation.eulerAngles.ToString());
			}
		}

		/// <summary>
		/// checks if the given gamobject is equals to the oldGameobject returns true if not
		/// </summary>
		private static bool CheckOldObject(GameObject targetObject)
		{
			if (oldObject != targetObject)
			{
				ResetPosition();
				ResetRotation();
				oldObject = targetObject;
				return true;
			}

			if (targetObject == null)
			{
				return false;
			}
			return false;
		}

		/// <summary>
		/// checks for input and calls GetClosestVertex
		/// </summary
		private static void CheckInput(GameObject targetObject)
		{
			var e = Event.current;
			if (e.isKey)
			{
				if (e.character.ToString() == vertexSnapInput)
				{
					Mesh targetMesh = targetObject.GetComponent<MeshFilter>().sharedMesh;

					if (targetMesh == null || targetObject == null)
					{
						return;
					}
					Vector3 closestVertex = GetClosestVertex(targetMesh, TargetPosition, targetObject.transform.position, targetObject.transform.localScale);

					TargetPosition = closestVertex;
				}
				if (e.character.ToString() == undoInput)
				{
					TargetPosition = UndoRedoPosition.ReturnPosition(TargetPosition);
				}
				else if (e.character.ToString() == redoInput)
				{
					TargetPosition = UndoRedoPosition.ReturnRedoPosition();
				}
                
            }
		}

		/// <summary>
		/// this is an function that will check the meshes vertex for the closes vertex from the given position
		/// </summary>
		/// <param name="mesh"></param>
		/// <param name="position"></param>
		/// <param name="meshOffset"></param>
		/// <returns></returns>
		private static Vector3 GetClosestVertex(Mesh mesh, Vector3 position, Vector3 meshOffset,Vector3 scale)
		{
			float minDistance = Mathf.Infinity;

			Vector3 closestVertex = Vector3.zero;

			foreach (Vector3 vertex in mesh.vertices)
			{
				Vector3 scaledVertex = Vector3.Scale(vertex, scale);

				float distance = Vector3.Distance(scaledVertex + meshOffset, position);

				if (distance < minDistance)
				{
					minDistance = distance;
					closestVertex = scaledVertex + meshOffset;
				}
			}
			return closestVertex;
		}
	}
}