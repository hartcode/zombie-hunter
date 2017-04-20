using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AssemblyCSharp
{
	public class MapBlockData
	{

		int rows;
		int cols;

		int[,] floorarray =  new [,] {
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
		};

		// mainResources [0] = null;
		// mainResources [1] = "Main/Tree";
		// mainResources [2] = "Main/Column";
		// mainResources [3] = "Main/Pot";
		// mainResources [4] = "Main/Box";
		// mainResources [5] = "Main/Barrel";
		// mainResources [6] = "Main/wall";

		int[,] array =  new [,] {
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
		};

		GameObject[] floorPrefabs;
		String[] floorResources;
		GameObject[] mainPrefabs;
		String[] mainResources;

		int minFloorPrefabs = 0;
		int maxFloorPrefabs;
		int minMainPrefabs = 0;
		int maxMainPrefabs;


		public MapBlockData ()
		{
			rows = 20;
			cols = 20;

			List<String> floorResourcesArray = new List<String> ();
		    List<String> mainResourcesArray = new List<String> ();

			floorResourcesArray.Add (null);
			floorResourcesArray.Add ("Flooring/Grass");
			floorResourcesArray.Add ("Flooring/DirtRoad");
			floorResourcesArray.Add ("Flooring/Water");
			floorResourcesArray.Add ("Flooring/Floor");

	
			mainResourcesArray.Add (null);
			mainResourcesArray.Add ("Main/Tree");
			mainResourcesArray.Add ("Main/Column");
			mainResourcesArray.Add ("Main/Pot");
			mainResourcesArray.Add ("Main/Box");
			mainResourcesArray.Add ("Main/Barrel");
			mainResourcesArray.Add ("Main/wall");



			floorPrefabs = new GameObject[floorResourcesArray.Count];
			for (int i = 0; i < floorResourcesArray.Count; i++) {
				floorPrefabs [i] = (GameObject)Resources.Load (floorResourcesArray[i], typeof(GameObject));
			}


			mainPrefabs = new GameObject[mainResourcesArray.Count];
			for (int i = 0; i < mainResourcesArray.Count; i++) {
				mainPrefabs [i] = (GameObject)Resources.Load (mainResourcesArray[i], typeof(GameObject));
			}
		}

		public MapBlockData(int rows, int cols, int[,] floorarray, int[,]array, String[] floorResources, String[] mainResources)
		{
			this.rows = rows;
			this.cols = cols;

			this.floorarray = new int[rows, cols];
			this.array = new int[rows, cols];
			for (int x = 0; x < rows; x++) {
				for (int y = 0; y < cols; y++) {
					this.floorarray[x,y] = floorarray[x,y];
					this.array[x,y] = array[x,y];
				}
			}

			maxFloorPrefabs = floorResources.Length;
			floorPrefabs = new GameObject[maxFloorPrefabs+1];
			this.floorResources = new String[maxFloorPrefabs + 1];
			for (int i = 0; i < maxFloorPrefabs; i++) {
				this.floorResources[i] = floorResources[i];
				//floorPrefabs[i] = (GameObject)Resources.Load (floorResources[i], typeof(GameObject));
			}
		
			maxMainPrefabs = mainResources.Length;
			mainPrefabs = new GameObject[maxMainPrefabs+1];
			this.mainResources = new String[maxMainPrefabs + 1];
			for (int i = 0; i < maxMainPrefabs;i++) {
				this.mainResources[i] = mainResources[i];
				//mainPrefabs[i] = (GameObject)Resources.Load (mainResources[i], typeof(GameObject));
			}

		}

		public int getRows()
		{
			return rows;
		}

		public int getCols() {
			return cols;
		}

		public GameObject getFloor(int x, int y) {
			GameObject retval = null;
			int arrayID = floorarray [x, y];
			if (arrayID >= minFloorPrefabs && arrayID <= maxFloorPrefabs) {
				retval = floorPrefabs [arrayID];
			}
			return retval;
		}

		public GameObject getMain(int x, int y) {
			GameObject retval = null;
			int arrayID = array [x, y];
			if (arrayID >= minMainPrefabs && arrayID <= maxMainPrefabs) {
				retval = mainPrefabs [arrayID];
			}
			return retval;
		}

		public int getMainInt(int x, int y) {
			return array [x, y];
		}

		public void setMainInt(int x, int y, int newid) {
			array[x,y] = newid;
		}

		public int getFloorInt(int x, int y) {
			return floorarray [x, y];
		}

		public void setFloorInt(int x, int y, int newid) {
			floorarray[x,y] = newid;
		}

		public String getFloorResource(int x, int y) {
			String retval = null;
			int arrayID = floorarray [x, y];
			if (arrayID >= minFloorPrefabs && arrayID <= maxFloorPrefabs) {
				retval = floorResources [arrayID];
			}
			return retval;
		}

		public String getMainResource(int x, int y) {
			String retval = null;
			int arrayID = array [x, y];
			if (arrayID >= minMainPrefabs && arrayID <= maxMainPrefabs) {
				retval = mainResources [arrayID];
			}
			return retval;
		}

	}
}
