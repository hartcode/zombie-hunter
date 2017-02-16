using UnityEngine;
using System.Collections;

public class DamageCollision: MonoBehaviour {

	public FlashColor flashColor;
	public FlashScale flashScale;
	public ParticleSystem myParticleSystem;
	public HitPoints hitpoints;
	private bool flashColorExists = false;
	private bool flashScaleExists = false;
	private bool particleSystemExists = false;
	private bool hitPointsExists = false;
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

		if (hitpoints == null) {
			hitpoints = GetComponent<HitPoints> ();
		}
		hitPointsExists = hitpoints != null;

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
			if (hitPointsExists) {
				hitpoints.change (-1);
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
			if (hitPointsExists) {
				hitpoints.change (-1);
			}
		}
	}


}
