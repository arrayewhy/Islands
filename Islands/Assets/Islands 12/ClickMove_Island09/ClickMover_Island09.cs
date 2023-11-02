using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMover_Island09 : MonoBehaviour
{
	[Header ("Components:")]

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private Transform master;

	[Header ("Variables:")]

	[SerializeField]
	private float speed = 4f;

	// Coroutines

	private IEnumerator do_ClickMove_Cos, do_ClickMove_Linear;

	private void Update ()
	{
		if (Input.GetMouseButtonDown (0))
		{
			if (do_ClickMove_Cos != null) StopCoroutine (do_ClickMove_Cos);

			do_ClickMove_Cos = ClickMove_Cos (master.position, cam.ScreenToWorldPoint (Input.mousePosition));
			StartCoroutine (do_ClickMove_Cos);
		}

		//if (Input.GetMouseButton (0))
		//{
		//	if (do_ClickMove_Linear != null) StopCoroutine (do_ClickMove_Linear);

		//	do_ClickMove_Linear = ClickMove_Linear (master.position, cam.ScreenToWorldPoint (Input.mousePosition));
		//	StartCoroutine (do_ClickMove_Linear);
		//}
	}

	private IEnumerator ClickMove_Cos (Vector2 startPos, Vector2 targPos)
	{
		// Distance between Start and End

		float fullDist = Vector2.Distance (startPos, targPos);

		// Direction

		Vector2 dir = (targPos - startPos).normalized;

		// While gap between Transform and End is large enough

		float gap = Vector2.Distance (master.position, targPos);

		while (gap > 0.01f)
		{
			float gapNormalized = gap / fullDist;
			
			float gapCos = gapNormalized * 1.571f - 1.571f;
			
			float speedMod = Mathf.Cos (gapCos);

			// Apply Movement

			float moveX = speed * speedMod * Time.deltaTime * dir.x;
			float moveY = speed * speedMod * Time.deltaTime * dir.y;

			master.position += new Vector3 (moveX, moveY);

			// Update Gap

			gap = Vector2.Distance (master.position, targPos);

			yield return null;
		}

		//print ("End.");

		master.position = targPos;
	}

	private IEnumerator ClickMove_Linear (Vector2 startPos, Vector2 targPos)
	{
		Vector2 dir = (targPos - startPos).normalized;

		while (Vector2.Distance (master.position, targPos) > 0.01f)
		{
			Vector2 nextPos = new Vector2 (master.position.x, master.position.y) + dir * speed * Time.deltaTime;

			if (dir.x > 0 && master.position.x > targPos.x) break;
			if (dir.x < 0 && master.position.x < targPos.x) break;
			if (dir.y > 0 && master.position.y > targPos.y) break;
			if (dir.y < 0 && master.position.y < targPos.y) break;

			master.position = new Vector3 (nextPos.x, nextPos.y, master.position.z);

			yield return null;
		}

		//print ("End.");
	}
}
