using UnityEngine;
using System.Collections;

public class FlashScale : ScaleChange {
	public int FlashCount = 1;
	private int flashIndex = 0;
	private int flashMax = 0;
	private bool end = false;

	new void Start() {
		base.Start();
		flashMax = FlashCount * 2;
	}

	public new bool Change() {
		bool retval = false;
		if (base.Change ()) {
			flashIndex = 0;
			end = false;
			retval = true;
		}
		return retval;
	}

	// Update is called once per frame
	void Update () {
		if (change) {
			float t = (Time.time - startTime) / duration;
			if (!end) {
				if (t >= 1) {
					startTime = Time.time;
					Vector3 temp = endScale;
					endScale = startScale;
					startScale = temp;
					t = (Time.time - startTime) / duration;
					if (flashIndex < flashMax) {
						flashIndex++;
					} 
					if (flashIndex == flashMax) {
						end = true;
						change = false;
					}
				}


				Vector3 lerpedScale = Vector3.Lerp (startScale, endScale, 
					t);
				transformToScaleChange.localScale = lerpedScale;
			}
		}
	}
}