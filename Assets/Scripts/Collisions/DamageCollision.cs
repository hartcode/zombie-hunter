using UnityEngine;
using System.Collections;

public class DamageCollision: MonoBehaviour {

	public FlashColor flashColor;
	public FlashScale flashScale;
	public ParticleSystem myParticleSystem;
	private bool flashColorExists = false;
	private bool flashScaleExists = false;
	private bool particleSystemExists = false;
	public string Tag = "Damage";

	// Use this for initialization
	void Start () {
		if (flashColor == null) {
			flashColor = GetComponent<FlashColor> ();
		}
		flashColorExists = flashColor != null;

		if (flashScale == null) {
			flashScale = GetComponent<FlashScale> ();
		}
		flashScaleExists = flashScale != null;

		if (myParticleSystem == null) {
			myParticleSystem = GetComponent<ParticleSystem> ();
		}
		particleSystemExists = myParticleSystem != null;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.CompareTag (Tag)) {
			if (flashColorExists) {
				flashColor.Change ();
			}
			if (flashScaleExists) {
				flashScale.Change ();
			}
			if (particleSystemExists) {
				myParticleSystem.Play();
			}
		}
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.CompareTag (Tag)) {
			if (flashColorExists) {
				flashColor.Change ();
			}
			if (flashScaleExists) {
				flashScale.Change ();
			}
			if (particleSystemExists) {
				myParticleSystem.Play();
			}
		}
	}

}
