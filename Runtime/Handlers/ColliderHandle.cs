using UnityEditor;
using UnityEngine;
using Transform = UnityEngine.Transform;

namespace DeTools.PivotTool.Handlers
{
	/// <summary>
	/// colliderhandles handles the center of the colliders + adds new mesh to mesh collider
	/// </summary>
	public class ColliderHandle
	{
		///update 1.1.2 make change 2d colliders

		/// <summary>
		/// Sets the center of the 3d colliders that uses center it also changes the mesh in the meshcollider
		/// </summary>
		/// <param name="SelectedObject"></param>
		/// <param name="meshFilter"></param>
		public static void SetColliderCenter(Transform SelectedObject, MeshFilter meshFilter)
		{
			Mesh originalMesh = meshFilter.sharedMesh;

			if (SelectedObject.gameObject.GetComponent<MeshCollider>() != null)
			{
				SelectedObject.gameObject.GetComponent<MeshCollider>().sharedMesh = originalMesh;
			}
			bool changeCenterCollider = true;

			if (changeCenterCollider)
			{
				Bounds bounds = originalMesh.bounds;
				Vector3 center = (bounds.min + bounds.max) / 2;
				if (Selection.activeTransform == null)
				{
					return;
				}
				Vector3 localScale = Selection.activeTransform.localScale;
				center = Vector3.Scale(center, localScale);
				Vector3 centerPosition = center + Selection.activeTransform.position;


				Collider[] colliders = SelectedObject.gameObject.GetComponentsInChildren<Collider>();
				foreach (Collider collider in colliders)
				{
					string colliderType = collider.GetType().ToString();
					switch (colliderType)
					{
						case "UnityEngine.BoxCollider":
							BoxCollider boxCollider = collider as BoxCollider;
							boxCollider.center = centerPosition;
							break;
						case "UnityEngine.CapsuleCollider":
							CapsuleCollider capsuleCollider = collider as CapsuleCollider;
							capsuleCollider.center = centerPosition;
							break;
						case "UnityEngine.SphereCollider":
							SphereCollider sphereCollider = collider as SphereCollider;
							sphereCollider.center = centerPosition;
							break;
						case "UnityEngine.WheelCollider":
							WheelCollider wheelCollider = collider as WheelCollider;
							wheelCollider.center = centerPosition;
							break;
					}
				}
			}
		}
	}
}