using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
		private BoardManager boardManager;
		private MapCreator mapCreator;
		private HandPrefabScript hand;
		public int difficulty = 3;
		public List<Coordinate> startTilesCoordinates;
		public List<Coordinate> movesCoordinates;
		public List<TileScript> movesTiles;
		private Enums.BoardState boardState;	

		
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


		
		// SETTING THE TARGET
		public void setTarget (Coordinate targetCoordinate)
		{
				
				boardState = Enums.BoardState.Blocked;
			
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
			
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();


						//TODO (esto de momento ha sido por poner, nada muy pensado
						if (tilePrefab.getState () == Enums.TilePrefabState.Target) {

								tilePrefab.setState (Enums.TilePrefabState.Used);

						}

						// IF TARGET = FIXED ->get exit ->...-> new target.
			    
			    
						if (tilePrefab.getState () == Enums.TilePrefabState.Available || tilePrefab.getState () == Enums.TilePrefabState.Normal) {

								if (targetCoordinate.getX () == prefabCoord.getX () && targetCoordinate.getY () == prefabCoord.getY ()) {
								
										movesCoordinates.Add (targetCoordinate);
										movesTiles.Add (tilePrefab.getTileScript ());

										tilePrefab.setState (Enums.TilePrefabState.Target);

						
								} else {
										tilePrefab.setState (Enums.TilePrefabState.Normal);
								}
						}
				
			
				}

				enableHand ();

				boardState = Enums.BoardState.Free;
		}


		// ENABLING AVAILABLE HAND TILES
		void 	enableHand ()
		{

				TileScript target = movesTiles [movesTiles.Count - 1];

				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						
						TilePrefabScript tilePrefab = (TilePrefabScript)o;

						TileScript handTile = tilePrefab.getTileScript ();
			                

						if (isReady (handTile)) {

				 
								if (tilePrefab.getState () == Enums.TilePrefabState.Blocked) {
					
										tilePrefab.setState (Enums.TilePrefabState.Ready);
						
								} 
				
						} else if (tilePrefab.getState () == Enums.TilePrefabState.Ready) {
				
								tilePrefab.setState (Enums.TilePrefabState.Blocked);
				
						}

						
				}
		
		
		}

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

	
	
		//AUX METHODS
	
		// Checks if a tile can be placed

		public bool isReady (TileScript currentTile)
		{
				bool res = false;
				int exit = 0;
		
				TileScript previous = movesTiles [movesTiles.Count - 2];
				TileScript target = movesTiles [movesTiles.Count - 1];
			
				if (movesTiles [movesTiles.Count - 2].getType () == "Start") {
			
						Coordinate destExit1 = mapCreator.getCoordinateFromExit (target .getCoordinates (), currentTile.getExits () [0]);
						Coordinate destExit2 = mapCreator.getCoordinateFromExit (target .getCoordinates (), currentTile.getExits () [1]);
				
						if (destExit1.getX () == previous.getCoordinates ().getX () && destExit1.getY () == previous.getCoordinates ().getY ()) {
								res = true;
							
						} else if (destExit2.getX () == previous.getCoordinates ().getX () && destExit2.getY () == previous.getCoordinates ().getY ()) {
								res = true;
						}
				
			
				} else {
			
			
			
			
				}
		
		
		
		
		
				/* 1º Get Target exit and get the opposite exit
						 * 2º Compare with any of the exits on the prefab.
				*/
		
				return res;
		
		}

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
	
