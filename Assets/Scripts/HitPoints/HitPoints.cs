using UnityEngine;
using System.Collections;

public class HitPoints : MonoBehaviour {

	public int hitPoints;
	public int maxHitPoints;

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
			/// TODO: destorying the object doesn't give the particle system enough time to fire.
			DestroyObject (this.gameObject);
		}

	}

	public void change(int points) {
		if (points < 0) {
			damage (-1 * points);
		} else {
			heal (points);
		}
	}
}
