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
		private TileScript targetEntr;
		private Vector3 targetPos;
		
		// This will be used to store the location of fixed tiles.
		private TileScript tempTile;
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
			
				
			
				// Starting the game.
				startGame ();
	
					
		}
		
		void startGame ()
		{
				// Finding the avaiable starting tiles.
				
				
		
				storeStartTiles ();
				enableStartTiles ();
				
				boardState = Enums.BoardState.Free;
		}

		public void turn (Coordinate handTileCoordinate)
		{
				boardState = Enums.BoardState.Blocked;
				//	Moving the selected tile into position
				//placeTile ();
				//	Finding the next target
				//findNextTarget ();
				// Preparing the board and the hand.
				//prepareBoardAndHand ();
				boardState = Enums.BoardState.Free;
		}


		/*public bool isReady (TileScript currentTile, TileScript previous)
		{
		
				bool res = false;
				int exit = 0;
	
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
						int entry = getEntry (previous.getFreeExit ());
						if (currentTile.getExits () [0] == entry) {
								currentTile.setFreeExit (currentTile.getExits () [1]);
								res = true;
						} else if (currentTile.getExits () [1] == entry) {
								currentTile.setFreeExit (currentTile.getExits () [1]);
								res = true;
						}
			
				}
			
				return res;
			
		}
	*/
	
	
	
		//First turn logic.
	
		void  storeStartTiles ()
		{
				//Getting the start tile and obtaining all the coordinates around it.
		
				Coordinate startTileCoordinate = mapCreator.getCompletePath () [0].getCoordinates ();
		
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
				
								if (coord.equals (tileScript.getCoordinates ())) {
									
										startTilesCoordinates.Add (tileScript.getCoordinates ());
					
					
								}
						}
				}
		}
	
		public void enableStartTiles ()
		{
		
				foreach (Coordinate coord in startTilesCoordinates) {
			
						object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
						foreach (object o in obj) {
								TilePrefabScript tilePrefab = (TilePrefabScript)o;
				
								Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
				
				
								if (coord.equals (prefabCoord)) {
					
										if (tilePrefab.getState () == Enums.TilePrefabState.Normal) {
												tilePrefab.setState (Enums.TilePrefabState.Available);
										}
					
								}
				
				
						}
			
				}
		}

		public void setFirstTarget (Coordinate targetCoordinate)
		{
		
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
			
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
			
			
						if (tilePrefab.getState () == Enums.TilePrefabState.Available) {
								if (targetCoordinate.equals (prefabCoord)) {
										targetCoord = prefabCoord;
										targetPos = tilePrefab.transform.position;
										tilePrefab.setState (Enums.TilePrefabState.Target);
								} else {
										tilePrefab.setState (Enums.TilePrefabState.Normal);
								}
						} 
			
			
				}
				enableFirstHand ();
		
		}
		
		void enableFirstHand ()
		{
		
				Coordinate startTileCoordinate = mapCreator.getCompletePath () [0].getCoordinates ();
		
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
						TileScript prefabScript = tilePrefab.getTileScript ();
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
						
						if (tilePrefab.getState () == Enums.TilePrefabState.Blocked) {
						
						
								Coordinate dest1 = mapCreator.getCoordinateFromExit (targetCoord, prefabScript.getExits () [0]);
								Coordinate dest2 = mapCreator.getCoordinateFromExit (targetCoord, prefabScript.getExits () [1]);
				
								bool isValid = false;
								if (startTileCoordinate.equals (dest1)) {						
										isValid = true;
										tilePrefab.getTileScript ().setFreeExit (2);
								} else if (startTileCoordinate.equals (dest2)) {
										isValid = true;
										tilePrefab.getTileScript ().setFreeExit (1);
								}
								if (isValid) {
										tilePrefab.getTileScript ().setCoordinates (targetCoord);
										tilePrefab.setState (Enums.TilePrefabState.Ready);
								}
						}
				}
				
		
				boardState = Enums.BoardState.Free;
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
		
