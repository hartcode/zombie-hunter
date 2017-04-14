using System;

namespace AssemblyCSharp
{
	public class SaveFileJob : ThreadedJob
	{
		
		public MapData input;
		public string path;


		protected override void ThreadFunction()
		{
			MapFile mapfile = new MapFile();
			mapfile.SaveFile (input, path);
		}
	}
}
