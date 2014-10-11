using UnityEngine;
using System.Collections;

public class TilePrefabScript : MonoBehaviour
{

		private string name;
		private string type;
		private Vector3 position;
		private int rotation;
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
				drawOriginalTile ();
		}

		public void setTile (string type)
		{

				/*
				 * Almacenar los datos, no solo el type.
				*/

				this.type = type;
				TileSprites = Resources.LoadAll<Sprite> (@"Sprites/TileSheet");
				HLSprites = Resources.LoadAll<Sprite> (@"Sprites/HighLights");
				drawOriginalTile ();


		}

		public void  drawOriginalTile ()
		{
				
				GetComponent<SpriteRenderer> ().sprite = TileSprites [3];
				
		}

}
