using UnityEngine;
using System.Collections;

public class TilePrefabScript : MonoBehaviour
{

		private TileScript tile;
		private Vector3 position;
		private Sprite[] TileSprites;
		private Sprite[] HLSprites;
			
		// Use this for initialization
		void Start ()
		{
				

		}
	
		
		// Update is called once per frame
		void Update ()
		{

				
						
		}
	
		void OnMouseOver ()
		{
				GetComponent<SpriteRenderer> ().sprite = TileSprites [0];
		}

		void OnMouseExit ()
		{
				drawOriginalTile (tile.getType ());
		}

		public void setTile (TileScript tile)
		{

				this.tile = tile;
				TileSprites = Resources.LoadAll<Sprite> (@"Sprites/TileSheet");
				HLSprites = Resources.LoadAll<Sprite> (@"Sprites/HighLights");

				drawOriginalTile (tile.getType ());
				GetComponent<Transform> ().Rotate (0, 0, tile.getRotation () * 60, Space.World);


		}

		public void  drawOriginalTile (string type)
		{
				int index = getTypeIndex (type);
				GetComponent<SpriteRenderer> ().sprite = TileSprites [index];
				
				
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
}
