using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AssemblyCSharp {
	public class MapBlockView : MonoBehaviour {

		public int blockX;
		public int blockY;
		public MapBlockData mapBlockData;
		//public Map map;


		// Use this for initialization
		void Start () {
			
		}
	
		public void MoveObject(int x, int y, int newX, int newY, GameObject obj) {}

		public void RemoveObject(int x, int y) {
			
		}
	
		public void AddObject(int x, int y, GameObject obj) {

		}
	}
}