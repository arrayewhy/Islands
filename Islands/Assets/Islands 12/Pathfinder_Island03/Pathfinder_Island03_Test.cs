using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder_Island03_Test : MonoBehaviour
{
	[Header ("Components:")]

	[SerializeField]
	private Transform master;
	[SerializeField]
	private Camera cam;

	private Pathfinder_Island03 Pathfinder { get { return GetComponent<Pathfinder_Island03> (); } }

	// Variables

	private Vector3[] currPath;

	private float speed = 2f;

	//private void Start () { StartCoroutine ("Test"); }

	private IEnumerator Test ()
	{
		while (enabled)
		{
			// Check for new Path
			if (Input.GetMouseButton (0))
			{
				currPath = Pathfinder.ShortestPath (master.position, cam.ScreenToWorldPoint (Input.mousePosition));
			}

			//if (currPath != null) yield return Move ();

			yield return null;
		}
	}

	private IEnumerator Move ()
	{
		for (int i = 0; i < currPath.Length - 1; i++)
		{
			while (Vector3.Dot (Vector3.Normalize (currPath[i + 1] - currPath[i]), Vector3.Normalize (currPath[i + 1] - master.position)) > 0)
			{
				// Check for new Path
				if (Input.GetMouseButton (0))
				{
					currPath = Pathfinder.ShortestPath (master.position, Input.mousePosition);
					yield break;
				}
				else
				{
					// Move from one point to the next

					master.position += Vector3.Normalize (currPath[i + 1] - master.position) * speed * Time.deltaTime;
				}

				yield return null;
			}

			// Position Master at point

			master.position = currPath[i+1];

			yield return null;
		}

		currPath = null;
	}
}