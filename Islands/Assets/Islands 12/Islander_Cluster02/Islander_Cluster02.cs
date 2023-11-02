using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Islander_Cluster02 : MonoBehaviour
{
	[Header ("Components:")]

	[SerializeField]
	private Camera cam;

	[Header ("Scripts:")]

	[SerializeField]
	private Gridder_Island10 gridder;

	[Header ("Tiles:")]

	[SerializeField]
	private Sprite defaultTile;
	[SerializeField]
	private Sprite centerTile;
	[SerializeField]
	private Sprite topTile;
	[SerializeField]
	private Sprite topRightTile;
	[SerializeField]
	private Sprite rightTile;
	[SerializeField]
	private Sprite bottomRightTile;
	[SerializeField]
	private Sprite bottomTile;
	[SerializeField]
	private Sprite bottomLeftTile;
	[SerializeField]
	private Sprite leftTile;
	[SerializeField]
	private Sprite topLeftTile;

	private void Update ()
	{
		Vector2 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

		if (!gridder.OnGrid (mousePos)) return;
	}
}
