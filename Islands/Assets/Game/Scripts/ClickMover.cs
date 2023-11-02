using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMover : MonoBehaviour
{
	[Header("Components:")]

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private Transform master;

	[Header ("Scripts:")]

	[SerializeField]
	private Gridder_Island10 gridder;

	[Header("Variables:")]

	[SerializeField]
	private float speed = 4f;

	// Coroutines

	private IEnumerator do_ClickMove_Linear;

	private void Update()
	{
		if (Input.GetMouseButton(0))
		{
			// Get Mouse Position
			Vector2 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

			// Check if On Grid
			if (!gridder.OnGrid (mousePos)) return;

			// End Existing Movement
			if (do_ClickMove_Linear != null) StopCoroutine(do_ClickMove_Linear);

			// Set Target Position
			Vector2 targPos = gridder.Get_WorldPosition (mousePos);

			// Start New Movement
			do_ClickMove_Linear = ClickMove_Linear(master.position, targPos);
			StartCoroutine(do_ClickMove_Linear);
		}
	}

	private IEnumerator ClickMove_Linear(Vector2 startPos, Vector2 targPos)
	{
		Vector2 dir = (targPos - startPos).normalized;

		while (Vector2.Distance(master.position, targPos) > 0.01f)
		{
			Vector2 nextPos = new Vector2(master.position.x, master.position.y) + dir * speed * Time.deltaTime;

			if (dir.x > 0 && master.position.x > targPos.x) break;
			if (dir.x < 0 && master.position.x < targPos.x) break;
			if (dir.y > 0 && master.position.y > targPos.y) break;
			if (dir.y < 0 && master.position.y < targPos.y) break;

			master.position = new Vector3(nextPos.x, nextPos.y, master.position.z);

			yield return null;
		}

		//print ("End.");
	}
}
