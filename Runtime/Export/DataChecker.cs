using UnityEditor;
using UnityEngine;

namespace DeTools.PivotTool.Export
{
    /// <summary>
    /// DataChecker is control of checking if something is something and then returning an bool
    /// </summary>
    public class DataChecker
	{
        /// <summary>
        /// checks if an object is an prefab
        /// </summary>
        public static bool IsPrefab(Object obj)
		{
			return AssetDatabase.Contains(obj);
		}

        /// <summary>
        /// checks if something is null
        /// </summary>
        public static bool IsNull(Object obj)
		{
			return obj == null || obj.Equals(null);
		}
	}
}