using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class AsciiMapScript : MonoBehaviour
{

	public float OriginX;
	public float OriginY;
	public float CharacterWidth;
	public float CharacterHeight;

	public GameObject prefabParent;

	protected MapData mapData;

	// Use this for initialization
	void Start ()
	{
		mapData = new MapData ();

		if (prefabParent == null) {
			prefabParent = GameObject.Find ("AsciiMapCharacters");
		}

		GameObject prefabWall = (GameObject)Resources.Load ("Main/Wall", typeof(GameObject));

		for (int x = -1; x <= mapData.getCols(); x++) {
			for (int y = -1; y <= mapData.getRows(); y++) {

				if (x < 0 || x == mapData.getCols() || y < 0 || y == mapData.getRows()) {
					CreateMapObject (x, y,prefabWall);
				} else {
					GameObject floorObject = mapData.getFloor (x, y);
					if (floorObject != null) {
						CreateMapObject (x, y,floorObject);
					}
					GameObject mainObject = mapData.getMain (x, y);
					if (mainObject != null) {
						CreateMapObject (x, y, mainObject);
					}
				}
			}
		}
		GameObject player = GameObject.Find ("Player");
		if (player == null) {
			throw new MissingReferenceException ("Player gameobject is missing");
		}

		// throw the player in the center of the map.
		Vector3 playerPosition = calculateTransformPosition((int)(mapData.getCols() / 2),(int)(mapData.getRows() / 2));
		player.transform.position = playerPosition;
	}

	Vector3 calculateTransformPosition(int x, int y) {
		Vector3 retval;
		retval = new Vector3 (OriginX + (x * CharacterWidth), OriginY + (-y * CharacterHeight), 0);
		return retval;
	}

	void CreateMapObject (int x, int y, GameObject mapPrefab)
	{
		if (mapPrefab != null) {
			GameObject prefab = (GameObject)Instantiate (mapPrefab, calculateTransformPosition(x,y), Quaternion.identity, prefabParent.transform);
			prefab.isStatic = true;
		} else {
			throw new MissingReferenceException ("Map Prefab Reference Missing");
		}
	}
}
