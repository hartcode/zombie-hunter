using UnityEngine;
using System.Collections;

public class ScaleChange : MonoBehaviour {
	public Vector3 startScale = new Vector3(1.25f,1.25f,1f);
	public Vector3 endScale = new Vector3(2f,2f,2f);
	public float duration = 1.0F;
	public bool change = true;
	protected float startTime;
	public Transform transformToScaleChange = null;
	public float delay;
	public bool RandomDelay = false;
	protected bool go = false;

	public void Start () {
		if (transformToScaleChange == null) {
			transformToScaleChange = this.transform;
		}
		if (transformToScaleChange == null) {
			throw new MissingComponentException ("Missing Transform Component");
		}
		transformToScaleChange.localScale  = startScale;
		startTime = Time.time;
		if (RandomDelay) {
			delay = Random.value;
		}
	}

	// Update is called once per frame
	void Update () {
		if (!go && change && Time.time - startTime > delay) {
			startTime = Time.time;
			go = true;
		} else {
			transformToScaleChange.localScale = startScale;
		}

		if (change && go) {
			float t = (Time.time - startTime) / duration;
			Vector3 lerpedScale = Vector3.Lerp (startScale, endScale, Mathf.SmoothStep (0, 1, t));
			transformToScaleChange.localScale = lerpedScale;
		}
	}

	public bool Change()
	{ 
		bool retval = false;
		if (change != true) {
			change = true;
			retval = true;
		}
		return retval;
	}
}
