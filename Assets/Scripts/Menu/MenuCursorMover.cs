using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuCursorMover : MonoBehaviour {

	public float delay;

	protected float startTime;
	protected bool go = false;
	protected FlashColor flashColor;
	public FlashColor position1;
	public FlashColor position2;

	private Vector3 start = new Vector3 (-0.65f, -.4f, 0f);
	private Vector3 bottom = new Vector3(-0.65f, -.8f, 0f);
	private int position = 0;

	void Start() {
		startTime = Time.time;
		flashColor = GetComponent<FlashColor> ();
		if (flashColor == null) {
			throw new MissingComponentException ("Missing FlashColor Component");
		}
		if (position1 == null) {
			throw new MissingReferenceException ("Missing Position 1");
		}
		if (position2== null) {
			throw new MissingReferenceException ("Missing Position 2");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!go && Time.time - startTime > delay) {
			go = true;
			flashColor.change = true;
			position1.change = true;
		}
		if (go) {
			if (Input.GetKey ("down")) {
				transform.localPosition = bottom;
				position = 2;
				flashColor.Restart ();
				position2.Restart();
				position1.change = false;
			}
			if (Input.GetKey ("up")) {
				transform.localPosition = start;
				position = 1;
				flashColor.Restart ();
				position1.Restart();
				position2.change = false;
			}
			if (Input.GetButton ("Fire1")) {
				if (position == 1) {
					// New Game
					SceneManager.LoadScene (1);
				} else if (position == 2) {
					// Load
				}
			}
		}
	}
}
