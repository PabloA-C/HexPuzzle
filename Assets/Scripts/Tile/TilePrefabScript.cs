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
	
		void OnMouseDown ()
		{
				//On the hand
				if (state == Enums.TilePrefabState.Ready) {	

						
						//Board.placeTile(This)

				}


				//On the board		
				if (state == Enums.TilePrefabState.Available) {	
			
						//Board.setTarget(this coords)
			
				}
		}
	
		void OnMouseEnter ()
		{

				//On the hand
				if (state == Enums.TilePrefabState.Ready) {	
			
						//Board.placeTile(This)
						highlight (true);

			
				}

				//On the board
				if (state == Enums.TilePrefabState.Available) {	
			
						//Board.setTarget(this coords)
						highlight (true);
			
				}
		
		}
	
		void OnMouseExit ()
		{

				//On the hand
				if (state == Enums.TilePrefabState.Ready) {	
			
						//Board.placeTile(This)
						highlight (false);
			
			
				}
				//On the board
				if (state == Enums.TilePrefabState.Available) {	
			
						//Board.setTarget(this coords)
						highlight (false);
				
				}
		}
	
		public void setState (Enums.TilePrefabState newState)
		{
				
				if (state == Enums.TilePrefabState.Ready) {

						if (newState == Enums.TilePrefabState.Blocked) {
								transform.Translate (new Vector3 (0, 0, 10), Space.World);

						}
				}

			
				if (state == Enums.TilePrefabState.Blocked) {
			
						if (newState == Enums.TilePrefabState.Ready) {
								transform.Translate (new Vector3 (0, 0, -10), Space.World);
				
						}
				}

		
				if (state == Enums.TilePrefabState.Available) {
			
			
				}
				
				

				this.state = newState;
				setAlpha ();
		}
	
		public void setAlpha ()
		{
				float a = 1;
		
				switch (state) {
				case Enums.TilePrefabState.Ready:
			
						a = 1f;
						break;
				case Enums.TilePrefabState.Blocked:
			
						a = 0.5f;
						break;
			
				}
		
				Color color = GetComponent<SpriteRenderer> ().color;
				color.a = a;
				renderer.material.color = color;
		}
	
		public void highlight (bool mouseEntered)
		{
	
				Vector3 originalScale = transform.localScale;
				originalScale.x = 1F; 
				originalScale.y = 1F;

				Vector3 bigScale = originalScale;
				bigScale.x = 1.3F; 
				bigScale.y = 1.3F; 
				
		
				if (mouseEntered) {
						transform.Translate (new Vector3 (.16f * handPosition, 0, -9), Space.World);
						GetComponent<Transform> ().localScale.Set (1.2f, 1.2f, 1f);
						transform.localScale = bigScale;
						
				} else {
						transform.Translate (new Vector3 (-.16f * handPosition, 0, 9), Space.World);
						GetComponent<Transform> ().localScale.Set (1f, 1f, 1f);
						transform.localScale = originalScale;
						
				}
		
				

		}

		public void setHandTile (TileScript tile, int handPosition)
		{
		
				this.tileScript = tile;
				this.handPosition = handPosition;
		
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
				setState (Enums.TilePrefabState.Fixed);
				TileSprites = Resources.LoadAll<Sprite> (@"Sprites/TileSheet");
				HLSprites = Resources.LoadAll<Sprite> (@"Sprites/HighLights");
				originalPosition = transform.position;
				position = transform.position;
				GetComponent<SpriteRenderer> ().sprite = TileSprites [getTypeIndex (tile.getType ())];
				transform.Rotate (0, 0, tile.getRotation () * 60, Space.World);
		
		}
	
		public int getTypeIndex (string type)
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
		public TileScript getTileScript ()
		{
				return tileScript;
		}
	
}

