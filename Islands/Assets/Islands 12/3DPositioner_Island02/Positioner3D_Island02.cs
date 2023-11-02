/// Basic Tool for getting the 3D World Position of a Raycast Hit.
/// Common Mistake: Raycasts need Rigidbodies!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioner3D_Island02 : MonoBehaviour
{
	[Header ("Components:")]

	[SerializeField]
	private Camera cam;

	[Header ("Variables:")]

	[SerializeField]
	private string targetTag;

	[Header ("Debug:")]

	[SerializeField]
	private Transform debugObject;

#if UNITY_EDITOR
	private void Update ()
	{
		if (Input.GetMouseButton (0))
		{
			Vector3 clickPoint = GetClickPoint ();
			if (debugObject != null) debugObject.position = clickPoint;
			print (clickPoint);
		}
	}
#endif

	public Vector3 GetClickPoint ()
	{
		return RaycastHitPos (cam, Input.mousePosition, targetTag);
	}

	private Vector3 RaycastHitPos (Camera camera, Vector2 screenPos, string targTag = "")
	{
		RaycastHit hit;

		Ray ray = camera.ScreenPointToRay (screenPos);

		if (Physics.Raycast (ray, out hit))
		{
			if (hit.rigidbody != null) // If a Rigidbody is Found
			{
				/// If a Target Tag has NOT been specified,
				/// return the Hit Position.
				/// Else, make sure the Tags Match before returning.

				if (targTag == "") return hit.point;

				if (hit.transform.CompareTag (targTag)) return hit.point;
			}
		}

		return default;
	}
}
