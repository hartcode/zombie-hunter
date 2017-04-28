using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class MapBlockView : MonoBehaviour
	{

		private int blockX;
		private int blockY;
		public MapBlockData mapBlockData;

		public int MapRows = 20;
		public int MapCols = 20;
		public float characterWidth = 0.08f;
		public float characterHeight = 0.16f;
		protected ResourceManager resourceManager = null;
		protected string mapPath;
		protected MapFile mapfile;
		protected AsciiMapScript asciiMapScript = null;

		private GameObject prefabWall;
		private GameObject prefabWorldWall;
		private bool isInitialized = false;


		void Start ()
		{
			

		}

		public IEnumerator Initialize (int blockX, int blockY, MapBlockData mapBlockData, String mapPath, YieldDirection yieldDirection, ResourceManager resourceManager, AsciiMapScript asciiMapScript)
		{
			this.blockX = blockX;
			this.blockY = blockY;
			this.mapBlockData = mapBlockData;
			this.mapPath = mapPath;
			this.resourceManager = resourceManager;
			this.asciiMapScript = asciiMapScript;
			this.mapfile = new MapFile ();
			if (this.prefabWall == null) {
				this.prefabWall = resourceManager.getGameObject ("Main/Wall");
			}
			if (this.prefabWorldWall == null) {
				this.prefabWorldWall = resourceManager.getGameObject ("Main/worldwall");
			}

			if (mapBlockData == null) {
				// Empty map blocks will display as walls
				CreateMapObject (MapRows / 2, MapCols / 2, prefabWorldWall, this.gameObject, "Main/WorldWall", 6, MapLayer.Floor);
			} else {

				if (yieldDirection == YieldDirection.YieldRight) {
					for (int x = 0; x < mapBlockData.getRows (); x++) {
						if (this != null) {
							for (int y = 0; y < mapBlockData.getCols (); y++) {
								if (x < 0 || x == mapBlockData.getRows () || y < 0 || y == mapBlockData.getCols ()) {
									CreateMapObject (x, y, prefabWall, this.gameObject, "Main/Wall", 6, MapLayer.Floor);
								} else {
									//GameObject floorObject = mapData.getFloor (x, y);
									GameObject floorObject = resourceManager.getGameObject (mapBlockData.getFloorResource (x, y));
									if (floorObject != null) {
										CreateMapObject (x, y, floorObject, this.gameObject, mapBlockData.getFloorResource (x, y), mapBlockData.getFloorInt (x, y), MapLayer.Floor);
									}
									//GameObject mainObject = mapData.getMain (x, y);
									GameObject mainObject = resourceManager.getGameObject (mapBlockData.getMainResource (x, y));
									if (mainObject != null) {
										CreateMapObject (x, y, mainObject, this.gameObject, mapBlockData.getMainResource (x, y), mapBlockData.getMainInt (x, y), MapLayer.Main);
									}
								}
							}
						}
						yield return null;
					}
				} else if (yieldDirection == YieldDirection.YieldLeft) {
					for (int x = mapBlockData.getRows () - 1; x >= 0; x--) {
						if (this != null) {
							for (int y = 0; y < mapBlockData.getCols (); y++) {
								if (x < 0 || x == mapBlockData.getRows () || y < 0 || y == mapBlockData.getCols ()) {
									CreateMapObject (x, y, prefabWall, this.gameObject, "Main/Wall", 6, MapLayer.Floor);
								} else {
									//GameObject floorObject = mapData.getFloor (x, y);
									GameObject floorObject = resourceManager.getGameObject (mapBlockData.getFloorResource (x, y));
									if (floorObject != null) {
										CreateMapObject (x, y, floorObject, this.gameObject, mapBlockData.getFloorResource (x, y), mapBlockData.getFloorInt (x, y), MapLayer.Floor);
									}
									//GameObject mainObject = mapData.getMain (x, y);
									GameObject mainObject = resourceManager.getGameObject (mapBlockData.getMainResource (x, y));
									if (mainObject != null) {
										CreateMapObject (x, y, mainObject, this.gameObject, mapBlockData.getMainResource (x, y), mapBlockData.getMainInt (x, y), MapLayer.Main);
									}
								}
							}
						}
						yield return null;
					}
				} else if (yieldDirection == YieldDirection.YieldDown) {
					if (this != null) {
						for (int y = 0; y < mapBlockData.getCols (); y++) {
							for (int x = 0; x < mapBlockData.getRows (); x++) {
								if (x < 0 || x == mapBlockData.getRows () || y < 0 || y == mapBlockData.getCols ()) {
									CreateMapObject (x, y, prefabWall, this.gameObject, "Main/Wall", 6, MapLayer.Floor);
								} else {
									//GameObject floorObject = mapData.getFloor (x, y);
									GameObject floorObject = resourceManager.getGameObject (mapBlockData.getFloorResource (x, y));
									if (floorObject != null) {
										CreateMapObject (x, y, floorObject, this.gameObject, mapBlockData.getFloorResource (x, y), mapBlockData.getFloorInt (x, y), MapLayer.Floor);
									}
									//GameObject mainObject = mapData.getMain (x, y);
									GameObject mainObject = resourceManager.getGameObject (mapBlockData.getMainResource (x, y));
									if (mainObject != null) {
										CreateMapObject (x, y, mainObject, this.gameObject, mapBlockData.getMainResource (x, y), mapBlockData.getMainInt (x, y), MapLayer.Main);
									}
								}
							}
						}
						yield return null;
					}
				} else if (yieldDirection == YieldDirection.YieldUp) {
					for (int y = mapBlockData.getCols () - 1; y >= 0; y--) {
						if (this != null) {
							for (int x = 0; x < mapBlockData.getRows (); x++) {
								if (x < 0 || x == mapBlockData.getRows () || y < 0 || y == mapBlockData.getCols ()) {
									CreateMapObject (x, y, prefabWall, this.gameObject, "Main/Wall", 6, MapLayer.Floor);
								} else {
									//GameObject floorObject = mapData.getFloor (x, y);
									GameObject floorObject = resourceManager.getGameObject (mapBlockData.getFloorResource (x, y));
									if (floorObject != null) {
										CreateMapObject (x, y, floorObject, this.gameObject, mapBlockData.getFloorResource (x, y), mapBlockData.getFloorInt (x, y), MapLayer.Floor);
									}
									//GameObject mainObject = mapData.getMain (x, y);
									GameObject mainObject = resourceManager.getGameObject (mapBlockData.getMainResource (x, y));
									if (mainObject != null) {
										CreateMapObject (x, y, mainObject, this.gameObject, mapBlockData.getMainResource (x, y), mapBlockData.getMainInt (x, y), MapLayer.Main);
									}
								}
							}
						}
						yield return null;
					}
				} else {
					for (int x = 0; x < mapBlockData.getRows (); x++) {
						if (this != null) {
							for (int y = 0; y < mapBlockData.getCols (); y++) {
								if (x < 0 || x == mapBlockData.getRows () || y < 0 || y == mapBlockData.getCols ()) {
									CreateMapObject (x, y, prefabWall, this.gameObject, "Main/Wall", 6, MapLayer.Floor);
								} else {
									//GameObject floorObject = mapData.getFloor (x, y);
									GameObject floorObject = resourceManager.getGameObject (mapBlockData.getFloorResource (x, y));
									if (floorObject != null) {
										CreateMapObject (x, y, floorObject, this.gameObject, mapBlockData.getFloorResource (x, y), mapBlockData.getFloorInt (x, y), MapLayer.Floor);
									}
									//GameObject mainObject = mapData.getMain (x, y);
									GameObject mainObject = resourceManager.getGameObject (mapBlockData.getMainResource (x, y));
									if (mainObject != null) {
										CreateMapObject (x, y, mainObject, this.gameObject, mapBlockData.getMainResource (x, y), mapBlockData.getMainInt (x, y), MapLayer.Main);
									}
								}
							}
						}
						yield return null;
					}
				}
			}
			isInitialized = true;
		}

		public void MoveObject (int x, int y, int newX, int newY, GameObject obj)
		{
			if (isInitialized) {
				if (mapBlockData != null) {
					if (newX >= 0 && newX < mapBlockData.getRows () &&
					    newY >= 0 && newY < mapBlockData.getCols ()) {
						AddObject (newX, newY, obj);			
						RemoveObject (x, y, obj);
					} else {
						// move object to a different BlockView
						this.asciiMapScript.MoveObject(newX, newY, obj);
						RemoveObject (x, y, obj);
					}
				}
			} else {
				throw new UnityException ("MapBlockView isn't initialized");
			}
		}

		public void RemoveObject (int x, int y, GameObject obj)
		{
			if (isInitialized) {
				if (mapBlockData != null) {
					if (obj != null) {
						MapValue mapValue = obj.GetComponent<MapValue> ();
						if (mapValue != null) {
							switch (mapValue.layer) {
							case MapLayer.Floor:
								mapBlockData.setFloorInt (x, y, null);
								break;
							case MapLayer.Main:
								mapBlockData.setMainInt (x, y, null);
								break;
							}
						}
					}
				}
			} else {
				throw new UnityException ("MapBlockView isn't initialized");
			}
		}

		public void AddObject (int x, int y, GameObject obj)
		{
			if (isInitialized) {
				if (mapBlockData != null) {
					if (obj != null) {
						obj.transform.parent = this.gameObject.transform;
						MapPosition mapPosition = obj.GetComponent<MapPosition> ();
						if (mapPosition != null) {
							mapPosition.originX = x;
							mapPosition.originY = y;
							mapPosition.mapBlockView = this;
						}
						obj.transform.localPosition = calculateTransformPosition (x, y);
						obj.transform.position = calculateTransformPosition (x, y)
						MapValue mapValue = obj.GetComponent<MapValue> ();
						if (mapValue != null) {
							switch (mapValue.layer) {
							case MapLayer.Floor:
								mapBlockData.setFloorInt (x, y, mapValue.strValue);
								break;
							case MapLayer.Main:
								mapBlockData.setMainInt (x, y, mapValue.strValue);
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
					GameObject prefab = (GameObject)Instantiate (mapPrefab, parent.transform.position + calculateTransformPosition (x, y), Quaternion.identity, parent.transform);
					MapPosition mapPosition = prefab.GetComponent<MapPosition> ();
					if (mapPosition != null) {
						mapPosition.originX = x;
						mapPosition.originY = y;
						mapPosition.mapBlockView = this;
					}
					MapValue mapValue =	prefab.AddComponent<MapValue> ();
					mapValue.intValue = resourceInt;
					mapValue.strValue = resourceName;
					mapValue.layer = layer;
				}
			} else {
				throw new MissingReferenceException ("Map Prefab Reference Missing");
			}
		}

		private Vector3 calculateTransformPosition (int x, int y)
		{
			Vector3 retval;
			retval = new Vector3 ((x * characterWidth), (-y * characterHeight), 0);
			return retval;
		}

		void OnDisable ()
		{
			SaveMapThreaded (blockX, blockY);
			DestroyObject (this.gameObject);
		}

		void SaveMap (int Worldx, int Worldy)
		{
			if (this.mapBlockData != null) {
				mapfile.SaveFile (this.mapBlockData, mapPath);
			}
			
		}

		void SaveMapThreaded (int Worldx, int Worldy)
		{
			if (this.mapBlockData != null) {
				SaveFileJob saveFileJob = new SaveFileJob ();
				saveFileJob.input = this.mapBlockData;
				saveFileJob.path = mapPath;
				saveFileJob.Start ();
			}
		}
	}
}