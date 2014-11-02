	using UnityEngine;
using System.Collections;
	
public class Enums : MonoBehaviour
{
		
		public enum TilePrefabState
		{
					
					
				Blocked, //Can't be selected
				Ready, //Can be selected
				Used,    // Can't be selected but moved to a position and full alpha.
	
				Available, // Initial grass tiles.
				Target,  // Highlight
				Normal,   // Normal tile	
				Fixed,   // Tile that belongs to the map.
				Water
			}
		;
		
		public enum BoardState
		{
				Blocked,
				Free,
				Unsolvable
					
						
			}
		;
	
			
}
