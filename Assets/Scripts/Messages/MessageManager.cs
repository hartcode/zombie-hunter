using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MessageManager : MonoBehaviour
{

	private static MessageManager instance = null;

	private Dictionary<string, List<Component>> Listeners = new Dictionary<string, List<Component> > ();

	public static MessageManager Instance {
		get {
			if (!instance) {
				instance = new MessageManager ();
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

	public void AddListener (Component Sender, string messageName)
	{
		if (!Listeners.ContainsKey (messageName)) {
			Listeners.Add (messageName, new List<Component> ());
		}
		Listeners [messageName].Add (Sender);
	}

	public void RemoveListener (Component Sender, string messageName)
	{
		if (Listeners.ContainsKey (messageName)) {
			for (int i = Listeners [messageName].Count - 1; i >= 0; i--) {
				if (Listeners [messageName] [i].GetInstanceID () == Sender.GetInstanceID ()) {
					Listeners [messageName].RemoveAt (i);
				}
			}
		}
	}

	public void PostMessage (Component Sender, string messageName)
	{
		if (Listeners.ContainsKey (messageName)) {
			for (int i = Listeners [messageName].Count - 1; i >= 0; i--) {
				Component listener = Listeners [messageName] [i];
				if (listener != null) {
					listener.SendMessage (messageName, Sender, SendMessageOptions.DontRequireReceiver);
				} else {
					Listeners [messageName].RemoveAt (i);
				}
			}
		}
	}

	public void ClearListeners ()
	{
		Listeners.Clear ();
	}

	public void RemoveRedundancies ()
	{
		Dictionary<string, List<Component>> tempListeners = new Dictionary<string, List<Component>> ();
		foreach (KeyValuePair<string, List<Component>> Item in Listeners) {
			for (int i = Item.Value.Count - 1; i >= 0; i--) {
				if (Item.Value [i] == null) {
					Item.Value.RemoveAt (i);
				}
			}

			if (Item.Value.Count > 0) {
				tempListeners.Add (Item.Key, Item.Value);
			}
		}

		Listeners = tempListeners;
	}

	/*void OnLevelWasLoaded ()
	{
		RemoveRedundancies ();
	}
	*/
}
