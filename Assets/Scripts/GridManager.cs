using UnityEngine;
using System.Collections;

public class GridManager: MonoBehaviour
{
		//following public variable is used to store the hex model prefab;
		//instantiate it by dragging the prefab on this variable using unity editor
		public GameObject prefab;

		//Hexagon tile width and height in game world
		private float tileWidth;
		private float tileHeight;


		//next two variables can also be instantiated using unity editor
		private int numTilesX = 11;
		private int numTilesY = 20;


		//The grid should be generated on game start
		void Start ()
		{

				tileWidth = prefab.renderer.bounds.size.x;
				tileHeight = prefab.renderer.bounds.size.y;

				test ();
		}
	

		void test ()
		{


				GameObject tileGrid = new GameObject ("HexGrid");
	
				float gridVerticalOffset = 0f;

				for (int y = 0; y<numTilesY; y++) {
						float oddRowOffset = 0;
						int extraTile = 1;
						if (y % 2 != 0) {
								extraTile = 0;
								oddRowOffset = tileWidth / 2;
						}

						gridVerticalOffset -= tileHeight * 0.25f;

						for (int x = 0; x<numTilesX+extraTile; x++) {
						
								GameObject tile = (GameObject)Instantiate (prefab);
								tile.name = "Tile " + (x + y);
								tile.transform.position = new Vector3 (tileWidth * x + oddRowOffset, tileHeight * y + gridVerticalOffset, 0);
								tile.transform.parent = tileGrid.transform;
								

						}
				}

				

				
		}
	



}
