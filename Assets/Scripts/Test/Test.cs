using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test : MonoBehaviour
{
		private BoardManager boardManager;
		private MapCreator mapCreator ;
		public int difficulty = 2;

	
		// Use this for initialization
		void Start ()
		{

				mapCreator = new MapCreator (difficulty);
			
				List<TileScript> map = mapCreator.getMap ();
				List<TileScript> puzzle = mapCreator.getPuzzle ();
				List<TileScript> hand = mapCreator.getHand ();
			
				int cont = -7;


				foreach (TileScript tile in map) {
			
						GameObject tilePrefab = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
			
			
						tilePrefab.transform.position = new Vector3 (cont, 0, 0);
						tilePrefab.GetComponent<TilePrefabScript> ().setMapTile (tile);
						tilePrefab.name = "Tile " + cont;
						tilePrefab.transform.transform.parent = GameObject.Find ("Test").transform;
						cont++;
				}

				cont = -6;

				foreach (TileScript tile in puzzle) {

						GameObject tilePrefab = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
						
						tilePrefab.transform.position = new Vector3 (cont, 0, 0);
						tilePrefab.GetComponent<TilePrefabScript> ().setMapTile (tile);
						tilePrefab.name = "Tile " + cont;
						tilePrefab.transform.transform.parent = GameObject.Find ("Test").transform;
						cont++;
				}
			
			

		
				foreach (TileScript tile in hand) {
			
						GameObject tilePrefab = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
			
			
						tilePrefab.transform.position = new Vector3 (cont, -2, 0);
						tilePrefab.GetComponent<TilePrefabScript> ().setMapTile (tile);
						tilePrefab.name = "Tile " + cont;
						tilePrefab.transform.transform.parent = GameObject.Find ("Test").transform;
						tilePrefab.GetComponent<TilePrefabScript> ().setState (Enums.TilePrefabState.Ready);
						cont++;
				}


				

	
		}
}
