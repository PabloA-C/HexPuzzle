using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
public class TileScript
{
		private string type, name;
		private Coordinate coordinates;
		private int rotation;
		private int exit1;
		private int exit2;
		private bool isFixed;
		public int freeExit;

		// Use this for initialization
		void start ()
		{
				
		}
	
		public TileScript ()
		{
	
		}
	
		public TileScript (Coordinate coordinate, string type)
		{
	
					
				this.type = type;
				this.name = type + " tile";
	
				this.coordinates = coordinate;
				this.exit1 = 0;
				this.exit2 = 0;
			
		}
	
		public TileScript (string type, string name, Coordinate coordinate, int prevExitPosition, int leftOrRight)
		{
				this.type = type;
				this.name = name;
				this.coordinates = coordinate;
				rotation = 1;
				this.exit1 = 0;
				this.exit2 = 0;
					
	
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
						rotation = exit1 - 1;
								
						break;
				case "Turn":
							
						if (leftOrRight == 1) {
								rotation = exit1 - 1;
								if (exit1 > 4) {
										exit2 = exit1 - 4;
								} else {
										exit2 = exit1 + 2;
								}
	
									
									
						} else {
								rotation = exit1 - 3;
								if (exit1 > 2) {
										exit2 = exit1 - 2;
								} else {
										exit2 = exit1 + 4;
								}
						}
							
						break;
				
				case "SharpTurn":
							
						if (leftOrRight == 1) {
								rotation = exit1 - 1;
								if (exit1 > 5) {
										exit2 = exit1 - 5;
								} else {
										exit2 = exit1 + 1;
								}
					
					
					
						} else {
								rotation = exit1 - 2;
								if (exit1 > 1) {
										exit2 = exit1 - 1;
								} else {
										exit2 = exit1 + 5;
								}
						}
						break;
				}
			
					
				this.name = name + " - Rotation: " + rotation;
					
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
		
		public void setCoordinates (Coordinate coordinates)
		{
				this.coordinates = coordinates;
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
			
		public void setFixed (bool isFixed)
		{
				this.isFixed = isFixed;
	
		}
	
		public bool getIsFixed ()
		{
				return isFixed;
		}
	
		public void setFreeExit (int freeExit)
		{
		
				if (freeExit == 1) {
						this.freeExit = exit1;
				} else {
						this.freeExit = exit2;
				}
	
		}
	
		public int getFreeExit ()
		{
				return freeExit;
		}
}
