using UnityEngine;
using System.Collections;

public class FreeRangeCharacterController : MyCharacterController
{
	private Vector3 movingDirection;
	protected Vector3 lastposition;
	// Update is called once per frame
	void FixedUpdate ()
	{
		float vertical = 0;
		float horizontal = 0;
		if (Input.GetAxis("Vertical") > 0) {
			vertical = 1;
			direction = new Vector3 (horizontal, vertical, 0);
			movingDirection = new Vector3 (0, 1, 0);
			Move ();
		} else if (Input.GetAxis("Vertical") < 0) {
			vertical = -1;
			direction = new Vector3 (horizontal, vertical, 0);
			movingDirection = new Vector3 (0, -1, 0);
			Move ();
		}
		if (Input.GetAxis("Horizontal") < 0) {
			horizontal = -1;
			direction = new Vector3 (horizontal, vertical, 0);
			movingDirection = new Vector3 (-1, 0, 0);
			Move ();
		} else if (Input.GetAxis("Horizontal") > 0) {
			horizontal = 1;
			direction = new Vector3 (horizontal, vertical, 0);
			movingDirection = new Vector3 (1, 0, 0);
			Move ();
		}
	}

	protected void Move ()
	{
		Vector3 updatedPosition = transform.position + movingDirection * characterSpeed * Time.deltaTime;
		if (transform.position != updatedPosition) {
			lastposition = transform.position;
			transform.position = updatedPosition;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll != null) {
			transform.position = lastposition;
		}
	}

}
