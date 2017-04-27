using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System.Text;
using System;

public class AsciiMapScript : MonoBehaviour
{

	public float OriginX;
	public float OriginY;
	public float characterWidth;
	public float characterHeight;

	public GameObject prefabParent;
	public GameObject prefabMapBlockView;
	public String mapDataPath = "Assets/Maps/Test/";

	protected MapBlockData[,] mapDataGroup = new MapBlockData[3,3];
	protected MapFile mapfile;
	public int Worldx = 3;
	public int Worldy = 3;
	protected int MapCols = 20;
	protected int MapRows = 20;
	protected GameObject player;
	protected ArrayList lfj = new ArrayList ();

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
		mapDataGroup[x, y] = mapfile.LoadFile (mapPath);
		StartCoroutine(InstantiateMap (mapDataGroup[x, y], Worldx, Worldy));
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

	IEnumerator InstantiateMap(MapBlockData mapData, int Worldx, int Worldy) {
		return InstantiateMap(mapData,Worldx,Worldy, YieldDirection.NoYield);
    }

	IEnumerator InstantiateMap(MapBlockData mapData, int Worldx, int Worldy, YieldDirection yieldDirection)
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
			mapBlockView.Initialize (Worldx, Worldy, mapData);
			yield return null;
		}
	}


	void UnLoadMap(int Worldx, int Worldy, int x, int y) {
		GameObject  world = GameObject.Find (getWorldName (Worldx, Worldy));
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
					Debug.Log (loadFileJob.x);
					Debug.Log (loadFileJob.y);
					mapDataGroup[loadFileJob.x,loadFileJob.y] = loadFileJob.output;
					StartCoroutine (InstantiateMap (loadFileJob.output, loadFileJob.Worldx, loadFileJob.Worldy, loadFileJob.yieldDirection));
				}
			}
		}
		if (player != null) {
			Vector3 worldstart = calculateTransformPosition(Worldx, Worldy);
			Vector3 worldend = new Vector3(MapRows *characterWidth ,-MapCols * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
			if (player.transform.position.x < worldstart.x) {
				UnLoadMap(Worldx + 1, Worldy -1,  2, 0);
				UnLoadMap(Worldx + 1, Worldy,     2, 1);
				UnLoadMap(Worldx + 1, Worldy + 1, 2, 2);
				Worldx--;
				for (int x = 1; x >= 0; x--) {
					for (int y = 0; y < 3; y++) {
						mapDataGroup [x+1, y] = mapDataGroup [x, y];
					}
				}
				LoadMap (Worldx - 1, Worldy-1,  0, 0, YieldDirection.YieldLeft);
				LoadMap (Worldx - 1, Worldy,    0, 1, YieldDirection.YieldLeft);
				LoadMap (Worldx - 1, Worldy+1,  0, 2, YieldDirection.YieldLeft);
			}
			if (player.transform.position.x > worldend.x) {
				UnLoadMap(Worldx - 1, Worldy -1,  0, 0);
				UnLoadMap(Worldx - 1, Worldy,     0, 1);
				UnLoadMap(Worldx - 1, Worldy + 1, 0, 2);
				Worldx++;
				for (int x = 0; x <= 1; x++) {
					for (int y = 0; y < 3; y++) {
						mapDataGroup [x, y] = mapDataGroup [x+1, y];
					}
				}
				LoadMap (Worldx + 1, Worldy-1,  2, 0, YieldDirection.YieldRight);
				LoadMap (Worldx + 1, Worldy,    2, 1, YieldDirection.YieldRight);
				LoadMap (Worldx + 1, Worldy+1,  2, 2, YieldDirection.YieldRight);
			}
			if (player.transform.position.y > worldstart.y) {
				UnLoadMap(Worldx - 1, Worldy + 1, 0, 2);
				UnLoadMap(Worldx, Worldy +1    ,  1, 2);
				UnLoadMap(Worldx + 1, Worldy + 1, 2, 2);
				Worldy--;
				for (int y = 1; y >= 0; y--) {
					for (int x = 0; x < 3; x++) {
						mapDataGroup [x, y+1] = mapDataGroup [x, y];
					}
				}
				LoadMap (Worldx - 1, Worldy -1, 0, 0, YieldDirection.YieldUp);
				LoadMap (Worldx, Worldy - 1,    1, 0, YieldDirection.YieldUp);
				LoadMap (Worldx + 1, Worldy -1, 2, 0, YieldDirection.YieldUp);
			}
			if (player.transform.position.y < worldend.y) {
				UnLoadMap(Worldx - 1, Worldy - 1, 0, 0);
				UnLoadMap(Worldx, Worldy -1    ,  1, 0);
				UnLoadMap(Worldx + 1, Worldy - 1, 2, 0);
				Worldy++;
				for (int y = 0; y <= 1; y++) {
					for (int x = 0; x < 3; x++) {
						mapDataGroup [x, y] = mapDataGroup [x, y+1];
					}
				}
				LoadMap (Worldx - 1, Worldy +1, 0, 2, YieldDirection.YieldDown);
				LoadMap (Worldx, Worldy + 1,    1, 2, YieldDirection.YieldDown);
				LoadMap (Worldx + 1, Worldy +1, 2, 2, YieldDirection.YieldDown);
			}

		}
	}
}
