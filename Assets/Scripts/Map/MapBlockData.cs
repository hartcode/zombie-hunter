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
			
		String[] floorResources;
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
			this.floorResources = new String[maxFloorPrefabs + 1];
			for (int i = 0; i < maxFloorPrefabs; i++) {
				this.floorResources[i] = floorResources[i];
				//floorPrefabs[i] = (GameObject)Resources.Load (floorResources[i], typeof(GameObject));
			}
		
			maxMainPrefabs = mainResources.Length;
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


		public int getMainInt(int x, int y) {
			return array [x, y];
		}

		public void setMainInt(int x, int y, String newid) {
			int i = 0; 
			// exit while if we reach the end of the array or find a match
			while(i < maxMainPrefabs && newid != mainResources[i]) {
				i++;
			}
			// check to see if we need to add the resource to the array
			if (i >= maxMainPrefabs) {
				// Add new resource to floorResources array
				string[] temp = new String[maxMainPrefabs + 1];
				for (int a = 0; a < maxMainPrefabs; a++) {
					temp[a] = mainResources[a];
				}
				maxMainPrefabs++;
				temp[maxMainPrefabs-1] = newid;
				mainResources = temp;
			}

			// either we found the resource in the array or we added it.
			array [x, y] = i;
		}

		public int getFloorInt(int x, int y) {
			return floorarray [x, y];
		}

		public void setFloorInt(int x, int y, String newid) {
			int i = 0; 
			// exit while if we reach the end of the array or find a match
			while(i < maxFloorPrefabs && newid != floorResources[i]) {
				i++;
			}
			// check to see if we need to add the resource to the array
			if (i >= maxFloorPrefabs) {
				// Add new resource to floorResources array
				string[] temp = new String[maxFloorPrefabs + 1];
				for (int a = 0; a < maxFloorPrefabs; a++) {
					temp[a] = floorResources[a];
				}
				maxFloorPrefabs++;
				temp[maxFloorPrefabs-1] = newid;
				floorResources = temp;
			}

			// either we found the resource in the array or we added it.
			floorarray [x, y] = i;
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
