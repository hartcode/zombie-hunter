using UnityEngine;
using System.Collections;

public class HitPoints : MonoBehaviour {

	public int hitPoints;
	public int maxHitPoints;
	public SpriteRenderer rrenderer;
	protected float killTime;
	public float duration = 1.0f;
	private bool kill = false;

	public MapPosition mapPosition;
	public void heal (int points)
	{
		if (points < 0) {
			throw new UnityException ("Cannot heal negative points");
		}
		hitPoints += points;
		if (hitPoints > maxHitPoints) {
			hitPoints = maxHitPoints;
		}
	}

	public void damage (int points) {
		if (points < 0) {
			throw new UnityException ("Cannot damage negative points");
		}
		this.hitPoints -= points;

		if (this.hitPoints <= 0) {
			killTime = Time.time;
			kill = true;
			rrenderer.enabled = false;
		}

	}

	public void change(int points) {
		if (points < 0) {
			damage (-1 * points);
		} else {
			heal (points);
		}
	}

	void FixedUpdate() {
		if (kill) {
			float t = (Time.time - killTime);
			
			if (t > duration) {
				if (mapPosition != null) {
					mapPosition.removeFromMap ();
				}
				DestroyObject (this.gameObject);
			}
		}
	}
}
