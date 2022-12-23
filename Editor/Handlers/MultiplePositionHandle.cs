using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.Handlers
{
	/// <summary>
	/// MultiplePositionHandle contains an function that wil calculate the center of an sertan amount of positions and also handles these position gizmos
	/// </summary>
	public class MultiplePositionHandle : Editor
    {
        /// <summary>
        /// bool to check if the multiPivot tool is enabled
        /// </summary>
        public static bool EnableMultiPivot = false;
        /// <summary>
        /// amount of positions that wil be displayed when tool is enabled
        /// </summary>
        public static int amountOfHandles = 2;
        /// <summary>
        /// list that saves the position vector3 
        /// </summary>
        public static List<Vector3> position = new List<Vector3>();

        /// <summary>
        /// string that contains "Handle "
        /// </summary>
        private const string _HANDLETEXT = "Handle ";

		/// <summary>
		/// string that contains "Center Position"
		/// </summary>
		const string CENTERPOSITIONTEXT = "Center Position";
        /// <summary>
        /// drawscene function wil be drawn by the sceneguimanager
        /// </summary>
        /// 
        public static void DrawScene()
        {
            CheckList();
            DrawHandles();
        }

        /// <summary>
        /// Amount that wil be added to the multiplePosition tool
        /// </summary>
        /// <param name="amount"></param>
        public static void AddHandleAmount(int amount)
        {
            amountOfHandles += amount;

            if (amountOfHandles <= 1)
            {
                amountOfHandles = 2;
            }

            for (int i = amountOfHandles; i < position.Count; i++)
            {
                position.Remove(position[i]);
            }
        }

        /// <summary>
        /// sets the target position to the center
        /// </summary>
        public static void SetCenter()
        {
            PositionHandle.targetPosition = CalculateCenter();

        }

        /// <summary>
        /// calculates the center from the current position list
        /// </summary>
        /// <returns> vec3 center</returns>
        public static Vector3 CalculateCenter()
        {
            Vector3 center = Vector3.zero;

            foreach (Vector3 pos in position)
            {
                center += pos;
            }

            return center /= position.Count;
        }

        /// <summary>
        /// checks if the list is not empty
        /// </summary>
        private static void CheckList()
        {
            if (position.Count < amountOfHandles)
            {
                position.Add(Vector3.zero);
            }
        }

        /// <summary>
        /// draws gui handlers
        /// </summary>
        private static void DrawHandles()
        {
            if (EnableMultiPivot)
            {
                Tools.hidden = true;
                EditorGUI.BeginChangeCheck();
                List<Vector3> tempList = new List<Vector3>();
                for (int i = 0; i < position.Count; i++)
                {
                    tempList.Add(Handles.PositionHandle(position[i], Quaternion.identity));
                    Handles.Label(position[i], _HANDLETEXT + (i + 1).ToString());
                }

                if (EditorGUI.EndChangeCheck())
                {
                    position = tempList;
                }


                GUI.color = Color.magenta;
                Vector3 center = CalculateCenter();
                if (center != Vector3.zero)
                {
                    Handles.PositionHandle(center, Quaternion.identity);
                    Handles.Label(center, CENTERPOSITIONTEXT);
                }
            }
            else
            {
                if (!PositionHandle.showPositionTool)
                {
                    Tools.hidden = false;
                }
            }
        }
    }
}