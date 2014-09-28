using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapCreator
{
		private List<TileScript> completePath;
		private int pathWidth, pathHeight, pathLenght;
		private int floorY, ceilY;
		private bool pathFound = false;
		//Water 12%;
		//Placed tiles 16% ;
		

		void Start ()
		{
				
		
		}

		public MapCreator (int difficulty)
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
				
				floorY = (int)-Mathf.Floor (Random.Range (0, pathHeight));
				ceilY = pathHeight + floorY;
			
		}
		
		public void run ()
		{
				List<TileScript> path = new System.Collections.Generic.List<TileScript> ();

				TileScript start = new TileScript ("Start", "Start", new Coordinate (0, 0), 0);

				Backtracking (start, path);
	
			
		}

		public void Backtracking (TileScript newTile, List<TileScript> currentPath)
		{
		
				Debug.Log ("Start BT");
				List<TileScript> newPath = new System.Collections.Generic.List<TileScript> ();
				
				foreach (TileScript tile in currentPath) {
						newPath.Add (tile);
			
						Debug.Log ("Added old tile");
				}
				
				newPath.Add (newTile);
				Debug.Log ("Added new tile");
				if (newPath.Count != 1) {
						Debug.Log ("First Iteration");

						//Averiguar las coordenadas de la siguiente pieza
						
				} else {

						Debug.Log ("Not the first Iteration");
						if (newPath.Count == pathLenght + 2) {
				
								pathFound = true;

						} else {
								Debug.Log ("Not the last iteration");
				
								//Como averiguar las coordenadas de la siguiente pieza
					
								if (newPath.Count == pathLenght + 1) {

										Debug.Log ("One iteration to go");
										
										// TileScript start = new TileScript ("Finish", "Finish", -coords-, 0);
					
								} else {
										Debug.Log ("More than one iteration to go");

					
										List<int> possibleTiles = new System.Collections.Generic.List<int> ();
										possibleTiles.Add (1);
										possibleTiles.Add (2);
										possibleTiles.Add (3);
										
										///AAAND NO PATH FOUND
										while (possibleTiles.Count>0) {
						
												int randomVal = (int)Mathf.Floor (Random.Range (0, possibleTiles.Count));
												Debug.Log (possibleTiles [randomVal]);
												possibleTiles.RemoveAt (randomVal);
												
												
										}
					
								
					
										/*
										Debug.Log (possibleTiles.Count);
					
										int rand = (int)Mathf.Ceil (Random.Range (0, possibleTiles.Count));
						
										possibleTiles.Remove (rand);
						
										Debug.Log ("Removed " + rand);
										Debug.Log (possibleTiles.Count);

									
					
					*/
					
					
								}

						}


				}

				/*


		


								



								}


					

				*/

		}

		public Coordinate firstPieceCoordinate ()
		{
		
				int direction = (int)-Mathf.Floor (Random.Range (1, 6));
				int x = 0;
				int y = 0;
		
				switch (direction) {
			
				case 1:
						y--;
						break;
				case 2:
						x++;
						y--;		
						break;
				case 3:
						x--;
						break;
				case 4:
						x++;
						break;
				case 5:
						y++;
						break;
				case 6:
						x++;
						y++;
						break;
				}
		
				return new Coordinate (x, y);
		
		}

		public bool isOnBounds (Coordinate coordinate)
		{
				bool res = false;
				if (coordinate.getY () < floorY || coordinate.getY () > ceilY) {
						res = true;
				}
				return res;
		}

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

		
		
	

	


