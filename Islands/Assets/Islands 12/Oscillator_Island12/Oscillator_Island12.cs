using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator_Island12 : MonoBehaviour
{
	[SerializeField]
	private bool oscillate;

	[SerializeField]
	private float influence = 1f;

	// Coroutines

	private IEnumerator do_Oscillate;

	private void Update ()
	{
		if (oscillate)
		{
			oscillate = false;

			if (do_Oscillate != null) return;

			do_Oscillate = Oscillate ();
			StartCoroutine (do_Oscillate);
		}
	}

	private IEnumerator Oscillate ()
	{
		float customTimeline_Start = Time.time;
		float customTimeline_Value = 0f;

		while (enabled)
		{
			Vector3 tempPos = transform.position;
			tempPos.y = Mathf.Sin (customTimeline_Value) * (EaseIn (customTimeline_Value) * influence);
			transform.position = tempPos;

			customTimeline_Value += Time.deltaTime;

			yield return null;
		}

		do_Oscillate = null;
	}

	private float EaseIn (float tVal)
	{
		if (tVal > 2f) return 1f;

		float easeIn = Mathf.Cos (tVal - 2f);

		return easeIn > 0f ? easeIn : 0f;
	}
}