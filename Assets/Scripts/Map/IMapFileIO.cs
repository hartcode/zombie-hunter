using System;
using System.IO;
using UnityEngine;
namespace AssemblyCSharp
{
	public interface IMapFileIO
	{
		MapData LoadFile(BinaryReader reader);
		void SaveFile(MapData mapData, BinaryWriter writer);
	}
}
