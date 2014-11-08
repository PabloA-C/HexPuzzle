using UnityEngine;
using System.Collections;
using System.Collections.Generic;
	
public class PuzzleManager : MonoBehaviour
{

		private BoardManager boardManager;
		private MapCreator mapCreator;
		private HandPrefabScript hand;
		private int difficulty;
		public List<Coordinate> startTilesCoordinates;
		public List<TileScript> moves;
		private Coordinate targetCoord;
		private int targetEntr;
		private Vector3 targetPos;
		private bool isBlocked = false;
			
	
			
// Use this for initialization
		void Start ()
		{
				difficulty = GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().getDifficulty ();
			
				GameObject.Find ("NormalButton").GetComponent<DifficultyButtonScript> ().setState (Enums.SelectorState.Blocked);
				GameObject.Find ("HardButton").GetComponent<DifficultyButtonScript> ().setState (Enums.SelectorState.Blocked);
				GameObject.Find ("ExpertButton").GetComponent<DifficultyButtonScript> ().setState (Enums.SelectorState.Blocked);
				GameObject.Find ("Back").GetComponent<BackScript> ().setState (Enums.SelectorState.Blocked);
		
				Vector3 scale = GameObject.Find ("Selector").GetComponent<Transform> ().localScale;
				scale.x = 0.5F; 
				scale.y = 0.5F; 
				GameObject.Find ("Selector").GetComponent<Transform> ().localScale = scale;
			
				switch (difficulty) {
				case 1:
				
						GameObject.Find ("Selector").GetComponent<Transform> ().Translate (new Vector3 (0, 4f, 0), Space.World);
						GameObject.Find ("Quit").GetComponent<Transform> ().Translate (new Vector3 (0, 4f, 0), Space.World);
						GameObject.Find ("Back").GetComponent<Transform> ().Translate (new Vector3 (0, 4f, 0), Space.World);
						break;
				case 2:
						GameObject.Find ("Selector").GetComponent<Transform> ().Translate (new Vector3 (0, 4f, 0), Space.World);
						GameObject.Find ("Quit").GetComponent<Transform> ().Translate (new Vector3 (0, 4f, 0), Space.World);
						GameObject.Find ("Back").GetComponent<Transform> ().Translate (new Vector3 (0, 4f, 0), Space.World);
						break;
				case 3:
						GameObject.Find ("Selector").GetComponent<Transform> ().Translate (new Vector3 (0, 4.8F, 0), Space.World);
						GameObject.Find ("Quit").GetComponent<Transform> ().Translate (new Vector3 (0, 4.8f, 0), Space.World);
						GameObject.Find ("Back").GetComponent<Transform> ().Translate (new Vector3 (0, 4.8f, 0), Space.World);
						break;
				
				
				}
		
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
		}

		public void turn ()
		{
				//	Moving the selected tile into position
				placeTile ();
				//	Finding the next target
				Coordinate result = findNextTarget (targetCoord, moves [moves.Count - 1].getFreeExit ());	
				
				
				if (result.getX () == -2) {
		
						checkVictory ();
		
				} else if (result.getX () == -1) {
						isBlocked = true;
						disableHand ();
				} else {
				
						enableHand ();
				}
		
	
				
		}
	
		void placeTile ()
		{
		
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
						TileScript prefabScript = tilePrefab.getTileScript ();
			
			
						if (prefabCoord.getX () == -1 && prefabCoord.getY () == -1) {
								
								tilePrefab.setOriginalPos (tilePrefab.transform.position);
								
								Vector3 newPos = targetPos;
								newPos.Set (targetPos.x, targetPos.y, tilePrefab.originalPosition.z);
								tilePrefab.transform.position = newPos;
								tilePrefab.transform.Translate (new Vector3 (0, 0, -5), Space.World);
								prefabScript.setCoordinates (targetCoord);
								tilePrefab.setState (Enums.TilePrefabState.Used);
								moves.Add (prefabScript);
				
						} else if (tilePrefab.getState () == Enums.TilePrefabState.Ready) {
			
								tilePrefab.setState (Enums.TilePrefabState.Blocked);
						}
			
			
				}
		
		}
   		
		Coordinate findNextTarget (Coordinate coord, int prevExit)
		{
				Coordinate res = new Coordinate (-1, -1);
		
				Coordinate destCoord = mapCreator.getCoordinateFromExit (coord, prevExit);
				
				
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
						TileScript prefabScript = tilePrefab.getTileScript ();
			
			
						if (prefabCoord.equals (destCoord)) {
				
								if (tilePrefab.getState () == Enums.TilePrefabState.Normal) {
										targetCoord = prefabCoord;
										targetPos = tilePrefab.transform.position;
										targetEntr = getEntry (prevExit);
										res = targetCoord;						
										tilePrefab.setState (Enums.TilePrefabState.Target);
					
								} else if (tilePrefab.getState () == Enums.TilePrefabState.Fixed) {
								
										if (prefabScript.getType () == "Finish") {
												res = new Coordinate (-2, -2);
										} else if (!(prefabScript.getType () == "Start")) {
								
												int entry = getEntry (prevExit);
												
												bool isValid = false;
												int exit = 0;
						
												if (prefabScript.getExits () [0] == entry) {
														isValid = true;
														exit = prefabScript.getExits () [1];
											
												} else if (prefabScript.getExits () [1] == entry) {
														isValid = true;
														exit = prefabScript.getExits () [0];
												}
												
												if (isValid) {
												
														res = findNextTarget (prefabCoord, exit);
												
												}
							
										}
				
								}
					
								
						}
			
		
		
				}
				return res;
		}

		void enableHand ()
		{
				
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
						TileScript prefabScript = tilePrefab.getTileScript ();
			
						if (tilePrefab.getState () == Enums.TilePrefabState.Blocked) {
								Debug.Log ("Checking one");
								bool isValid = false;
								if (prefabScript.getExits () [0] == targetEntr) {
										isValid = true;
										prefabScript.setFreeExit (2);
							
								} else if (prefabScript.getExits () [1] == targetEntr) {
										isValid = true;
										prefabScript.setFreeExit (1);
								}
								if (isValid) {
										tilePrefab.setState (Enums.TilePrefabState.Ready);
								}
				
						}
			
			
				}
		
		
		
		}
	
		void disableHand ()
		{
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
						TileScript prefabScript = tilePrefab.getTileScript ();
			
						if (tilePrefab.getState () == Enums.TilePrefabState.Ready) {
							 
								tilePrefab.setState (Enums.TilePrefabState.Blocked);			
				
						}
				
				}
		}
		
		void checkVictory ()
		{
				bool res = false;
				
				int handSize = 0;
				
				switch (difficulty) {
				case 1:
						handSize = 8;
						break;
				case 2:
						handSize = 16;
						break;
				case 3:
						handSize = 26;
						break;

				}
		
				if (moves.Count == handSize) {
						GameObject.Find ("NormalButton").GetComponent<DifficultyButtonScript> ().setState (Enums.SelectorState.Free);
						GameObject.Find ("HardButton").GetComponent<DifficultyButtonScript> ().setState (Enums.SelectorState.Free);
						GameObject.Find ("ExpertButton").GetComponent<DifficultyButtonScript> ().setState (Enums.SelectorState.Free);
						GameObject.Find ("Back").GetComponent<BackScript> ().setState (Enums.SelectorState.Blocked);
						
				} else {
						disableHand ();
				}
		
		}
		
		
		public void backStep ()
		{
		
				if (moves.Count == 0) {
				
						backToStart ();
						
				} else {
						backNormal ();
				}
		}
		
		void backToStart ()
		{
				
				object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
				foreach (object o in obj) {
						TilePrefabScript tilePrefab = (TilePrefabScript)o;
			
						Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
			
						if (tilePrefab.getState () == Enums.TilePrefabState.Target) {
								tilePrefab.state = Enums.TilePrefabState.Normal;
						} 
				}
				
				disableHand ();
				enableStartTiles ();
		}
		
		
		//AQUI
		void backNormal ()
		{
				//If we found a dead end
				if (isBlocked) {
		
						moves.RemoveAt (moves.Count - 1);
		
						object[] obj = GameObject.FindObjectsOfType (typeof(TilePrefabScript));
						foreach (object o in obj) {
								TilePrefabScript tilePrefab = (TilePrefabScript)o;
			
								Coordinate prefabCoord = tilePrefab.getTileScript ().getCoordinates ();
			
								if (tilePrefab.getState () == Enums.TilePrefabState.Used && prefabCoord.equals (targetCoord)) {
							
										tilePrefab.moveBack ();
										tilePrefab.transform.Translate (new Vector3 (0, 0, 5), Space.World);
										tilePrefab.state = Enums.TilePrefabState.Blocked;
										tilePrefab.setAlpha ();
										
								} 
						}
						isBlocked = false;
				
						//If we just want to go back.
				} else {
				
				
				
				}
				
				disableHand ();
		
				
				if (moves.Count == 0) {
				
						enableFirstHand ();
						
				} else {
						enableHand ();
				}
				
			
				
		}
	
		
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
										tilePrefab.hover (false);							
										tilePrefab.setState (Enums.TilePrefabState.Target);
										GameObject.Find ("Back").GetComponent<BackScript> ().setState (Enums.SelectorState.Free);

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
			
		}
			
		int getEntry (int prevExit)
		{
	
				int res = 0;
				
				switch (prevExit) {
				case 1:
						return 4;
						break;
				case 2:
						return 5;
						break;
				case 3:
						return 6;
						break;
				case 4:
						return 1;
						break;
				case 5:
						return 2;
						break;
				case 6:
						return 3;
						break;
			
			
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
}
		
