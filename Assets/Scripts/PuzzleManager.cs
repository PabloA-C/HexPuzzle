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

				// Starting the game.
				startGame ();
		}
	
		void startGame ()
		{

				// Finding the avaiable starting tiles.
				storeStartTiles ();

				enableStartTiles ();
			
			
		}

				
		void enableStartTiles ()
		{

				enableTiles (startTilesCoordinates);
		
		}

		void  storeStartTiles ()
		{
				//Getting the start tile and obtaining all the coordinates around it.
				
				List<TileScript> completePath = mapCreator.getCompletePath ();
				Coordinate startTile = completePath [0].getCoordinates ();

				int extra = oneIfEven (startTile.getY ());
	
				Coordinate exit1 = new Coordinate (startTile.getX () - extra, startTile.getY () - 1);
				Coordinate exit2 = new Coordinate (startTile.getX () + 1 - extra, startTile.getY () - 1);	
				Coordinate exit3 = new Coordinate (startTile.getX () + 1, startTile.getY ());	
				Coordinate exit4 = new Coordinate (startTile.getX () + 1 - extra, startTile.getY () + 1);	
				Coordinate exit5 = new Coordinate (startTile.getX () - extra, startTile.getY () + 1);	
				Coordinate exit6 = new Coordinate (startTile.getX () - 1, startTile.getY ());	
			
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
	
	
		//Aux methods

		public void enableTiles (List<Coordinate> coordinates)
		{
			
				foreach (Coordinate coord in coordinates) {
					
						object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
						foreach (object o in obj) {
								TilePrefabScript tilePrefab = (TilePrefabScript)o;
			
								Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
			
								
								if (coord.getX () == prefabCoord.getX () && coord.getY () == prefabCoord.getY ()) {

										
										if (tilePrefab.getState () == Enums.TilePrefabState.Unavailable) {
												tilePrefab.setState (Enums.TilePrefabState.Available);
										}

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
	
