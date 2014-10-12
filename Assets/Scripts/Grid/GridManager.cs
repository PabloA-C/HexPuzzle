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

		public void createGrid (MapCreator map)
		{

				numTilesX = map.ceilX - map.floorX + 1;
				numTilesY = map.ceilY - map.floorY + 1;

				for (int y = 0; y<numTilesY; y++) {
						int extraTile = 1;
						if (y % 2 != 0) {
								extraTile = 0;
							
						}
			
						for (int x = 0; x<numTilesX+extraTile; x++) {
			
								bool isMapTile = false;
								foreach (TileScript mapTile in map.getMap()) {
								
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

		public void createTiles ()
		{

				GameObject tileGrid = new GameObject ("Grid");
				tileGrid.transform.parent = GameObject.Find ("Puzzle").transform;
		
				float gridVerticalOffset = 0f;
				int oddRowsCount = 1;
		
				bool alreadyCounted = true;
				float oddRowOffset = 0;
		
				foreach (TileScript tile in grid) {
						
						int x = tile.getCoordinates ().getX ();
						int y = tile.getCoordinates ().getY ();
			
						if (!alreadyCounted && y % 2 != 0) {
								alreadyCounted = true;
								oddRowOffset = tileWidth / 2;
								oddRowsCount++;
								gridVerticalOffset -= tileHeight * 0.25f;
						}
			
						if (alreadyCounted && y % 2 == 0) {
								oddRowOffset = 0;
								alreadyCounted = false;
								gridVerticalOffset -= tileHeight * 0.25f;
				
						}
			
						GameObject tilePrefab = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
			
						
						tilePrefab.name = tile.getName ();
						tilePrefab.transform.position = new Vector3 (tileWidth * x + oddRowOffset, tileHeight * y + gridVerticalOffset, 0);
						tilePrefab.GetComponent<TilePrefabScript> ().setTile (tile);
						tilePrefab.transform.parent = tileGrid.transform;

					
						
						
						
				}
		
		
		
				float gridOffsetX = -((numTilesX - 1) / 2) * tileWidth - tileWidth / 2;
				float gridOffsetY = -((numTilesY / 2) * tileHeight) + (oddRowsCount * tileHeight * 0.25f);
		
				tileGrid.transform.position = new Vector3 (gridOffsetX, gridOffsetY, 0);
		}


		
}
