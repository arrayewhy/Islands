using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadRotater2D_Island04 : MonoBehaviour
{
	[Header ("Components:")]

	[SerializeField]
	private Camera cam;

	[Header ("Parts:")]

	[SerializeField]
	private Transform eyes;
	private Vector3 eyesHome;

	[SerializeField]
	private Transform mouth;
	private Vector3 mouthHome;

	[SerializeField]
	private Transform brows;
	private Vector3 browsHome;

	[SerializeField]
	private Transform face;

	[Header ("Debug:")]

	public bool debug = true;
	public Transform debugObj;

	[Range (-1.571f, 1.571f)]
	public float t;
	public float sinVal;
	public Transform sinTestObj;

	[Range (0, 4f)]
	public float mouseDistFromEyeBase;

	private void Start ()
	{
		eyesHome = eyes.position;
		mouthHome = mouth.position;
		browsHome = brows.position;
	}

	private void Update ()
	{
		sinVal = Mathf.Sin (t);
		sinTestObj.position = new Vector3 (sinVal, sinTestObj.position.y, sinTestObj.position.z);

		float speed = 4f;
		transform.Translate (new Vector3 (Input.GetAxis ("Horizontal"), 0) * speed * Time.deltaTime);

		// Mouse Pos

		Vector3 mPos = cam.ScreenToWorldPoint (Input.mousePosition);
		mPos = new Vector3 (mPos.x, mPos.y, face.position.z);
		Vector3 mHozi = new Vector3 (mPos.x, face.position.y, face.position.z);
		Vector3 mVert = new Vector3 (face.position.x, mPos.y, face.position.z);

		// Eyes

		mouseDistFromEyeBase = Vector3.Distance (eyesHome, mPos);
		if (mouseDistFromEyeBase > 4f) mouseDistFromEyeBase = 4f;

		float eyeThresholdX = 0.4f;
		float eyeThresholdY = 0.2f;

		if (Mathf.Abs (mHozi.x - eyesHome.x) < eyeThresholdX)
			eyes.position = new Vector3 (eyesHome.x + mPos.x, eyes.position.y, eyes.position.z);

		if (Mathf.Abs (mVert.y - eyesHome.y) < eyeThresholdY)
			eyes.position = new Vector3 (eyes.position.x, eyesHome.y + mPos.y, eyes.position.z);

		// Mouth

		float mouthThresholdX = 0.4f;
		float mouthThresholdY = 0.2f;

		if (Mathf.Abs (mHozi.x - mouthHome.x) < mouthThresholdX)
		{
			mouth.position = new Vector3 (mouthHome.x + mPos.x, mouth.position.y, mouth.position.z);
		}

		if (Mathf.Abs (mVert.y - mouthHome.y) < mouthThresholdY)
		{
			mouth.position = new Vector3 (mouth.position.x, mouthHome.y + mPos.y, mouth.position.z);
		}

		// Brows

		float browsThresholdX = 0.4f;
		float browsThresholdY = 0.2f;

		if (Mathf.Abs (mHozi.x - browsHome.x) < browsThresholdX)
		{
			brows.position = new Vector3 (browsHome.x + mPos.x, brows.position.y, brows.position.z);
		}

		if (Mathf.Abs (mVert.y - browsHome.y) < browsThresholdY)
		{
			brows.position = new Vector3 (brows.position.x, browsHome.y + mPos.y, brows.position.z);
		}

		if (!debug) return;

		debugObj.position = mPos;
	}
}