using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager: MonoBehaviour
{

		private float tileWidth;
		private float tileHeight;
		private int numTilesX = 7;
		private int numTilesY = 5;
		public List<TileScript> tileArray;

		//The grid should be generated on game start
		void Start ()
		{
				GameObject auxPrefab = Instantiate (Resources.Load ("Grass")) as GameObject;
				
				tileWidth = auxPrefab.renderer.bounds.size.x;
				tileHeight = auxPrefab.renderer.bounds.size.y;
				Destroy (auxPrefab);
				tileArray = new System.Collections.Generic.List<TileScript> ();
				createGrid ();
	
		}

		public void createGrid ()
		{
			
				for (int y = 0; y<numTilesY; y++) {
						int extraTile = 1;
						if (y % 2 != 0) {
								extraTile = 0;
							
						}
			
						for (int x = 0; x<numTilesX+extraTile; x++) {
			
								TileScript tile = new TileScript ();
								tile.setCoordinates (x, y);
								tile.setType ("Grass");
								tileArray.Add (tile);
								print ("Created " + tile.getName ());
								
	
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
		
				foreach (TileScript tile in tileArray) {
			
						print ("Iterating: " + tile.getName ());
			
						int x = tile.getX ();
						int y = tile.getY ();
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
						tileObject.transform.parent = tileGrid.transform;
			
				}
		
		
		
				float gridOffsetX = -((numTilesX - 1) / 2) * tileWidth - tileWidth / 2;
				float gridOffsetY = -((numTilesY / 2) * tileHeight) + (oddRowsCount * tileHeight * 0.25f);
		
				tileGrid.transform.position = new Vector3 (gridOffsetX, gridOffsetY, 0);
		}
	



}
