using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Text;
using System;

public class AsciiMapScript : MonoBehaviour
{
	public ResourceManager resourceManager = null;
	public float OriginX;
	public float OriginY;
	public float characterWidth;
	public float characterHeight;

	public GameObject prefabParent;
	public GameObject prefabMapBlockView;
	public String mapDataPath = "Assets/Maps/Test/";

	protected GameObject[,] worlds = new GameObject[3,3];
	protected MapFile mapfile;
	public int Worldx = 3;
	public int Worldy = 3;
	protected int MapCols = 20;
	protected int MapRows = 20;
	protected GameObject player;
	protected ArrayList lfj = new ArrayList ();
	private Vector3 worldstart;
	private Vector3 worldend;

	String getMapPath(int x, int y) {
		StringBuilder sb = new StringBuilder (mapDataPath);
		sb.Append ("map");
		sb.Append (x);
		sb.Append ("x");
		sb.Append (y);
		sb.Append(".txt");
		return sb.ToString ();
	}

	String getWorldName(int x, int y) {
		return getObjectName ("World", x, y);
	}

	String getObjectName(String name, int x, int y) {
		StringBuilder sb = new StringBuilder (name);;
		sb.Append (x);
		sb.Append ("x");
		sb.Append (y);
		return sb.ToString ();
	}

	// Use this for initialization
	void Start ()
	{
		mapfile = new MapFile();
		resourceManager = new ResourceManager ();
		worldstart = calculateTransformPosition(Worldx, Worldy);
		worldend = new Vector3(MapRows *characterWidth ,-MapCols * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
		if (prefabParent == null) {
			prefabParent = GameObject.Find ("AsciiMapCharacters");
		}

		if (prefabMapBlockView == null) {
			prefabMapBlockView = (GameObject)Resources.Load ("Main/MapBlockView", typeof(GameObject));
		}

		LoadMap (Worldx-1, Worldy-1, -1+1, -1+1,YieldDirection.NoYield);
		LoadMap (Worldx-1, Worldy, -1+1, 0+1,YieldDirection.NoYield);
		LoadMap (Worldx-1, Worldy+1, -1+1, 1+1,YieldDirection.NoYield);
		LoadMap (Worldx, Worldy-1, 0+1, -1+1,YieldDirection.NoYield);

		LoadMap (Worldx, Worldy+1, 0+1, 1+1,YieldDirection.NoYield);
		LoadMap (Worldx+1, Worldy-1, 1+1, -1+1,YieldDirection.NoYield);
		LoadMap (Worldx+1, Worldy, 1+1, 0+1,YieldDirection.NoYield);
		LoadMap (Worldx+1, Worldy+1, 1+1, 1+1,YieldDirection.NoYield);

		LoadMap (Worldx, Worldy, 0+1, 0+1,YieldDirection.NoYield);

		player = GameObject.Find ("Player");
		if (player == null) {
			throw new MissingReferenceException ("Player gameobject is missing");
		}

		// throw the player in the center of the map.
		Vector3 playerPosition = new Vector3(10 *characterWidth ,-10 * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
		player.transform.position = playerPosition;
	}

	Vector3 calculateTransformPosition(int Worldx, int Worldy) {
		Vector3 retval;
		retval = new Vector3 ((MapRows * characterWidth * Worldx), (MapCols * characterHeight * -Worldy), 0);
		return retval;
	}

	void calculateXYPosition(Vector3 pos, ref int x, ref int y, int Worldx, int Worldy) {
		x = (int)((pos.x -(MapRows * characterWidth * Worldx) - OriginX)/characterWidth);
		y = (int)((pos.y - (MapCols * characterHeight * -Worldy) -OriginY) / -characterHeight);
	}




	void LoadMap(int Worldx, int Worldy, int x, int y, YieldDirection yieldDirection) {
		String mapPath = getMapPath (Worldx, Worldy);

		InstantiateMap (mapfile.LoadFile (mapPath), Worldx, Worldy, x, y);
	}

	void LoadMapThreaded(int Worldx, int Worldy, int x, int y, YieldDirection yieldDirection) {
		String mapPath = getMapPath (Worldx, Worldy);
		LoadFileJob loadFileJob = new LoadFileJob ();
		loadFileJob.Worldx = Worldx;
		loadFileJob.Worldy = Worldy;
		loadFileJob.x = x;
		loadFileJob.y = y;
		loadFileJob.input = mapPath;
		loadFileJob.yieldDirection = yieldDirection;
		lfj.Add (loadFileJob);
		loadFileJob.Start ();
	}

	void InstantiateMap(MapBlockData mapData, int Worldx, int Worldy, int x, int y) {
		InstantiateMap(mapData,Worldx,Worldy, x, y, YieldDirection.NoYield);
    }

	void InstantiateMap(MapBlockData mapData, int Worldx, int Worldy, int x, int y, YieldDirection yieldDirection)
	{
		GameObject world = GameObject.Find (getWorldName (Worldx, Worldy));
		if (world == null) {
			world = (GameObject)Instantiate (prefabMapBlockView, new Vector3(0,0,0) , Quaternion.identity, prefabParent.transform);
			world.name = getWorldName (Worldx, Worldy);
			world.transform.position = calculateTransformPosition (Worldx, Worldy);

			MapBlockView mapBlockView = world.GetComponent<MapBlockView> ();
			if (mapBlockView == null) {
				throw new MissingComponentException ("Expected to find the MapBlockView Component");
			}
			worlds [x, y] = world;
			StartCoroutine (mapBlockView.Initialize (Worldx, Worldy, mapData, getMapPath(Worldx, Worldy), yieldDirection, resourceManager));

		}
	}


	void UnLoadMap(int Worldx, int Worldy, int x, int y) {
		GameObject world = worlds [x, y];
		if (world != null) {
			DestroyObject (world);
			world = null;
		}
	}


	void FixedUpdate()
	{
		if (lfj != null) {
			foreach (LoadFileJob loadFileJob in lfj.ToArray()) {
				if (loadFileJob.Update ()) {
					lfj.Remove (loadFileJob);
					InstantiateMap (loadFileJob.output, loadFileJob.Worldx, loadFileJob.Worldy, loadFileJob.x, loadFileJob.y, loadFileJob.yieldDirection);
				}
			}
		}
		if (player != null) {
			if (player.transform.position.x < worldstart.x) {
				UnLoadMap(Worldx + 1, Worldy -1,  2, 0);
				UnLoadMap(Worldx + 1, Worldy,     2, 1);
				UnLoadMap(Worldx + 1, Worldy + 1, 2, 2);
				Worldx--;
				worldstart = calculateTransformPosition(Worldx, Worldy);
				worldend = new Vector3(MapRows *characterWidth ,-MapCols * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
				for (int x = 1; x >= 0; x--) {
					for (int y = 0; y < 3; y++) {
						worlds [x+1, y] = worlds [x, y];
					}
				}
				LoadMapThreaded (Worldx - 1, Worldy-1,  0, 0, YieldDirection.YieldLeft);
				LoadMapThreaded (Worldx - 1, Worldy,    0, 1, YieldDirection.YieldLeft);
				LoadMapThreaded (Worldx - 1, Worldy+1,  0, 2, YieldDirection.YieldLeft);
			}
			if (player.transform.position.x > worldend.x) {
				UnLoadMap(Worldx - 1, Worldy -1,  0, 0);
				UnLoadMap(Worldx - 1, Worldy,     0, 1);
				UnLoadMap(Worldx - 1, Worldy + 1, 0, 2);
				Worldx++;
				worldstart = calculateTransformPosition(Worldx, Worldy);
				worldend = new Vector3(MapRows *characterWidth ,-MapCols * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
				for (int x = 0; x <= 1; x++) {
					for (int y = 0; y < 3; y++) {
						worlds [x, y] = worlds [x+1, y];
					}
				}
				LoadMapThreaded (Worldx + 1, Worldy-1,  2, 0, YieldDirection.YieldRight);
				LoadMapThreaded (Worldx + 1, Worldy,    2, 1, YieldDirection.YieldRight);
				LoadMapThreaded (Worldx + 1, Worldy+1,  2, 2, YieldDirection.YieldRight);
			}
			if (player.transform.position.y > worldstart.y) {
				UnLoadMap(Worldx - 1, Worldy + 1, 0, 2);
				UnLoadMap(Worldx, Worldy +1    ,  1, 2);
				UnLoadMap(Worldx + 1, Worldy + 1, 2, 2);
				Worldy--;
				worldstart = calculateTransformPosition(Worldx, Worldy);
				worldend = new Vector3(MapRows *characterWidth ,-MapCols * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
				for (int y = 1; y >= 0; y--) {
					for (int x = 0; x < 3; x++) {
						worlds [x, y+1] = worlds [x, y];
					}
				}
				LoadMapThreaded (Worldx - 1, Worldy -1, 0, 0, YieldDirection.YieldUp);
				LoadMapThreaded (Worldx, Worldy - 1,    1, 0, YieldDirection.YieldUp);
				LoadMapThreaded (Worldx + 1, Worldy -1, 2, 0, YieldDirection.YieldUp);
			}
			if (player.transform.position.y < worldend.y) {
				UnLoadMap(Worldx - 1, Worldy - 1, 0, 0);
				UnLoadMap(Worldx, Worldy -1    ,  1, 0);
				UnLoadMap(Worldx + 1, Worldy - 1, 2, 0);
				Worldy++;
				worldstart = calculateTransformPosition(Worldx, Worldy);
				worldend = new Vector3(MapRows *characterWidth ,-MapCols * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
				for (int y = 0; y <= 1; y++) {
					for (int x = 0; x < 3; x++) {
						worlds [x, y] = worlds [x, y+1];
					}
				}
				LoadMapThreaded (Worldx - 1, Worldy +1, 0, 2, YieldDirection.YieldDown);
				LoadMapThreaded (Worldx, Worldy + 1,    1, 2, YieldDirection.YieldDown);
				LoadMapThreaded (Worldx + 1, Worldy +1, 2, 2, YieldDirection.YieldDown);
			}

		}
	}
}
