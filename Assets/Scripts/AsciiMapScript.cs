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
	public String mapDataPath = "Assets/Maps/Test/";

	protected MapData[,] mapDataGroup = new MapData[3,3];
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

		if (prefabWall == null) {
			prefabWall = (GameObject)Resources.Load ("Main/Wall", typeof(GameObject));
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
		Vector3 playerPosition = calculateTransformPosition((int)(mapDataGroup[1,1].getCols() / 2),(int)(mapDataGroup[1,1].getRows() / 2), Worldx, Worldy);
		player.transform.position = playerPosition;
	}

	Vector3 calculateTransformPosition(int x, int y, int Worldx, int Worldy) {
		Vector3 retval;
		retval = new Vector3 (OriginX + (x * CharacterWidth) + (MapRows * CharacterWidth * Worldx), OriginY + (-y * CharacterHeight) + (MapCols * CharacterHeight * -Worldy), 0);
		return retval;
	}

	void calculateXYPosition(Vector3 pos, ref int x, ref int y, int Worldx, int Worldy) {
		x = (int)((pos.x -(MapRows * CharacterWidth * Worldx) - OriginX)/CharacterWidth);
		y = (int)((pos.y - (MapCols * CharacterHeight * -Worldy) -OriginY) / -CharacterHeight);
	}

	void CreateMapObject (int x, int y, GameObject mapPrefab, int Worldx, int Worldy, GameObject parent)
	{
		if (mapPrefab != null) {
			if (parent != null) {
				GameObject prefab = (GameObject)Instantiate (mapPrefab, calculateTransformPosition (x, y, Worldx, Worldy), Quaternion.identity, parent.transform);
				prefab.name = getObjectName("obj", x, y);
			}
		} else {
			throw new MissingReferenceException ("Map Prefab Reference Missing");
		}
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

	void SaveMap(int Worldx, int Worldy, int x, int y) {
		UnLoadMap (Worldx, Worldy, x, y);
		String mapPath = getMapPath (Worldx, Worldy);
		if (mapDataGroup[x,y] != null) {
			mapfile.SaveFile (mapDataGroup[x,y], mapPath);
		}
	}

	void SaveMapThreaded(int Worldx, int Worldy, int x, int y) {
		UnLoadMap (Worldx, Worldy, x, y);
		String mapPath = getMapPath (Worldx, Worldy);
		SaveFileJob saveFileJob = new SaveFileJob ();
		saveFileJob.input = mapDataGroup[x,y];
		saveFileJob.path = mapPath;
		saveFileJob.Start ();
	}



IEnumerator InstantiateMap(MapData mapData, int Worldx, int Worldy) {
		return InstantiateMap(mapData,Worldx,Worldy, YieldDirection.NoYield);
}

	IEnumerator InstantiateMap(MapData mapData, int Worldx, int Worldy, YieldDirection yieldDirection)
	{
		GameObject world = GameObject.Find (getWorldName (Worldx, Worldy));
		GameObject worldFloor;
		GameObject worldMain;
		if (world == null) {
			world = new GameObject (getWorldName (Worldx, Worldy));
			world.transform.parent = prefabParent.transform;

			worldFloor = new GameObject ("worldFloor");
			worldFloor.transform.parent = world.transform;

			worldMain = new GameObject ("worldMain");
			worldMain.transform.parent = world.transform;

			if (mapData == null) {
				// Empty map blocks will display as walls
				if (yieldDirection == YieldDirection.YieldRight) {
					for (int x = 0; x < MapRows; x++) {
						for (int y = 0; y < MapCols; y++) {
							CreateMapObject (x, y, prefabWall, Worldx, Worldy, worldFloor);
						}
						yield return null;
					}
				} else if (yieldDirection == YieldDirection.YieldLeft) {
					for (int x = MapRows-1; x >= 0; x--) {
						for (int y = 0; y < MapCols; y++) {
							CreateMapObject (x, y, prefabWall, Worldx, Worldy, worldFloor);
						}
						yield return null;
					}
				} else if (yieldDirection == YieldDirection.YieldDown) {
					for (int y = 0; y < MapCols; y++) {
						for (int x = 0; x < MapRows; x++) {
							CreateMapObject (x, y, prefabWall, Worldx, Worldy, worldFloor);
						}
						yield return null;
					}
				} else if (yieldDirection == YieldDirection.YieldUp) {
					for (int y = MapCols-1; y >= 0; y--) {
						for (int x = 0; x < MapRows ; x++) {
							CreateMapObject (x, y, prefabWall, Worldx, Worldy, worldFloor);
						}
						yield return null;
					}
				}else  {
					for (int x = 0; x < MapRows ; x++) {
						for (int y = 0; y < MapCols; y++) {
							CreateMapObject (x, y, prefabWall, Worldx, Worldy, worldFloor);
						}
					}
				}
			} else {

				if (yieldDirection == YieldDirection.YieldRight) {
					for (int x = 0; x < mapData.getRows (); x++) {
						for (int y = 0; y < mapData.getCols (); y++) {
							if (x < 0 || x == mapData.getRows () || y < 0 || y == mapData.getCols ()) {
								CreateMapObject (x, y, prefabWall, Worldx, Worldy, worldFloor);
							} else {
								//GameObject floorObject = mapData.getFloor (x, y);
								GameObject floorObject = (GameObject)Resources.Load (mapData.getFloorResource (x, y), typeof(GameObject));
								if (floorObject != null) {
									CreateMapObject (x, y, floorObject, Worldx, Worldy, worldFloor);
								}
								//GameObject mainObject = mapData.getMain (x, y);
								GameObject mainObject = (GameObject)Resources.Load (mapData.getMainResource (x, y), typeof(GameObject));
								if (mainObject != null) {
									CreateMapObject (x, y, mainObject, Worldx, Worldy, worldMain);
								}
							}
						}
						yield return null;
					}
				} else if (yieldDirection == YieldDirection.YieldLeft) {
					for (int x = mapData.getRows () - 1; x >= 0; x--) {
						for (int y = 0; y < mapData.getCols (); y++) {
							if (x < 0 || x == mapData.getRows () || y < 0 || y == mapData.getCols ()) {
								CreateMapObject (x, y, prefabWall, Worldx, Worldy, worldFloor);
							} else {
								//GameObject floorObject = mapData.getFloor (x, y);
								GameObject floorObject = (GameObject)Resources.Load (mapData.getFloorResource (x, y), typeof(GameObject));
								if (floorObject != null) {
									CreateMapObject (x, y, floorObject, Worldx, Worldy, worldFloor);
								}
								//GameObject mainObject = mapData.getMain (x, y);
								GameObject mainObject = (GameObject)Resources.Load (mapData.getMainResource (x, y), typeof(GameObject));
								if (mainObject != null) {
									CreateMapObject (x, y, mainObject, Worldx, Worldy, worldMain);
								}
							}
						}
						yield return null;
					}
				} else if (yieldDirection == YieldDirection.YieldDown) {
					for (int y = 0; y < mapData.getCols (); y++) {
						for (int x = 0; x < mapData.getRows (); x++) {
							if (x < 0 || x == mapData.getRows () || y < 0 || y == mapData.getCols ()) {
								CreateMapObject (x, y, prefabWall, Worldx, Worldy, worldFloor);
							} else {
								//GameObject floorObject = mapData.getFloor (x, y);
								GameObject floorObject = (GameObject)Resources.Load (mapData.getFloorResource (x, y), typeof(GameObject));
								if (floorObject != null) {
									CreateMapObject (x, y, floorObject, Worldx, Worldy, worldFloor);
								}
								//GameObject mainObject = mapData.getMain (x, y);
								GameObject mainObject = (GameObject)Resources.Load (mapData.getMainResource (x, y), typeof(GameObject));
								if (mainObject != null) {
									CreateMapObject (x, y, mainObject, Worldx, Worldy, worldMain);
								}
							}
						}
						yield return null;
					}
				} else if (yieldDirection == YieldDirection.YieldUp) {
					for (int y = mapData.getCols () - 1; y >= 0; y--) {
						for (int x = 0; x < mapData.getRows (); x++) {
							if (x < 0 || x == mapData.getRows () || y < 0 || y == mapData.getCols ()) {
								CreateMapObject (x, y, prefabWall, Worldx, Worldy, worldFloor);
							} else {
								//GameObject floorObject = mapData.getFloor (x, y);
								GameObject floorObject = (GameObject)Resources.Load (mapData.getFloorResource (x, y), typeof(GameObject));
								if (floorObject != null) {
									CreateMapObject (x, y, floorObject, Worldx, Worldy, worldFloor);
								}
								//GameObject mainObject = mapData.getMain (x, y);
								GameObject mainObject = (GameObject)Resources.Load (mapData.getMainResource (x, y), typeof(GameObject));
								if (mainObject != null) {
									CreateMapObject (x, y, mainObject, Worldx, Worldy, worldMain);
								}
							}
						}
						yield return null;
					}
				} else {
					for (int x = 0; x < mapData.getRows (); x++) {
						for (int y = 0; y < mapData.getCols (); y++) {
							if (x < 0 || x == mapData.getRows () || y < 0 || y == mapData.getCols ()) {
								CreateMapObject (x, y, prefabWall, Worldx, Worldy, worldFloor);
							} else {
								//GameObject floorObject = mapData.getFloor (x, y);
								GameObject floorObject = (GameObject)Resources.Load (mapData.getFloorResource (x, y), typeof(GameObject));
								if (floorObject != null) {
									CreateMapObject (x, y, floorObject, Worldx, Worldy, worldFloor);
								}
								//GameObject mainObject = mapData.getMain (x, y);
								GameObject mainObject = (GameObject)Resources.Load (mapData.getMainResource (x, y), typeof(GameObject));
								if (mainObject != null) {
									CreateMapObject (x, y, mainObject, Worldx, Worldy, worldMain);
								}
							}
						}
					}
				}

			}
		}
	}


	void UnLoadMap(int Worldx, int Worldy, int x, int y) {
		GameObject  world = GameObject.Find (getWorldName (Worldx, Worldy));
		Transform worldMainT = world.transform.FindChild ("worldMain");
		if (worldMainT != null) {
			for (int i = 0; i < worldMainT.childCount; i++) {
				Transform child = worldMainT.GetChild (i);
					int xobj = 0;
					int yobj = 0;
					String childName = child.name;
				int xindex = childName.IndexOf('x');
				if (xindex >= 0) {
					xobj = Int32.Parse (childName.Substring (3, xindex-3));
					yobj = Int32.Parse (childName.Substring (xindex+1));
					Vector3 pos = calculateTransformPosition (xobj, yobj, Worldx, Worldy);
					if (child.transform.position != pos) {
						int newXobj = 0;
						int newYobj = 0;
						calculateXYPosition (child.transform.position, ref newXobj, ref newYobj, Worldx, Worldy);
						// child has moved
						int newX = 1;
						int newY = 1;
						newX += (int)(newXobj / mapDataGroup [x, y].getRows ());
						newY += (int)(newYobj / mapDataGroup [x, y].getCols ());

						newXobj = (int)(newXobj % mapDataGroup [x, y].getRows ());
						newYobj = (int)(newYobj % mapDataGroup [x, y].getCols ());
						if (newXobj < 0) {
							newXobj += mapDataGroup [x, y].getRows ();
						}
						if (newYobj < 0) {
							newYobj += mapDataGroup [x, y].getCols ();
						}
							
						Debug.Log("new XY ("+ newX + ", " + newY + ")");
						Debug.Log("new XYobj ("+ newXobj + ", " + newYobj + ")");
						Debug.Log("XY ("+ x + ", " + y + ")");
						Debug.Log("XYobj ("+ xobj + ", " + yobj + ")");
						Debug.Log ("Child has moved");
						mapDataGroup[newX, newY].setMainInt(newXobj, newYobj, mapDataGroup [x, y].getMainInt (xobj, yobj));
						mapDataGroup [x, y].setMainInt (xobj, yobj, 0);
					
					}
				}
			}
		}

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
			Vector3 worldstart = calculateTransformPosition(0,0, Worldx, Worldy);
			Vector3 worldend = calculateTransformPosition (MapRows, MapCols, Worldx, Worldy);
			if (player.transform.position.x < worldstart.x) {
				SaveMap(Worldx + 1, Worldy -1,  2, 0);
				SaveMap(Worldx + 1, Worldy,     2, 1);
				SaveMap(Worldx + 1, Worldy + 1, 2, 2);
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
				SaveMap(Worldx - 1, Worldy -1,  0, 0);
				SaveMap(Worldx - 1, Worldy,     0, 1);
				SaveMap(Worldx - 1, Worldy + 1, 0, 2);
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
				SaveMap(Worldx - 1, Worldy + 1, 0, 2);
				SaveMap(Worldx, Worldy +1    ,  1, 2);
				SaveMap(Worldx + 1, Worldy + 1, 2, 2);
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
				SaveMap(Worldx - 1, Worldy - 1, 0, 0);
				SaveMap(Worldx, Worldy -1    ,  1, 0);
				SaveMap(Worldx + 1, Worldy - 1, 2, 0);
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
