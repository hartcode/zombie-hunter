using System;
using System.Collections.Generic;

namespace AssemblyCSharp
{
    class PathFinding
    {
		MapBlockData mapBlockData = null;

		public PathFinding(MapBlockData mapBlockData) {
			this.mapBlockData = mapBlockData;
		}
   

		// checks if a position in the map is open
		bool isMapPositionOpen(int x, int y)
		{
			bool retval = false;
			if (x >= 0 && x < this.mapBlockData.getRows() && y >= 0 && y < this.mapBlockData.getCols())
			{
				retval = this.mapBlockData.getMainInt (x, y) == 0 || this.mapBlockData.getFloorInt (x, y) != 3;
			}
			else
			{
				// mapPosition is on another map
			}

			return retval;
		}


		// Gets a list of open positions around the given node
		List<MapNode> getOpenNodesAround(MapNode node)
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
							if (isMapPositionOpen(node.x + x, node.y + y))
							{
								MapNode newNode = new MapNode();
								newNode.x = node.x + x;
								newNode.y = node.y + y;
								retval.Add(newNode);
							}
						}
					}
				}
			}
			return retval;
		}


		// Pathfinding Algorithm
		public List<MapNode> pathFinding(MapNode start, MapNode end, int maxCounter = 20)
		{
			List<MapNode> retval = new List<MapNode>();
			List<AStarNode> main = new List<AStarNode>();
			AStarNode lastNode = null;
			int counter = 0;
			AStarNode startNode = new AStarNode (start, counter);
			main.Add(startNode);

			bool found = false;
			for (int i = 0; i < main.Count; i++)
			{
				AStarNode node = main[i];
				if (end.x == node.x && end.y == node.y)
				{
					found = true;
				}
			}
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
						if (end.x == node.x && end.y == node.y)
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
