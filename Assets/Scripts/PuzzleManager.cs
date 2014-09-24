using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
		GridManager gridManager;

		// Use this for initialization
		void Start ()
		{
				gridManager = GetComponent<GridManager> ();
				
				gridManager.drawTiles ();

				
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
