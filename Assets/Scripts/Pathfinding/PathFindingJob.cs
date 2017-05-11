using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class PathFindingJob : ThreadedJob
	{
		public int inputStartX;
		public int inputStartY;
		public int inputEndX;
		public int inputEndY;
		public int maxCounter;

		public PathFinding pathFinding;
		public List<MapNode> outputPath; 

		protected override void ThreadFunction()
		{
			outputPath = pathFinding.pathFinding(new MapNode(inputStartX, inputStartY),new MapNode(inputEndX, inputEndY));
		}
	}
}
