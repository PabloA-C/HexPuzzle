using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
		private BoardManager boardManager;
		private MapCreator mapCreator ;
		public int difficulty = 3;

		// Use this for initialization
		void Start ()
		{
				
				mapCreator = new MapCreator (difficulty);
				boardManager = GetComponent<BoardManager> ();
				boardManager.createGrid (mapCreator);
				boardManager.createTiles ();
				boardManager = GetComponent<BoardManager> ();
				GameObject.Find ("Hand").GetComponent<HandPrefabScript> ().setHand (mapCreator.getHand (), difficulty);
				startGame ();

		
		}
	
		void startGame ()
		{
	
		}

		void placeTile ()
		{

		}


}
