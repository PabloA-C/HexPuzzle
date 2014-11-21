using UnityEngine;
using System.Collections;

public class YouWinScript : MonoBehaviour
{
	
		public void animate ()
		{
				
				
				GetComponent<SpriteRenderer> ().sortingOrder = 10;
					
		
				GetComponent<Animator> ().Play ("InAndOut");
				
				
			
		}
	
		
		
}
	