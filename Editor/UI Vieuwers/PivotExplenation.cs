using DeTools.PivotTool.Window;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.UIVieuwer
{
	/// <summary>
	/// ccontains an tab that explains the way the tool works
	/// </summary>
	public class PivotExplenation
	{
		/// <summary>
		/// an string that contains "Pivot tool"
		/// </summary>
		const string pivotToolName = "Pivot Tool";

		/// <summary>
		/// an string that contains "Pivot Movement"
		/// </summary>
		const string PivotMovementNaam = "Utilitys";

		/// an string that contains "Settings"
		/// </summary>
		const string settingsNaam = "Settings";

		/// <summary>
		/// front size of the content
		/// </summary>
		const int contentText = 11;

		/// <summary>
		/// foldout boolean
		/// </summary>
		private static bool pivotTool = true, pivotMovement = true, settings = true;


		/// <summary>
		/// Draws ui for the Pivot explenation class
		/// </summary>
		public static void DrawUI()
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			GUILayout.BeginVertical();

			pivotTool = EditorGUILayout.Foldout(pivotTool, pivotToolName);
			if (pivotTool)
			{
				PivotTool();
				GUILayout.Space(10);
			}
			PivotToolEditor.AddHorizontalLine(Color.black);

			pivotMovement = EditorGUILayout.Foldout(pivotMovement, PivotMovementNaam);
			if (pivotMovement)
			{
				PivotMovement();
				GUILayout.Space(10);
			}
			PivotToolEditor.AddHorizontalLine(Color.black);

			settings = EditorGUILayout.Foldout(settings, settingsNaam);
			if (settings)
			{
				Settings();
				GUILayout.Space(10);
			}

			PivotToolEditor.AddHorizontalLine(Color.black);
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}

		/// <summary>
		/// Shows explenation about the pivot tool
		/// </summary>
		private static void PivotTool()
		{
			DrawTextSpace("Pivot tool is the main tab. ", contentText);
			DrawTextSpace("Here you can enable the Gizmo with the top button", contentText);
			DrawTextSpace("", contentText);
			DrawTextSpace("After selecting an Gameobject you can reset the pivot simply by setting the gizmo on the desired location.", contentText);
			DrawTextSpace("", contentText);
			DrawTextSpace("Vertex snapping There is an smal feature that wil be activated with the C key", contentText);
			DrawTextSpace("for some reason it didnt want to work on the selected object so there for I created this.", contentText);
			DrawTextSpace("If you want to use vertex snap on deselected objects you can just use the noraml button V for this.", contentText);
		}

		/// <summary>
		/// Shows explenation about the Pivot Movement
		/// </summary>
		private static void PivotMovement()
		{
			DrawTextSpace("Pivot Movement is an tab for the precision movement of the GUID.", contentText);
			DrawTextSpace(" You can edit the slider size in the settings tab to make it more precize or an bigger range. ", contentText);
			DrawTextSpace("", contentText);
			DrawTextSpace("Using the sliders for position wil couse an smal delay sometimes still looking to fix this.", contentText);
			DrawTextSpace("Furthermore the position wil be saved and ussed in the other tab Pivot tool", contentText);
		}

		/// <summary>
		/// Shows explenation about the Settings
		/// </summary>
		private static void Settings()
		{
			DrawTextSpace("At the settings tab you can edit everything that is posible to make your own flow the best you can.", contentText);
			DrawTextSpace("", contentText);
			DrawTextSpace("Miss something? You can email me for suggestions at timme.albert.kingma@gmail.com.\r\n", contentText);
			DrawTextSpace("Please add in the titel witch tool your suggesting an feature for.", contentText);
		}

		/// <summary>
		/// function for drawing an text space with content size
		/// </summary>
		/// <param name="content"></param>
		/// <param name="contentSize"></param>
		private static void DrawTextSpace(string content, int contentSize)
		{
			GUILayout.BeginVertical();
			GUI.skin.label.fontSize = contentSize;
			GUILayout.Label(content);
			GUILayout.EndVertical();
		}
	}
}