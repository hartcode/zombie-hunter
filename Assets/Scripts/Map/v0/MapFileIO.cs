using System;
using System.IO;
using System.Collections;
using UnityEngine;

namespace AssemblyCSharp.Map.v0
{
	public class MapFileIO : IMapFileIO
	{

		public MapBlockData LoadFile (BinaryReader reader)
		{
			MapBlockData retval = null;
			if (reader == null) {
				throw new NullReferenceException ("Parameter reader cannot be null");
			}

			int floorResourceCount = reader.ReadInt32 ();
			String[] floorResources = new String[floorResourceCount];
			for (int i = 0; i < floorResourceCount; i++) {
				floorResources [i] = reader.ReadString ();
			}

			int mainResourceCount = reader.ReadInt32 ();
			String[] mainResources = new String[mainResourceCount];
			for (int i = 0; i < mainResourceCount; i++) {
				mainResources [i] = reader.ReadString ();
			}

			int rows = reader.ReadInt32 ();
			int cols = reader.ReadInt32 ();

			int[,] floorarray = new int[rows, cols];
			for (int x = 0; x < rows; x++) {
				for (int y = 0; y < cols; y++) {
					floorarray [x, y] = reader.ReadInt32 ();
				}
			}

			int[,] array = new int[rows, cols];
			for (int x = 0; x < rows; x++) {
				for (int y = 0; y < cols; y++) {
					array [x, y] = reader.ReadInt32 ();
				}
			}

			retval = new MapBlockData (rows, cols, floorarray, array, floorResources, mainResources);

			return retval;
		}

		public void SaveFile (MapBlockData mapData, BinaryWriter writer)
		{
			if (mapData == null) {
				throw new NullReferenceException ("Parameter mapData cannot be null");
			}
			if (writer == null) {
				throw new NullReferenceException ("Parameter writer cannot be null");
			}

			Int32 rows = mapData.getRows ();
			Int32 cols = mapData.getCols ();
			ArrayList floorResources = new ArrayList ();
			ArrayList mainResources = new ArrayList ();
			Int32[,] floorarray = new Int32[rows, cols];
			Int32[,] array = new Int32[rows, cols];
			for (int x = 0; x < rows; x++) {
				for (int y = 0; y < cols; y++) {
					String floorResource = mapData.getFloorResource (x, y);
						if (!floorResources.Contains (floorResource)) {
							floorResources.Add (floorResource);
							floorarray [x, y] = floorResources.Count - 1;
						} else {
							floorarray [x, y] = floorResources.IndexOf (floorResource);
						}

					String mainResource = mapData.getMainResource (x, y);
						if (!mainResources.Contains (mainResource)) {
							mainResources.Add (mainResource);
							array [x, y] = mainResources.Count - 1;
						} else {
							array [x, y] = mainResources.IndexOf (mainResource);
						}


				}
			}

			writer.Write ((Int32)floorResources.Count);
			for (int i = 0; i < floorResources.Count; i++) {
				String floorResource = (String)floorResources [i];
				if (floorResource == null) {
					floorResource = "";
				}
				writer.Write (floorResource);
			}

			writer.Write ((Int32)mainResources.Count);
			for (int i = 0; i < mainResources.Count; i++) {
				String mainResource = (String)mainResources [i];
				if (mainResource == null) {
					mainResource = "";
				}
				writer.Write (mainResource);
			}

			writer.Write ((Int32)rows);
			writer.Write ((Int32)cols);

			for (int x = 0; x < rows; x++) {
				for (int y = 0; y < cols; y++) {
					writer.Write ((Int32)floorarray [x, y]);
				}
			}

			for (int x = 0; x < rows; x++) {
				for (int y = 0; y < cols; y++) {
					writer.Write ((Int32)array [x, y]);
				}
			}


		}

	}
}
