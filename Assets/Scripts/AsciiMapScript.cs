using UnityEngine;
using System.Collections;

public class AsciiMapScript : MonoBehaviour {

	public float OriginX;
	public float OriginY;
	public float CharacterWidth;
	public float CharacterHeight;
	public int rows;
	public int cols;

	public GameObject prefabParent;
	public GameObject prefabTree;
	public GameObject prefabDefaultTerrain;

	// Use this for initialization
	void Start () {
		if (prefabParent == null) {
			prefabParent = GameObject.Find ("AsciiMapCharacters");
		}


		for (int y = 0; y < rows; y++) {
			for (int x = 0; x < cols; x++) {
				CreateTerrain (x, y);
				CreateTree (x, y);
			}
		}
	}

	void CreateTerrain(int x, int y)
	{
		if (prefabDefaultTerrain != null) {
			GameObject prefab = (GameObject)Instantiate (prefabDefaultTerrain, new Vector3 (OriginX + (x * CharacterWidth), OriginY + (-y * CharacterHeight), 0), Quaternion.identity, prefabParent.transform);
			prefab.isStatic = true;
		} else {
			throw new MissingReferenceException ("Default Terrain Reference Missing");
		}
	}

	void CreateTree(int x, int y)
	{
		if (prefabTree != null) {
			bool createTree = Random.value > .95f;
			if (createTree) {
				GameObject prefab = (GameObject)Instantiate (prefabTree, new Vector3 (OriginX + (x * CharacterWidth), OriginY + (-y * CharacterHeight), 0), Quaternion.identity, prefabParent.transform);
				ScaleChange scaleChange = prefab.GetComponent<ScaleChange> ();
				if (scaleChange != null) {
					scaleChange.delay = Random.value;
				}
				prefab.isStatic = true;
			}
		} else {
			throw new MissingReferenceException ("Tree Reference Missing");
		}
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
