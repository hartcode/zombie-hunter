using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AssemblyCSharp {
	public class MapBlockView : MonoBehaviour {

		private int blockX;
		private int blockY;
		public MapBlockData mapBlockData;

		public int MapRows = 20;
		public int MapCols = 20;
		public float characterWidth = 0.08f;
		public float characterHeight = 0.16f;


		private GameObject prefabWall;
		private bool isInitialized = false;
		//private Map map;

		public void Initialize (int blockX, int blockY, MapBlockData mapBlockData) {
			this.blockX = blockX;
			this.blockY = blockY;
			this.mapBlockData = mapBlockData;				

			if (prefabWall == null) {
				prefabWall = (GameObject)Resources.Load ("Main/Wall", typeof(GameObject));
			}

			if (mapBlockData == null) {
				// Empty map blocks will display as walls
					for (int x = 0; x < MapRows ; x++) {
						for (int y = 0; y < MapCols; y++) {
						CreateMapObject (x, y, prefabWall, this.gameObject, "Main/Wall", 6, MapLayer.Floor);
						}
					}
			} else {
				for (int x = 0; x < mapBlockData.getRows (); x++) {
					for (int y = 0; y < mapBlockData.getCols (); y++) {
						if (x < 0 || x == mapBlockData.getRows () || y < 0 || y == mapBlockData.getCols ()) {
							CreateMapObject (x, y, prefabWall, this.gameObject, "Main/Wall", 6, MapLayer.Floor);
							} else {
							GameObject floorObject = (GameObject)Resources.Load (mapBlockData.getFloorResource (x, y), typeof(GameObject));
								if (floorObject != null) {
								CreateMapObject (x, y, floorObject, this.gameObject, mapBlockData.getFloorResource (x, y), mapBlockData.getFloorInt(x, y), MapLayer.Floor);
								}
							GameObject mainObject = (GameObject)Resources.Load (mapBlockData.getMainResource (x, y), typeof(GameObject));
								if (mainObject != null) {
								CreateMapObject (x, y, mainObject, this.gameObject, mapBlockData.getMainResource (x, y), mapBlockData.getMainInt(x, y), MapLayer.Main);
								}
							}
						}
					}
			}
			isInitialized = true;
		}
	
		public void MoveObject(int x, int y, int newX, int newY, GameObject obj) {
			if (isInitialized) {
				if (mapBlockData != null) {
					if (x >= 0 && x < mapBlockData.getRows () &&
					   y >= 0 && y < mapBlockData.getCols ()) {
						AddObject (x, y, obj);			
						RemoveObject (x, y);
					} else {
						// move object to a different BlockView
					}
				}
			} else {
				throw new UnityException ("MapBlockView isn't initialized");
			}
		}

		public void RemoveObject(int x, int y) {
				if (isInitialized) {
				if (mapBlockData != null) {
					mapBlockData.setMainInt (x, y, 0);
				}
			} else {
				throw new UnityException ("MapBlockView isn't initialized");
			}
		}
	
		public void AddObject(int x, int y, GameObject obj) {
				if (isInitialized) {
				if (mapBlockData != null) {
					if (obj != null) {
						obj.transform.parent = this.gameObject.transform;
						MapValue mapValue = obj.GetComponent<MapValue> ();
						if (mapValue != null) {
							switch (mapValue.layer) {
							case MapLayer.Floor:
								mapBlockData.setFloorInt (x, y, mapValue.intValue);
								break;
							case MapLayer.Main:
								mapBlockData.setMainInt (x, y, mapValue.intValue);
								break;
							}
						}
					}
				}
			} else {
				throw new UnityException ("MapBlockView isn't initialized");
			}
		}

		private void CreateMapObject (int x, int y, GameObject mapPrefab, GameObject parent, string resourceName, int resourceInt, MapLayer layer)
		{
			if (mapPrefab != null) {
				if (parent != null) {
					GameObject prefab = (GameObject)Instantiate (mapPrefab,parent.transform.position + calculateTransformPosition(x,y) , Quaternion.identity, parent.transform);
					MapPosition mapPosition = prefab.GetComponent<MapPosition> ();
					if (mapPosition == null) {
						throw new MissingComponentException ("Map Object is missing the MapPosition component");
					}
					mapPosition.originX = x;
					mapPosition.originY = y;
					mapPosition.mapBlockView = this;
					MapValue mapValue =	prefab.AddComponent<MapValue> ();
					mapValue.intValue = resourceInt;
					mapValue.strValue = resourceName;
					mapValue.layer = layer;
				}
			} else {
				throw new MissingReferenceException ("Map Prefab Reference Missing");
			}
		}

		private Vector3 calculateTransformPosition(int x, int y) {
			Vector3 retval;
			retval = new Vector3 ((x * characterWidth), (-y * characterHeight), 0);
			return retval;
		}
	}
}