using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.Service
{
	/// <summary>
	/// undoRedoPivot contains everything to undo/redo the pivot position
	/// </summary>
	public class UndoRedoPivot
	{
		/// <summary>
		/// the amount of pivots that wil be saved
		/// </summary>
		public static int amountOfSavedPivots = 50;

		/// <summary>
		/// oldPivots that are saved
		/// </summary>
		private static List<Mesh> savedOldPivots = new List<Mesh>();
		/// <summary>
		/// savedNewPivots that are saved
		/// </summary>
		private static List<Mesh> savedNewPivots = new List<Mesh>();

		/// <summary>
		/// reset saved pivot positions
		/// </summary>
		public static void ResetSaved()
		{
			savedOldPivots.Clear();
			savedNewPivots.Clear();
		}

		/// <summary>
		/// set the amount that may be saved
		/// </summary>
		/// <param name="amount"></param>
		public static void SetSavedPivotAmount(int amount)
		{
			amountOfSavedPivots = amount;

			if (amountOfSavedPivots <= 0)
			{
				amountOfSavedPivots = 1;
			}
		}

		/// <summary>
		/// add an mesh to the list oldPivot
		/// </summary>
		/// <param name="PosToAdd"></param>
		public static void AddOldPivot(Mesh PosToAdd)
		{
			savedOldPivots.Add(PosToAdd);
			if (savedOldPivots.Count > amountOfSavedPivots)
			{
				for (int i = 0; i < amountOfSavedPivots - savedOldPivots.Count; i++)
				{
					savedOldPivots.RemoveAt(i);
				}
			}
		}

		/// <summary>
		/// add an mesh to the list newPivot
		/// </summary>
		/// <param name="PosToAdd"></param>
		public static void AddNewPivot(Mesh PosToAdd)
		{
			savedNewPivots.Add(PosToAdd);
			if (savedNewPivots.Count > amountOfSavedPivots)
			{
				for (int i = 0; i < amountOfSavedPivots - savedNewPivots.Count; i++)
				{
					savedOldPivots.RemoveAt(i);
				}
			}
		}

		/// <summary>
		/// returns an mesh from the oldpivot list
		/// </summary>
		/// <returns></returns>
		public static Mesh GetOldPivot()
		{
			if (savedOldPivots.Count <= 0)
			{
				return Selection.activeTransform.GetComponent<MeshFilter>().sharedMesh;
			}
			Mesh oldPivot = savedOldPivots[0]; ;
			savedOldPivots.RemoveAt(0);
			return oldPivot;
		}

		/// <summary>
		/// returns an mesh from the newpivot list
		/// </summary>
		/// <returns></returns>
		public static Mesh GetNewPivot()
		{
			if (savedNewPivots.Count <= 0)
			{
				return Selection.activeTransform.GetComponent<MeshFilter>().sharedMesh;
			}

			Mesh newPivot = savedNewPivots[0];
			savedNewPivots.RemoveAt(0);
			return newPivot;
		}
	}
}