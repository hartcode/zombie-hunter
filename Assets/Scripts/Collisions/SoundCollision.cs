using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundCollision : MonoBehaviour {

	public string Tag = "Damage";

	public AudioClip impact;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag (Tag)) {
			audio.PlayOneShot(impact, 0.2F);	
		}
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.CompareTag (Tag)) {
			audio.PlayOneShot (impact, 0.2F);
		}
	}




}
