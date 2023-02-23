using DeTools.PivotTool.Handlers;
using System;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.UIVieuwer
{
	/// <summary>
	/// PivotMovement contains the ui tab that manages the position of the pivot by sliders or other calculations
	/// </summary>
	public class PivotMovement
	{
		/// <summary>
		/// is an string that contains "ResetPosition"
		/// </summary>
		const string resetPosText = "ResetPosition";
		/// <summary>
		/// is an string that contains "Center Pivot Position"
		/// </summary>
		const string centerPivotText = "Center Pivot Position";


		/// <summary>
		///  is an string that contains "General"
		/// </summary>
		const string generalText = "General";
		/// <summary>
		///  is an string that contains "MultiplePivot"
		/// </summary>
		const string multiplePivotText = "MultiplePivot";
		/// <summary>
		///  is an string that contains "Sliders"
		/// </summary>
		const string sliderText = "Sliders";
		/// <summary>
		///  is an string that contains "Buttons"
		/// </summary>
		const string pivotButtonText = "Buttons";


		/// <summary>
		///  is an string that contains "Gizmo X: "
		/// </summary>
		const string gizmoX = "Gizmo X: ";
		/// <summary>
		///  is an string that contains "Gizmo Y: "
		/// </summary>
		const string gizmoY = "Gizmo Y: ";
		/// <summary>
		///  is an string that contains "Gizmo Z: "
		/// </summary>
		const string gizmoZ = "Gizmo Z: ";

		/// <summary>
		/// contains the space that is between the movement sliders
		/// </summary>
		const float spaceBetweenSlider = 10;
		/// <summary>
		/// this is the lenght of the sliders also the - so by default its  -25 0 +25
		/// </summary>
		public static float lenghtOfSlider = 25;

		/// <summary>
		/// foldout boolean
		/// </summary>
		private static bool generalMoveOptions = true, multiPivotTool = true,pivotSlider = true,pivotButtons = true;

		/// <summary>
		/// Draws the ui of the pivotmovement class
		/// </summary>
		public static void DrawUI()
		{
			GetSaves();

			if (Selection.activeTransform != null
			&& Selection.activeTransform.gameObject != null &&
			Selection.activeTransform.gameObject.transform != null &&
			Selection.activeTransform.gameObject.transform.GetComponent<MeshFilter>() != null)
			{
				generalMoveOptions = EditorGUILayout.Foldout(generalMoveOptions, generalText);
				if (generalMoveOptions)
				{
					DrawGeneralMoveOptions();
				}
				multiPivotTool = EditorGUILayout.Foldout(multiPivotTool, multiplePivotText);
				if (multiPivotTool)
				{
					DrawMultiPivotTool();
				}
			}
			else
			{
				GUILayout.Label("Select an GameObject");
			}
		}

		/// <summary>
		/// draws the general movement options
		/// </summary>
		private static void DrawGeneralMoveOptions()
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(4);

			pivotSlider = EditorGUILayout.Foldout(pivotSlider, sliderText);
			GUILayout.EndHorizontal();
			if (pivotSlider)
			{
				DrawPositionSliders();
				GUILayout.Space(20);
			}

			GUILayout.BeginHorizontal();
			GUILayout.Space(4);
			pivotButtons = EditorGUILayout.Foldout(pivotButtons, pivotButtonText);
			GUILayout.EndHorizontal();

			if (pivotButtons)
			{
				PivotToolMain.SetPivotButton(20);
				GUILayout.Space(10);
				if (GUILayout.Button(centerPivotText))
				{
					CenterPivot();
				}
				GUILayout.Space(10);
				GUILayout.ExpandWidth(true);
				if (GUILayout.Button(resetPosText))
				{
					GameObject targetObject = Selection.activeTransform.gameObject;
					PositionHandle.targetPosition = targetObject.transform.position;
				}
			}

			GUILayout.Space(10);
		}
		
		/// <summary>
		/// draws the multiPivotTool
		/// </summary>
		private static void DrawMultiPivotTool()
		{
			string multiPosToolTitel = MultiplePositionHandle.EnableMultiPivot ? "Disable MultiPivot Tool" : "Enable MultiPivot Tool";
			GUILayout.BeginHorizontal();
			if (GUILayout.Button("-", GUILayout.Width(100)))
			{
				MultiplePositionHandle.AddHandleAmount(-1);
			}
			if (GUILayout.Button(multiPosToolTitel))
			{
				MultiplePositionHandle.EnableMultiPivot = !MultiplePositionHandle.EnableMultiPivot;
			}
			if (GUILayout.Button("+",  GUILayout.Width(100)))
			{
				MultiplePositionHandle.AddHandleAmount(1);
			}
			GUILayout.EndHorizontal();
			if (GUILayout.Button("CalculatePivot"))
			{
				MultiplePositionHandle.SetCenter();
			}
		}

		/// <summary>
		/// Draws the position sliders
		/// </summary>
		private static void DrawPositionSliders()
		{
			PositionHandle.targetPosition = new Vector3(
			DrawSlider(gizmoX + PositionHandle.targetPosition.x,
			PositionHandle.targetPosition.x, spaceBetweenSlider, lenghtOfSlider),
			PositionHandle.targetPosition.y, PositionHandle.targetPosition.z);


			PositionHandle.targetPosition = new Vector3(
			PositionHandle.targetPosition.x,
			DrawSlider(gizmoY + PositionHandle.targetPosition.y, PositionHandle.targetPosition.y, spaceBetweenSlider, lenghtOfSlider),
			PositionHandle.targetPosition.z);


			PositionHandle.targetPosition = new Vector3(
			PositionHandle.targetPosition.x,
			PositionHandle.targetPosition.y,
			DrawSlider(gizmoZ + PositionHandle.targetPosition.z, PositionHandle.targetPosition.z, spaceBetweenSlider, lenghtOfSlider));
		}

		/// <summary>
		/// centers the pivot on the selected mesh
		/// </summary>
		private static void CenterPivot()
		{
			//add scale
			Mesh mesh = Selection.activeTransform.gameObject.transform.GetComponent<MeshFilter>().sharedMesh;
			Bounds bounds = mesh.bounds;
			Vector3 center = (bounds.min + bounds.max) / 2;
			if (Selection.activeTransform == null)
			{
				return;
			}
			Vector3 localScale = Selection.activeTransform.localScale;
			center = Vector3.Scale(center, localScale);
			PositionHandle.targetPosition = center + Selection.activeTransform.position;
		}

		/// <summary>
		/// gets the saves from the lenght of the sliders
		/// </summary>
		private static void GetSaves()
		{
			lenghtOfSlider = PlayerPrefs.GetFloat(PivotSettings.sliderLenght, 25f);
		}

		/// <summary>
		/// draws an slider
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="spaceAmount"></param>
		/// <param name="sliderValue"></param>
		/// <returns>returns the outcome of the slider</returns>
		private static float DrawSlider(string name, float value, float spaceAmount, float sliderValue)
		{
			GUILayout.Label(name);
			float returnValue = GUILayout.HorizontalSlider(value, -sliderValue, sliderValue);
			GUILayout.Space(spaceAmount);
			return (float)Math.Round(returnValue * 100f) / 100f; ;
		}
	}
}