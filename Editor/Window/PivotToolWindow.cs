using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.Window
{
	/// <summary>
	/// The base of the editor tool here the window will be drawled from and also the correct size
	/// </summary>
	public class PivotToolWindow : EditorWindow
	{
		/// <summary>
        /// String of Window name witch is the main tab
		/// </summary>
		const string window = "Tools/";
        /// <summary>
        /// String of detools name witch is company name
        /// </summary>
        const string deTools = "DeTools/";

        /// <summary>
        /// string of the tool name
        /// </summary>
        const string ToolName = "PivotTool";

        /// <summary>
        /// height of the tool that may be drawn this is the min the max will be calculated with the maxMultiplication
        /// </summary>
        const int height = 300;

        /// <summary>
        /// Width of the tool that may be drawn this is the min the max will be calculated with the maxMultiplication
        /// </summary>
        const int width = 400;

        /// <summary>
        /// maxMultiplication is the multiplication factor for the max rect size of the tool window
        /// </summary>
        const int maxMultiplication = 2;

		[MenuItem(window+deTools+ToolName)]
		private static void Init()
		{
			PivotToolEditor window = GetWindow<PivotToolEditor>();
			window.titleContent = new GUIContent(ToolName);
			window.minSize = new Vector2(width, height);
			window.maxSize = new Vector2(width * maxMultiplication, height * maxMultiplication);
			window.Show();
		}
    }
}