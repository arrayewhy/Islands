using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlasher_Island11 : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer sr;
	[SerializeField]
	private int flashCount = 2;
	[SerializeField]
	private float delayTime = 0.1f;

	[Header ("Debug:")]

	[SerializeField]
	private bool start;
	[SerializeField]
	private bool stop;

	private bool flashing;

	// Coroutines

	private IEnumerator do_Flash;

	private void Update ()
	{
		if (start)
		{
			start = false;

			if (flashing) return;

			do_Flash = Flash (sr, flashCount, delayTime);
			StartCoroutine (do_Flash) ;
		}
	}

	private IEnumerator Flash (SpriteRenderer sr,  int flashes = 0, float delay = 0.05f)
	{
		flashing = true;

		float initAlpha = sr.color.a;
		float currAlpha = initAlpha;

		// Infinite Flashing

		if (flashes == 0)
		{
			while (enabled)
			{
				currAlpha *= -1f;

				sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, currAlpha);

				for (float timer = 0; timer < delay; timer += Time.deltaTime)
				{
					CheckStop (sr, initAlpha);
					yield return null;
				}

				yield return null;
			}
		}

		// Flash Sprite

		for (int i = flashes; i > 0; i--)
		{
			currAlpha *= -1f;

			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, currAlpha);

			for (float timer = 0; timer < delay; timer += Time.deltaTime)
			{
				CheckStop (sr, initAlpha);
				yield return null;
			}

			yield return null;
		}

		sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, initAlpha);

		// Reset

		flashing = false;

		do_Flash = null;
	}

	private void CheckStop (SpriteRenderer sr, float initAlpha)
	{
		if (stop)
		{
			stop = false;

			sr.color = new Color (sr.color.r, sr.color.g, sr.color.b, initAlpha);

			// Reset

			StopCoroutine (do_Flash);
			do_Flash = null;

			flashing = false;
		}
	}
}