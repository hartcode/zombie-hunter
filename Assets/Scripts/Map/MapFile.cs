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
					if (fileVersion > maploaders.length || fileVersion < 0) {
						throw new Exception("File Version {0} is unsupported", fileVersion);
					}

					retval = maploaders[fileVersion].LoadFile(reader);

				}finally {
					if (reader != null) {
						reader.close();
						reader.dispose();
						reader = null;
					}
					if (stream != null) {
						stream.flush();
						stream.close();
						stream.dispose();
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

					writer.WriteInt32(VersionNumber);
					maploaders[maploaders.Length].SaveFile(mapData, writer);

				}finally {
					if (writer != null) {
						writer.close();
						writer.dispose();
						writer = null;
					}
					if (stream != null) {
						stream.flush();
						stream.close();
						stream.dispose();
						stream = null;
					}
				}
		}

	}
}
