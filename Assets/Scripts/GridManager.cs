using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager: MonoBehaviour
{

		private float tileWidth;
		private float tileHeight;
		private int numTilesX = 7;
		private int numTilesY = 5;
		public List<TileScript> grid;
		public List<Coordinate> usedCoordinates;

		//The grid should be generated on game start
		void Start ()
		{
				GameObject auxPrefab = Instantiate (Resources.Load ("Grass")) as GameObject;
				tileWidth = auxPrefab.renderer.bounds.size.x;
				tileHeight = auxPrefab.renderer.bounds.size.y;
				Destroy (auxPrefab);
				grid = new System.Collections.Generic.List<TileScript> ();
				
	
		}

		public void createGrid (int difficulty, List<TileScript> map)
		{

				switch (difficulty) {
				case 1:
						numTilesX = 7;
						numTilesY = 5;
						break;
				case 2:
						numTilesX = 11;
						numTilesY = 9;
						break;
				case 3:
						numTilesX = 15;
						numTilesY = 13;
						break;
				}


				for (int y = 0; y<numTilesY; y++) {
						int extraTile = 1;
						if (y % 2 != 0) {
								extraTile = 0;
							
						}
			
						for (int x = 0; x<numTilesX+extraTile; x++) {
			
								bool isMapTile = false;
								foreach (TileScript mapTile in map) {
								
										if (mapTile.getCoordinates ().getX () == x && mapTile.getCoordinates ().getY () == y) {
												grid.Add (mapTile);
												isMapTile = true;
										}

								}

								if (!isMapTile) {
										TileScript tile = new TileScript (new Coordinate (x, y), "Grass");
										grid.Add (tile);
								}
								
						}
						
				}
				
		}

		

		public void drawTiles ()
		{

				GameObject tileGrid = new GameObject ("Grid");
				tileGrid.transform.parent = GameObject.Find ("Puzzle").transform;

				float gridVerticalOffset = 0f;
				int oddRowsCount = 1;
		
				bool alreadyCounts = true;
				float oddRowOffset = 0;
		
				foreach (TileScript tile in grid) {
			
						print ("Iterating: " + tile.getName ());
			
						int x = tile.getCoordinates ().getX ();
						int y = tile.getCoordinates ().getY ();
						string type = tile.getType ();
						string name = tile.getName ();
			
			
						if (!alreadyCounts && y % 2 != 0) {
								alreadyCounts = true;
								oddRowOffset = tileWidth / 2;
								oddRowsCount++;
								gridVerticalOffset -= tileHeight * 0.25f;
						}
			
						if (alreadyCounts && y % 2 == 0) {
								oddRowOffset = 0;
								alreadyCounts = false;
								gridVerticalOffset -= tileHeight * 0.25f;
				
						}
			
						GameObject tileObject = Instantiate (Resources.Load (type)) as GameObject;
						tileObject.name = name;
						tileObject.transform.position = new Vector3 (tileWidth * x + oddRowOffset, tileHeight * y + gridVerticalOffset, 0);
						tileObject.transform.Rotate (0, 0, tile.getRotation () * 60, Space.World);
						tileObject.transform.parent = tileGrid.transform;
			
				}
		
		
		
				float gridOffsetX = -((numTilesX - 1) / 2) * tileWidth - tileWidth / 2;
				float gridOffsetY = -((numTilesY / 2) * tileHeight) + (oddRowsCount * tileHeight * 0.25f);
		
				tileGrid.transform.position = new Vector3 (gridOffsetX, gridOffsetY, 0);
		}
	

		
		
		
		
}
