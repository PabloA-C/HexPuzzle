using UnityEngine;
using System.Collections;

public class TilePrefabScript : MonoBehaviour
{

		private Sprite[] TileSprites;
		private Sprite[] HLSprites;
		private TileScript tileScript;
		private Vector3 originalPosition;
		private Vector3 position;
		private int handPosition;
		public Enums.TilePrefabState state;
		private PuzzleManager puzzleManager;

		
		void OnMouseDown ()
		{
				
				
				//On the hand
				if (state == Enums.TilePrefabState.Ready) {	
						// This coordinates are used to identify the tile on the puzzle manager, nothing else.
						GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playPlacement ();
						tileScript.setCoordinates (new Coordinate (-1, -1));
						puzzleManager.turn ();
				}


				//On the board		
				if (state == Enums.TilePrefabState.Available) {	

						// This coordinates are used to identify the tile on the puzzle manager, nothing else.
						GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playPlacement ();
						puzzleManager.setFirstTarget (getTileScript ().getCoordinates ());
			
				}
		}
	
		void OnMouseEnter ()
		{
			
				

				//On the hand
				if (state == Enums.TilePrefabState.Ready) {	
						GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playSelection ();
						//Board.placeTile(This)
						hover (true);

			
				}

				//On the board
				if (state == Enums.TilePrefabState.Available) {	
						GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playSelection ();
						//Board.setTarget(this coords)
						hover (true);
			
				}
		
		}
	
		void OnMouseExit ()
		{
				
				//On the hand
				if (state == Enums.TilePrefabState.Ready) {	
			
						//Board.placeTile(This)
						hover (false);
			
			
				}
				//On the board
				if (state == Enums.TilePrefabState.Available) {	
			
						//Board.setTarget(this coords)
						hover (false);
				
				}
		}
	
		public void setState (Enums.TilePrefabState newState)
		{
				// Changing the Z coord of the tile
				
				if (newState == Enums.TilePrefabState.Ready) {
						transform.Translate (new Vector3 (0, 0, -5), Space.World);
			
				}

				if (newState == Enums.TilePrefabState.Blocked) {
						transform.Translate (new Vector3 (0, 0, 5), Space.World);
			
				}


				if (newState == Enums.TilePrefabState.Used) {
						
						Vector3 originalScale = transform.localScale;
						originalScale.x = 1F; 
						originalScale.y = 1F; 
						transform.localScale = originalScale;
			
				}
		
				if (newState == Enums.TilePrefabState.Available) {
						transform.Translate (new Vector3 (0, 0, -5), Space.World);
						GetComponent<SpriteRenderer> ().sprite = HLSprites [0];
						
				}


				if (newState == Enums.TilePrefabState.Normal) {
						transform.Translate (new Vector3 (0, 0, 5), Space.World);
						GetComponent<SpriteRenderer> ().sprite = TileSprites [getTypeIndex ("Grass")];
			
				}


				if (newState == Enums.TilePrefabState.Target) {
						transform.Translate (new Vector3 (0, 0, 5), Space.World);
						GetComponent<SpriteRenderer> ().sprite = HLSprites [1];
			
				}

				

				this.state = newState;
				setAlpha ();
		}


		void moveBack ()
		{
				transform.position = originalPosition;
		}

		void setAlpha ()
		{
				float a = 1f;
		
				if (state == Enums.TilePrefabState.Blocked) {
			
						a = 0.5f;
						
			
				}
		
				Color color = GetComponent<SpriteRenderer> ().color;
				color.a = a;
				renderer.material.color = color;
		}
	
		public void hover (bool mouseEntered)
		{
	
				Vector3 originalScale = transform.localScale;
				originalScale.x = 1F; 
				originalScale.y = 1F;

				Vector3 bigScale = originalScale;
				bigScale.x = 1.3F; 
				bigScale.y = 1.3F; 
		
				if (mouseEntered) {
						transform.Translate (new Vector3 (.16f * handPosition, 0, -4), Space.World);
						GetComponent<Transform> ().localScale.Set (1.2f, 1.2f, 1f);
						transform.localScale = bigScale;
						
				} else {
						transform.Translate (new Vector3 (-.16f * handPosition, 0, 4), Space.World);
						GetComponent<Transform> ().localScale.Set (1f, 1f, 1f);
						transform.localScale = originalScale;
						
				}

				

		}

		public void setHandTile (TileScript tile, int handPosition)
		{
		
				this.tileScript = tile;
				this.handPosition = handPosition;
				this.puzzleManager = GameObject.Find ("Puzzle").GetComponent<PuzzleManager> ();
		
				TileSprites = Resources.LoadAll<Sprite> (@"Sprites/TileSheet");
				HLSprites = Resources.LoadAll<Sprite> (@"Sprites/HighLights");
				originalPosition = transform.position;
				position = transform.position;
				GetComponent<SpriteRenderer> ().sprite = TileSprites [getTypeIndex (tile.getType ())];
				transform.Rotate (0, 0, tile.getRotation () * 60, Space.World);
				setAlpha ();
		
		}
	
		public void setMapTile (TileScript tile)
		{


				this.tileScript = tile;
				this.puzzleManager = GameObject.Find ("Puzzle").GetComponent<PuzzleManager> ();
				setState (Enums.TilePrefabState.Fixed);
				TileSprites = Resources.LoadAll<Sprite> (@"Sprites/TileSheet");
				HLSprites = Resources.LoadAll<Sprite> (@"Sprites/HighLights");
				originalPosition = transform.position;
				position = transform.position;
				GetComponent<SpriteRenderer> ().sprite = TileSprites [getTypeIndex (tile.getType ())];
				transform.Rotate (0, 0, tile.getRotation () * 60, Space.World);
		
		}
	
		int getTypeIndex (string type)
		{
				int res = 0;
				switch (type) {
				case "Water":
						res = 1;
						break;
				case "Straight":
						res = 2;
						break;
				case "Turn":
						res = 3;
						break;
				case "SharpTurn":
						res = 4;
						break;
				case "Start":
						res = 5;
						break;
				case "Finish":
						res = 6;
						break;
			
				}
				return res;
		
		}

		public Enums.TilePrefabState getState ()
		{
				return state;
		}

		public TileScript getTileScript ()
		{
				return tileScript;
		}
	
}

