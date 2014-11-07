using UnityEngine;
using System.Collections;

public class DifficultyButtonScript : MonoBehaviour
{
		
		public int difficulty;

	
		void OnMouseDown ()
		{
		
				GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playPlacement ();
				GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().setDifficulty (difficulty);
				Application.LoadLevel ("Puzzle");
				
		}
		
	
		void OnMouseEnter ()
		{
				GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playSelection ();
				hover (true);
		
		}
	
		void OnMouseExit ()
		{
				
				hover (false);
	
	
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
