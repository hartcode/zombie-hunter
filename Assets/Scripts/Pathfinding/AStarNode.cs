using System;

namespace AssemblyCSharp
{
    class AStarNode : MapNode
    {
        public int Counter;

		public AStarNode(MapNode node, int counter) : base(node.x, node.y) {
			this.Counter = counter;
		}
    }
}
