using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
	[Header ("Components:")]

	[SerializeField]
	private Camera cam;

	[SerializeField]
	private Transform cursor;

	[Header ("Scripts:")]

	[SerializeField]
	private Gridder_Island10 gridder;

	private void Update ()
	{
		Vector2 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

		if (!gridder.OnGrid (mousePos)) return;

		cursor.position = gridder.Get_WorldPosition (mousePos);
	}
}
