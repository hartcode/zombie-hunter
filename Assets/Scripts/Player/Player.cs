using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	protected int lastplayerx;
	protected int lastplayery;
	public int playerx;
	public int playery;

	// Update is called once per frame
	void Update () {
		lastplayerx = playerx;
		lastplayery = playery;
		playerx = Mathf.RoundToInt (transform.localPosition.x / AsciiMapScript.Instance.characterWidth);
		playery = Mathf.RoundToInt (transform.localPosition.y / -AsciiMapScript.Instance.characterHeight);
		// if player has moved trigger a new path find
		if (playerx != lastplayerx || playery != lastplayery) {
			MessageManager.Instance.PostMessage (this, "playerHasMoved");
		}
	}
}
