using DeTools.PivotTool.Handlers;
using DeTools.PivotTool.Window;
using System;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.UIVieuwer
{
	/// <summary>
	/// PivotMovement contains the ui tab that manages the position of the pivot by sliders or other calculations
	/// </summary>
	public class PivotUtilitys
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
		const string positionSliderText = "Position Sliders";
		/// <summary>
		///  is an string that contains "Sliders"
		/// </summary>
		const string rotationSliderText = "Rotation Sliders";
		/// <summary>
		///  is an string that contains "Buttons"
		/// </summary>
		const string pivotButtonText = "Buttons";


		/// <summary>
		///  is an string that contains "Gizmo X: "
		/// </summary>
		const string gizmoX = "X: ";
		/// <summary>
		///  is an string that contains "Gizmo Y: "
		/// </summary>
		const string gizmoY = "Y: ";
		/// <summary>
		///  is an string that contains "Gizmo Z: "
		/// </summary>
		const string gizmoZ = "Z: ";

		/// <summary>
		/// contains the space that is between the movement sliders
		/// </summary>
		const float spaceBetweenSlider = 15;
		/// <summary>
		/// this is the lenght of the sliders also the - so by default its  -25 0 +25
		/// </summary>
		public static float lenghtOfSlider = 25;

		/// <summary>
		/// foldout boolean
		/// </summary>
		private static bool generalMoveOptions = true, multiPivotTool = true,positionSliders = true, rotationSliders = true, pivotButtons = true;

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

			positionSliders = EditorGUILayout.Foldout(positionSliders, positionSliderText);
			GUILayout.EndHorizontal();
			if (positionSliders)
			{
				DrawPositionSliders();
				GUILayout.Space(10);
				PivotToolEditor.AddHorizontalLine(Color.black);
			}

			GUILayout.BeginHorizontal();

			rotationSliders = EditorGUILayout.Foldout(rotationSliders, rotationSliderText);
			GUILayout.EndHorizontal();
			if (rotationSliders)
            {
				DrawRotationSliders();
				GUILayout.Space(20);
				PivotToolEditor.AddHorizontalLine(Color.black);
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
					PositionHandle.TargetPosition = targetObject.transform.position;
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
			float x = DrawFloatFields(gizmoX,PositionHandle.TargetPosition.x, spaceBetweenSlider);
			PositionHandle.TargetPosition = new Vector3(x,PositionHandle.TargetPosition.y, PositionHandle.TargetPosition.z);
			x = DrawSlider(PositionHandle.TargetPosition.x, spaceBetweenSlider, -lenghtOfSlider, lenghtOfSlider);
			PositionHandle.TargetPosition = new Vector3(x, PositionHandle.TargetPosition.y, PositionHandle.TargetPosition.z);


			float y = DrawFloatFields(gizmoY, PositionHandle.TargetPosition.y, spaceBetweenSlider);
			PositionHandle.TargetPosition = new Vector3(x,y, PositionHandle.TargetPosition.z);
			y = DrawSlider(PositionHandle.TargetPosition.y, spaceBetweenSlider, -lenghtOfSlider, lenghtOfSlider);
			PositionHandle.TargetPosition = new Vector3(x,y, PositionHandle.TargetPosition.z);


			float z = DrawFloatFields(gizmoZ, PositionHandle.TargetPosition.z, spaceBetweenSlider);
			PositionHandle.TargetPosition = new Vector3(x, y, z);
			z = DrawSlider(PositionHandle.TargetPosition.z, spaceBetweenSlider, -lenghtOfSlider, lenghtOfSlider);
			PositionHandle.TargetPosition = new Vector3(x, y, z);
		}

		/// <summary>
		/// Draws the rotation sliders
		/// </summary>
		private static void DrawRotationSliders()
		{
			float x = DrawFloatFields(gizmoX, PositionHandle.TargetRotation.eulerAngles.x, spaceBetweenSlider);
			PositionHandle.TargetRotation = Quaternion.Euler(new Vector3(x, PositionHandle.TargetRotation.eulerAngles.y, PositionHandle.TargetRotation.eulerAngles.z));
			x = DrawSlider(PositionHandle.TargetRotation.eulerAngles.x, spaceBetweenSlider,0, 90);
			PositionHandle.TargetRotation = Quaternion.Euler(new Vector3(x, PositionHandle.TargetRotation.eulerAngles.y, PositionHandle.TargetRotation.eulerAngles.z));


			float y = DrawFloatFields(gizmoY, PositionHandle.TargetRotation.eulerAngles.y, spaceBetweenSlider);
			PositionHandle.TargetRotation = Quaternion.Euler(new Vector3(x, y, PositionHandle.TargetRotation.eulerAngles.z));
			y = DrawSlider( PositionHandle.TargetRotation.eulerAngles.y, spaceBetweenSlider,0, 360);
			PositionHandle.TargetRotation = Quaternion.Euler(new Vector3(x, y, PositionHandle.TargetRotation.eulerAngles.z));


			float z = DrawFloatFields(gizmoZ, PositionHandle.TargetRotation.eulerAngles.z, spaceBetweenSlider);
			PositionHandle.TargetRotation = Quaternion.Euler(new Vector3(x, y, z));
			z = DrawSlider(PositionHandle.TargetRotation.eulerAngles.z, spaceBetweenSlider, 0,360);
			PositionHandle.TargetRotation = Quaternion.Euler(new Vector3(x, y, z));
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
			PositionHandle.TargetPosition = center + Selection.activeTransform.position;
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
		/// <param name="maxSliderValue"></param>
		/// <param name="minSliderValue"></param>
		/// <returns>returns the outcome of the slider</returns>
		private static float DrawSlider(float value, float spaceAmount, float minSliderValue, float maxSliderValue)
		{
			float returnFloat = value;
			returnFloat = GUILayout.HorizontalSlider(value, minSliderValue, maxSliderValue);
			GUILayout.Space(spaceAmount);
			return (float)Math.Round(returnFloat * 100f) / 100f;
		}

		/// <summary>
		/// draws an slider
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <param name="spaceAmount"></param>
		/// <returns></returns>
		private static float DrawFloatFields(string name, float value, float spaceAmount)
        {
			float returnFloat = value;
			GUILayout.BeginHorizontal();
			GUILayout.Space(spaceAmount);
			GUILayout.Label(name);
			returnFloat = EditorGUILayout.FloatField(returnFloat);
			GUILayout.Space(10);
			GUILayout.EndHorizontal();
			return (float)Math.Round(returnFloat * 100f) / 100f;
		}
	}
}