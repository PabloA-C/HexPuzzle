using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileScript
{
		private string type, name;
		private Coordinate coordinates;
		private int rotation;
		private int exit1 = 0;
		private int exit2 = 0;
		
		// Use this for initialization
		void Start ()
		{
			
		}

		public TileScript (string type, string name, Coordinate coordinate, int rotation)
		{
				this.type = type;
				this.name = name;
				this.coordinates = coordinate;
				this.rotation = rotation;
	
				int mod = 1;
				if (exit1 > 3) {
						mod = -1;
				}

				switch (type) {
				
				case "Straight":
						exit1 = 1 + rotation;
						exit2 = mod * 3;
						break;
				case "Turn":
						exit1 = 1 + rotation;
						exit2 = mod * 2;
						break;

				case "SharpTurn":
						exit1 = 1 + rotation;
						exit2 = mod;
						break;
				}
		
		}

		public List<int> getExits ()
		{
				List<int> exits = new System.Collections.Generic.List<int> ();
				
				exits.Add (exit1);
				exits.Add (exit2);
				return exits;
			
		
		}

		public Coordinate getCoordinates ()
		{
				return coordinates;
		}
		
		public int getRotation ()
		{
				return rotation;
		}

		public string getName ()
		{
				return name;
		}

		public string getType ()
		{
				return type;
		}
		

}