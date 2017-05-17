using UnityEngine;
using System.Collections;

public class FreeRangePhysicsCharacterController : MyCharacterController
{
	private Rigidbody2D myrigidbody2D;


	// Use this for initialization
	protected override void Start () {
		base.Start ();
		myrigidbody2D = GetComponent<Rigidbody2D> ();
		if (myrigidbody2D == null) {
			throw new MissingComponentException ("Missing Rigidbody2D Component");
		}
	}

	// better directioning
	void Update () {

		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		if (x != 0 || y != 0) {
			direction = new Vector3 (x, y, 0);
		}
		Vector2 mydirection = new Vector2 (x,y);
		myrigidbody2D.MovePosition(myrigidbody2D.position + mydirection * characterSpeed * Time.deltaTime);
	}
}
