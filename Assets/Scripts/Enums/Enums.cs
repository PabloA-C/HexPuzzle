using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour
{
	
		public enum TilePrefabState
		{
				
				Ready,   //Can be selected
				Blocked, //Can't be selected
				Used,    // Can't be selected but moved to a position and full alpha.s
				Available, //Initial grass tiles.
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
