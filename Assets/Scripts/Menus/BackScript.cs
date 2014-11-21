using UnityEngine;
using System.Collections;

public class BackScript : MonoBehaviour
{

		private Enums.SelectorState state;
		bool selected = false;
	
		void OnMouseDown ()
		{
				if (state == Enums.SelectorState.Free) {
						GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playPlacement ();
						GameObject.Find ("Puzzle").GetComponent<PuzzleManager> ().backStep ();
				}
		
		}
	
		void OnMouseOver ()
		{
				if (state == Enums.SelectorState.Free) {
						if (!selected) {
								hover (true);
						}
				}
		}
	
		void OnMouseEnter ()
		{
				if (state == Enums.SelectorState.Free) {
						GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playSelection ();
						
				}
		}
	
		void OnMouseExit ()
		{
				if (state == Enums.SelectorState.Free) {
						hover (false);
						selected = false;
				}
		
		}
	
		public void setState (Enums.SelectorState newState)
		{
				if (newState == Enums.SelectorState.Free) {
			
			
						Color color = GetComponent<SpriteRenderer> ().color;
						color.a = 1;
						renderer.material.color = color;
						hover (false);
						
				} 
		
				if (newState == Enums.SelectorState.Blocked) {
			
			
						Color color = GetComponent<SpriteRenderer> ().color;
						color.a = 0;
						renderer.material.color = color;
						hover (false);
			
				}
				state = newState;
				setAlpha ();
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
						
						transform.localScale = bigScale;
			
				} else {
						transform.localScale = originalScale;
			
				}
		
		
		
		}
		
		public void setAlpha ()
		{
		
				float a = 1f;
						
		
				if (state == Enums.SelectorState.Blocked) {
			
						a = 0.5f;
			
				
				}
		
				Color color = GetComponent<SpriteRenderer> ().color;
				color.a = a;
				renderer.material.color = color;
		}
}
