using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ChangeColor))]
public class Shooter : MonoBehaviour {

	public MyCharacterController controller;
	public GameObject prefabParent;
	public GameObject prefabBullet;
	private FireableObject bullet;
	public SpriteRenderer myrenderer;
	public AsciiMapScript asciiMap;
	public AudioClip fireSound;
	AudioSource aaudio;
	public float FireSpeed = 2;
	float lastFireTime;
	ChangeColor colorChange;
	public Vector3 direction;

	// Use this for initialization
	void Start () {
		lastFireTime = Time.time;
		if (myrenderer == null) {
			myrenderer = GetComponent<SpriteRenderer> ();
			if (myrenderer == null) {
				throw new MissingComponentException ("Missing SpriteRenderer Component");
			}
		}
		colorChange = GetComponent<ChangeColor> ();
		aaudio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		float currentTime = Time.time;
		if (currentTime - lastFireTime > (FireSpeed) && bullet == null && Input.GetButton("Fire1") && myrenderer.isVisible) {
			float characterWidth = asciiMap.characterWidth;
			float characterHeight = asciiMap.characterHeight;

			direction = new Vector3(controller.direction.x, controller.direction.y, 0);
			if (direction.x > 0) {
				direction.x = 1;
			} else if (direction.x < 0) {
				direction.x = -1;
			}
			if (direction.y > 0) {
				direction.y = 1;
			} else if (direction.y < 0) {
				direction.y = -1;
			}
			Vector3 fireableStartPosition = new Vector3(gameObject.transform.position.x + (direction.x*characterWidth), gameObject.transform.position.y + (direction.y*characterHeight), 0);
			GameObject prefab = (GameObject)Instantiate (prefabBullet, fireableStartPosition, Quaternion.identity,prefabParent.transform);
			bullet = prefab.GetComponent<FireableObject> ();
			bullet.Fire (direction);
			//if (!audio.isPlaying) {
				aaudio.PlayOneShot (fireSound, 0.1F);
			//}
			lastFireTime = Time.time;
			if (colorChange != null) {
				colorChange.Restart ();
			}
		}
		// make sure the audio stops if the bullet stops
		if (bullet == null && aaudio.isPlaying) {
			aaudio.Stop ();
		}
	}
}
