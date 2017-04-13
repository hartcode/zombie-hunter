using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Reset : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetButton ("Reset")) {
			SceneManager.LoadScene (1);
		}
	}
}
