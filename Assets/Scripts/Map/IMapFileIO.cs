using System;
using System.IO;
using UnityEngine;
namespace AssemblyCSharp
{
	public interface IMapFileIO
	{
		MapBlockData LoadFile(BinaryReader reader);
		void SaveFile(MapBlockData mapData, BinaryWriter writer);
	}
}
