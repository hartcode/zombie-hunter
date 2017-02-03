using UnityEngine;
using System.Collections;

public class AsciiMapScript : MonoBehaviour
{

	public float OriginX;
	public float OriginY;
	public float CharacterWidth;
	public float CharacterHeight;
	public int rows;
	public int cols;

	public GameObject prefabParent;
	public GameObject prefabTree;
	public GameObject prefabGrass;
	public GameObject prefabDirtRoad;

	int[,] floorarray =  new [,] {
		{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
		{ 1, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
		{ 2, 2, 2, 2, 2, 2, 2, 2, 0, 1 },
		{ 2, 2, 2, 2, 2, 2, 2, 2, 0, 1 },
		{ 1, 0, 0, 0, 1, 0, 2, 2, 0, 1 },
		{ 1, 0, 0, 0, 0, 1, 2, 2, 0, 1 },
		{ 1, 0, 0, 0, 0, 0, 2, 2, 0, 1 },
		{ 1, 0, 0, 0, 0, 0, 2, 2, 0, 1 },
		{ 1, 0, 0, 0, 0, 0, 2, 2, 1, 1 },
		{ 1, 1, 1, 1, 1, 1, 2, 2, 1, 1 }
	};

	int[,] array =  new [,] {
		{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
		{ 1, 1, 0, 0, 0, 0, 0, 0, 0, 1 },
		{ 1, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
		{ 1, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
		{ 1, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
		{ 1, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
		{ 1, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
		{ 1, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
		{ 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
		{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
	};

	// Use this for initialization
	void Start ()
	{
		if (prefabParent == null) {
			prefabParent = GameObject.Find ("AsciiMapCharacters");
		}


		for (int x = 0; x < cols; x++) {
			for (int y = 0; y < rows; y++) {
				if (floorarray [y, x] == 1) {
					CreateGrassTerrain (x, y);
				} else if (floorarray[y,x] == 2) {
					CreateDirtRoadTerrain (x, y);
				}
				if (array[y,x] == 1) {
					CreateTree (x, y);
				}
			}
		}
		GameObject player = GameObject.Find ("Player");
		if (player == null) {
			throw new MissingReferenceException ("Player gameobject is missing");
		}

		// throw the player in the center of the map.
		Vector3 playerPosition = calculateTransformPosition((int)(cols / 2),(int)(rows / 2));
		player.transform.position = playerPosition;
	}

	Vector3 calculateTransformPosition(int x, int y) {
		Vector3 retval;
		retval = new Vector3 (OriginX + (x * CharacterWidth), OriginY + (-y * CharacterHeight), 0);
		return retval;
	}


	void CreateGrassTerrain (int x, int y)
	{
		if (prefabGrass != null) {
			GameObject prefab = (GameObject)Instantiate (prefabGrass, calculateTransformPosition(x,y), Quaternion.identity, prefabParent.transform);
			prefab.isStatic = true;
		} else {
			throw new MissingReferenceException ("Grass Terrain Reference Missing");
		}
	}
	void CreateDirtRoadTerrain (int x, int y)
	{
		if (prefabDirtRoad != null) {
			GameObject prefab = (GameObject)Instantiate (prefabDirtRoad, calculateTransformPosition(x,y), Quaternion.identity, prefabParent.transform);
			prefab.isStatic = true;
		} else {
			throw new MissingReferenceException ("Dirt Road Terrain Reference Missing");
		}
	}

	void CreateTree (int x, int y)
	{
		if (prefabTree != null) {
			//bool createTree = Random.value > .95f;
			//if (createTree) {
			GameObject prefab = (GameObject)Instantiate (prefabTree, calculateTransformPosition(x,y), Quaternion.identity, prefabParent.transform);
			ScaleChange scaleChange = prefab.GetComponent<ScaleChange> ();
			if (scaleChange != null) {
				scaleChange.delay = Random.value;
			}
			prefab.isStatic = true;
			//}
		} else {
			throw new MissingReferenceException ("Tree Reference Missing");
		}
	}
}
