using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	public MyCharacterController controller;
	public GameObject prefabParent;
	public GameObject prefabBullet;
	private FireableObject bullet;
	private SpriteRenderer myrenderer;
	public AsciiMapScript asciiMap;

	// Use this for initialization
	void Start () {
		if (myrenderer == null) {
			myrenderer = GetComponent<SpriteRenderer> ();
			if (myrenderer == null) {
				throw new MissingComponentException ("Missing SpriteRenderer Component");
			} 
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (bullet == null && Input.GetButton("Fire1") && myrenderer.isVisible) {
			Vector3 position = new Vector3(
				gameObject.transform.position.x + controller.direction.x * asciiMap.CharacterWidth,
				gameObject.transform.position.y + controller.direction.y * asciiMap.CharacterHeight, 
				gameObject.transform.position.z);
			GameObject prefab = (GameObject)Instantiate (prefabBullet, position, Quaternion.identity,prefabParent.transform);
			bullet = prefab.GetComponent<FireableObject> ();
			bullet.Fire (controller.direction);
		}
	}
}
