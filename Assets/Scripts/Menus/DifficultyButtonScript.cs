using UnityEngine;
using System.Collections;

public class DifficultyButtonScript : MonoBehaviour
{
		
		public int difficulty;
		private Enums.SelectorState state;
		
		void OnMouseDown ()
		{
				if (state == Enums.SelectorState.Free) {
				
						GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playPlacement ();
						GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().setDifficulty (difficulty);
						Application.LoadLevel ("Puzzle");
				}
				
		}
	
		void OnMouseEnter ()
		{
				if (state == Enums.SelectorState.Free) {
						GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playSelection ();
						hover (true);
				}
		}
	
		void OnMouseExit ()
		{
				if (state == Enums.SelectorState.Free) {
						hover (false);
				}
	
		}
		
		public void setState (Enums.SelectorState newState)
		{
				if (newState == Enums.SelectorState.Free) {
						
				
						Color color = GetComponent<SpriteRenderer> ().color;
						color.a = 1;
						renderer.material.color = color;
				} 
				
				if (newState == Enums.SelectorState.Blocked) {
				
						
						Color color = GetComponent<SpriteRenderer> ().color;
						color.a = 0;
						renderer.material.color = color;
			
				}
				state = newState;
		
		}
		
		public void hover (bool mouseEntered)
		{
		
				Vector3 originalScale = transform.localScale;
				originalScale.x = 1F; 
				originalScale.y = 1F;
		
				Vector3 bigScale = originalScale;
				bigScale.x = 1.2F; 
				bigScale.y = 1.2F; 
		
				if (mouseEntered) {
						transform.Translate (new Vector3 (0, 0, -4), Space.World);
						transform.localScale = bigScale;
			
				} else {
						transform.Translate (new Vector3 (0, 0, 4), Space.World);
						transform.localScale = originalScale;
			
				}
		
		
		
		}
}
