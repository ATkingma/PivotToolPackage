using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace DeTools.PivotTool.Handlers
{
    /// <summary>
    /// NavMeshHandle handles the navmesh components
    /// </summary>
    public class NavMeshHandle
    {
        private const string tempObjectName = "tempObject";
        private const string UNDO_ADJUST_PIVOT = "Move Pivot";

        public static void SetNavmesh(Transform meshTransform, Vector3 pivotPosition)
        {
            NavMeshObstacle obstacle = meshTransform.GetComponent<NavMeshObstacle>();
            if (obstacle != null)
            {
                GameObject obstacleObj = new GameObject(tempObjectName);
                obstacleObj.transform.SetParent(meshTransform, false);
                EditorUtility.CopySerialized(obstacle, obstacleObj.AddComponent(obstacle.GetType()));
                Undo.RegisterCreatedObjectUndo(obstacleObj, UNDO_ADJUST_PIVOT);
            }
        }
    }
}
