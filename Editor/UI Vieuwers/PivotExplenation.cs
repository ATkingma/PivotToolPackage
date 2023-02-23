using DeTools.PivotTool.Window;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.UIViewer
{
    /// <summary>
    /// Contains a tab that explains the way the tool works
    /// </summary>
    public class PivotExplanation
    {
        /// <summary>
        /// A string that contains "Pivot tool"
        /// </summary>
        const string pivotToolName = "Pivot Adjuster";

        /// <summary>
        /// A string that contains "Pivot Movement"
        /// </summary>
        const string pivotMovementName = "Pivot Movement";

        /// <summary>
        /// A string that contains "Settings"
        /// </summary>
        const string settingsName = "Settings";

        /// <summary>
        /// Font size of the content
        /// </summary>
        const int contentTextSize = 11;

        /// <summary>
        /// Foldout boolean
        /// </summary>
        private static bool pivotTool = true, pivotMovement = true, settings = true;

        /// <summary>
        /// Draws UI for the Pivot Explanation class
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

            pivotMovement = EditorGUILayout.Foldout(pivotMovement, pivotMovementName);
            if (pivotMovement)
            {
                PivotMovement();
                GUILayout.Space(10);
            }
            PivotToolEditor.AddHorizontalLine(Color.black);

            settings = EditorGUILayout.Foldout(settings, settingsName);
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
        /// Shows explanation about the Pivot Tool
        /// </summary>
        private static void PivotTool()
        {
            DrawTextSpace("Pivot tool is the main tab.", contentTextSize);
            DrawTextSpace("Here you can enable the Gizmo with the top button.", contentTextSize);
            DrawTextSpace("", contentTextSize);
            DrawTextSpace("After selecting a GameObject, you can reset the pivot simply by setting the Gizmo on the desired location.", contentTextSize);
            DrawTextSpace("", contentTextSize);
            DrawTextSpace("Vertex snapping: There is a small feature that will be activated with the C key.", contentTextSize);
            DrawTextSpace("For some reason, it didn't want to work on the selected object. Therefore, I created this.", contentTextSize);
            DrawTextSpace("If you want to use vertex snap on deselected objects, you can just use the normal button V for this.", contentTextSize);
        }

        /// <summary>
        /// Shows explanation about the Pivot Movement
        /// </summary>
        private static void PivotMovement()
        {
            DrawTextSpace("Pivot Movement is a tab for the precision movement of the Gizmo.", contentTextSize);
            DrawTextSpace("You can edit the slider size in the Settings tab to make it more precise or a bigger range.", contentTextSize);
            DrawTextSpace("", contentTextSize);
            DrawTextSpace("Using the sliders for position will cause a small delay sometimes. Still looking to fix this.", contentTextSize);
            DrawTextSpace("Furthermore, the position will be saved and used in the other tab Pivot Tool.", contentTextSize);
        }
        /// <summary>
        /// Shows explanation about the Settings.
        /// </summary>
        private static void Settings()
        {
            DrawTextSpace("At the settings tab, you can edit everything that is possible to make your own flow the best you can.", contentTextSize);
            DrawTextSpace("", contentTextSize);
            DrawTextSpace("Miss something? You can email me for suggestions at detoolsassetstore@gmail.com.", contentTextSize);
            DrawTextSpace("Please add in the title which tool you're suggesting a feature for.", contentTextSize);
        }

        /// <summary>
        /// Function for drawing a text space with content size.
        /// </summary>
        /// <param name="content">The content to be displayed.</param>
        /// <param name="contentSize">The size of the content.</param>
        private static void DrawTextSpace(string content, int contentSize)
        {
            GUILayout.BeginVertical();
            GUI.skin.label.fontSize = contentSize;
            GUILayout.Label(content);
            GUILayout.EndVertical();
        }

    }
}