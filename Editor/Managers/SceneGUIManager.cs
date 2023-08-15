using DeTools.PivotTool.Handlers;
using DeTools.PivotTool.Window;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.Manager
{
    //needs to be one onscenegui because it otherwise did not work and also its nice to see everything here

    /// <summary>
    /// draws al the scene ui
    /// </summary>
    [CustomEditor(typeof(MeshFilter))]
    public class SceneGUIManager : Editor
    {
        private void OnSceneGUI()
        {
            PivotToolEditor.CheckInput();
            PositionHandle.DrawScene();
            MultiplePositionHandle.DrawScene();
        }
    }
}