using UnityEngine;
using System.Collections;

public class TileScript
{
		private string type, name;
		private int x, y;
		private int exit1, exit2;

		// Use this for initialization
		void Start ()
		{
				
		}

		bool canBePlaced (int currentTileExit)
		{
				bool res = false;
				int desiredInput;

				if (currentTileExit < 4) {
						desiredInput = currentTileExit + 3;
				} else {
						desiredInput = currentTileExit - 3;
				}

				if (exit1 == desiredInput || exit2 == desiredInput) {
						res = true;
				}
				return res;
		}

	
		public void setCoordinates (int x, int y)
		{
				this.x = x;
				this.y = y;
				this.name = "Tile " + x + y;
		}

		public void setType (string type)
		{
				this.type = type;
		}

		public 	int getX ()
		{
				return x;
		}

		public int getY ()
		{
				return y;
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