using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Collections.Generic;

public class PathFindingMovement : MonoBehaviour {

	public float characterSpeed = 1.5f;
	private Rigidbody2D myrigidbody2D;
	public Vector2 direction;

	public int MoveStepTime = 10;
	private int moveStep = 0;
	public AsciiMapScript asciiMapScript = null;
	protected PathFinding pathFinding = null;
	protected MapPosition mapPosition = null;
	protected MapNode currentGoal = null;
	protected MapNode movementGoal = null;
	protected List<MapNode> movementPath = null;
	public GameObject player = null;
	public int playerx;
	public int playery;
	public int lastplayerx;
	public int lastplayery;

	public int movex;
	public int movey;
	public int posx;
	public int posy;



	void Start () {
		setAsciiMapScript(asciiMapScript);

		myrigidbody2D = GetComponent<Rigidbody2D> ();
		if (myrigidbody2D == null) {
			throw new MissingComponentException ("Missing Rigidbody2D Component");
		}

		mapPosition = GetComponent<MapPosition> ();
		if (mapPosition == null) {
			throw new MissingComponentException ("Missing MapPosition Component");
		}
	}

	public void setAsciiMapScript(AsciiMapScript asciiMapScript) {
		if (asciiMapScript != null) {
			this.asciiMapScript = asciiMapScript;
			pathFinding = new PathFinding (asciiMapScript);
		}
	}
		
	void FixedUpdate () {
		moveStep++;
	
		if (moveStep == MoveStepTime) {
			if (player != null && this.asciiMapScript != null) {
				lastplayerx = playerx;
				lastplayery = playery;
				playerx = Mathf.RoundToInt (player.transform.localPosition.x / this.asciiMapScript.characterWidth);
				playery = Mathf.RoundToInt (player.transform.localPosition.y / -this.asciiMapScript.characterHeight);
				if (playerx != lastplayerx || playery != lastplayery) {
					movementPath = null;
					movementGoal = null;
				}
				currentGoal = new MapNode (playerx,
					playery);
			}
			if ((movementPath == null || movementPath.Count == 0) && pathFinding != null && currentGoal != null) {
				movementPath = pathFinding.pathFinding (new MapNode (mapPosition.screenCurrentX, mapPosition.screenCurrentY), currentGoal, 10);

			}
			if (movementGoal == null && movementPath.Count > 0) {
				movementGoal = movementPath [0];
				movementPath.RemoveAt (0);
				movex = movementGoal.x;
				movey = movementGoal.y;
			}
			posx = mapPosition.screenCurrentX;
			posy = mapPosition.screenCurrentY;
			if (movementGoal != null) {
				int dirx = 0;
				int diry = 0;
				if (movementGoal.x - mapPosition.screenCurrentX > 0) {
					dirx = 1;
				} else if (movementGoal.x - mapPosition.screenCurrentX < 0) {
					dirx = -1;
				}
				if (movementGoal.y - mapPosition.screenCurrentY > 0) {
					diry = -1;
				} else if (movementGoal.y - mapPosition.screenCurrentY < 0) {
					diry = 1;
				}
				direction = new Vector2 (dirx, diry);
				if (mapPosition.screenCurrentX == movementGoal.x && mapPosition.screenCurrentY == movementGoal.y) {
					direction = new Vector2 ();
					movementGoal = null;
					movementPath = null;
				}
			} 

			Move ();
			moveStep = 0;
		}
	}
		
	void Move() {
		myrigidbody2D.MovePosition(myrigidbody2D.position + direction * characterSpeed * Time.deltaTime);
	}
}
