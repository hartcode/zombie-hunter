using System;
using UnityEngine;
namespace AssemblyCSharp
{
	public class MapData
	{

		int rows;
		int cols;
		int[,] floorarray =  new [,] {
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 0, 0, 0, 1, 0, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 0, 0, 0, 0, 1, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 0, 0, 0, 0, 0, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 0, 0, 0, 0, 0, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 0, 0, 0, 0, 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 0, 0, 0, 1, 0, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 0, 0, 0, 0, 1, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 0, 0, 0, 0, 0, 2, 2, 0, 10, 1, -1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 0, 0, 0, 0, 0, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 0, 0, 0, 0, 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
			{ 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
		};

		int[,] array =  new [,] {
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1}
		};

		GameObject[] floorPrefabs;
		String[] floorResources;
		GameObject[] mainPrefabs;
		String[] mainResources;

		int minFloorPrefabs = 0;
		int maxFloorPrefabs;
		int minMainPrefabs = 0;
		int maxMainPrefabs;


		public MapData ()
		{
			rows = 20;
			cols = 20;

			maxFloorPrefabs = 2;
			floorResources = new String[maxFloorPrefabs+1];
			floorResources [0] = null;
			floorResources [1] = "Flooring/Grass";
			floorResources [2] = "Flooring/DirtRoad";


			maxMainPrefabs = 1;
			mainResources = new String[maxMainPrefabs+1];
			mainResources [0] = null;
			mainResources [1] = "Main/Tree";

			floorPrefabs = new GameObject[maxFloorPrefabs+1];
			for (int i = 0; i < maxFloorPrefabs+1,i++) {
				floorPrefabs [i] = (GameObject)Resources.Load (floorResources[i], typeof(GameObject));
			}


			mainPrefabs = new GameObject[maxMainPrefabs+1];
			for (int i = 0; i < maxMainPrefabs+1,i++) {
				mainPrefabs [i] = (GameObject)Resources.Load (mainResources[i], typeof(GameObject));
			}
		}

		public MapData(int rows, int cols, int[,] floorarray, int[,]array, String[] floorResources, String[] mainResources)
		{
			this.rows = rows;
			this.cols = cols;

			for (int x = 0; x < rows; x++) {
				for (int y = 0; y < cols; y++) {
					this.floorarray[x,y] = floorarray[x,y];
					this.array[x,y] = array[x,y];
				}
			}

			maxFloorPrefabs = floorResources.Length;
			floorPrefabs = new GameObject[maxFloorPrefabs+1];
			for (int i = 0; i < maxFloorPrefabs) {
				this.floorResources[i] = floorResources[i];
				floorPrefabs[i] = (GameObject)Resources.Load (floorResources[i], typeof(GameObject));
			}

			maxMainPrefabs = mainResources.Length;
			mainPrefabs = new GameObject[maxMainPrefabs+1];
			for (int i = 0; i < maxMainPrefabs) {
				this.mainResources[i] = mainResources[i];
				mainPrefabs[i] = (GameObject)Resources.Load (mainResources[i], typeof(GameObject));
			}

		}

		public int getRows()
		{
			return rows;
		}

		public int getCols() {
			return rows;
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
