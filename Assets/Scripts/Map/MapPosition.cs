using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class MapPosition : MonoBehaviour {

	public int originX;
	public int originY;
	public float characterWidth = 0.08f;
	public float characterHeight = 0.16f;
	public MapBlockView mapBlockView;

	public int currentX {
		get { return Mathf.RoundToInt(transform.localPosition.x / characterWidth);}
	}

	public int currentY {
		get { return Mathf.RoundToInt(transform.localPosition.y / -characterHeight);}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (transform.hasChanged) {
			if (originX != currentX || originY != currentY) {
			// Call Move
				Debug.Log("current " + currentX + "," + currentY + " was " + originX + " , " + originY);
				mapBlockView.MoveObject (originX, originY, currentX, currentY, this.gameObject);
				originX = currentX;
				originY = currentY;
			}
			transform.hasChanged = false;
		}
	}
}
