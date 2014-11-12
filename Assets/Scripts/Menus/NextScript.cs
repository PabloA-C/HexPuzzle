using UnityEngine;
using System.Collections;

public class NextScript : MonoBehaviour
{

		int count = 1;

		void OnMouseDown ()
		{
				GameObject.Find ("Difficulty").GetComponent<DifficultyScript> ().playPlacement ();
	
				loadNext ();
		
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
		
		void loadNext ()
		{
				if (count == 5) {
						Application.LoadLevel ("Main");
				} else {
						count++;
						string slide = count.ToString ();
						GameObject.Find (slide).GetComponent<Transform> ().Translate (new Vector3 (0, 0, -count));
		
			
						
				}
	
			
				
		}
		
		
		
}
