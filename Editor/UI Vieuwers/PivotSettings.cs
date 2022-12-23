using DeTools.PivotTool.Service;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.UIVieuwer
{
	/// <summary>
	/// contains the gui of the settings
	/// </summary>
	public class PivotSettings
	{
		/// <summary>
		/// foldout boolean
		/// </summary>
		private static bool pivotMovement = true, undoRedoSaves = true, positionInfo = true,componentSettings=true;
		/// <summary>
		/// sliderLenght playerpref key
		/// </summary>
		public static string sliderLenght { get; } = "Slider Lenght";

		/// <summary>
		/// setting boolean that wil be used in an toggle
		/// </summary>
		public static bool useDistanceInfo { get; private set; } = false;
		/// <summary>
		/// setting boolean that wil be used in an toggle
		/// </summary>
		public static bool newPositionInfo { get; private set; } = false;
		/// <summary>
		/// setting boolean that wil be used in an toggle
		/// </summary>
		public static bool oldPositionInfo { get; private set; } = false;

		/// <summary>
		/// setting boolean that wil be used in an toggle
		/// </summary>
		public static bool centerCollider { get; private set; } = false;
		/// <summary>
		/// setting boolean that wil be used in an toggle
		/// </summary>
		public static bool centerNavmesh { get; private set; } = false;


		/// <summary>
		/// string that contains "Use distance info"
		/// </summary>
		const string DistanceInfoText = "Use distance info";
		/// <summary>
		/// string that contains "Use old position info"
		/// </summary>
		const string oldPosInfoText = "Use old position info";
		/// <summary>
		/// string that contains "Use new position info"
		/// </summary>
		const string newPosInfoText = "Use new position info";

		/// <summary>
		/// string that contains "Pivot Movement"
		/// </summary>
		const string pivotMovementText = "Pivot Movement";
		/// <summary>
		/// string that contains "Undo/Redo Saves"
		/// </summary>
		const string undoRedoSavesText = "Undo/Redo Saves";
		/// <summary>
		/// string that contains "Position Info"
		/// </summary>
		const string positionInfoText = "Position Info";
		/// <summary>
		/// string that contains "Component Settings"
		/// </summary>
		const string componantText = "Component Settings";


		/// <summary>
		/// string that contains "Max Undo Saves"
		/// </summary>
		const string maxPositionText = "Max Undo Saves";
		/// <summary>
		/// string that contains "Undo Distance"
		/// </summary>
		const string distanceText = "Undo Distance";

		/// <summary>
		/// string that contains "Set center of colliders"
		/// </summary>
		const string centerColliderText = "Set center of colliders";
		/// <summary>
		/// string that contains "Set center of navmesh"
		/// </summary>
		const string centerNavText = "Set center of navmesh";


		/// <summary>
		/// draws the ui of pivotsettings class
		/// </summary>
		public static void DrawUI()
		{
			pivotMovement = EditorGUILayout.Foldout(pivotMovement, pivotMovementText);
			if (pivotMovement)
			{
				PivotMovement.lenghtOfSlider = FloatInputField(sliderLenght, PivotMovement.lenghtOfSlider);
			}
			undoRedoSaves = EditorGUILayout.Foldout(undoRedoSaves, undoRedoSavesText);
			if (undoRedoSaves)
			{
				UndoRedoPosition.maxPositionSave = Mathf.RoundToInt(FloatInputField(maxPositionText, UndoRedoPosition.maxPositionSave));
				UndoRedoPosition.undoRedoDistance = FloatInputField(distanceText, UndoRedoPosition.undoRedoDistance);
			}
			positionInfo = EditorGUILayout.Foldout(positionInfo, positionInfoText);
			if (positionInfo)
			{
				useDistanceInfo = GUILayout.Toggle(useDistanceInfo, DistanceInfoText);
				oldPositionInfo = GUILayout.Toggle(oldPositionInfo, oldPosInfoText);
				newPositionInfo = GUILayout.Toggle(newPositionInfo, newPosInfoText);
			}

			componentSettings = EditorGUILayout.Foldout(componentSettings, componantText);
			if (componentSettings)
			{
				centerCollider = GUILayout.Toggle(centerCollider, centerColliderText);
				centerNavmesh = GUILayout.Toggle(centerNavmesh, centerNavText);
			}
		}

		/// <summary>
		/// draws an FloatInputField field 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		/// <returns>the out come of the float field</returns>
		private static float FloatInputField(string name, float value)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label(name);
			value = EditorGUILayout.FloatField(value);
			GUILayout.EndHorizontal();
			SaveFloatValue(name, value);
			return value;
		}

		/// <summary>
		/// saves an float in the playerprefs
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		private static void SaveFloatValue(string name, float value)
		{
			PlayerPrefs.SetFloat(name, value);
		}
	}
}