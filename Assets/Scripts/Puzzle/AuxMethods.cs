using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AuxMethods : MonoBehaviour
{

		public int mapWidth, mapHeight, pathLenght;
		public int floorX, floorY;
		public int ceilX, ceilY;
		
		//Setting the bounding grid.
		public  void setBounds (int difficulty)
		{
				
				switch (difficulty) {
				case 1:
						mapWidth = 7;
						mapHeight = 5;
						pathLenght = 12;
						break;
				case 2:
						mapWidth = 9;
						mapHeight = 5;
						pathLenght = 24;
						break;
				case 3:
						mapWidth = 9;
						mapHeight = 7;
						pathLenght = 38;
						break;
				}
		
		
				floorX = 0;
				ceilX = mapWidth - 1;
		
		
				floorY = 0;
				ceilY = mapHeight - 1;
		
		}
	
		// Returns a start tile on a random position inside the board.
		public TileScript getSartTile ()
		{
		
				int startY = (int)Mathf.Floor (Random.Range (0, mapHeight));	
		
				int extra = oneIfEven (startY);
		
				int startX = (int)Mathf.Floor (Random.Range (0, mapWidth + extra));	
		
		
				Coordinate startTileCoordinate = new Coordinate (startX, startY);
		
				TileScript res = new TileScript (startTileCoordinate, "Start");
				return res;
		
		}

	
		//Sets the exit of the start tile (selecting a random one inside the bounds).
		public int startExit (Coordinate coordinate)
		{
		
				int res = 0;
				List<int> allExits = new System.Collections.Generic.List<int> ();
				for (int i =1; i<7; i++) {
						allExits.Add (i); 
				}		
		
		
				while (res == 0) {
			
						int randomVal = (int)Mathf.Floor (Random.Range (0, allExits.Count));
						int possibleExit = allExits [randomVal];
						Coordinate possibleFirstTile = getCoordinateFromExit (coordinate, possibleExit);
			
						if (isOnBounds (possibleFirstTile)) {
								res = possibleExit;
						} else {
								allExits.RemoveAt (randomVal);
						}
				} 
		
		
				return res;
		}
	
	
	
		//Get the coordinates of the next tile given the previous tile coordinates and it's exit.
		public Coordinate getCoordinateFromExit (Coordinate currentCoordinate, int exit)
		{
		
				Coordinate res = new Coordinate ();
				int extra = oneIfEven (currentCoordinate.getY ());
		
				switch (exit) {
			
				case 1:
						res.setX (currentCoordinate.getX () - extra);
						res.setY (currentCoordinate.getY () - 1);
						break;
				case 2:
						res.setX (currentCoordinate.getX () + 1 - extra);
						res.setY (currentCoordinate.getY () - 1);		
						break;
				case 3:
						res.setX (currentCoordinate.getX () + 1);
						res.setY (currentCoordinate.getY ());
						break;
				case 4:
						res.setX (currentCoordinate.getX () + 1 - extra);
						res.setY (currentCoordinate.getY () + 1);
						break;
				case 5:
						res.setX (currentCoordinate.getX () - extra);
						res.setY (currentCoordinate.getY () + 1);
						break;
				case 6:
						res.setX (currentCoordinate.getX () - 1);
						res.setY (currentCoordinate.getY ());
						break;
				}
				return res;
		
		
		}
	
	
		//Checking if the coordinate of a tile to be placed is inside the map's grid or not.
		public bool isOnBounds (Coordinate coordinate)
		{
				bool res = true;
		
		
				int extra = oneIfEven (coordinate.getY ());
		
		
		
				if (coordinate.getY () < floorY || coordinate.getY () > ceilY || coordinate.getX () < floorX || coordinate.getX () > ceilX + extra) {
						res = false;
			
			
				}
		
		
				return res;
		}
		//Checking if the tile about to place a tile is free or not.
		public bool placeUnused (Coordinate coordinate, List<TileScript> tiles)
		{
				bool res = true;
		
				foreach (TileScript tile in tiles) {
			
			
						if (coordinate.getX () == tile.getCoordinates ().getX () && coordinate.getY () == tile.getCoordinates ().getY ()) {
				
								res = false;
				
						}
				}
				return res;
		
		}
	
		//Checking if a row is even or odd.
		public int oneIfEven (int row)
		{
				int res = 0;
		
				if (row % 2 == 0) {
						res = 1;
				}
				return res;
		}
	
	
	
		//Lists all the map's tiles that aren't part of the path.
		public List<Coordinate> getFreeCoordinates (List<TileScript> completePath)
		{
		
		
				List<Coordinate> freeCoordinates = new System.Collections.Generic.List<Coordinate> ();
		
				int numTilesX = ceilX - floorX + 1;
				int numTilesY = ceilY - floorY + 1;
		
				for (int y = 0; y<numTilesY; y++) {
						int extraTile = 1;
						if (y % 2 != 0) {
								extraTile = 0;
				
						}
			
						for (int x = 0; x<numTilesX+extraTile; x++) {
				
								bool unused = true;
								foreach (TileScript tile in completePath) {
					
										if (tile.getCoordinates ().getX () == x && tile.getCoordinates ().getY () == y) {
												unused = false;
										}
								}
								if (unused) {
										Coordinate newCoord = new Coordinate (x, y);
										freeCoordinates.Add (newCoord);
								}
						}
			
				}
		
		
		
				return freeCoordinates;
		}
}
