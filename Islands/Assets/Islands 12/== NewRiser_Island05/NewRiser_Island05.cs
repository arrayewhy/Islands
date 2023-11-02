using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TransitionType { EaseIn, EaseOut, EaseInIn }

public class NewRiser_Island05 : MonoBehaviour
{
	[Header ("Components:")]

	[SerializeField]
	private Camera cam;

    [SerializeField]
    private Transform buttonTop;

	[SerializeField]
	private Transform master;

	[Header ("Variables:")]

	[SerializeField]
	private TransitionType type;
	[SerializeField]
	private float offset = 0.5f;
	[SerializeField]
	private float speed = 8f;

	[Space (10)]

	[SerializeField]
	private float rangeUp = 0.5f;
	[SerializeField]
	private float rangeDown = 0.5f;
	[SerializeField]
	private float rangeLeft = 0.5f;
	[SerializeField]
	private float rangeRight = 0.5f;

	private Vector3 basePos;

	private void OnEnable ()
	{
		basePos = buttonTop.position;

		switch (type)
		{
			case TransitionType.EaseIn:
				StartCoroutine ("Rise_EaseIn");
				break;
			case TransitionType.EaseOut:
				StartCoroutine ("Rise_EaseInOut");
				break;
			case TransitionType.EaseInIn:
				StartCoroutine ("Rise_EaseIn_EaseIn");
				break;
		}
	}

	private IEnumerator Rise_EaseIn ()
	{
		while (enabled)
		{
			// Get Mouse Position
			Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

			if (MouseEnter (mousePos))
			{
				float currTime = 0f;
				float targTime = 1.571f;

				print ("Mouse Over");

				while (MouseHover (mousePos))
				{
					mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

					if (currTime < targTime)
					{
						currTime += Time.deltaTime * speed;

						buttonTop.position = new Vector3 (basePos.x, basePos.y + Mathf.Sin (currTime) * offset, basePos.z);
					}

					yield return null;
				}

				while (MouseHover (mousePos))
				{
					if (Input.GetMouseButtonDown (0))
					{
						if (currTime < targTime * 2f)
						{
							currTime += Time.deltaTime * speed * 4f;

							buttonTop.position = new Vector3 (basePos.x, basePos.y + Mathf.Sin (currTime) * offset, basePos.z);
						}
					}

					mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

					yield return null;
				}

				while (!MouseHover (mousePos))
				{
					mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

					if (currTime < targTime * 2f)
					{
						currTime += Time.deltaTime * speed;

						buttonTop.position = new Vector3 (basePos.x, basePos.y + Mathf.Sin (currTime) * offset, basePos.z);
					}

					yield return null;
				}
			}

			yield return null;
		}
	}

	private IEnumerator Rise_EaseInOut ()
	{
		while (enabled)
		{
			// Get Mouse Position
			Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

			if (MouseEnter (mousePos))
			{
				float currTime = -3.142f;

				print ("Mouse Over");

				while (MouseHover (mousePos))
				{
					mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

					if (currTime < 0f)
					{
						currTime += Time.deltaTime * speed;

						buttonTop.position = new Vector3 (basePos.x, basePos.y + (Mathf.Cos (currTime) + 1) / 2f * offset, basePos.z);
					}

					yield return null;
				}

				while (!MouseHover (mousePos))
				{
					mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

					if (currTime >= -3.142f)
					{
						currTime -= Time.deltaTime * speed;

						buttonTop.position = new Vector3 (basePos.x, basePos.y + (Mathf.Cos (currTime) + 1) / 2f * offset, basePos.z);
					}

					yield return null;
				}
			}

			yield return null;
		}
	}

	private IEnumerator Rise_EaseIn_EaseIn ()
	{
		while (enabled)
		{
			// Get Mouse Position
			Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

			if (MouseEnter (mousePos))
			{
				float currTime = -1.571f;

				print ("Mouse Over");

				while (currTime < 0f)
				{
					currTime += Time.deltaTime * speed;

					buttonTop.position = new Vector3 (basePos.x, basePos.y + Mathf.Cos (currTime) * offset, basePos.z);

					yield return null;
				}

				currTime = -1.571f;

				// Wait
				while (MouseHover (mousePos))
				{
					mousePos = cam.ScreenToWorldPoint (Input.mousePosition);

					yield return null;
				}

				while (currTime > -3.142f)
				{
					currTime -= Time.deltaTime * speed;

					buttonTop.position = new Vector3 (basePos.x, basePos.y + (Mathf.Cos (currTime) + 1) * offset, basePos.z);

					yield return null;
				}
			}

			yield return null;
		}
	}

	private bool MouseEnter (Vector3 mousePos)
	{
		return mousePos.y < basePos.y + rangeUp &&
				mousePos.y > basePos.y - rangeDown &&
				mousePos.x > basePos.x - rangeLeft &&
				mousePos.x < basePos.x + rangeRight;
	}

	private bool MouseHover (Vector3 mousePos)
	{
		return mousePos.y < basePos.y + rangeUp + offset && // Extended Range by Offset
				mousePos.y > basePos.y - rangeDown && 
				mousePos.x > basePos.x - rangeLeft && 
				mousePos.x < basePos.x + rangeRight;
	}
}
