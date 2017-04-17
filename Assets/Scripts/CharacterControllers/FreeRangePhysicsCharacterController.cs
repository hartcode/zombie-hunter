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
	/*void FixedUpdate () {
		if(Input.GetAxis("Vertical") > 0)//Press up arrow key to move up on the Y AXIS
		{
			direction = new Vector3 (0,1,0);

		}
		if(Input.GetAxis("Vertical") < 0)//Press up arrow key to move down on the Y AXIS
		{
			direction = new Vector3 (0,-1,0);
		}
		if(Input.GetAxis("Horizontal") < 0)//Press up arrow key to move left on the X AXIS
		{
			direction = new Vector3 (-1,0,0);
		}
		if(Input.GetAxis("Horizontal") > 0)//Press up arrow key to move right on the X AXIS
		{   direction = new Vector3 (1,0,0);
		}
		Vector2 mydirection = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		myrigidbody2D.MovePosition(myrigidbody2D.position + mydirection * characterSpeed * Time.deltaTime);
	}*/

	// better directioning
	void Update () {

		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		if (x != 0 && y != 0) {
			direction = new Vector3 (x, y, 0);
		}
		Vector2 mydirection = new Vector2 (x,y);
		myrigidbody2D.MovePosition(myrigidbody2D.position + mydirection * characterSpeed * Time.deltaTime);
	}
}
