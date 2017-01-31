using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour {

	public float characterSpeed = 1.5f;
	private Vector3 direction;
	private Vector3 lastposition;

	public int MoveStepTime = 10;
	private int moveStep = 0;

	public int DirectionStepTime = 40;
	private int directionStep = 0;

		
	void Update () {
		moveStep++;
		directionStep++;

		if (directionStep == DirectionStepTime) {
			ChangeDirection ();
			directionStep = 0;
		}	
		if (moveStep == MoveStepTime) {
			Move ();
			moveStep = 0;
		}
	}

	void ChangeDirection()
	{
		float randomlong = Random.value;
		if(randomlong <= 0.25f)
		{
			direction = new Vector3 (0,1,0);
		
		}else 
			if(randomlong > .25f && randomlong <= 0.5f)
			{
				direction = new Vector3 (0,-1,0);
			} else
				if(randomlong > .5f && randomlong <= 0.75f)
				{
				direction = new Vector3 (-1,0,0);
				} else
					if(randomlong > .75f)
					{   
						direction = new Vector3 (1,0,0);
					}
	}

	void Move() {
		Vector3 updatedPosition = transform.position + direction * characterSpeed * Time.deltaTime;
		if (transform.position != updatedPosition) {
			lastposition = transform.position;
			transform.position = updatedPosition;
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll != null && !coll.CompareTag("Damage")) {
			transform.position = lastposition;
		}
	}
}
