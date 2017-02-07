using System;
using System.IO;
using UnityEngine;
namespace AssemblyCSharp.Map.v0
{
	public interface MapFileIO : IMapFileIO
	{

		public MapData LoadFile(BinaryReader reader) {
			MapData retval = null;
				if (reader == null) {
					throw new NullReferenceException("Parameter reader cannot be null");
				}

        int floorResourceCount = reader.ReadInt32();
        String[] floorResources = new String[floorResourceCount];
        for (int i = 0; i < floorResourceCount; i++){
          floorResources[i] = reader.ReadString();
        }

        int mainResourceCount = reader.ReadInt32();
        String[] mainResources = new String[mainResourceCount];
        for (int i = 0; i < mainResourceCount; i++){
          mainResources[i] = reader.ReadString();
        }

        int rows = reader.ReadInt32();
        int cols = reader.ReadInt32();

        int[,] floorarray =  new [rows,cols];
        for (int x = 0; x < rows; x++) {
          for (int y = 0; y < cols; y++) {
            floorarray[x,y] = reader.ReadInt32();
          }
        }

        int[,] array =  new [rows,cols];
        for (int x = 0; x < rows; x++) {
          for (int y = 0; y < cols; y++) {
            array[x,y] = reader.ReadInt32();
          }
        }

        retval = new MapData(rows, cols, floorarray, array, floorResources, mainResources);

				return retval;
		}

    public void SaveFile(MapData mapData, BinaryWriter writer) {
      if (mapData == null) {
        throw new NullReferenceException("Parameter mapData cannot be null");
      }
      if (writer == null) {
        throw new NullReferenceException("Parameter writer cannot be null");
      }

      int rows = mapData.getRows();
      int cols = mapData.getCols();
      Vector<String> floorResources = new Vector<String>();
      Vector<String> mainResources = new Vector<String>();
      int[,] floorarray =  new [rows,cols];
      int[,] array =  new [rows,cols];
      for (int x = 0; x < rows; x ++) {
        for (int y = 0; y < cols; y++) {
          String floorResource = mapData.getFloorResource(x, y);
          if (!floorResources.contains(floorResource)) {
            floorResources.add(floorResource);
            floorarray[x,y] = floorResources.Count;
          } else {
            floorarray[x,y] = floorResources.Find(floorResource);
          }

          String mainResource = mapData.getMainResource(x, y);
          if (!mainResources.contains(mainResource)) {
            mainResources.add(mainResource);
            array[x,y] = mainResources.Count;
          } else {
            array[x,y] = mainResources.Find(mainResource);
          }

        }
      }

      writer.WriteInt32(floorResources.Count);
      for(int i=0; i < floorResources.Count; i++){
        writer.WriteString(floorResources.getAt(i));
      }

      writer.WriteInt32(mainResources.Count);
      for(int i=0; i < mainResources.Count; i++){
        writer.WriteString(mainResources.getAt(i));
      }

      writer.WriteInt32(rows);
      writer.WriteInt32(cols);

      for (int x = 0; x < rows; x++) {
        for (int y = 0; y < cols; y++) {
          writer.WriteInt32(floorarray[x,y]);
        }
      }

      for (int x = 0; x < rows; x++) {
        for (int y = 0; y < cols; y++) {
          writer.WriteInt32(array[x,y]);
        }
      }


    }

	}
}
