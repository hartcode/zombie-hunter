using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class PathFindingMovement : MonoBehaviour {

	public float characterSpeed = 1.5f;
	private Rigidbody2D myrigidbody2D;
	private Vector2 direction;

	public int MoveStepTime = 10;
	private int moveStep = 0;

	void Start () {
		myrigidbody2D = GetComponent<Rigidbody2D> ();
		if (myrigidbody2D == null) {
			throw new MissingComponentException ("Missing Rigidbody2D Component");
		}
	}
		
	void FixedUpdate () {
		moveStep++;
	
		if (moveStep == MoveStepTime) {
			Move ();
			moveStep = 0;
		}
	}
		
	void Move() {
		myrigidbody2D.MovePosition(myrigidbody2D.position + direction * characterSpeed * Time.deltaTime);
	}
}
