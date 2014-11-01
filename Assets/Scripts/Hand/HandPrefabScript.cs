using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandPrefabScript : MonoBehaviour
{
		
		private List<TileScript> hand;
		private float tileWidth, tileHeight;
		private int difficulty;

		public void setHand (List<TileScript> hand, int difficulty)
		{
				this.hand = new System.Collections.Generic.List<TileScript> ();
				this.hand = hand;
				this.difficulty = difficulty;

				GameObject auxPrefab = Instantiate (Resources.Load ("Template")) as GameObject;
				tileWidth = auxPrefab.renderer.bounds.size.x;
				tileHeight = auxPrefab.renderer.bounds.size.y;
				Destroy (auxPrefab);
		
				drawHand ();
	
		}

		public void drawHand ()
		{
				GameObject handArea = new GameObject ("Hand area");
				handArea.transform.parent = GameObject.Find ("Hand").transform;
				handArea.transform.position = GameObject.Find ("Hand").transform.position;
				
				// 1 if its the left hand, -1 if its the right one. This will be used at the selection time
				int handPosition = 1;	

				int half = hand.Count / 2;
				int chunk1 = 0;
				int chunk2 = 0;

				switch (difficulty) {
				case 1:
						chunk1 = 4;
						break;
				case 2:
						chunk1 = 5;
						chunk2 = 3;
						break;
				case 3:
						chunk1 = 7;
						chunk2 = 6;
						break;
				}

				float chunk1HeightStart = -((chunk1 * tileHeight * 0.75f) / 2) + (tileHeight / 2) * 0.75f;
				float chunk2HeightStart = -((chunk2 * tileHeight * 0.75f) / 2) + (tileHeight / 2) * 0.75f;
				float chunk1x = 7.7f;
				float chunk2x = 7.9f + tileWidth;
		
				int cont = 0;
				int cont2 = 0;
						
				float y = chunk1HeightStart;
				foreach (TileScript tile in hand) {
		
						if (cont2 == chunk1) {
								y = chunk2HeightStart;
						}
			
						if (cont == half) {
								y = chunk1HeightStart;
								handPosition = -1;
								cont2 = 0;
						}


						float x = 1;
						
						if (cont < half) {
								x = -1;
						}

						if (cont2 < chunk1) {
								x = x * chunk1x;
						} else {
								x = x * chunk2x;
						}


						GameObject tilePrefab = Instantiate (Resources.Load ("Prefabs/TilePrefab")) as GameObject;
						tilePrefab.GetComponent<TilePrefabScript> ().setHandTile (tile, handPosition);
						tilePrefab.name = "Tile " + cont;
						tilePrefab.transform.parent = handArea.transform;
						tilePrefab.transform.position = handArea.transform.position + new Vector3 (x, y, cont);
					

						cont++;
						cont2++;
						y += tileHeight * 0.75f;
			
				}
		}

		public List<TileScript> getHand ()
		{
				return hand;
		}
}
