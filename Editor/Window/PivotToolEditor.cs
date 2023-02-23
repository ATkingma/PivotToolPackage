using DeTools.PivotTool.Handlers;
using DeTools.PivotTool.Service;
using DeTools.PivotTool.UIVieuwer;
using DeTools.PivotTool.UIViewer;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.Window
{
    /// <summary>
    /// this is the main editor where all the drawing of ui starts and also some ui functionality resides inside 
    /// </summary>
    public class PivotToolEditor : EditorWindow
	{
		/// <summary>
		/// string of the header of the main category
		/// </summary>
		const string headerTool = "Pivot Adjuster";
        /// <summary>
        /// string of the sub category 1
        /// </summary>
        const string category1 = "Pivot Movement";
        /// <summary>
        /// string of the sub category 2
        /// </summary>
        const string category2 = "Settings";
        /// <summary>
        /// string of the sub category 3
        /// </summary>
        const string category3 = "Explanation";

        /// <summary>
        /// input string for toggle input check
        /// </summary>
        const string toggleInput = "/";
		/// <summary>
		/// input string for redoing the pivot location
		/// </summary>
		const string redoPivotInput = "x";
		/// <summary>
		/// input string for undoing the pivot location
		/// </summary>
		const string undoPivotInput = "z";

		/// <summary>
		/// Front size of header
		/// </summary>
		const int headerSize = 13;
        /// <summary>
        /// Front size of content
        /// </summary>
        const int contentSize = 11;

        /// <summary>
        /// current selected tab this will be set by the category buttons
        /// </summary>
        private int tabSelected;
        /// <summary>
        /// position of the current scroll position
        /// </summary>
        private Vector2 scrollPos = Vector2.zero;

		private void OnEnable()
		{
			Selection.selectionChanged += Repaint;
		}

		private void OnDisable()
		{
			Selection.selectionChanged -= Repaint;
		}

		private void OnDestroy()
		{
			UndoRedoPivot.ResetSaved();
			UndoRedoPosition.ResetSaved();
			PositionHandle.showPositionTool = false;
			MultiplePositionHandle.EnableMultiPivot = false;
			Tools.hidden = false;
		}

		public void OnGUI()
		{
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
			GUILayout.Space(10);
			GUILayout.BeginHorizontal();
			GUILayout.Space(25);

			CatagoryButtons();

			GUILayout.Space(30);
			GUILayout.EndHorizontal();
			AddHorizontalLine(Color.black);

			GUILayout.BeginVertical();
			DrawCatagory();
			GUILayout.EndVertical();
			EditorGUILayout.EndScrollView();
			CheckInput();
		}

		/// <summary>
		/// Manages the chosen catagory
		/// </summary>
		private void CatagoryButtons()
		{
			GUI.backgroundColor = tabSelected == 0 ? Color.gray : Color.white;
			if (GUILayout.Button(headerTool))
			{
				tabSelected = 0;
			}

			GUI.backgroundColor = tabSelected == 1 ? Color.gray : Color.white;
			if (GUILayout.Button(category1))
			{
				tabSelected = 1;
			}

			GUI.backgroundColor = tabSelected == 2? Color.gray : Color.white;
			if (GUILayout.Button(category2))
			{
				tabSelected = 2;
			}

			GUI.backgroundColor = tabSelected == 3 ? Color.gray : Color.white;
			if (GUILayout.Button(category3))
			{
				tabSelected = 3;
			}
		}

        /// <summary>
        /// functions checks tabSelected variable within a switcase and draws the right ui
        /// </summary>
        private void DrawCatagory()
		{
			CheckInput();
			switch (tabSelected)
			{
				case 0:
					PivotToolMain.DrawUI();
					break;
				case 1:
					PositionHandle.showPositionTool = true;
					PivotMovement.DrawUI();
					break;
				case 2:
					PivotSettings.DrawUI();
					break;
				case 3:
                    PivotExplanation.DrawUI();
					break;
			}
		}

        /// <summary>
        /// Checks if the keyboard makes any input with the toggle input variable as its key
        /// </summary>
        public static void CheckInput()
		{
			
			var e = Event.current;
			if (e.isKey)
			{
				if (e.character.ToString() == toggleInput)
				{
					PositionHandle.showPositionTool = !PositionHandle.showPositionTool;
					PositionHandle.ResetValues();
					PositionHandle.targetPosition = Selection.activeTransform.position;
				}

				if (e.character.ToString() == undoPivotInput)
				{
					UndoRedoPivot.AddNewPivot(Selection.activeTransform.GetComponent<MeshFilter>().sharedMesh);
					PivotService.SetnewMesh(UndoRedoPivot.GetOldPivot());
				}

				if (e.character.ToString() == redoPivotInput)
				{
					UndoRedoPivot.AddOldPivot(Selection.activeTransform.GetComponent<MeshFilter>().sharedMesh);
					PivotService.SetnewMesh(UndoRedoPivot.GetNewPivot());
				}
				
			}
		}

		/// <summary>
		/// Draws an horizontal line in the given color
		/// </summary>
		public static void AddHorizontalLine(Color lineColor)
		{
			GUI.backgroundColor = lineColor;
			EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
			GUI.backgroundColor = Color.white;
		}

		/// <summary>
		/// Draws an Header text
		/// </summary>
		public static void DrawHeader(string headerText)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Space(5);
			GUI.skin.label.fontSize = headerSize;
			GUILayout.Label(headerText);
			GUILayout.EndHorizontal();
			GUI.skin.label.fontSize = contentSize;
		}
	}
}