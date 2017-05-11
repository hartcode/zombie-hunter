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
	public GameObject player;
	public static String mapName = "Level1";
	protected String mapDataPath = "Maps/World/"+mapName+"/";
	protected String saveMapDataPath = "SavedGames/Maps/World/"+mapName+"/";

	private static int DisplayBlocksXSize = 5;  // specifies a 3x3 array of world blocks to hold in memory
	private static int DisplayBlocksYSize = 5; 
	private int DisplayBlocksXCenter = 2; // specifies the position in the world block array that is the center block
	private int DisplayBlocksYCenter = 2;

	protected GameObject[,] worlds = new GameObject[DisplayBlocksXSize,DisplayBlocksYSize];
	protected MapBlockView[,] mapBlocks = new MapBlockView[DisplayBlocksXSize,DisplayBlocksYSize];
	protected MapFile mapfile;
  	public int Worldx = 3;  // World Starting Block
	public int Worldy = 3;  // World Starting Block
	protected int MapCols = 20;
	protected int MapRows = 20;
	protected ArrayList lfj = new ArrayList ();
	private Vector3 worldstart;
	private Vector3 worldend;

	// checks if a position in the map is open
	public bool isMapPositionOpen(int screenSpaceX, int screenSpaceY)
	{
		bool retval = false;
		int worldsx = ((screenSpaceX / MapRows) - (Worldx)) + DisplayBlocksXCenter;
		int worldsy = ((screenSpaceY / MapCols) - (Worldy)) + DisplayBlocksYCenter;
		int x = screenSpaceX % MapRows;
		int y = screenSpaceY % MapCols;
		if (worldsx >= 0 && worldsx < DisplayBlocksXSize && worldsy >= 0 && worldsy < DisplayBlocksYSize) {
				MapBlockView mapBlockView = this.mapBlocks [worldsx, worldsy];
				if (mapBlockView != null && mapBlockView.mapBlockData != null) {
				retval = mapBlockView.mapBlockData.getMainResource (x, y) == "" && mapBlockView.mapBlockData.getFloorResource (x, y) != "Flooring/Water";
				}
		}
		return retval;
	}



	String getMapPath(int x, int y, String mapPath) {
		StringBuilder sb = new StringBuilder (mapPath);
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

		for (int x = 0; x < DisplayBlocksXSize; x++) {
			int worldxx = Worldx - ((DisplayBlocksXSize - 1) /2) + x;
			for (int y = 0; y < DisplayBlocksYSize; y++) {
				int worldyy = Worldy - ((DisplayBlocksYSize - 1) /2) + y;
				LoadMap (worldxx, worldyy, x, y, YieldDirection.NoYield);
			}
		}

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
		String saveMapPath = getMapPath (Worldx, Worldy,saveMapDataPath);
		MapBlockData mapdata = mapfile.LoadFile (saveMapPath);
		if (mapdata == null) {
			String mapPath = getMapPath (Worldx, Worldy,mapDataPath);
			mapdata = mapfile.LoadFile (mapPath);
		}
		InstantiateMap (mapdata, Worldx, Worldy, x, y);
	}

	void LoadMapThreaded(int Worldx, int Worldy, int x, int y, YieldDirection yieldDirection) {
		String saveMapPath = getMapPath (Worldx, Worldy, saveMapDataPath);
		String mapPath = getMapPath (Worldx, Worldy, mapDataPath);
		LoadFileJob loadFileJob = new LoadFileJob ();
		loadFileJob.Worldx = Worldx;
		loadFileJob.Worldy = Worldy;
		loadFileJob.x = x;
		loadFileJob.y = y;
		loadFileJob.input = saveMapPath;
		loadFileJob.input2 = mapPath;
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
			this.mapBlocks [x, y] = mapBlockView;
			StartCoroutine (mapBlockView.Initialize (Worldx, Worldy, mapData, getMapPath(Worldx, Worldy, saveMapDataPath), yieldDirection, resourceManager, this));

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
			if (player.transform.position.x < worldstart.x) {  // Player moves Left

				for (int y = 0; y < DisplayBlocksYSize; y++) {
					int worldyy = Worldy - ((DisplayBlocksYSize - 1) /2) + y;
					UnLoadMap(Worldx + ((DisplayBlocksXSize - 1) /2), worldyy,  DisplayBlocksXCenter + ((DisplayBlocksXSize - 1) /2), y);
				}
				Worldx--;
				worldstart = calculateTransformPosition(Worldx, Worldy);
				worldend = new Vector3(MapRows *characterWidth ,-MapCols * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
				for (int x = DisplayBlocksXSize-2; x >= 0; x--) {
					for (int y = 0; y < DisplayBlocksYSize; y++) {
						worlds [x+1, y] = worlds [x, y];
						this.mapBlocks [x + 1, y] = this.mapBlocks [x, y];
					}
				}

				for (int y = 0; y < DisplayBlocksYSize; y++) {
					int worldyy = Worldy - ((DisplayBlocksYSize - 1) /2) + y;
					LoadMapThreaded(Worldx - ((DisplayBlocksXSize - 1) /2), worldyy,  DisplayBlocksXCenter - ((DisplayBlocksXSize - 1) / 2), y, YieldDirection.YieldLeft);
				}
			}
			if (player.transform.position.x > worldend.x) { // Player Moves Right
				for (int y = 0; y < DisplayBlocksYSize; y++) {
					int worldyy = Worldy - ((DisplayBlocksYSize - 1) /2) + y;
					UnLoadMap(Worldx - ((DisplayBlocksXSize - 1) /2), worldyy,  DisplayBlocksXCenter - ((DisplayBlocksXSize - 1) /2), y);
				}
				Worldx++;
				worldstart = calculateTransformPosition(Worldx, Worldy);
				worldend = new Vector3(MapRows *characterWidth ,-MapCols * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
				for (int x = 0; x <= DisplayBlocksXSize-2; x++) {
					for (int y = 0; y < DisplayBlocksYSize; y++) {
						worlds [x, y] = worlds [x+1, y];
						this.mapBlocks [x, y] = this.mapBlocks [x+1, y];
					}
				}
				for (int y = 0; y < DisplayBlocksYSize; y++) {
					int worldyy = Worldy - ((DisplayBlocksYSize - 1) /2) + y;
					LoadMapThreaded(Worldx + ((DisplayBlocksXSize - 1) /2), worldyy,  DisplayBlocksXCenter + ((DisplayBlocksXSize - 1) / 2), y, YieldDirection.YieldRight);
				}
			}
			if (player.transform.position.y > worldstart.y) {  // Player moves up
				for (int x = 0; x < DisplayBlocksXSize; x++) {
					int worldxx = Worldx - ((DisplayBlocksXSize - 1) /2) + x;
					UnLoadMap(worldxx,Worldy + ((DisplayBlocksYSize - 1) /2),  x, DisplayBlocksYCenter + ((DisplayBlocksYSize - 1) /2));
				}
					
				Worldy--;
				worldstart = calculateTransformPosition(Worldx, Worldy);
				worldend = new Vector3(MapRows *characterWidth ,-MapCols * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
				for (int y = DisplayBlocksYSize-2; y >= 0; y--) {
					for (int x = 0; x < DisplayBlocksXSize; x++) {
						worlds [x, y+1] = worlds [x, y];
						this.mapBlocks [x, y+1] = this.mapBlocks [x, y+1];
					}
				}
				for (int x = 0; x < DisplayBlocksXSize; x++) {
					int worldxx = Worldx - ((DisplayBlocksXSize - 1) /2) + x;
					LoadMapThreaded(worldxx, Worldy - ((DisplayBlocksYSize - 1) /2), x,  DisplayBlocksYCenter - ((DisplayBlocksYSize - 1) / 2), YieldDirection.YieldUp);
				}
			}
			if (player.transform.position.y < worldend.y) { // player moves down
				for (int x = 0; x < DisplayBlocksXSize; x++) {
					int worldxx = Worldx - ((DisplayBlocksXSize - 1) /2) + x;
					UnLoadMap(worldxx,Worldy - ((DisplayBlocksYSize - 1) /2),  x, DisplayBlocksYCenter - ((DisplayBlocksYSize - 1) /2));
				}

				Worldy++;
				worldstart = calculateTransformPosition(Worldx, Worldy);
				worldend = new Vector3(MapRows *characterWidth ,-MapCols * characterHeight,0) + calculateTransformPosition(Worldx, Worldy);
				for (int y = 0; y <= DisplayBlocksYSize-2; y++) {
					for (int x = 0; x < DisplayBlocksXSize; x++) {
						worlds [x, y] = worlds [x, y+1];
						this.mapBlocks [x, y] = this.mapBlocks [x, y+1];
					}
				}
				for (int x = 0; x < DisplayBlocksXSize; x++) {
					int worldxx = Worldx - ((DisplayBlocksXSize - 1) /2) + x;
					LoadMapThreaded(worldxx, Worldy + ((DisplayBlocksYSize - 1) /2), x,  DisplayBlocksYCenter + ((DisplayBlocksYSize - 1) / 2), YieldDirection.YieldUp);
				}
			}

		}
	}

	public void MoveObject (int newX, int newY, GameObject obj)
	{
		int x = newX;
		int y = newY;
		int newWorldX = DisplayBlocksXCenter;  // Center X 
		int newWorldY = DisplayBlocksYCenter;  // Center Y
		if (newX < 0) {
			newWorldX -= 1;
			x = MapRows + newX;
		}
		if (newX >= MapRows) {
			newWorldX += 1;
			x = newX - MapRows;
		}
		if (newY < 0) {
			newWorldY -= 1;
			y = MapCols + newY;
		}
		if (newY >= MapCols) {
			newWorldY += 1;
			y = newY - MapCols;
		}
		GameObject newWorld = worlds[newWorldX ,newWorldY];
		MapBlockView mapBlockView = newWorld.GetComponent<MapBlockView> ();
		if (mapBlockView != null) {
			
			mapBlockView.AddObject (x, y, obj);
		}

	}
}
