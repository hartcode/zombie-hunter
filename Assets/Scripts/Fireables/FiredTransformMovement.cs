using UnityEngine;
using System.Collections;

public class FiredTransformMovement : FireableObject {

	protected override void Update() {
		base.Update ();
		if (this.directionSet) {
			transform.position = transform.position + direction * fireableSpeed * Time.deltaTime;
		}
	}
}