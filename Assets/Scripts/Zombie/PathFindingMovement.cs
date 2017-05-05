using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class PathFindingMovement : MonoBehaviour {

	public float characterSpeed = 1.5f;
	private Rigidbody2D myrigidbody2D;
	private Vector2 direction;

	public int MoveStepTime = 10;
	private int moveStep = 0;
	public AsciiMapScript asciiMapScript = null;
	protected PathFinding pathFinding = null;
	protected MapPosition mapPosition = null;
	protected MapNode currentGoal = new MapNode(40,40);
	protected MapNode movementGoal = null;
	protected List<MapNode> movementPath = null;

	void Start () {
		if (asciiMapScript != null) {
			pathFinding = new PathFinding (asciiMapScript);
		}

		myrigidbody2D = GetComponent<Rigidbody2D> ();
		if (myrigidbody2D == null) {
			throw new MissingComponentException ("Missing Rigidbody2D Component");
		}

		mapPosition = GetComponent<MapPosition> ();
		if (mapPosition == null) {
			throw new MissingComponentException ("Missing MapPosition Component");
		}
	}
		
	void FixedUpdate () {
		moveStep++;
	
		if (moveStep == MoveStepTime) {
			if (movementPath == null) {
				movementPath = pathFinding.pathFinding (new MapNode (mapPosition.screenCurrentX, mapPosition.screenCurrentY), currentGoal, 20);

			}
			if (movementGoal == null && movementPath.Count > 0) {
				movementGoal = movementPath [0];
				movementPath.RemoveAt (0);
				direction = new Vector2 (mapPosition.screenCurrentX - movementGoal.x, mapPosition.screenCurrentY - movementGoal.y);
			}
			if (movementGoal != null && mapPosition.screenCurrentX == movementGoal.x && mapPosition.screenCurrentY == movementGoal.y) {
				direction = new Vector2 ();
				movementGoal = null;
			} 

			Move ();
			moveStep = 0;
		}
	}
		
	void Move() {
		myrigidbody2D.MovePosition(myrigidbody2D.position + direction * characterSpeed * Time.deltaTime);
	}
}
