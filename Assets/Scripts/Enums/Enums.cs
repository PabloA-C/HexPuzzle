using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour
{
	
		public enum TilePrefabState
		{
				
				
				Blocked, //Can't be selected
				Ready, //Can be selected
				Used,    // Can't be selected but moved to a position and full alpha.

				Target,  // Objective 
				Available, // Initial grass tiles.
				Normal,   // Normal tile
				Water, 
				Fixed    // Tile that belongs to the map.
				
		}
		;
	
		public enum BoardState
		{
				Blocked,
				Free,
				
					
		}
		;

		
}
