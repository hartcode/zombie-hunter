using UnityEngine;
using System.Collections;

public class MyCharacterController : MonoBehaviour {

	public float characterSpeed = 3f;
	public Vector3 direction;


	protected virtual void Start()
	{
		direction = new Vector3 (0, 1, 0);
	}


}
