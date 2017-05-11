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
	protected int playerx;
	protected int playery;
	protected int lastplayerx;
	protected int lastplayery;
	public bool showInspectorPath = false;
	protected List<GameObject> inspectorPathObjects = new List<GameObject> ();
	public GameObject inspectorPrefab;
	protected bool isPathfinding = false;
	protected PathFindingJob pathFindingJob = new PathFindingJob();

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
			this.pathFindingJob.pathFinding = this.pathFinding;
		}
	}
		
	void FixedUpdate () {
		if (pathFindingJob.Update ()) {
			// if pathfinding is finished
			this.movementPath= this.pathFindingJob.outputPath;
			isPathfinding = false;
			if (showInspectorPath && movementPath != null) {
				foreach (MapNode node in movementPath) {
					GameObject prefab = (GameObject)Instantiate (inspectorPrefab, new Vector3 (node.x * this.asciiMapScript.characterWidth, node.y * -this.asciiMapScript.characterHeight, 0), Quaternion.identity);
					this.inspectorPathObjects.Add (prefab);
				}	
			}
		}
		moveStep++;
	
		if (moveStep == MoveStepTime) {
			// Get the players position
			if (player != null && this.asciiMapScript != null) {
				lastplayerx = playerx;
				lastplayery = playery;
				playerx = Mathf.RoundToInt (player.transform.localPosition.x / this.asciiMapScript.characterWidth);
				playery = Mathf.RoundToInt (player.transform.localPosition.y / -this.asciiMapScript.characterHeight);
				// if player has moved trigger a new path find
				if (playerx != lastplayerx || playery != lastplayery) {
					currentGoal = new MapNode (playerx,	playery);
					movementPath = null;
					movementGoal = null;
					// clean up inspector objects
					foreach (GameObject go in inspectorPathObjects) {
						DestroyObject (go);
					}
					inspectorPathObjects.Clear ();
				}
			}
			// we don't currently have a path, so lets go find one.
			if (!isPathfinding && (movementPath == null || movementPath.Count == 0) && pathFinding != null && currentGoal != null) {
				isPathfinding = true;
				pathFindingJob.inputStartX = mapPosition.screenCurrentX;
				pathFindingJob.inputStartY = mapPosition.screenCurrentY;
				pathFindingJob.inputEndX = currentGoal.x;
				pathFindingJob.inputEndY = currentGoal.y;
				pathFindingJob.maxCounter = 10;
				pathFindingJob.Start ();
			}
				

			// if we have a path and need to pull a direction to move in from the path
			if (movementGoal == null && movementPath != null && movementPath.Count > 0) {
				movementGoal = movementPath [0];
				movementPath.RemoveAt (0);
				// remove inspector object
				if (inspectorPathObjects.Count > 0) {
					DestroyObject (inspectorPathObjects [0]);
					inspectorPathObjects.RemoveAt (0);
				}
			}

			// calculate the direction we need to move in
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
