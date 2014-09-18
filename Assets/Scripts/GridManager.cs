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
		private int numTilesX = 8;
		private int numTilesY = 7;


		//The grid should be generated on game start
		void Start ()
		{
				prefab = Instantiate (Resources.Load ("Grass")) as GameObject;
				prefab.renderer.enabled = false;

				tileWidth = prefab.renderer.bounds.size.x;
				tileHeight = prefab.renderer.bounds.size.y;
				

				test ();
		}
	

		void test ()
		{


				GameObject tileGrid = new GameObject ("HexGrid");

				float gridVerticalOffset = 0f;
				int oddRowsCount = 0;

				for (int y = 0; y<numTilesY; y++) {
						float oddRowOffset = 0;
						int extraTile = 1;
						if (y % 2 != 0) {
								extraTile = 0;
								oddRowOffset = tileWidth / 2;
								oddRowsCount++;
						}

						for (int x = 0; x<numTilesX+extraTile; x++) {
						

								int rand = (int)Mathf.Floor (Random.Range (0, 7));
								GameObject tile = new GameObject ();
				
								switch (rand) {
								case 0:
										tile = Instantiate (Resources.Load ("Grass")) as GameObject;
										break;
								case 1:
										tile = Instantiate (Resources.Load ("Start")) as GameObject;
										break;
								case 2:
										tile = Instantiate (Resources.Load ("Finish")) as GameObject;
										break;
								case 3:
										tile = Instantiate (Resources.Load ("Straight")) as GameObject;
										break;
								case 4:
										tile = Instantiate (Resources.Load ("Turn")) as GameObject;
										break;
								case 5:
										tile = Instantiate (Resources.Load ("SharpTurn")) as GameObject;
										break;
								case 6:
										tile = Instantiate (Resources.Load ("Water")) as GameObject;
										break;

					
								}
				

								tile.name = "Tile " + x + y;
								tile.transform.position = new Vector3 (tileWidth * x + oddRowOffset, tileHeight * y + gridVerticalOffset, 0);
								
							
								
								int rand2 = (int)Mathf.Floor (Random.Range (1, 7));

								if (rand != 1 && rand != 2) {
										tile.transform.Rotate (0, 0, rand2 * 60, Space.World);
								}
								tile.transform.parent = tileGrid.transform;
								
								

						}
						if (y != numTilesY - 1) {
								gridVerticalOffset -= tileHeight * 0.25f;
						}
				}


				float gridOffsetX = -((numTilesX + 1) / 2) * tileWidth;
				float gridOffsetY = -((numTilesY / 2) * tileHeight) + (oddRowsCount * tileHeight * 0.25f);
				
				tileGrid.transform.position = new Vector3 (gridOffsetX, gridOffsetY, 0);

				
		}
	



}
