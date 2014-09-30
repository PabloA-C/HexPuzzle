using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
		private GridManager gridManager;
		private MapCreator mapCreator ;
		public int difficulty = 1;

		// Use this for initialization
		void Start ()
		{
				
				mapCreator = new MapCreator (difficulty);
				gridManager = GetComponent<GridManager> ();
				gridManager.createGrid (difficulty, mapCreator.getMap ());
				gridManager.drawTiles ();

				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
