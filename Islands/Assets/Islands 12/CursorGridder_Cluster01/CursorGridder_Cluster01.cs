using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CursorGridder_Cluster01 : MonoBehaviour
{
	[Header ("Components:")]

	[SerializeField]
	private Camera cam;

	[Header ("Scripts:")]

	[SerializeField]
	private Gridder_Island10 gridder;

	private void Update ()
	{
		Color gridCol = Color.yellow;

		Vector2 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

		// Check On Grid

		bool onGrid = true;

		Vector2Int gridCount = gridder.GridCount ();

		float cellSize = gridder.CellSize ();

		Vector2 gbl = gridder.GridBottomLeft ();

		if (!gridder.OnGrid (mousePos))
		{
			gridCol = Color.red;
			onGrid = false;
		}

		if (!onGrid) return;

		// Get Position on Grid

		gridder.unitPosOnGrid = gridder.Get_UnitPositionOnGrid (mousePos);
		gridder.worldPos = gridder.Get_WorldPosition (mousePos);

		DrawCursorLocation (gbl, cellSize, mousePos);

		// Reset

		//gridder.Set_UnitPositionOnGrid (new Vector2 (99999, 99999));
		//gridder.Set_WorldPosition (new Vector2 (99999, 99999));
	}

	#region Debug Drawing __________________________________________________

	private void DrawCursorLocation (Vector2 gbl, float cellSize, Vector2 mousePos)
	{
		Color gridCol = Color.green;

		float pointOffset = gridder.CellSize () / 1.5f;

		Vector2 unitPos = gridder.Get_UnitPositionOnGrid (mousePos);
		
		Vector2 bl = new Vector2 (gbl.x + cellSize * (unitPos.x - 1) + pointOffset, gbl.y + cellSize * unitPos.y - pointOffset);
		Vector2 br = new Vector2 (gbl.x + cellSize * unitPos.x - pointOffset, gbl.y + cellSize * unitPos.y - pointOffset);
		Vector2 tl = new Vector2 (gbl.x + cellSize * (unitPos.x - 1) + pointOffset, gbl.y + cellSize * (unitPos.y - 1) + pointOffset);
		Vector2 tr = new Vector2 (gbl.x + cellSize * unitPos.x - pointOffset, gbl.y + cellSize * (unitPos.y - 1) + pointOffset);

		// Hori Top Btm
		Debug.DrawLine (bl, br, gridCol);
		Debug.DrawLine (tl, tr, gridCol);

		// Vert Left Right
		Debug.DrawLine (tl, bl, gridCol);
		Debug.DrawLine (tr, br, gridCol);
	}

	#endregion
}
