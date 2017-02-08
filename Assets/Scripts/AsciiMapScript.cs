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
	public int Worldx = 1;
	public int Worldy = 1;
	protected int MapCols = 20;
	protected int MapRows = 20;
	protected GameObject player;
	protected ArrayList lfj = new ArrayList ();

	String getMapPath(int x, int y) {
		StringBuilder sb = new StringBuilder ("map");
		sb.Append (x);
		sb.Append ("x");
		sb.Append (y);
		sb.Append(".txt");
		return sb.ToString ();
	}

	String getWorldName(int x, int y) {
		StringBuilder sb = new StringBuilder ("World");;
		sb.Append (x);
		sb.Append ("x");
		sb.Append (y);
		return sb.ToString ();
	}

	// Use this for initialization
	void Start ()
	{
		mapData = new MapData ();
		mapfile = new MapFile();
	
		String startingMapFile = getMapPath (Worldx, Worldy);
		//mapfile.SaveFile(mapData,startingMapFile);
		mapfile.SaveFile(mapData,getMapPath(0,0));
		mapfile.SaveFile(mapData,getMapPath(0,1));
		mapfile.SaveFile(mapData,getMapPath(1,0));
		mapfile.SaveFile(mapData,getMapPath(1,1));
		mapfile.SaveFile(mapData,getMapPath(0,2));
		mapfile.SaveFile(mapData,getMapPath(2,0));
		mapfile.SaveFile(mapData,getMapPath(2,1));
		mapfile.SaveFile(mapData,getMapPath(1,2));
		mapfile.SaveFile(mapData,getMapPath(2,2));
		mapfile.SaveFile(mapData,getMapPath(0,3));
		mapfile.SaveFile(mapData,getMapPath(1,3));
		mapfile.SaveFile(mapData,getMapPath(2,3));
		mapfile.SaveFile(mapData,getMapPath(3,0));
		mapfile.SaveFile(mapData,getMapPath(3,1));
		mapfile.SaveFile(mapData,getMapPath(3,2));
		mapfile.SaveFile(mapData,getMapPath(3,3));

		if (prefabParent == null) {
			prefabParent = GameObject.Find ("AsciiMapCharacters");
		}

		if (prefabWall == null) {
			prefabWall = (GameObject)Resources.Load ("Main/Wall", typeof(GameObject));
		}

		LoadMap (0, 0);
		LoadMap (0, 1);
		LoadMap (1, 0);
		LoadMap (1, 1);
		LoadMap (2, 1);
		LoadMap (1, 2);
		LoadMap (0, 2);
		LoadMap (2, 0);
		LoadMap (2, 2);
	

		player = GameObject.Find ("Player");
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

	void CreateMapObject (int x, int y, GameObject mapPrefab, int Worldx, int Worldy, GameObject parent)
	{
		if (mapPrefab != null) {
			GameObject prefab = (GameObject)Instantiate (mapPrefab, calculateTransformPosition(x,y, Worldx, Worldy), Quaternion.identity, parent.transform);
			prefab.isStatic = true;
		} else {
			throw new MissingReferenceException ("Map Prefab Reference Missing");
		}
	}


	void LoadMap(int Worldx, int Worldy) {
			String mapPath = getMapPath (Worldx, Worldy);
			mapData = mapfile.LoadFile (mapPath);
			InstantiateMap (mapData, Worldx, Worldy);
	}

	void LoadMapThreaded(int Worldx, int Worldy) {
		String mapPath = getMapPath (Worldx, Worldy);
		LoadFileJob loadFileJob = new LoadFileJob ();
		loadFileJob.Worldx = Worldx;
		loadFileJob.Worldy = Worldy;
		loadFileJob.input = mapPath;
		lfj.Add (loadFileJob);
		loadFileJob.Start ();
	}

	void InstantiateMap(MapData mapData, int Worldx, int Worldy)
	{

		if (mapData == null) {

		} else {
			GameObject world = GameObject.Find (getWorldName (Worldx, Worldy));
			if (world == null) {
				world = new GameObject (getWorldName (Worldx, Worldy));
				world.transform.parent = prefabParent.transform;
				for (int x = 0; x < mapData.getCols (); x++) {
					for (int y = 0; y < mapData.getRows (); y++) {

						if (x < 0 || x == mapData.getCols () || y < 0 || y == mapData.getRows ()) {
							CreateMapObject (x, y, prefabWall, Worldx, Worldy, world);
						} else {
							//GameObject floorObject = mapData.getFloor (x, y);
							GameObject floorObject = (GameObject)Resources.Load (mapData.getFloorResource(x,y), typeof(GameObject));
							if (floorObject != null) {
								CreateMapObject (x, y, floorObject, Worldx, Worldy, world);
							}
							//GameObject mainObject = mapData.getMain (x, y);
							GameObject mainObject = (GameObject)Resources.Load (mapData.getMainResource(x,y), typeof(GameObject));
							if (mainObject != null) {
								CreateMapObject (x, y, mainObject, Worldx, Worldy, world);
							}
						}
					}
				}
			}
		}
	}
	IEnumerator InstantiateCoroutineMap(MapData mapData, int Worldx, int Worldy)
	{
	
		if (mapData == null) {

		} else {
			GameObject world = GameObject.Find (getWorldName (Worldx, Worldy));
			if (world == null) {
				world = new GameObject (getWorldName (Worldx, Worldy));
				world.transform.parent = prefabParent.transform;
				for (int x = 0; x < mapData.getCols (); x++) {
					for (int y = 0; y < mapData.getRows (); y++) {

						if (x < 0 || x == mapData.getCols () || y < 0 || y == mapData.getRows ()) {
						CreateMapObject (x, y, prefabWall, Worldx, Worldy, world);
						} else {
							//GameObject floorObject = mapData.getFloor (x, y);
							GameObject floorObject = (GameObject)Resources.Load (mapData.getFloorResource(x,y), typeof(GameObject));
							if (floorObject != null) {
							CreateMapObject (x, y, floorObject, Worldx, Worldy, world);
							}
							//GameObject mainObject = mapData.getMain (x, y);
							GameObject mainObject = (GameObject)Resources.Load (mapData.getMainResource(x,y), typeof(GameObject));
							if (mainObject != null) {
							CreateMapObject (x, y, mainObject, Worldx, Worldy, world);
							}
						}
					}
					yield return null;
				}
			}
		}
	}

	void UnLoadMap(int Worldx, int Worldy) {
		GameObject  world = GameObject.Find (getWorldName (Worldx, Worldy));
		if (world != null) {
			DestroyObject (world);
		}
	}

	void FixedUpdate()
	{
		if (lfj != null) {
			foreach (LoadFileJob loadFileJob in lfj.ToArray()) {
				if (loadFileJob.Update ()) {
					lfj.Remove (loadFileJob);
					IEnumerator coroutine = 
						InstantiateCoroutineMap (loadFileJob.output, loadFileJob.Worldx, loadFileJob.Worldy);
					StartCoroutine (coroutine);
				}
			}
		}
		if (player != null) {
			Vector3 worldstart = calculateTransformPosition(0,0, Worldx, Worldy);
			Vector3 worldend = calculateTransformPosition (MapRows, MapCols, Worldx, Worldy);
			if (player.transform.position.x < worldstart.x) {
				UnLoadMap (Worldx + 1, Worldy-1);
				UnLoadMap (Worldx + 1, Worldy);
				UnLoadMap (Worldx + 1, Worldy+1);
				Worldx--;
				LoadMapThreaded (Worldx - 1, Worldy-1);
				LoadMapThreaded (Worldx - 1, Worldy);
				LoadMapThreaded (Worldx - 1, Worldy+1);
			}
			if (player.transform.position.x > worldend.x) {

				UnLoadMap (Worldx - 1, Worldy-1);
				UnLoadMap (Worldx - 1, Worldy);
				UnLoadMap (Worldx - 1, Worldy+1);
				Worldx++;
				LoadMapThreaded (Worldx + 1, Worldy-1);
				LoadMapThreaded (Worldx + 1, Worldy);
				LoadMapThreaded (Worldx + 1, Worldy+1);
			}
			if (player.transform.position.y > worldstart.y) {
				UnLoadMap (Worldx - 1, Worldy+1);
				UnLoadMap (Worldx, Worldy+1);
				UnLoadMap (Worldx + 1, Worldy+1);
				Worldy--;
				LoadMapThreaded (Worldx - 1, Worldy - 1);
				LoadMapThreaded (Worldx, Worldy - 1);
				LoadMapThreaded (Worldx + 1, Worldy - 1);
			}
			if (player.transform.position.y < worldend.y) {
				UnLoadMap (Worldx - 1, Worldy-1);
				UnLoadMap (Worldx, Worldy-1);
				UnLoadMap (Worldx + 1, Worldy-1);
				Worldy++;
				LoadMapThreaded (Worldx - 1, Worldy + 1);
				LoadMapThreaded (Worldx, Worldy + 1);
				LoadMapThreaded (Worldx + 1, Worldy + 1);
			}
		}
	}
}
