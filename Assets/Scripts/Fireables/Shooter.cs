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
			Vector3 position = new Vector3(
				gameObject.transform.position.x, // + controller.direction.x * asciiMap.CharacterWidth,
				gameObject.transform.position.y, // + controller.direction.y * asciiMap.CharacterHeight,
				gameObject.transform.position.z);
			GameObject prefab = (GameObject)Instantiate (prefabBullet, position, Quaternion.identity,prefabParent.transform);
			bullet = prefab.GetComponent<FireableObject> ();
			bullet.Fire (controller.direction);
			//if (!audio.isPlaying) {
				aaudio.PlayOneShot (fireSound, 0.1F);
			//}
			lastFireTime = Time.time;
			colorChange.Restart ();
		}
		// make sure the audio stops if the bullet stops
		if (bullet == null && aaudio.isPlaying) {
			aaudio.Stop ();
		}
	}
}
