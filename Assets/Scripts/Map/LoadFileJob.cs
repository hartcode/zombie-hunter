using System;

namespace AssemblyCSharp
{
	public class LoadFileJob : ThreadedJob
	{
		public String input;
		public String input2;
		public int Worldx;
		public int Worldy;
		public int x;
		public int y;
        public YieldDirection yieldDirection;
		public MapBlockData output;


		protected override void ThreadFunction()
		{
			MapFile mapfile = new MapFile();
			output = mapfile.LoadFile (input);
			if (output == null) {
				output = mapfile.LoadFile (input2);
			}
		}
	}
}
