using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
		private GridManager gridManager;
		private MapCreator mapCreator ;
		public int difficulty = 3;

		// Use this for initialization
		void Start ()
		{
				
				mapCreator = new MapCreator (difficulty);
				gridManager = GetComponent<GridManager> ();
				gridManager.createGrid (mapCreator);
				gridManager.createTiles ();
				gridManager = GetComponent<GridManager> ();
				GameObject.Find ("Hand").GetComponent<HandPrefabScript> ().setHand (mapCreator.getHand ());
		
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
