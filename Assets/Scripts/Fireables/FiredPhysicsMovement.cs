using UnityEngine;
using System.Collections;

public class FiredPhysicsMovement : FireableObject {

	private Rigidbody2D myrigidbody2d;

	void Start() {
		myrigidbody2d = GetComponent<Rigidbody2D> ();
		if (myrigidbody2d == null) {
			throw new MissingComponentException ("RigidBody2d is missing");
		}
	}

	public override void Fire (Vector3 direction) {
		base.Fire (direction);
		Start ();
		if (this.directionSet) {
			myrigidbody2d.AddForce (direction * fireableSpeed, ForceMode2D.Impulse);
		}
	}
}