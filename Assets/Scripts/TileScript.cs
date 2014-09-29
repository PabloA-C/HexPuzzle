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

		public TileScript ()
		{
		}
		

		public TileScript (string type, string name, Coordinate coordinate, int rotation)
		{
				this.type = type;
				this.name = name;
				this.coordinates = coordinate;
				this.rotation = rotation;

				switch (type) {
				
				case "Straight":
						exit1 = 1 + rotation;
						exit2 = 4 + rotation;
						break;
				case "Turn":
						exit1 = 1 + rotation;
						exit2 = 3 + rotation;
						break;

				case "SharpTurn":
						exit1 = 1 + rotation;
						exit2 = 3 + rotation;
						break;
				}
		
		}

		public void TileScriptFromPosition (string type, string name, Coordinate coordinate, int prevExitPosition, int leftOrRight)
		{
				this.type = type;
				this.name = name;
				this.coordinates = coordinate;
				this.rotation = rotation;

				
					
				if (prevExitPosition > 3) {
						exit1 = prevExitPosition - 3;
				} else {
						exit1 = prevExitPosition + 3;
				}

			
				switch (type) {
			
				case "Straight":
						
						if (exit1 > 3) {
								exit2 = exit1 - 3;
						} else {
								exit2 = exit1 + 3;
						}

						break;
				case "Turn":
						
						if (leftOrRight == 1) {
								if (exit1 > 4) {
										exit2 = exit1 - 4;
								} else {
										exit2 = exit1 + 2;
								}
								
						} else {

								if (exit1 > 2) {
										exit2 = exit1 - 2;
								} else {
										exit2 = exit1 + 4;
								}
						}

						break;
			
				case "SharpTurn":
						
						if (leftOrRight == 1) {
								if (exit1 > 5) {
										exit2 = exit1 - 5;
								} else {
										exit2 = exit1 + 1;
								}
				
						} else {
				
								if (exit1 > 1) {
										exit2 = exit1 - 1;
								} else {
										exit2 = exit1 + 5;
								}
						}
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