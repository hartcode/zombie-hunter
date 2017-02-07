using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Text;
using System;

public class AsciiMapScript : MonoBehaviour
{

	public float OriginX;
	public float OriginY;
	public float CharacterWidth;
	public float CharacterHeight;

	public GameObject prefabParent;
	public GameObject prefabWall;

	protected MapData mapData;
	protected MapFile mapfile;
	protected int Worldx = 0;
	protected int Worldy = 0;
	protected int MapCols = 20;
	protected int MapRows = 20;

	String getMapPath(int x, int y) {
		StringBuilder sb = new StringBuilder ("map");
		sb.Append (x);
		sb.Append ("x");
		sb.Append (y);
		sb.Append(".txt");
		return sb.ToString ();
	}

	// Use this for initialization
	void Start ()
	{
		mapData = new MapData ();
		mapfile = new MapFile();
	
		String startingMapFile = getMapPath (Worldx, Worldy);
		mapfile.SaveFile(mapData,startingMapFile);



		if (prefabParent == null) {
			prefabParent = GameObject.Find ("AsciiMapCharacters");
		}

		if (prefabWall == null) {
			prefabWall = (GameObject)Resources.Load ("Main/Wall", typeof(GameObject));
		}

		LoadMap (Worldx, Worldy);
		LoadMap (0, 1);
		LoadMap (1, 0);
		LoadMap (1, 1);

		GameObject player = GameObject.Find ("Player");
		if (player == null) {
			throw new MissingReferenceException ("Player gameobject is missing");
		}

		// throw the player in the center of the map.
		Vector3 playerPosition = calculateTransformPosition((int)(mapData.getCols() / 2),(int)(mapData.getRows() / 2), Worldx, Worldy);
		player.transform.position = playerPosition;
	}

	Vector3 calculateTransformPosition(int x, int y, int Worldx, int Worldy) {
		Vector3 retval;
		retval = new Vector3 (OriginX + (x * CharacterWidth) + (MapRows * CharacterWidth * Worldx), OriginY + (-y * CharacterHeight) + (MapCols * CharacterHeight * -Worldy), 0);
		return retval;
	}

	void CreateMapObject (int x, int y, GameObject mapPrefab, int Worldx, int Worldy)
	{
		if (mapPrefab != null) {
			GameObject prefab = (GameObject)Instantiate (mapPrefab, calculateTransformPosition(x,y, Worldx, Worldy), Quaternion.identity, prefabParent.transform);
			prefab.isStatic = true;
		} else {
			throw new MissingReferenceException ("Map Prefab Reference Missing");
		}
	}


	void LoadMap(int Worldx, int Worldy) {
		String mapPath = getMapPath (Worldx, Worldy); 
		mapData = mapfile.LoadFile(mapPath);
		for (int x = 0; x < mapData.getCols(); x++) {
			for (int y = 0; y < mapData.getRows(); y++) {

				if (x < 0 || x == mapData.getCols() || y < 0 || y == mapData.getRows()) {
					CreateMapObject (x, y,prefabWall, Worldx, Worldy);
				} else {
					GameObject floorObject = mapData.getFloor (x, y);
					if (floorObject != null) {
						CreateMapObject (x, y,floorObject, Worldx, Worldy);
					}
					GameObject mainObject = mapData.getMain (x, y);
					if (mainObject != null) {
						CreateMapObject (x, y, mainObject, Worldx, Worldy);
					}
				}
			}
		}
	}
}
