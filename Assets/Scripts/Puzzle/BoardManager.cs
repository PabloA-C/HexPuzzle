using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager: MonoBehaviour
{
		private float tileWidth, tileHeight;
		private int numTilesX, numTilesY;
		public List<TileScript> gameGrid;
		
		void Start ()
		{

				GameObject auxPrefab = Instantiate (Resources.Load ("Template")) as GameObject;
				tileWidth = auxPrefab.renderer.bounds.size.x;
				tileHeight = auxPrefab.renderer.bounds.size.y;
				Destroy (auxPrefab);
				gameGrid = new System.Collections.Generic.List<TileScript> ();
		
		}

		public void run (MapCreator mapCreator)
		{

				createGrid (mapCreator);
				createTiles ();

		}

		// Creating the list of tilescripts.

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
			
								bool isUsed = false;
								
								foreach (TileScript mapTile in map.getPuzzle()) {
								
										if (mapTile.getCoordinates ().getX () == x && mapTile.getCoordinates ().getY () == y) {
												gameGrid.Add (mapTile);
												isUsed = true;
										}

								}

								
										
								foreach (Coordinate waterCoord in map.getWaterCoordinates()) {
										if (waterCoord.getX () == x && waterCoord.getY () == y) {
													
												TileScript waterTile = new TileScript (new Coordinate (x, y), "Water");
												gameGrid.Add (waterTile);
												isUsed = true;
										}
						

										
								}
								
								if (!isUsed) {
										TileScript tile = new TileScript (new Coordinate (x, y), "Grass");
										gameGrid.Add (tile);
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
		
				foreach (TileScript tile in gameGrid) {
						
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
						tilePrefab.GetComponent<TilePrefabScript> ().setMapTile (tile);
						tilePrefab.transform.parent = tileGrid.transform;

						if (tilePrefab.GetComponent<TilePrefabScript> ().getTileScript ().getType () == "Grass") {
								tilePrefab.GetComponent<TilePrefabScript> ().setState (Enums.TilePrefabState.Normal);
						} else if (tilePrefab.GetComponent<TilePrefabScript> ().getTileScript ().getType () == "Water") {
								tilePrefab.GetComponent<TilePrefabScript> ().setState (Enums.TilePrefabState.Water);
						} else {
								tilePrefab.GetComponent<TilePrefabScript> ().setState (Enums.TilePrefabState.Fixed);
						}
						
				
						
				}
		
		
		
				float gridOffsetX = -((numTilesX - 1) / 2) * tileWidth - tileWidth / 2;
				float gridOffsetY = -((numTilesY / 2) * tileHeight) + (oddRowsCount * tileHeight * 0.25f);
		
				tileGrid.transform.position = new Vector3 (gridOffsetX, gridOffsetY, 0);
		}


		public List<TileScript> getGameGrid ()
		{
				return gameGrid;
		}
}
