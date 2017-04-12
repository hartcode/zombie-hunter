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

	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetAxis("Vertical") > 0)//Press up arrow key to move forward on the Y AXIS
		{
			direction = new Vector3 (0,1,0);
			Vector2 mydirection = new Vector2 (direction.x, direction.y);
			myrigidbody2D.MovePosition(myrigidbody2D.position + mydirection * characterSpeed * Time.deltaTime);
		}
		if(Input.GetAxis("Vertical") < 0)//Press up arrow key to move forward on the Y AXIS
		{
			direction = new Vector3 (0,-1,0);
			Vector2 mydirection = new Vector2 (direction.x, direction.y);
			myrigidbody2D.MovePosition(myrigidbody2D.position + mydirection * characterSpeed * Time.deltaTime);
		}
		if(Input.GetAxis("Horizontal") < 0)//Press up arrow key to move forward on the Y AXIS
		{
			direction = new Vector3 (-1,0,0);
			Vector2 mydirection = new Vector2 (direction.x, direction.y);
			myrigidbody2D.MovePosition(myrigidbody2D.position + mydirection * characterSpeed * Time.deltaTime);
		}
		if(Input.GetAxis("Horizontal") > 0)//Press up arrow key to move forward on the Y AXIS
		{   direction = new Vector3 (1,0,0);
			Vector2 mydirection = new Vector2 (direction.x, direction.y);
			myrigidbody2D.MovePosition(myrigidbody2D.position + mydirection * characterSpeed * Time.deltaTime);
		}
	}
}
