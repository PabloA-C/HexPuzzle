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
		
		public List<TileScript> moves;
		
		private Coordinate targetCoord;
		private Vector3 targetPos;
		
		// This will be used to store the location of fixed tiles.
		private  Coordinate tempCoord;
		private int tempExit;
			
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
				moves = new System.Collections.Generic.List<TileScript> ();
			
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

												
												tilePrefab.transform.position = targetPos;
												tilePrefab.getTileScript ().setCoordinates (targetCoord);
												moves.Add (tilePrefab.getTileScript ());
													
												exit = tilePrefab.getTileScript ().getFreeExit ();
												nextCoordinate = mapCreator.getCoordinateFromExit (targetCoord, exit);
												tilePrefab.setState (Enums.TilePrefabState.Used);
						
										}
								}
						}
						
						
						//TODO detect winning	
								
						string outCome = setTarget (nextCoordinate, exit);
								
						while (outCome == "Fixed") {
										
								outCome = setTarget (tempCoord, tempExit);
									
						}
								
						enableHand ();
						boardState = Enums.BoardState.Free;
				
						
			
				}
				
		}
		
		// SETTING THE NEW TARGET
		public string setTarget (Coordinate targetCoordinate, int prevExit)
		{
			
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				bool isOutOfBounds = true;
				
				string outCome = "";
		
				foreach (object o in obj) {
						
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
				
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
	
	
						if (!(tilePrefab.getState () == Enums.TilePrefabState.Ready) && !(tilePrefab.getState () == Enums.TilePrefabState.Blocked)) {
	
								if (targetCoordinate.getX () == prefabCoord.getX () && targetCoordinate.getY () == prefabCoord.getY ()) {
										isOutOfBounds = false;
										
										if (tilePrefab.getState () == Enums.TilePrefabState.Normal) {
												outCome = "Normal";
												
												targetCoord = prefabCoord;
												targetPos = tilePrefab.transform.position;
												tilePrefab.setState (Enums.TilePrefabState.Target);
													
												Debug.Log ("Ok!");
										} else if (tilePrefab.getState () == Enums.TilePrefabState.Fixed) {
												outCome = "Fixed";
												int entry = getEntry (prevExit);
												
												int exit1 = tilePrefab.getTileScript ().getExits () [0];
												int exit2 = tilePrefab.getTileScript ().getExits () [1];
						
												if (exit1 == entry) {
												
														tilePrefab.getTileScript ().setFreeExit (exit2);
														tempExit = exit2;
							
												} else {
														tilePrefab.getTileScript ().setFreeExit (exit1);
														tempExit = exit1;
												}
						
												tempCoord = mapCreator.getCoordinateFromExit (prefabCoord, tempExit);
											
						
												Debug.Log ("Fixed!");
										} else {
												outCome = "Unsolvable";
												boardState = Enums.BoardState.Unsolvable;
												Debug.Log ("Unavaiable!");
						
										}
								}
						} 
						
				}		
				
				if (isOutOfBounds) {
						boardState = Enums.BoardState.Unsolvable;
						Debug.Log ("Out");
				}
				
				
				
				return outCome;	
		}
	
	
		// ENABLING THE HAND
		void enableHand ()
		{
			
				TileScript target = moves [moves.Count - 1];
			
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
			
				TileScript previous = moves [moves.Count - 1];
	
				Coordinate destCurrentExit1 = mapCreator.getCoordinateFromExit (targetCoord, currentTile.getExits () [0]);
				Coordinate destCurrentExit2 = mapCreator.getCoordinateFromExit (targetCoord, currentTile.getExits () [1]);
		
		
				if (previous.getType () == "Start") { 
	
						if (destCurrentExit1.getX () == previous.getCoordinates ().getX () && destCurrentExit1.getY () == previous.getCoordinates ().getY ()) {
								currentTile.setFreeExit (currentTile.getExits () [1]);
								res = true;
					
						} else if (destCurrentExit2.getX () == previous.getCoordinates ().getX () && destCurrentExit2.getY () == previous.getCoordinates ().getY ()) {
								res = true; 
								currentTile.setFreeExit (currentTile.getExits () [0]);
						}
				
				
				} else {
				
						if (currentTile.getExits () [0] == previous.getFreeExit () || currentTile.getExits () [1] == previous.getFreeExit ()) {
								res = true; 
						}
				
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
									
										targetCoord = prefabCoord;
										targetPos = tilePrefab.transform.position;
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
	
				
				moves.Add (completePath [0]);
	
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
		
