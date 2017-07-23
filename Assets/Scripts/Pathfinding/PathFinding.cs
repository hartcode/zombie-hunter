using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
    public class PathFinding
    {
		AsciiMapScript asciiMapScript = null;

		public PathFinding(AsciiMapScript asciiMapScript) {
			this.asciiMapScript = asciiMapScript;
		}
  
	
		// Gets a list of open positions around the given node
		List<MapNode> getOpenNodesAround(MapNode screenSpaceNode)
		{
			List<MapNode> retval = new List<MapNode>();
			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					if (x != 0 || y != 0)
					{
						if (x == 0 || y == 0)
						{
							if (this.asciiMapScript.isMapPositionOpen(screenSpaceNode.x + x, screenSpaceNode.y + y))
							{
								MapNode newNode = new MapNode();
								newNode.x = screenSpaceNode.x + x;
								newNode.y = screenSpaceNode.y + y;
								retval.Add(newNode);
							}
						}
					}
				}
			}
			return retval;
		}


		// Pathfinding Algorithm
		public List<MapNode> pathFinding(MapNode screenSpaceStart, MapNode screenSpacEnd, int maxCounter = 20)
		{
			List<MapNode> retval = new List<MapNode>();
			List<AStarNode> main = new List<AStarNode>();
			AStarNode lastNode = null;
			int counter = 0;
			AStarNode startNode = new AStarNode (screenSpaceStart, counter);
			main.Add(startNode);

			bool found = false;
			int lowest = Int32.MaxValue;
			MapNode foundNode = null;
			for (int i = 0; i < main.Count; i++)
			{
				AStarNode node = main[i];
				if (screenSpacEnd.x == node.x && screenSpacEnd.y == node.y)
				{
					if (lowest > node.Counter) {
						foundNode = node;
					}
				}
			}
			found = foundNode != null;

			while (!found)
			{
				counter++;
				if (counter > maxCounter)
				{
					found = true;
				}
				else
				{
					int cnt = main.Count;
					for (int i = 0; i < cnt; i++)
					{
						List<MapNode> opennodes = this.getOpenNodesAround(main[i]);
						foreach (MapNode node in opennodes)
						{
							bool isfound = false;
							for (int a = 0; a < main.Count; a++)
							{
								if (main[a].x == node.x && main[a].y == node.y && main[a].Counter <= counter)
								{
									isfound = true;
								}
							}
							if (!isfound)
							{
								main.Add(new AStarNode(node, counter));
							}
						}

					}
					found = false;
					for (int i = 0; i < main.Count; i++)
					{
						AStarNode node = main[i];
						if (screenSpacEnd.x == node.x && screenSpacEnd.y == node.y)
						{
							found = true;
							retval.Add(node);
							lastNode = node;
						}
					}
				}
			}
			if (counter <= maxCounter)
			{
				while (counter > 1)
				{
					counter--;
					foreach (AStarNode node in main)
					{
						int difx = Math.Abs(node.x - lastNode.x);
						int dify = Math.Abs(node.y - lastNode.y);
						if (node.Counter == counter && (difx <= 1) && (dify <= 1) && lastNode.Counter != counter)
						{
							retval.Insert(0, node);
							lastNode = node;
						}
					}

				}
			}

			return retval;
		}
    }
}
