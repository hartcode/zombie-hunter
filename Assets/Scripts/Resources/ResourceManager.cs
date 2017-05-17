using System;
using UnityEngine;
using System.Collections.Generic;

	public class ResourceManager : MonoBehaviour
	{
		private static ResourceManager instance = null;
		protected Dictionary<String, GameObject> dict = null;

		public static ResourceManager Instance {
			get {
				if (!instance) {
					instance = new ResourceManager ();
				}
				return instance;
			}
		}

		void Awake ()
		{
			if (instance) {
				DestroyImmediate (gameObject);
				return;
			}
			instance = this;
		}

		public ResourceManager ()
		{
			dict = new Dictionary<String, GameObject> ();
		}

		public GameObject getGameObject(String resourceName) {
			GameObject retval;
			if (!dict.TryGetValue (resourceName, out retval)) {
				retval = (GameObject)Resources.Load (resourceName, typeof(GameObject));
				dict.Add (resourceName, retval);
			}
			return retval;
		}
	}
