using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class RandomPhysicsMovement : MonoBehaviour {

	public float characterSpeed = 1.5f;
	private Rigidbody2D myrigidbody2D;
	private Vector2 direction;

	public int MoveStepTime = 10;
	private int moveStep = 0;

	public int DirectionStepTime = 40;
	private int directionStep = 0;


	void Start () {
		myrigidbody2D = GetComponent<Rigidbody2D> ();
		if (myrigidbody2D == null) {
			throw new MissingComponentException ("Missing Rigidbody2D Component");
		}
	}
		
	void FixedUpdate () {
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
			direction = new Vector2 (0,1);
		
		}else 
			if(randomlong > .25f && randomlong <= 0.5f)
			{
				direction = new Vector2 (0,-1);
			} else
				if(randomlong > .5f && randomlong <= 0.75f)
				{
					direction = new Vector2 (-1,0);
				} else
					if(randomlong > .75f)
					{   direction = new Vector2 (1,0);
					}
	}

	void Move() {
		myrigidbody2D.MovePosition(myrigidbody2D.position + direction * characterSpeed * Time.deltaTime);
	}
}
