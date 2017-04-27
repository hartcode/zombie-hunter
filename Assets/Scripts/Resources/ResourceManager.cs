using System;
using UnityEngine;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class ResourceManager
	{
		Dictionary<String, GameObject> dict = null;
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
}

