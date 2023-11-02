using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder_Island03 : MonoBehaviour
{
	[Header ("Components:")]

	[SerializeField]
	private Transform master;
	[SerializeField]
	private Transform target;

	public Vector3[] ShortestPath (Vector3 start, Vector3 end, float checkDist = 2f, int rotAng = 5)
	{
		Vector3[] leftPath = PathPoints (start, end, checkDist, 1, rotAng);
		Vector3[] rightPath = PathPoints (start, end, checkDist, -1, rotAng);

		/// Shorter path gets returned if it has more than 1 point.
		/// Else, other path gets returned if it has more than 1 point.
		/// Else, we are inside a collider, so return Empty Array.

		if (rightPath.Length < leftPath.Length && rightPath.Length > 1)
		{
			// Draw Paths
			for (int i = 0; i < rightPath.Length - 1; i++)
				Debug.DrawLine (rightPath[i], rightPath[i + 1], Color.green);
			for (int i = 0; i < leftPath.Length - 1; i++)
				Debug.DrawLine (leftPath[i], leftPath[i + 1], Color.grey);

			return rightPath;
		}
		else if (leftPath.Length < 2) return new Vector3[0];

		if (leftPath.Length < rightPath.Length && leftPath.Length > 1)
		{
			// Draw Paths
			for (int i = 0; i < rightPath.Length - 1; i++)
				Debug.DrawLine (rightPath[i], rightPath[i + 1], Color.grey);
			for (int i = 0; i < leftPath.Length - 1; i++)
				Debug.DrawLine (leftPath[i], leftPath[i + 1], Color.green);

			return leftPath;
		}
		else if (rightPath.Length < 2) return new Vector3[0];

		// Draw Paths
		for (int i = 0; i < rightPath.Length - 1; i++)
			Debug.DrawLine (rightPath[i], rightPath[i + 1], Color.yellow);
		for (int i = 0; i < leftPath.Length - 1; i++)
			Debug.DrawLine (leftPath[i], leftPath[i + 1], Color.green);

		return leftPath;
	}

	public Vector3[] PathPoints (Vector3 start, Vector3 end, float checkDist, int rotDir, int rotAng)
	{
		List<Vector3> path = new List<Vector3> ();
		Vector3 castPoint = start;
		Vector3 dir = Vector3.Normalize (end - castPoint);
		RaycastHit2D hit = Physics2D.Raycast (castPoint, dir);

		/// If NO obstacle is found, way to Target is clear.
		/// Return Start and End points as Path.

		if (hit.transform == null) return new Vector3[2] { start, end };

		// Check if the Start Point is Inside the Collider
		if (Vector3.Distance (castPoint, hit.point) < 0.1f) return new Vector3[0];

		// Add Start Point as First Path Point
		path.Add (start);

		/// While we CANNOT see the End Point:
		/// 1. Check for colliders in front of the Master.
		/// 2. If there is a collider, change Direction, and check again.
		///		Repeat until no obstacle is found.
		/// 3. At which point, add the Cast Point as a Path Point
		///		and step the Cast Point forward, and start again from Step 1.
		///	4. When End point can be seen, add it to Path Points, and return.

		while (Physics2D.Raycast (castPoint, dir).transform != null)
		{
			while (Physics2D.Raycast (castPoint, dir, checkDist).transform != null)
			{
				// Rotate Direction Vector
				dir = Quaternion.Euler (0, 0, rotDir * rotAng) * dir;
			}

			// Update Cast Point
			castPoint += dir * checkDist;

			// Add Cast Point to Path Points
			path.Add (castPoint);

			dir = Vector3.Normalize (end - castPoint);
		}

		// Add End Point as Last Path Point
		path.Add (end);

		return path.ToArray ();
	}

#if UNITY_EDITOR

	[Header ("Debug:")]

	[SerializeField]
	private bool debug;
	private Vector3[] currPath;

	private void Update ()
	{
		if (!debug) return;
		currPath = ShortestPath (master.position, target.position);
	}

#endif
}