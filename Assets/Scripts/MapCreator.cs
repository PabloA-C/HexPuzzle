﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapCreator
{
		
		private List<TileScript> completePath;
		private int pathWidth, pathHeight, pathLenght;
		private int floorY, ceilY, floorX, ceilX;
		private bool pathFound = false;
		//************* Water 12%;
		//************* Placed tiles 16% ;
		

		void Start ()
		{
				
		
		}

		public MapCreator (int difficulty)
		{
				//Setting the bounds of the grid.
				setBounds (difficulty);
				Debug.Log (floorX + ", " + ceilX + " | " + floorY + ", " + ceilY);
		}
		
		public void run ()
		{
				//Creating the empty list to store the path trhough the iterations.
				List<TileScript> path = new System.Collections.Generic.List<TileScript> ();
				
				//Creating the start tile on a random location.
				TileScript startTile = getSartTile ();
			
				//Starting the backtracking algorithm.
				Backtracking (startTile, path);
	
			
		}

		public void Backtracking (TileScript newTile, List<TileScript> currentPath)
		{
		
				//Adding the previous tiles to the new list of tiles.
				List<TileScript> newPath = new System.Collections.Generic.List<TileScript> ();
			
				foreach (TileScript tile in currentPath) {
						newPath.Add (tile);

				}
			
				//Adding the current tile to the new list.
				newPath.Add (newTile);
		
				if (newPath.Count == pathLenght + 2) {
						// If the path has the desired lenght (plus 1 for the start and 1 for the finish tile,
						//we found the path!

						pathFound = true;

				} else {
						//If we didn't find the path yet, we go on.

						//We find the current tile's exit.
						int prevExitPosition = 0; 

						
						if (newPath.Count == 1) {
								//If we are on the first iteration the second tile will be placed at a random exit form the
								//start, within the bounding box. We use the method "startExit()" to find an available one.
				
								prevExitPosition = sartExit (newTile.getCoordinates ());
			
						} else {
								//If we are not on the first iteration we can find the next position using the current tile's  
								//coordinates and it's second exit to find it.


								prevExitPosition = newTile.getExits () [1];
						
						}

						//Now that we have the current tile's exit, we can use that information along the coordinate to obtain the
						//coordinate the next tile would have.
						Coordinate nextIterationCoordinate = getCoordinateFromExit (newTile.getCoordinates (), prevExitPosition);
			
						Debug.Log ("Current tile coordinate: " + newTile.getCoordinates ().getX () + ", " + newTile.getCoordinates ().getY () + ". Exit: " + prevExitPosition + ". Next Coordinate: " + nextIterationCoordinate.getX () + ", " + nextIterationCoordinate.getY ());
						

						if (isOnBounds (nextIterationCoordinate) && placeUnused (nextIterationCoordinate, newPath)) {
								//Checking if the next coordinate is on bounds and it's not used already.
				
								
								if (newPath.Count == pathLenght + 1) {
										//If we only have one tile to go, we add the Finish tile.
										TileScript finish = new TileScript ("Finish", "Finish Tille", nextIterationCoordinate, 0);
					
								} else {
										//We have more than one iteration to go. 

										/*
										We already know that we have a empty slot to place a new tile. We will place one
										at random calling backtracking again. If at any point one branch would not find an
										available path, it'll contenue on the previous branch with more options.

										Once a turn is chosen and the random orientation selected cannot produce a available
										path before going to the other two types of tiles, we try our luck turning to the 
										second possible turn first.
							
										The algorithm will find a branch that suits the desired lenght.
										*/
									
										//Listing all the possible type of tiles.
										List<int> possibleTiles = new System.Collections.Generic.List<int> ();
										
										for (int i = 1; i<4; i++) {
												possibleTiles.Add (i);
										}
										
										//************* AND NO PATH FOUND once its possible to find it
										while (possibleTiles.Count>0&&!pathFound) {
												//If there is still a type of tile to test on a slot.
							
												/*
												 To improve the execution time, we check if a path was found on a previous 
												attemp.
												*/

												int randomVal = (int)Mathf.Floor (Random.Range (0, possibleTiles.Count));
												int chosen = possibleTiles [randomVal];
												possibleTiles.RemoveAt (randomVal);
						
						
												Debug.Log ("RandomVal: " + randomVal + " - Value: " + chosen);
						
						
												/*
												if (chosen == 1) {	

														Debug.Log ("it was one");
														TileScript nextTile = new TileScript ();
														nextTile.TileScriptFromPosition ("Straight", "Tile " + newPath.Count, nextCoordinate, prevExitPosition, 0);
														
														Debug.Log ("Straight Chose a " + nextTile.getType () + " " + nextTile.getExits () [0] + " " + nextTile.getExits () [1]);
														pathFound = true;

												}

												
												if (chosen == 2) {	
												Debug.Log ("it was two");
												List<int> possibleTurns = new System.Collections.Generic.List<int> ();
												possibleTiles.Add (1);
												possibleTiles.Add (2);

												while (possibleTurns.Count>0&&!pathFound) {

														int randomTurn = (int)Mathf.Floor (Random.Range (0, possibleTurns.Count));
														int leftOrRight = possibleTiles [randomVal];
														possibleTurns.RemoveAt (randomVal);

														TileScript nextTile = new TileScript ();
														nextTile.TileScriptFromPosition ("Turn", "Tile " + newPath.Count, nextCoordinate, prevExitPosition, leftOrRight);
														Debug.Log ("Turn Chose a " + nextTile.getType () + " " + nextTile.getExits () [0] + " " + nextTile.getExits () [1]);
														pathFound = true;
														
														}
												}

												 		if (chosen == 3) {	
														Debug.Log ("it was three");
														List<int> possibleTurns = new System.Collections.Generic.List<int> ();
														possibleTiles.Add (1);
														possibleTiles.Add (2);
							
														while (possibleTurns.Count>0&&!pathFound) {
								
																int randomTurn = (int)Mathf.Floor (Random.Range (0, possibleTurns.Count));
																int leftOrRight = possibleTiles [randomVal];
																possibleTurns.RemoveAt (randomVal);
								
																TileScript nextTile = new TileScript ();
																nextTile.TileScriptFromPosition ("SharpTurn", "Tile " + newPath.Count, nextCoordinate, prevExitPosition, leftOrRight);
																Debug.Log ("SharpTurn Chose a " + nextTile.getType () + " " + nextTile.getExits () [0] + " " + nextTile.getExits () [1]);
																pathFound = true;
								
														}
												}
												*/
						
										}
					
					
					
					
					
					
								}
				
				
						

			
						} else {
								//The next coordinate is out of bounds and/or it's used already.				
								Debug.Log ("The next piece would be imposible to place");
						}

				}
				
			

		}

		//Setting the bounding grid.
		public void setBounds (int difficulty)
		{
				switch (difficulty) {
				case 1:
						pathWidth = 7;
						pathHeight = 5;
						pathLenght = 12;
						break;
				case 2:
						pathWidth = 11;
						pathHeight = 9;
						pathLenght = 20;
						break;
				case 3:
						pathWidth = 15;
						pathHeight = 13;
						pathLenght = 25;
						break;
				}
		
		
				floorX = 0;
				ceilX = pathWidth - 1;
		
		
				floorY = 0;
				ceilY = pathHeight - 1;

		}
	
		// Stablish a start tile on a random position inside the board.
		public TileScript getSartTile ()
		{
		
				int startY = (int)Mathf.Floor (Random.Range (0, pathHeight));	
		
				int extra = oneIfEven (startY);
		
				int startX = (int)Mathf.Floor (Random.Range (0, pathWidth + extra));	
		
		
				//TEST	Coordinate startTileCoordinate = new Coordinate (startX, startY);
				Coordinate startTileCoordinate = new Coordinate (6, 1);

				TileScript res = new TileScript ("Start", "StartTile", startTileCoordinate, 0);
				return res;
		
		}

	
	
	
		//Sets the exit of the start tile (selecting one inside the bounds).
		public int sartExit (Coordinate coordinate)
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


		//Checking if we can place a tile on a coordinate: isOnBounds && placeUnused.


		public bool isOnBounds (Coordinate coordinate)
		{
				bool res = true;


				int extra = oneIfEven (coordinate.getY ());


				
		
		
				if (coordinate.getY () < floorY || coordinate.getY () > ceilY || coordinate.getX () < floorX || coordinate.getX () > ceilX + extra) {
						res = false;
								
				
				}
				
				
				return res;
		}

		public bool placeUnused (Coordinate coordinate, List<TileScript> tiles)
		{
				bool res = true;
				
				foreach (TileScript tile in tiles) {

						
						if (coordinate.getX () == tile.getCoordinates ().getX () && coordinate.getY () == tile.getCoordinates ().getY ()) {

								res = false;
								Debug.Log ("The coordinate is already used");

						}
				}
				return res;
			
		}

		public int oneIfEven (int row)
		{
				int res = 0;

				if (row % 2 == 0) {
						res = 1;
				}
				return res;
		}

}
				
/*	

		public List<Coordinate> getNeighboursCoordinates (Coordinate coordinate)
		{
				List<Coordinate> res = new System.Collections.Generic.List<Coordinate> ();

				res.Add (new Coordinate (coordinate.getX (), coordinate.getY () - 1));
				res.Add (new Coordinate (coordinate.getX () + 1, coordinate.getY () - 1));
				res.Add (new Coordinate (coordinate.getX () - 1, coordinate.getY ()));
				res.Add (new Coordinate (coordinate.getX () + 1, coordinate.getY ()));
				res.Add (new Coordinate (coordinate.getX (), coordinate.getY () + 1));
				res.Add (new Coordinate (coordinate.getX () + 1, coordinate.getY () + 1));


				return res;
		}



			int rand = (int)Mathf.Floor (Random.Range (0, 7));
				GameObject tile = null;
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

				int rand2 = (int)Mathf.Floor (Random.Range (1, 7));
		
				if (rand != 1 && rand != 2) {
						tile.transform.Rotate (0, 0, rand2 * 60, Space.World);
				}
			*/

		
		
	

	


