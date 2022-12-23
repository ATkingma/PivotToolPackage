using System.Collections.Generic;
using UnityEngine;
namespace DeTools.PivotTool.Service
{
    /// <summary>
    /// UndoRedoPosition contains the functions that are needed to undo/redo the position of the handler
    /// </summary>
    public static class UndoRedoPosition
    {
        /// <summary>
        /// The distance that will be checked if its larger then undoRedoDistance then it saves the position
        /// </summary>
        public static float undoRedoDistance = 0.5f;
        /// <summary>
        /// max amount of positions that will be saved
        /// </summary>
        public static int maxPositionSave = 6000;
        /// <summary>vv
        /// Saved position list that keeps track of the undo function
        /// </summary>
        private static List<Vector3> savedPositions = new List<Vector3>();
        /// <summary>
        /// Saved position list that keeps track of the redo function
        /// </summary>
        private static List<Vector3> redoPositions = new List<Vector3>();

        /// <summary>
        /// Ressets the list of positions
        /// </summary>
        public static void ResetSaved()
        {
            savedPositions.Clear();
            redoPositions.Clear();
        }

        /// <summary>
        /// Saves the position of the handle
        /// </summary>
        /// <param name="position"></param>
        public static void SavePosition(Vector3 position)
        {
            if (savedPositions.Count >= maxPositionSave)
            {
                savedPositions.RemoveAt(0);
            }
            savedPositions.Add(position);
        }

        /// <summary>
        /// saves an redo position
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns>an saved position</returns>
        public static Vector3 ReturnPosition(Vector3 currentPosition)
        {
            Vector3 returnPosition = currentPosition;
            if (redoPositions.Count >= maxPositionSave)
            {
                redoPositions.RemoveAt(0);
            }
            redoPositions.Add(currentPosition);

            if (savedPositions.Count > 0)
            {
                returnPosition = savedPositions[savedPositions.Count - 1];
                savedPositions.RemoveAt(savedPositions.Count - 1);
            }
            return returnPosition;
        }

        /// <returns> redo position</returns>
        public static Vector3 ReturnRedoPosition()
        {
            Vector3 returnPosition = Vector3.zero;
            if (redoPositions.Count > 0)
            {
                returnPosition = redoPositions[redoPositions.Count - 1];
                redoPositions.RemoveAt(redoPositions.Count - 1);

            }
            return returnPosition;

        }
    }
}