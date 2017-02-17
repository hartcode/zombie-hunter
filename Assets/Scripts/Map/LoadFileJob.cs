using System;

namespace AssemblyCSharp
{
	public class LoadFileJob : ThreadedJob
	{
		public String input;
		public int Worldx;
		public int Worldy;
    public YieldDirection yieldDirection;
		public MapData output;


		protected override void ThreadFunction()
		{
			MapFile mapfile = new MapFile();
			output = mapfile.LoadFile (input);
		}
	}
}
