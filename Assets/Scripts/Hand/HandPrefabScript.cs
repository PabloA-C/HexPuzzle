using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandPrefabScript : MonoBehaviour
{
		
		private List<TileScript> hand;
		

		// Use this for initialization
		void Start ()
		{
	
				
				
		
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		public void setHand (List<TileScript> hand)
		{
				this.hand = new System.Collections.Generic.List<TileScript> ();
				this.hand = hand;

				drawHand ();
	
		}


		public void drawHand ()
		{
				GameObject handArea = new GameObject ("Hand area");
				handArea.transform.parent = GameObject.Find ("Hand").transform;
				handArea.transform.position = GameObject.Find ("Hand").transform.position;
		
				int half = hand.Count / 2;
			
				int cont = 0;
				

				foreach (TileScript tile in hand) {
						float x = 6.4f;
						if (cont < half) {
								x = -6.4f;
						}

						GameObject tilePrefab = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
						tilePrefab.GetComponent<TilePrefabScript> ().setTile (tile);
						tilePrefab.name = "Tile " + cont;
						tilePrefab.transform.parent = handArea.transform;
						tilePrefab.transform.position = handArea.transform.position + new Vector3 (x, 0, 0);
						cont++;
						
				}
		}
}
