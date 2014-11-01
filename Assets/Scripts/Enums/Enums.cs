using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour
{
	
		public enum TilePrefabState
		{
				
				
				Blocked, //Can't be selected
				Ready, //Can be selected
				Used,    // Can't be selected but moved to a position and full alpha.s
				Available, //Initial grass tiles.
				Target,
				Unavailable,	
				Fixed    // Tile that belongs to the map.
				
		}
		;
	
		public enum BoardState
		{
				WaitingForTarget,
				WaitingForTile,
				Blocked
					
	}
		;

		
}
