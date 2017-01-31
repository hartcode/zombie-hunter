using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {
	public Color startColor = Color.white;
	public Color endColor = Color.green;
	protected Color switchingStartColor;
	protected Color switchingEndColor;

	public float duration = 1.0F;
	public bool change = true;
	protected float startTime;
	public SpriteRenderer[] renderersToColorChange = null;

	public void Start () {
		if (renderersToColorChange == null || renderersToColorChange.Length == 0) {
			
			renderersToColorChange = GetComponentsInChildren<SpriteRenderer> (true);
		}
		if (renderersToColorChange == null || renderersToColorChange.Length == 0) {
			throw new MissingComponentException ("Missing SpriteRenderer Component");
		}
		switchingStartColor = startColor;
		switchingEndColor = endColor;
		foreach (SpriteRenderer rendererToColorChange in renderersToColorChange) {
			rendererToColorChange.color = switchingStartColor;
		}

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (change) {
			float t = (Time.time - startTime) / duration;
			Color lerpedColor = Color.Lerp (switchingStartColor, switchingEndColor, Mathf.SmoothStep (0, 1, t));
			foreach (SpriteRenderer rendererToColorChange in renderersToColorChange) {
				rendererToColorChange.color = lerpedColor;
			}
		}
	}

	public virtual bool Change()
	{ 
		bool retval = false;
		if (change != true) {
			change = true;
			retval = true;
	    }
		return retval;
	}

	public void Restart() {
		switchingStartColor = startColor;
		switchingEndColor = endColor;
		this.startTime = Time.time;
		Change ();
	}
}
