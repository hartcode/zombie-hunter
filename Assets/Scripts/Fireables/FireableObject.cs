using UnityEngine;
using System.Collections;


public class FireableObject: MonoBehaviour
{

	public float fireableSpeed = 1.5f;
	public string ShooterTag = "Player";
	protected Vector3 direction;
	protected bool directionSet = false;
	private bool seen = false;
	private SpriteRenderer myrenderer;

	protected virtual void Update() {
		if (myrenderer == null) {
			myrenderer = GetComponent<SpriteRenderer> ();
			if (myrenderer == null) {
				throw new MissingComponentException ("Missing SpriteRenderer Component");
			}
		}

		if (myrenderer.isVisible) {
			seen = true;
		}

		if (seen && !myrenderer.isVisible) {
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// bullets collide with the player immediately when they spawn
		// so we don't want to destroy the bullet when that happens.
		if (!coll.gameObject.CompareTag (ShooterTag)) {
			Destroy (gameObject);

		}
	}

	public virtual void Fire (Vector3 direction) {
		this.direction = direction;
		directionSet = true;
	}
}

