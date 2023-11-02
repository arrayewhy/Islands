using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Riser_Island01 : MonoBehaviour
{
	// Components

	[SerializeField]
	private Image uiImage;

    // Variables

    [SerializeField]
    private bool on;

	[SerializeField]
	private float elevation = 90f;
	[SerializeField]
	private float speed = 400f;

	// Coroutines

	private IEnumerator do_Mover { get { return Mover (); } }

	private void Start ()
	{
		StartCoroutine (do_Mover);
	}

	private IEnumerator Mover ()
	{
		Vector3 startPos = uiImage.rectTransform.localPosition;
		Vector3 endPos = startPos + new Vector3 (0, elevation, 0);

		while (enabled)
		{
			if (on)
			{
				uiImage.rectTransform.localPosition = startPos;

				while (on)
				{
					// If not at target height

					if (uiImage.rectTransform.localPosition.y < endPos.y)
					{
						Vector3 next = uiImage.rectTransform.localPosition + new Vector3 (0, speed * Time.deltaTime, 0);

						if (next.y < endPos.y)
						{
							uiImage.rectTransform.localPosition = next;
						}
						else
						{
							uiImage.rectTransform.localPosition = endPos;

							on = false;
						}
					}

					yield return null;
				}
			}

			yield return null;
		}
	}
}
