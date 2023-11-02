using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Gridder_Island10 : MonoBehaviour
{
	[Header ("Variables:")]

	[SerializeField]
	private int gridX = 2;
	[SerializeField]
	private int gridY = 2;
	[SerializeField]
	private float cellSize = 1f;

	private Vector2 GridBtmLeft { get { return new Vector2 (-gridX / 2 * cellSize, -gridY / 2 * cellSize); } }

	public Vector2Int GridCount () { return new Vector2Int (gridX, gridY); }
	public float CellSize () { return cellSize; }
	public Vector2 GridBottomLeft () { return GridBtmLeft; }

	public Vector2 Get_UnitPositionOnGrid (Vector2 mousePos)
	{
		return UnitPositionOnGrid (gridX, gridY, cellSize, GridBtmLeft, mousePos);
	}
	public Vector2 Get_WorldPosition (Vector2 mousePos)
	{
		return WorldPosition (
			GridBtmLeft, 
			cellSize,
			UnitPositionOnGrid (gridX, gridY, cellSize, GridBtmLeft, mousePos));
	}

	public bool OnGrid (Vector2 pos)
	{
		return pos.x > -gridX / 2 * cellSize &&
			pos.x < gridX / 2 * cellSize &&
			pos.y < gridY / 2 * cellSize &&
			pos.y > -gridY / 2 * cellSize;
	}

	private Vector2Int UnitPositionOnGrid (int countX, int countY, float cellSize, Vector2 gridBL, Vector2 mousePos)
	{
		Vector2Int pos = new Vector2Int (99999, 99999);

		for (int i = 0; i < countX + 1; i++)
		{
			if (gridBL.x + cellSize * i > mousePos.x)
			{
				pos.x = i;
				break;
			}
		}

		for (int i = 0; i < countY + 1; i++)
		{
			if (gridBL.y + cellSize * i > mousePos.y)
			{
				pos.y = i;
				break;
			}
		}

		return pos;
	}

	private Vector2 WorldPosition (Vector2 gridBtmLeft, float cSize, Vector2 unitPos)
	{
		Vector2 pos = new Vector2 (0, 0);

		pos.x = gridBtmLeft.x + cSize * unitPos.x - (cSize / 2f);
		pos.y = gridBtmLeft.y + cSize * unitPos.y - (cSize / 2f);

		return pos;
	}

	#region Debug ____________________________________________________________

	[Header ("Debug:")]

	[SerializeField]
	private bool debug;

	[SerializeField]
	public Vector2 unitPosOnGrid = new Vector2 (99999, 99999);
	[SerializeField]
	public Vector2 worldPos = new Vector2 (99999, 99999);

	private void Update ()
	{
		if (!debug) return;

		DrawGrid (gridX, gridY, cellSize, GridBtmLeft, Color.yellow);
	}

	public void DrawGrid (int countX, int countY, float cellSize, Vector2 gbl, Color gridCol)
	{
		for (int i = 0; i < countX + 1; i++)
		{
			Debug.DrawLine (
				new Vector2 (gbl.x + cellSize * i, countY / 2 * cellSize),
				new Vector2 (gbl.x + cellSize * i, -countY / 2 * cellSize), gridCol);
		}

		for (int i = 0; i < countY + 1; i++)
		{
			Debug.DrawLine (
				new Vector2 (-countX / 2 * cellSize, gbl.y + cellSize * i),
				new Vector2 (countX / 2 * cellSize, gbl.y + cellSize * i), gridCol);
		}
	}

	#endregion
}
