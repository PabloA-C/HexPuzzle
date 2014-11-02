using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
public class PuzzleManager : MonoBehaviour
{
		private BoardManager boardManager;
		private MapCreator mapCreator;
		private HandPrefabScript hand;
		public int difficulty = 3;
		private Enums.BoardState boardState;
		public List<Coordinate> startTilesCoordinates;
		public List<Coordinate> movesCoordinates;
		public List<TileScript> movesTiles;
		private Vector3 targetPosition;
			
// Use this for initialization
		void Start ()
		{
				// Generating the random map with a given difficulty.
				mapCreator = new MapCreator (difficulty);
	
				// Retrieving the BoardManager.
				boardManager = GetComponent<BoardManager> ();
	
				// Retrieving the hand.
				hand = GameObject.Find ("Hand").GetComponent<HandPrefabScript> ();
	
				// Drawing the tiles of the board on the screen.
				boardManager.run (mapCreator);
	
				// Drawing the tiles of the hand on the screen.
				hand.setHand (mapCreator.getHand (), difficulty);
	
				// Setting up the moves storing.
				movesCoordinates = new System.Collections.Generic.List<Coordinate> ();
				movesTiles = new System.Collections.Generic.List<TileScript> ();
			
				// Storing the start tiles.
				storeStartTiles ();
	
			
				// Starting the game.
				startGame ();
	
					
		}
		
		void startGame ()
		{
				// Finding the avaiable starting tiles.
				
				enableStartTiles (startTilesCoordinates);
				boardState = Enums.BoardState.Free;
		}
	
	
		// PLACING A TILE
		
		public void placeTile (Coordinate readyTile)
		{
				if (boardState == Enums.BoardState.Free) {
						boardState = Enums.BoardState.Blocked;
	
						int exit = 0;
						Coordinate nextCoordinate = new Coordinate (0, 0);
	
						object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
						foreach (object o in obj) {
					
								TilePrefabScript tilePrefab = (TilePrefabScript)o;
								Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
					
					
								if (tilePrefab.getState () == Enums.TilePrefabState.Ready) {
						
										if (readyTile.getX () == prefabCoord.getX () && readyTile.getY () == prefabCoord.getY ()) {
							
												tilePrefab.setState (Enums.TilePrefabState.Used);
												tilePrefab.transform.position = targetPosition;
												// The coord of the hand tile is now the same than the one on the board.
												prefabCoord = movesCoordinates [movesCoordinates.Count - 1];
													
												exit = tilePrefab.getTileScript ().getFreeExit ();
												nextCoordinate = mapCreator.getCoordinateFromExit (prefabCoord, exit);
												
	
										}
								}
						}
						
						if (!checkVictory ()) {
								setTarget (nextCoordinate, exit);
								//	boardState = Enums.BoardState.Free;
				
						} else {
								//TODO victory thing.
						}
			
				}
				
		}
		
		// SETTING THE NEW TARGET
		public void setTarget (Coordinate targetCoordinate, int prevExit)
		{
	
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
				
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
	
	
						if (!(tilePrefab.getState () == Enums.TilePrefabState.Ready) && !(tilePrefab.getState () == Enums.TilePrefabState.Blocked)) {
	
								if (targetCoordinate.getX () == prefabCoord.getX () && targetCoordinate.getY () == prefabCoord.getY ()) {
	
										if (tilePrefab.getState () == Enums.TilePrefabState.Normal) {
												Debug.Log ("Found one");
												tilePrefab.setState (Enums.TilePrefabState.Target);
											
										} 
								}
						}
				}		
			
		}
	
	
		// ENABLING THE HAND
		void enableHand ()
		{
			
				TileScript target = movesTiles [movesTiles.Count - 1];
			
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
				
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
				
						TileScript handTile = tilePrefab.getTileScript ();
				
				
				
						if (tilePrefab.getState () == Enums.TilePrefabState.Ready || tilePrefab.getState () == Enums.TilePrefabState.Blocked) {
					
								if (isReady (handTile)) {
						
						
										if (tilePrefab.getState () == Enums.TilePrefabState.Blocked) {
							
												tilePrefab.setState (Enums.TilePrefabState.Ready);
							
										} 
						
								} else if (tilePrefab.getState () == Enums.TilePrefabState.Ready) {
						
										tilePrefab.setState (Enums.TilePrefabState.Blocked);
						
								}
					
						}
				}
			
			
		}
	
		public bool isReady (TileScript currentTile)
		{
				bool res = false;
				int exit = 0;
			
				TileScript previous = movesTiles [movesTiles.Count - 2];
			
				// We suppose the open exit on the prev tile is the first one and check if it's actually the second or not.
				// Thus, we wont be using destPreviousExit1
				//Coordinate destPreviousExit1 = mapCreator.getCoordinateFromExit (previous .getCoordinates (), previous.getExits () [0]);
				Coordinate destPreviousExit2 = mapCreator.getCoordinateFromExit (previous .getCoordinates (), previous.getExits () [1]);
			
			
				TileScript target = movesTiles [movesTiles.Count - 1];
				Coordinate destTargetExit1 = mapCreator.getCoordinateFromExit (target .getCoordinates (), currentTile.getExits () [0]);
				Coordinate destTargetExit2 = mapCreator.getCoordinateFromExit (target .getCoordinates (), currentTile.getExits () [1]);
			
				if (previous.getType () == "Start") { 
	
						if (destTargetExit1.getX () == previous.getCoordinates ().getX () && destTargetExit1.getY () == previous.getCoordinates ().getY ()) {
								currentTile.setFreeExit (currentTile.getExits () [1]);
								res = true;
					
						} else if (destTargetExit2.getX () == previous.getCoordinates ().getX () && destTargetExit2.getY () == previous.getCoordinates ().getY ()) {
								res = true; 
								currentTile.setFreeExit (currentTile.getExits () [0]);
						}
				
				
				} else {
						/*
							// TODO THIS NEEDS TO BE TESTED, ONLY THE FIRT CASE WAS USED SO FAR.
							int prevExit = previous.getExits () [0];
			
							object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
							foreach (object o in obj) {
					
									TilePrefabScript tilePrefab = (TilePrefabScript)o;
					
									TileScript handTile = tilePrefab.getTileScript ();
					
									
									if (destPreviousExit2.getX () == handTile.getCoordinates ().getX () && destPreviousExit2.getY () == handTile.getCoordinates ().getY ()) {
					
											if (tilePrefab.getState () == Enums.TilePrefabState.Normal) {
													prevExit = previous.getExits () [1];
											}
	
									}
							}
	
							int availableEntry = prevExit;
							if (prevExit < 4) {
									availableEntry += 3;
							} else {
									availableEntry -= 3;
							}
								
	
							if (currentTile.getExits () [0] == availableEntry || currentTile.getExits () [1] == availableEntry) {
									res = true; 
							}
				*/
				}
			
				return res;
			
		}
	
	
	
	
	
	
		// FIRST TURN LOGIC
	
		public void enableStartTiles (List<Coordinate> coordinates)
		{
			
				foreach (Coordinate coord in coordinates) {
				
						object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
						foreach (object o in obj) {
								TilePrefabScript tilePrefab = (TilePrefabScript)o;
					
								Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
					
					
								if (coord.getX () == prefabCoord.getX () && coord.getY () == prefabCoord.getY ()) {
						
						
										if (tilePrefab.getState () == Enums.TilePrefabState.Normal) {
												tilePrefab.setState (Enums.TilePrefabState.Available);
										}
						
								}
					
					
						}
				
				
				
				}
		}
	
		// TODO CHECK THE LOGIC OF THE BLOCKING OF THE BOARD
		// SETTING THE TARGET
		public void setFirstTarget (Coordinate targetCoordinate)
		{
					
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
				
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
	
				    
						if (tilePrefab.getState () == Enums.TilePrefabState.Available) {
	
								if (targetCoordinate.getX () == prefabCoord.getX () && targetCoordinate.getY () == prefabCoord.getY ()) {
									
										movesCoordinates.Add (targetCoordinate);
										movesTiles.Add (tilePrefab.getTileScript ());
										targetPosition = tilePrefab.transform.position;
										tilePrefab.setState (Enums.TilePrefabState.Target);
							
								} else {
										tilePrefab.setState (Enums.TilePrefabState.Normal);
								}
						}
					
				
				}
	
				enableHand ();
	
				boardState = Enums.BoardState.Free;
					
	
		}
	
	
	
		//TODO CHECK EVERY TIME YOU ITERATE PREFABS THAT YOU'RE WORKING ON THE MAP OR HAND SET ONLY
	
			
		bool checkVictory ()
		{
				return false;
				//Checkinf if all the hand tiles have been placed, and the next target would be the finish tile.
				
		}
	
		void disableHand ()
		{
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
				
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
				
						TileScript handTile = tilePrefab.getTileScript ();
				
						if (tilePrefab.getState () == Enums.TilePrefabState.Ready) {
						
								tilePrefab.setState (Enums.TilePrefabState.Blocked);
						
									
					
						}
				}
		}
			
			
	
		//AUX METHODS
		
		// Checks if a tile can be placed on the current target
	
			
	
		void  storeStartTiles ()
		{
				//Getting the start tile and obtaining all the coordinates around it.
			
				List<TileScript> completePath = mapCreator.getCompletePath ();
				Coordinate startTileCoordinate = completePath [0].getCoordinates ();
	
				movesCoordinates.Add (startTileCoordinate);
				movesTiles.Add (completePath [0]);
	
				int extra = oneIfEven (startTileCoordinate.getY ());
			
				Coordinate exit1 = new Coordinate (startTileCoordinate.getX () - extra, startTileCoordinate.getY () - 1);
				Coordinate exit2 = new Coordinate (startTileCoordinate.getX () + 1 - extra, startTileCoordinate.getY () - 1);	
				Coordinate exit3 = new Coordinate (startTileCoordinate.getX () + 1, startTileCoordinate.getY ());	
				Coordinate exit4 = new Coordinate (startTileCoordinate.getX () + 1 - extra, startTileCoordinate.getY () + 1);	
				Coordinate exit5 = new Coordinate (startTileCoordinate.getX () - extra, startTileCoordinate.getY () + 1);	
				Coordinate exit6 = new Coordinate (startTileCoordinate.getX () - 1, startTileCoordinate.getY ());	
			
				List<Coordinate> allStartCoordinates = new System.Collections.Generic.List<Coordinate> ();
			
				allStartCoordinates.Add (exit1);
				allStartCoordinates.Add (exit2);
				allStartCoordinates.Add (exit3);
				allStartCoordinates.Add (exit4);
				allStartCoordinates.Add (exit5);
				allStartCoordinates.Add (exit6);
			
				startTilesCoordinates = new System.Collections.Generic.List<Coordinate> ();
			
				// Iterating through every possible tile around the start tile.
				foreach (Coordinate coord  in allStartCoordinates) {
				
						foreach (TileScript tileScript in boardManager.getGameGrid()) {
					
					
					
								if (coord.getX () == tileScript.getCoordinates ().getX () && coord.getY () == tileScript.getCoordinates ().getY ()) {
						
						
						
										startTilesCoordinates.Add (tileScript.getCoordinates ());
						
						
								}
					
					
					
						}
				
				
				}
		}
			
		int getEntry (int prevExit)
		{
	
				int availableEntry = 0;
				if (prevExit > 3) {
						availableEntry = prevExit - 3;
				} else {
						availableEntry = prevExit + 3;
				}
		
				return availableEntry;
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
}
		
