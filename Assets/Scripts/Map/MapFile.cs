using System;
using System.IO;
using UnityEngine;
namespace AssemblyCSharp
{
	public class MapFile
	{

		IMapFileIO[] maploaders;

		public MapFile()
		{
			maploaders = new IMapFileIO[1];
			maploaders[0] = new AssemblyCSharp.Map.v0.MapFileIO();
		}


		public MapData LoadFile(String path) {
			MapData retval = null;
				if (path == null) {
					throw new NullReferenceException("Parameter path cannot be null");
				}
				FileStream stream = null;
				BinaryReader reader = null;
				try {
					stream = File.OpenRead(path);
					reader = new BinaryReader(stream);

					int fileVersion = reader.ReadInt32();
					if (fileVersion > maploaders.Length || fileVersion < 0) {
					//TODO: display fileversion in exception
						throw new Exception("File Version {0} is unsupported");
					}

					retval = maploaders[fileVersion].LoadFile(reader);
			}catch(Exception)
			{
				//TODO: log exceptions					
			}finally {
					if (reader != null) {
						reader.Close();
						reader = null;
					}
					if (stream != null) {
						stream.Close();
						stream.Dispose();
						stream = null;
					}
				}
				return retval;
		}

		public void SaveFile(MapData mapData, String path) {
			if (mapData == null) {
				throw new NullReferenceException("Parameter mapData cannot be null");
			}
			if (path == null) {
				throw new NullReferenceException("Parameter path cannot be null");
			}
				FileStream stream = null;
				BinaryWriter writer = null;
				try {
					stream = File.OpenWrite(path);
					writer = new BinaryWriter(stream);

				writer.Write(maploaders.Length-1);
					maploaders[maploaders.Length-1].SaveFile(mapData, writer);

				}finally {
					if (writer != null) {
						writer.Close();
						writer = null;
					}
					if (stream != null) {
						stream.Close();
						stream.Dispose();
						stream = null;
					}
				}
		}

	}
}
